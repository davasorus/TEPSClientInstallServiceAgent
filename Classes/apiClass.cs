using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace testInstallServer.Classes
{
    public class apiClass
    {
        private loggingClass loggingClass = new loggingClass();

        //actual async task to get all db entries (will return entries as well)
        public async Task getAll()
        {
            try
            {
                var httpClient = new HttpClient();
                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                if (defaultRequestHeaders.Accept == null ||
                   !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new
                      MediaTypeWithQualityHeaderValue("application/json"));
                }

                HttpResponseMessage response = await httpClient.GetAsync("https://davasoruswebapi.azurewebsites.net/api/webapi/public/history");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    string logEntry = json;

                    List<apiObj> list = new List<apiObj>();

                    var json1 = json;
                    var objects = JsonConvert.DeserializeObject<List<apiObj>>(json1);

                    foreach (var obj in objects)
                    {
                        loggingClass.logEntryWriter(obj.appName, "info");

                        if (obj.appName.Contains("Copy Field Reports"))
                        {
                            string date = obj.tansactionDateTime;

                            var parsedDate = DateTime.Parse(date);

                            DateTime jsonDate = parsedDate.ToLocalTime();

                            //this.Dispatcher.Invoke(() => apiHistoryObjs.Collection.Add(new apiHistoryObj { AppName = obj.appName, AppVersion = obj.appVersion, ReleaseNotes = obj.releaseNotes, Date = jsonDate.ToString() }));
                        }
                    }
                }
                else
                {
                    string logEntry1 = $" Failed to call the Web Api: {response.StatusCode}";

                    loggingClass.nLogLogger(logEntry1, "error");

                    string content = await response.Content.ReadAsStringAsync();
                    string logEntry2 = $" Content: {content}";

                    loggingClass.nLogLogger(logEntry2, "error");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unable to connect to the remote server"))
                {
                    loggingClass.logEntryWriter("Unable to connect to end point, unable to check for update history", "error");

                    //this.Dispatcher.Invoke(() => apiHistoryObjs.Collection.Add(new apiHistoryObj { AppName = "Error", AppVersion = "0", ReleaseNotes = "Unable to retrieve update history", Date = DateTime.Now.ToString() }));
                }
                else
                {
                    string logEntry1 = ex.ToString();

                    loggingClass.logEntryWriter(logEntry1, "error");

                    //loggingClass.remoteErrorReporting(goodAppName, Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
                }
            }
        }
    }
}

internal class apiObj
{
    public string id { get; set; }
    public string appName { get; set; }
    public string appVersion { get; set; }
    public string releaseNotes { get; set; }
    public string tansactionDateTime { get; set; }
}

internal class jsonObj
{
    public string id { get; set; }
    public string appName { get; set; }
    public string appVersion { get; set; }
    public string releaseNotes { get; set; }
    public string tansactionDateTime { get; set; }
}