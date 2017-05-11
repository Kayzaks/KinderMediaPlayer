using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KinderMediaPlayer
{
    public class MediaElement
    {
        public enum MEDIA_TYPE : int
        {
            IMAGE = 1,
            SOUND = 2,
            VIDEO = 3,
        }

        public string Name
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public string Source
        {
            get; set;
        }

        public string Icon
        {
            get; set;
        }

        public MEDIA_TYPE Type
        {
            get; set;
        }

        public MediaElement(XmlNode inNode)
        {
            loadFromXML(inNode);
        }

        #region XML

        public void loadFromXML(XmlNode inNode)
        {
            // Load from an XML Node
            if (inNode == null)
            {
                return;
            }

            if (inNode.Attributes["NAME"] != null)
            {
                Name = inNode.Attributes["NAME"].Value;
            }
            if (inNode.Attributes["DESCRIPTION"] != null)
            {
                Description = inNode.Attributes["DESCRIPTION"].Value;
            }
            if (inNode.Attributes["SOURCE"] != null)
            {
                Source = inNode.Attributes["SOURCE"].Value;
            }
            if (inNode.Attributes["ICON"] != null)
            {
                Icon = inNode.Attributes["ICON"].Value;
            }
            if (inNode.Attributes["TYPE"] != null)
            {
                int newType = (int)MEDIA_TYPE.IMAGE;
                if (Int32.TryParse(inNode.Attributes["TYPE"].Value, out newType) == true)
                {
                    if (Enum.IsDefined(typeof(MEDIA_TYPE), newType))
                    {
                        Type = (MEDIA_TYPE)newType;
                    }
                }
            }
        }

        public XmlNode addToXML(XmlDocument inDoc)
        {
            // Save to an XML Node
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Source) || inDoc == null)
            {
                return null;
            }

            XmlNode thisNode = inDoc.CreateElement("MEDIAELEMENT");
            XmlAttribute attr;
            
            attr = inDoc.CreateAttribute("NAME");
            attr.Value = Name;
            thisNode.Attributes.Append(attr);

            attr = inDoc.CreateAttribute("DESCRIPTION");
            attr.Value = Description;
            thisNode.Attributes.Append(attr);

            attr = inDoc.CreateAttribute("SOURCE");
            attr.Value = Source;
            thisNode.Attributes.Append(attr);

            attr = inDoc.CreateAttribute("ICON");
            attr.Value = Icon;
            thisNode.Attributes.Append(attr);

            attr = inDoc.CreateAttribute("TYPE");
            attr.Value = ((int)Type).ToString();
            thisNode.Attributes.Append(attr);

            return thisNode;
        }

        #endregion XML
    }
}
