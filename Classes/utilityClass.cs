using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using testInstallServer.Classes;

namespace TEPSClientInstallService.Classes
{
    internal class utilityClass
    {
        private loggingClass loggingClass = new loggingClass();

        public string parseRequestBodyAsync(string body)
        {
            var jsonFilePackage = JsonConvert.DeserializeObject<serverConfigObj>(body);

            dynamic jsonObj = JsonConvert.DeserializeObject(body);

            serverConfigObj.MobileServer = jsonObj["MobileServer"];
            serverConfigObj.ESSServer = jsonObj["ESSServer"];
            serverConfigObj.MSPServer = jsonObj["MSPServer"];
            serverConfigObj.CADServer = jsonObj["CADServer"];
            serverConfigObj.GISServer = jsonObj["GISServer"];
            serverConfigObj.GISInstance = jsonObj["GISInstance"];

            parseORI(body);

            parseFDID(body);

            return jsonFilePackage.ToString();
        }

        public string parseRequestBodyForMasterHost(string body)
        {
            string response = "";

            return response;
        }

        private async Task<string> parseORI(string body)
        {
            serverConfigObj.configFileORIObjs.Clear();

            try
            {
                dynamic jsonObj = JsonConvert.DeserializeObject(body);
                foreach (var obj in jsonObj.Properties())
                {
                    if (obj.Name == "PoliceList")
                    {
                        foreach (var obj2 in obj.Value)
                        {
                            serverConfigObj.configFileORIObjs.Add(new jsonConfigFileORIObj { FieldName = obj2.FieldName, ORI = obj2.ORI });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");

                for (var i = 1; i < body.Length; i++)
                {
                    string txt = $"ORI{i}";

                    var jsonFilePackage = (JObject)JsonConvert.DeserializeObject(body);

                    JToken oriToken = jsonFilePackage.SelectToken("PoliceList[?(@.FieldName == '" + txt + "')]");

                    if (oriToken != null)
                    {
                        string ori = oriToken.Value<string>("ORI");

                        loggingClass.logEntryWriter(ori, "debug");

                        serverConfigObj.configFileORIObjs.Add(new jsonConfigFileORIObj { FieldName = txt, ORI = ori });
                    }
                    else
                    {
                        i = body.Length;

                        loggingClass.logEntryWriter(i.ToString(), "debug");
                    }
                }
            }

            return "";
        }

        private async Task<string> parseFDID(string body)
        {
            serverConfigObj.configFileFDIDObjs.Clear();

            try
            {
                dynamic jsonObj = JsonConvert.DeserializeObject(body);
                foreach (var obj in jsonObj.Properties())
                {
                    if (obj.Name == "FireList")
                    {
                        foreach (var obj2 in obj.Value)
                        {
                            serverConfigObj.configFileFDIDObjs.Add(new jsonConfigFileFDIDObj { FieldName = obj2.FieldName, FDID = obj2.FDID });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                for (var i = 1; i < body.Length; i++)
                {
                    string txt = $"FDID{i}";

                    var jsonFilePackage = (JObject)JsonConvert.DeserializeObject(body);

                    JToken fdidToken = jsonFilePackage.SelectToken("FireList[?(@.FieldName == '" + txt + "')]");

                    if (fdidToken != null)
                    {
                        string fdid = fdidToken.Value<string>("FDID");

                        serverConfigObj.configFileFDIDObjs.Add(new jsonConfigFileFDIDObj { FieldName = txt, FDID = fdid });
                    }
                    else
                    {
                        i = body.Length;
                    }
                }
            }

            return "";
        }

        public bool getProcessByName(string processName)
        {
            Process[] localbyName = Process.GetProcessesByName(processName);

            bool val = false;

            if (localbyName.Length > 1)
            {
                val = true;
            }

            Thread.Sleep(750);

            return val;
        }
    }
}

internal class serverConfigObj
{
    public static string ESSServer { get; set; }
    public static string MSPServer { get; set; }
    public static string CADServer { get; set; }
    public static string GISServer { get; set; }
    public static string GISInstance { get; set; }
    public static string MobileServer { get; set; }

    public static List<jsonConfigFileORIObj> configFileORIObjs = new List<jsonConfigFileORIObj>();
    public static List<jsonConfigFileFDIDObj> configFileFDIDObjs = new List<jsonConfigFileFDIDObj>();
}

internal class jsonConfigFileORIObj
{
    public string FieldName { get; set; }

    public string ORI { get; set; }
}

internal class jsonConfigFileFDIDObj
{
    public string FieldName { get; set; }

    public string FDID { get; set; }
}