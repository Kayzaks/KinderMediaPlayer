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
        #region MEDIA_TYPES

        // NOTE: This Enum and the following string list have to have the same ordering
        //       and indices.
        public enum MEDIA_TYPE : int
        {
            IMAGE = 0,
            SOUND = 1,
            VIDEO = 2,
        }
        public static List<string> MEDIA_TYPE_STRING = new List<string>()
        {
            "Image",
            "Audio",
            "Video"
        };

        public static bool isMediaType(int inInt)
        {
            return Enum.IsDefined(typeof(MEDIA_TYPE), inInt);
        }

        #endregion MEDIA_TYPES

        public string Name
        {
            get; set;
        }

        public string LongName
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return "[" + TypeName + "]";
                }
                else
                {
                    return Name + " - [" + TypeName + "]";
                }
            }
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

        public string TypeName
        {
            get
            {
                return MEDIA_TYPE_STRING[(int)Type];
            }
        }

        public MediaElement()
        {
            Name = "";
            Description = "";
            Source = null;
            Icon = null;
            Type = MEDIA_TYPE.IMAGE;
        }

        public MediaElement(MediaElement inElement)
        {
            // Copy the Element
            Name = inElement.Name;
            Description = inElement.Description;
            Source = inElement.Source;
            Icon = inElement.Icon;
            Type = inElement.Type;
        }

        public MediaElement(XmlNode inNode) : this()
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
                    if (isMediaType(newType))
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
