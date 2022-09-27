using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

        private async Task<string> parseORI(string body)
        {
            serverConfigObj.configFileORIObjs.Clear();

            try
            {
                for (var i = 1; i < body.Length; i++)
                {
                    string txt = $"ORI{i}";

                    var jsonFilePackage = (JObject)JsonConvert.DeserializeObject(body);

                    JToken oriToken = jsonFilePackage.SelectToken("PoliceList[?(@.FieldName == '" + txt + "')]");

                    if (oriToken != null)
                    {
                        string ori = oriToken.Value<string>("ORI");

                        serverConfigObj.configFileORIObjs.Add(new jsonConfigFileORIObj { FieldName = txt, ORI = ori });
                    }
                    else
                    {
                        i = body.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                string logEntry1 = ex.ToString();

                loggingClass.logEntryWriter(logEntry1, "error");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                return "error";
            }

            return "";
        }

        private async Task<string> parseFDID(string body)
        {
            serverConfigObj.configFileFDIDObjs.Clear();

            try
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
            catch (Exception ex)
            {
                string logEntry1 = ex.ToString();

                loggingClass.logEntryWriter(logEntry1, "error");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                return "error";
            }

            return "";
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