using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KinderMediaPlayer
{
    public static class ServiceSettings
    {
        // This password will be overwritten by the value in settings.xml (if it exists)
        private readonly static string STANDARD_PASSWORD = "admin";
        private readonly static string SETTINGS_FILE_NAME = "settings.xml";

        // Password is saved as a SHA256 hash
        private static string currentPassword = null;
        private static string backgroundSource = null;
        private static ObservableCollection<MediaElement> mediaElements;

        #region LOAD_SAVE_SETTINGS

        public static void loadSettings()
        {
            mediaElements = new ObservableCollection<MediaElement>();

            try
            {
                XmlDocument settingsFile = new XmlDocument();
                settingsFile.Load(SETTINGS_FILE_NAME);

                foreach(XmlNode currentNode in settingsFile.FirstChild.ChildNodes)
                {
                    if (string.Equals(currentNode.Name, "PASSWORD"))
                    {
                        currentPassword = currentNode.InnerText;
                    }

                    if (string.Equals(currentNode.Name, "MEDIAELEMENT"))
                    {
                        mediaElements.Add(new MediaElement(currentNode));
                    }

                    if (string.Equals(currentNode.Name, "BACKGROUND"))
                    {
                        if (currentNode.Attributes["SOURCE"] != null)
                        {
                            backgroundSource = currentNode.Attributes["SOURCE"].Value;
                        }
                    }
                }
            }
            catch(Exception e)
            {
                // We failed reading the settings, reset to default.
            }
            finally
            {
                if (string.IsNullOrEmpty(currentPassword) == true)
                {
                    currentPassword = hashPassword(STANDARD_PASSWORD);
                }
            }
        }

        public static void saveSettings()
        {
            // We assume that the Settings were loaded at some point
            // before this.

            XmlDocument xmlDoc = new XmlDocument();
            XmlAttribute attr;
            XmlNode rootNode = xmlDoc.CreateElement("SETTINGS");
            xmlDoc.AppendChild(rootNode);

            XmlNode passwordNode = xmlDoc.CreateElement("PASSWORD");
            passwordNode.InnerText = currentPassword;
            rootNode.AppendChild(passwordNode);
            
            XmlNode backgroundNode = xmlDoc.CreateElement("BACKGROUND");
            attr = xmlDoc.CreateAttribute("SOURCE");
            attr.Value = backgroundSource;
            backgroundNode.Attributes.Append(attr);
            rootNode.AppendChild(backgroundNode);
          
            // And save all the individual Media Elements
            foreach (MediaElement currentElement in mediaElements)
            {
                XmlNode mediaNode = currentElement.addToXML(xmlDoc);
                if (mediaNode != null)
                {
                    rootNode.AppendChild(mediaNode);
                }
            }
            
            xmlDoc.Save(SETTINGS_FILE_NAME);
        }

        #endregion LOAD_SAVE_SETTINGS

        #region MEDIA

        public static ObservableCollection<MediaElement> getMediaElements()
        {
            return mediaElements;
        }

        public static ObservableCollection<MediaElement> getCopyMediaElements()
        {
            if (mediaElements == null)
            {
                return new ObservableCollection<MediaElement>();
            }

            return new ObservableCollection<MediaElement>(mediaElements.Select(x => new MediaElement(x)));
        }

        public static void setMediaElements(ObservableCollection<MediaElement> inElements)
        {
            if (inElements != null)
            {
                mediaElements = inElements;
            }
        }

        public static string getBackgroundSource()
        {
            return backgroundSource;
        }

        #endregion MEDIA

        #region PASSWORD

        // Password checking / hashing routines

        public static bool checkPassword(string inPassword)
        {
            if (string.IsNullOrEmpty(currentPassword) == true || string.IsNullOrEmpty(inPassword) == true)
            {
                return false;
            }

            if (currentPassword.Equals(hashPassword(inPassword)) == true)
            {
                return true;
            }

            return false;
        }


        public static void setPassword(string inPassword)
        {
            if (string.IsNullOrEmpty(inPassword) == true)
            {
                return;
            }

            currentPassword = hashPassword(inPassword);
        }


        private static string hashPassword(string inPassword)
        {
            StringBuilder sBuilder = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Byte[] resultHash = hash.ComputeHash(Encoding.UTF8.GetBytes(inPassword));

                foreach (Byte currentByte in resultHash)
                {
                    sBuilder.Append(currentByte.ToString("x2"));
                }
            }

            return sBuilder.ToString();
        }

        #endregion PASSWORD
    }
}
