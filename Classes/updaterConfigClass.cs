using System.Collections.Generic;
using System.Xml;
using testInstallServer.Classes;

namespace TEPSClientInstallService.Classes
{
    public class updaterConfigClass
    {
        private loggingClass loggingClass = new loggingClass();

        private string xmlPath = null;

        private XmlDocument updaterConfig = new XmlDocument();

        //work done per filled in ORI field to add them to the updater config file
        public void oriSub(string ORI, string serverName)
        {
            xmlPath = @"C:\Program Files (x86)\New World Systems\New World Automatic Updater\NewWorld.Management.Updater.Service.exe.config";

            updaterConfig.Load(xmlPath);

            XmlNodeList Applications = updaterConfig.GetElementsByTagName("applications");

            XmlNode nodeAdd = updaterConfig.CreateElement("add");

            XmlAttribute id = updaterConfig.CreateAttribute("id");

            id.Value = ORI.ToUpper();

            XmlAttribute uri = updaterConfig.CreateAttribute("manifestUri");
            uri.Value = "http://" + serverName + "/MobileUpdates/" + ORI + "/Update.xml";

            XmlAttribute location = updaterConfig.CreateAttribute("location");

            location.Value = @"C:\Program Files\New World Systems\Aegis Mobile\";

            nodeAdd.Attributes.Append(id);
            nodeAdd.Attributes.Append(uri);
            nodeAdd.Attributes.Append(location);

            Applications[0].AppendChild(nodeAdd);

            string logEntry = ORI + " has been added to Updater Config.";

            loggingClass.logEntryWriter(logEntry, "info");

            updaterConfig.Save(xmlPath);
        }

        //work done per filled in fdid field to added them to the updater config file
        public void fdidSub(string FDID, string serverName)
        {
            xmlPath = @"C:\Program Files (x86)\New World Systems\New World Automatic Updater\NewWorld.Management.Updater.Service.exe.config";

            updaterConfig.Load(xmlPath);

            XmlNodeList Applications = updaterConfig.GetElementsByTagName("applications");

            XmlNode nodeAdd = updaterConfig.CreateElement("add");

            XmlAttribute id = updaterConfig.CreateAttribute("id");
            id.Value = FDID;

            XmlAttribute uri = updaterConfig.CreateAttribute("manifestUri");
            uri.Value = "http://" + serverName + "/MobileUpdates/" + FDID + "/Update.xml";

            XmlAttribute location = updaterConfig.CreateAttribute("location");

            location.Value = @"C:\Program Files\New World Systems\Aegis Fire Mobile\";

            nodeAdd.Attributes.Append(id);
            nodeAdd.Attributes.Append(uri);
            nodeAdd.Attributes.Append(location);

            Applications[0].AppendChild(nodeAdd);

            string logEntry = FDID + " has been added to Updater Config.";

            loggingClass.logEntryWriter(logEntry, "info");

            updaterConfig.Save(xmlPath);
        }

        //work done to add the police client to the updater config file
        public void policeClientSub(string serverName)
        {
            xmlPath = @"C:\Program Files (x86)\New World Systems\New World Automatic Updater\NewWorld.Management.Updater.Service.exe.config";

            updaterConfig.Load(xmlPath);

            XmlNodeList Applications = updaterConfig.GetElementsByTagName("applications");

            XmlNode nodeAdd = updaterConfig.CreateElement("add");

            XmlAttribute id = updaterConfig.CreateAttribute("id");
            id.Value = "Police Client";

            XmlAttribute uri = updaterConfig.CreateAttribute("manifestUri");
            uri.Value = "http://" + serverName + "/MobileUpdates/Police Client/Update.xml";

            XmlAttribute location = updaterConfig.CreateAttribute("location");

            location.Value = @"C:\Program Files\New World Systems\Aegis Mobile\";

            nodeAdd.Attributes.Append(id);
            nodeAdd.Attributes.Append(uri);
            nodeAdd.Attributes.Append(location);

            Applications[0].AppendChild(nodeAdd);

            string logEntry = "Police Client" + " has been added to Updater Config.";

            loggingClass.logEntryWriter(logEntry, "info");

            updaterConfig.Save(xmlPath);
        }

        //work done to add the fire client to the updater config file
        public void fireClientSub(string serverName)
        {
            xmlPath = @"C:\Program Files (x86)\New World Systems\New World Automatic Updater\NewWorld.Management.Updater.Service.exe.config";

            updaterConfig.Load(xmlPath);

            XmlNodeList Applications = updaterConfig.GetElementsByTagName("applications");

            XmlNode nodeAdd = updaterConfig.CreateElement("add");

            XmlAttribute id = updaterConfig.CreateAttribute("id");
            id.Value = "Fire Client";

            XmlAttribute uri = updaterConfig.CreateAttribute("manifestUri");
            uri.Value = "http://" + serverName + "/MobileUpdates/Fire Client/Update.xml";

            XmlAttribute location = updaterConfig.CreateAttribute("location");

            location.Value = @"C:\Program Files\New World Systems\Aegis Fire Mobile\";

            nodeAdd.Attributes.Append(id);
            nodeAdd.Attributes.Append(uri);
            nodeAdd.Attributes.Append(location);

            Applications[0].AppendChild(nodeAdd);

            string logEntry = "Fire Client" + " has been added to Updater Config.";

            loggingClass.logEntryWriter(logEntry, "info");

            updaterConfig.Save(xmlPath);
        }

        //work done to add the merge client to the updater config file
        public void mergeClientSub(string serverName)
        {
            xmlPath = @"C:\Program Files (x86)\New World Systems\New World Automatic Updater\NewWorld.Management.Updater.Service.exe.config";

            updaterConfig.Load(xmlPath);

            XmlNodeList Applications = updaterConfig.GetElementsByTagName("applications");

            XmlNode nodeAdd = updaterConfig.CreateElement("add");

            XmlAttribute id = updaterConfig.CreateAttribute("id");
            id.Value = "Merge Client";

            XmlAttribute uri = updaterConfig.CreateAttribute("manifestUri");
            uri.Value = "http://" + serverName + "/MobileUpdates/Merge Client/Update.xml";

            XmlAttribute location = updaterConfig.CreateAttribute("location");

            location.Value = @"C:\Program Files\New World Systems\Aegis Mobile\";

            nodeAdd.Attributes.Append(id);
            nodeAdd.Attributes.Append(uri);
            nodeAdd.Attributes.Append(location);

            Applications[0].AppendChild(nodeAdd);

            string logEntry = "Merge Client" + " has been added to Updater Config.";

            loggingClass.logEntryWriter(logEntry, "info");

            updaterConfig.Save(xmlPath);
        }

        //work done to compare information already in the updater config file to information that is in the utility
        //the information that is already present does not get added
        public void seeIfNodesExist(string textBox)
        {
            updaterConfig.Load(@"C:\Program Files (x86)\New World Systems\New World Automatic Updater\NewWorld.Management.Updater.Service.exe.config");

            XmlNodeList PoliceClientNode = updaterConfig.GetElementsByTagName("add");
            List<XmlNode> toRemove = new List<XmlNode>();

            foreach (XmlNode AddNode in PoliceClientNode)
            {
                XmlAttributeCollection attrColl = AddNode.Attributes;
                XmlAttribute AttrID = attrColl["id"];
                XmlAttribute AttrURI = attrColl["manifestUri"];

                if (AttrID != null)
                {
                    if ((AttrID.Value == "Police Client"))// && (AttrURI.Value == "http://" + MobileServer.Text + "/MobileUpdates/Police Client/Update.xml"))
                    {
                        toRemove.Add(AddNode);
                    }

                    if ((AttrID.Value == "Fire Client"))// && (AttrURI.Value == "http://" + MobileServer.Text + "/MobileUpdates/Fire Client/Update.xml"))
                    {
                        toRemove.Add(AddNode);
                    }

                    if ((AttrID.Value == "Merge Client"))// && (AttrURI.Value == "http://" + MobileServer.Text + "/MobileUpdates/Merge Client/Update.xml"))
                    {
                        toRemove.Add(AddNode);
                    }

                    if ((AttrID.Value == "NWS Updater"))// && (AttrURI.Value == "http://" + MobileServer.Text + "/MobileUpdates/Merge Client/Update.xml"))
                    {
                        toRemove.Add(AddNode);
                    }

                    if (textBox != "")
                    {
                        string upperORI = textBox.ToUpper();
                        if ((AttrID.Value == upperORI))// && (AttrURI.Value == "http://" + MobileServer.Text + "/MobileUpdates/" + ORI + "/Update.xml"))
                        {
                            toRemove.Add(AddNode);
                        }
                    }
                }
            }

            foreach (XmlNode xmlElement in toRemove)
            {
                try
                {
                    XmlNode node = xmlElement.ParentNode;
                    node.RemoveChild(xmlElement);

                    updaterConfig.Save(@"C:\Program Files (x86)\New World Systems\New World Automatic Updater\NewWorld.Management.Updater.Service.exe.config");
                }
                catch
                {
                    //loggingClass.queEntrywriter("ORI's and FDID's should not be the same");
                }
            }
        }
    }
}