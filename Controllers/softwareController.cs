using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;

namespace testInstallServer.Classes
{
    public class SoftwareController : ApiController
    {
        private loggingClass loggingClass = new loggingClass();
        private preReqStatusClass preReqStatusClass = new preReqStatusClass();
        private uninstallerClass uninstallerClass = new uninstallerClass();

        private string returnedValue = "";

        // GET
        // TODO #1 this needs to be replaced with something more useful -- maybe change name to GetHealthCheck()
        // this will allow a simple endpoint to check connectivity, also it could check the logfile for the agent and update for the text "ERROR"
        // counting up from the bottom of the file 50 or so lines
        public async Task<IHttpActionResult> GetString()
        {
            List<tupleData> tupleList = new List<tupleData>();

            return Json(tupleList);
        }

        // GET
       
        public async Task<IHttpActionResult> GetPresentFiles()
        {
            List<tupleData> tupleList = new List<tupleData>();
            var filePreReq = Directory.GetFiles(configValues.preReqRun);
            var fileAddOn = Directory.GetFiles(configValues.nwsAddonLocalRun);
            var fileClient = Directory.GetFiles(configValues.clientRun);

            List<string> custFilesPre = new List<string>()
            {
                masterPreReqList.dotNet47, masterPreReqList.dotNet48, masterPreReqList.sqlCE3532, masterPreReqList.sqlCE3564, masterPreReqList.sqlCE4032,
                masterPreReqList.sqlCE4064, masterPreReqList.nwpsGis32, masterPreReqList.nwpsGis64, masterPreReqList.msSync32, masterPreReqList.msSync64,
                masterPreReqList.msProServ64, masterPreReqList.msDbPro64, masterPreReqList.msProServ32, masterPreReqList.msProServ32, masterPreReqList.msDbPro32,
                masterPreReqList.nwpsUpdate, masterPreReqList.sqlClr32, masterPreReqList.sqlClr64, masterPreReqList.sqlClr32, masterPreReqList.sqlClr64,
                masterPreReqList.sqlClr201232, masterPreReqList.sqlClr201264, masterPreReqList.SCPD4, masterPreReqList.SCPD6, masterPreReqList.SCPD6AX
            };

            List<string> custFilesClient = new List<string>()
            {
                masterClientList.mspClient, masterClientList.cadClient32, masterClientList.cadClient64, masterClientList.cadIncObs64
            };

            List<string> custFilesAddon = new List<string>()
            {
            };

            // TODO 2.1
            //so it looks like our comparisons are still wonky
            //you may want to do two foreach loops 
            //     since we are checking one list to another list we would need to iterate through both lists.
            //        hint: you could actually drop the list searching code into the Classes/UtilityClass.cs file and off load the computational work to background thread
            //   
            //this is the response that lead me to this conclusion {\"responseCode\":\"400 Bad Request\",\"message\":\"System.Collections.Generic.List`1[System.String] not found on machine\"},

            foreach (var file in filePreReq)
            {
                if (file.Equals(custFilesPre))
                {
                    tupleList.Add(new tupleData() { responseCode = "200 OK", message = $"{custFilesPre} found on machine" });
                }
                else
                {
                    tupleList.Add(new tupleData()
                    { responseCode = "400 Bad Request", message = $"{custFilesPre} not found on machine" });
                }
            }

            foreach (var file in fileAddOn)
            {
                if (file.Equals(custFilesAddon))
                {
                    tupleList.Add(new tupleData() { responseCode = "200 OK", message = $"{custFilesAddon} found on machine" });
                }
                else
                {
                    tupleList.Add(new tupleData() { responseCode = "400 Bad Request", message = $"{custFilesAddon} not found on machine" });
                }
            }

            foreach (var file in fileClient)
            {
                if (file.Equals(custFilesClient))
                {
                    tupleList.Add(new tupleData() { responseCode = "200 OK", message = $"{custFilesClient} found on machine" });
                }
                else
                {
                    tupleList.Add(new tupleData() { responseCode = "400 Bad Request", message = $"{custFilesClient} not found on machine" });
                }
            }

            return Json(tupleList);
        }

        //GET
        //this searches through all of TEPS software (pre reqs, and Clients) to see what is installed/not installed
        public async Task<IHttpActionResult> GetInstalledSoftware()
        {
            List<tupleData> tupleList = new List<tupleData>();

            List<string> knownSoftwareList = new List<string>()
        {
            masterPreReqList.sqlCE3532Name,masterPreReqList.sqlCE3564Name, masterPreReqList.sqlCE4064Name, masterPreReqList.nwpsGis32Name, masterPreReqList.nwpsGis64Name,
            masterPreReqList.sql2008Clr32Name, masterPreReqList.sql2008Clr64Name, masterPreReqList.SCPD6Name,masterPreReqList.SCPD4Name, masterPreReqList.fireMobileName,
            masterPreReqList.policeMobileName, masterPreReqList.mergeName, masterPreReqList.printerName, masterPreReqList.printerDriverName, masterPreReqList.x86COM,
            masterPreReqList.x64COM, masterPreReqList.tepsUpdater, masterClientList.LERMS1, masterClientList.CAD2, masterClientList.incidentObserv1,  masterPreReqList.sql2012Clr32Name,
            masterPreReqList.sql2012Clr64Name
        };

            foreach (string s in knownSoftwareList)
            {
                if (preReqStatusClass.preReqCheckerAsync(s).Result.Equals(true))
                {
                    loggingClass.logEntryWriter($"{s} found on machine", "info");

                    tupleList.Add(new tupleData { responseCode = "200 OK", message = $"{s} found on machine" });
                }
                else
                {
                    loggingClass.logEntryWriter($"{s} not found on machine", "error");

                    tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = $"{s} not found on machine" });
                }
            }

            return Json(tupleList);
        }
    }
}