using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KinderMediaPlayer
{
    public class ServiceSettings
    {
        private readonly static string STANDARD_PASSWORD = "admin";
        private readonly static string SETTINGS_FILE_NAME = "settings.xml";

        private static string currentPassword = null;
        private static List<MediaElement> mediaElements;

        public static void loadSettings()
        {
            mediaElements = new List<MediaElement>();

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
            XmlNode rootNode = xmlDoc.CreateElement("SETTINGS");
            xmlDoc.AppendChild(rootNode);

            XmlNode passwordNode = xmlDoc.CreateElement("PASSWORD");
            passwordNode.InnerText = currentPassword;
            rootNode.AppendChild(passwordNode);

            foreach(MediaElement currentElement in mediaElements)
            {
                XmlNode mediaNode = currentElement.addToXML(xmlDoc);
                if (mediaNode != null)
                {
                    rootNode.AppendChild(mediaNode);
                }
            }
            
            xmlDoc.Save(SETTINGS_FILE_NAME);
        }


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
