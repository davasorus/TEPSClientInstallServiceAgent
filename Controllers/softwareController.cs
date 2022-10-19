using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using TEPSClientInstallService.Classes;

namespace testInstallServer.Classes
{
    public class SoftwareController : ApiController
    {
        private loggingClass loggingClass = new loggingClass();
        private preReqStatusClass preReqStatusClass = new preReqStatusClass();
        private uninstallerClass uninstallerClass = new uninstallerClass();
        private utilityClass utilityClass = new utilityClass();

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

        //TODO # 4
        // we need to pass the body to a parser in the utility class, and we want to return the string that contains the "hostname"
        // from there we want to store that value into a public static global string variable
        // from there we want to return 200 OK "Successfully configured" if the parser works, 400 Bad Request and then pass the error back as a string
        // HINT: you will need similar code that is already on the master service to handle the json array in the parser
        //POST
        public async Task<IHttpActionResult> PostConfiguration()
        {
            List<tupleData> tupleList = new List<tupleData>();

            var response = utilityClass.parseRequestBodyForMasterHost(Request.Content.ReadAsStringAsync().Result);

            return Json(tupleList);
        }

        // GET
        //returns a list of lists of files that should be present on machine

        public async Task<IHttpActionResult> GetPresentFiles()
        {
            List<tupleData> tupleList = new List<tupleData>();

            List<string> localPreReqs = new List<string>()
            {
                masterPreReqList.dotNet48, masterPreReqList.sqlCE3532, masterPreReqList.sqlCE3564, masterPreReqList.sqlCE4032,
                masterPreReqList.sqlCE4064, masterPreReqList.nwpsGis32, masterPreReqList.nwpsGis64, masterPreReqList.msSync32, masterPreReqList.msSync64,
                masterPreReqList.msProServ64, masterPreReqList.msDbPro64, masterPreReqList.msProServ32, masterPreReqList.msProServ32, masterPreReqList.msDbPro32,
                masterPreReqList.nwpsUpdate, masterPreReqList.sqlClr32, masterPreReqList.sqlClr64, masterPreReqList.sqlClr32, masterPreReqList.sqlClr64,
                masterPreReqList.sqlClr201232, masterPreReqList.sqlClr201264, masterPreReqList.SCPD4, masterPreReqList.SCPD6
            };

            List<string> localInstallers = new List<string>()
            {
                masterClientList.mspClient, masterClientList.cadClient64, masterClientList.cadIncObs64
            };

            List<string> localCustomAddons = new List<string>()
            {
            };

            try
            {
                foreach (string item in localPreReqs)
                {
                    if (File.Exists(Path.Combine(configValues.preReqRun, item)))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = $"{item} found on machine" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = $"{item} not found" });
                    }
                }
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");

                tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = ex.ToString() });
            }

            try
            {
                foreach (string item in localInstallers)
                {
                    if (File.Exists(Path.Combine(configValues.clientRun, item)))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = $"{item} found on machine" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = $"{item} not found" });
                    }
                }
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");

                tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = ex.ToString() });
            }

            try
            {
                foreach (string item in localCustomAddons)
                {
                    if (File.Exists(Path.Combine(configValues.nwsAddonLocalRun, item)))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = $"{item} found on machine" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = $"{item} not found" });
                    }
                }
            }
            catch (Exception ex)
            {
                loggingClass.logEntryWriter(ex.ToString(), "error");

                tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = ex.ToString() });
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