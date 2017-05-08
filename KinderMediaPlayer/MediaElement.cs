﻿using System;
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

        public string Source
        {
            get; set;
        }

        public MEDIA_TYPE Type
        {
            get; set;
        }

        public MediaElement(XmlNode inNode)
        {
            // Load from an XML Node
            if (inNode != null)
            {
                if (inNode.Attributes["NAME"] != null)
                {
                    Name = inNode.Attributes["NAME"].Value;
                }
                if (inNode.Attributes["SOURCE"] != null)
                {
                    Source = inNode.Attributes["SOURCE"].Value;
                }
                if (inNode.Attributes["TYPE"] != null)
                {
                    int newType = (int)MEDIA_TYPE.IMAGE;
                    if (Int32.TryParse(inNode.Attributes["TYPE"].Value, out newType) == true)
                    {
                        if (Enum.IsDefined(typeof(MEDIA_TYPE), newType))
                        {
                            Type = (MEDIA_TYPE) newType;
                        }
                    }
                }
            }
        }

        public XmlNode addToXML(XmlDocument inDoc)
        {
            // Save to an XML Node
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Source))
            {
                return null;
            }

            XmlNode thisNode = inDoc.CreateElement("MEDIAELEMENT");
            XmlAttribute attr;
            
            attr = inDoc.CreateAttribute("NAME");
            attr.Value = Name;
            thisNode.Attributes.Append(attr);

            attr = inDoc.CreateAttribute("SOURCE");
            attr.Value = Source;
            thisNode.Attributes.Append(attr);

            attr = inDoc.CreateAttribute("TYPE");
            attr.Value = ((int)Type).ToString();
            thisNode.Attributes.Append(attr);

            return thisNode;
        }
    }
}