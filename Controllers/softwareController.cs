using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;

namespace testInstallServer.Classes
{
    public class SoftwareController : ApiController
    {
        private loggingClass loggingClass = new loggingClass();
        private preReqStatusClass preReqStatusClass = new preReqStatusClass();
        private uninstallerClass uninstallerClass = new uninstallerClass();

        private readonly string sqlCE3532Name = "Microsoft SQL Server Compact 3.5 SP2 ENU";
        private readonly string sqlCE3564Name = "Microsoft SQL Server Compact 3.5 SP2 x64 ENU";
        private readonly string sqlCE4064Name = "Microsoft SQL Server Compact 4.0 x64 ENU";
        private readonly string nwpsGis32Name = "New World GIS Components x86";
        private readonly string nwpsGis64Name = "New World GIS Components x64";
        private readonly string nwpsUpdateName = "New World Automatic Updater";
        private readonly string sql2008Clr32Name = "Microsoft SQL Server System CLR Types";
        private readonly string sql2008Clr64Name = "Microsoft SQL Server System CLR Types (x64)";
        private readonly string sql2012Clr32Name = "Microsoft SQL Server System CLR Types";
        private readonly string sql2012Clr64Name = "Microsoft SQL Server System CLR Types (x64)";
        private readonly string SCPD6Name = "ScenePD 6 Desktop Edition";
        private readonly string SCPD4Name = "ScenePD 4";
        private readonly string fireMobileName = "Fire Mobile";
        private readonly string policeMobileName = "Law Enforcement Mobile";
        private readonly string mergeName = "Mobile Merge";
        private readonly string printerName = "NWPS Enterprise Mobile PDF Printer";
        private readonly string printerDriverName = "novaPDF 8 Printer Driver";
        private readonly string x86COM = "novaPDF 8 SDK COM (x86)";
        private readonly string x64COM = "novaPDF 8 SDK COM (x64)";
        private readonly string tepsUpdater = "Enterprise Updater";
        private readonly string LERMS1 = "New World MSP Client";
        private readonly string CAD2 = "New World Enterprise CAD Client";
        private readonly string incidentObserv1 = "Enterprise CAD Incident Observer Client";

        private readonly string dotNet47 = "dotNetFx471_Full_setup_Offline.exe";
        private readonly string dotNet48 = "ndp48-x86-x64-allos-enu.exe";
        private readonly string sqlCE3532 = "SSCERuntime_x86-ENU.msi";
        private readonly string sqlCE3564 = "SSCERuntime_x64-ENU.msi";
        private readonly string sqlCE4032 = "SSCERuntime_x86-ENU-4.0.exe";
        private readonly string sqlCE4064 = "SSCERuntime_x64-ENU-4.0.exe";
        private readonly string nwpsGis32 = "NewWorld.Gis.Components.x86.msi";
        private readonly string nwpsGis64 = "NewWorld.Gis.Components.x64.msi";
        private readonly string msSync64 = "Synchronization-v2.1-x64-ENU.msi";
        private readonly string msProServ64 = "ProviderServices-v2.1-x64-ENU.msi";
        private readonly string msDbPro64 = "DatabaseProviders-v3.1-x64-ENU.msi";
        private readonly string msSync32 = "Synchronization-v2.1-x86-ENU.msi";
        private readonly string msProServ32 = "ProviderServices-v2.1-x86-ENU.msi";
        private readonly string msDbPro32 = "DatabaseProviders-v3.1-x86-ENU.msi";
        private readonly string nwpsUpdate = "NewWorld.Management.Updater.msi";
        private readonly string sqlClr32 = "SQLSysClrTypesx86.msi";
        private readonly string sqlClr64 = "SQLSysClrTypesx64.msi";
        private readonly string sqlClr201232 = "SQLSysClrTypes2012.msi";
        private readonly string sqlClr201264 = "SQLSysClrTypesx642012.msi";
        private readonly string SCPD6 = "SPD6-4-8993.exe";
        private readonly string SCPD6AX = "SPDX6-4-3091.exe";
        private readonly string SCPD4 = "SPD4-0-92.exe";
        private readonly string mspClient = "NewWorldMSPClient.msi";
        private readonly string cadClient64 = "NewWorld.Enterprise.CAD.Client.x64.msi";
        private readonly string cadClient32 = "NewWorld.Enterprise.CAD.Client.x86.msi";
        private readonly string cadIncObs64 = "NewWorld.Enterprise.CAD.IncidentObserver.x64.msi";
        private string returnedValue = "";

        private readonly string preReqRun = @"C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\PreReqs";
        private readonly string nwsAddonLocalRun = @"C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\Addons";
        private readonly string clientRun = @"C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\Clients";
        private static string applicationName = "TEPS Automated Client Install Agent " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        private readonly string logFileName = $@"C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\Logging\{applicationName}.json";

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
            var filePreReq = Directory.GetFiles(preReqRun);
            var fileAddOn = Directory.GetFiles(nwsAddonLocalRun);
            var fileClient = Directory.GetFiles(clientRun);

            List<string> custFilesPre = new List<string>()
            {
                dotNet47, dotNet48, sqlCE3532, sqlCE3564, sqlCE4032,sqlCE4064, nwpsGis32, nwpsGis64, msSync32,
                msSync64, msProServ64, msDbPro64, msProServ32, msProServ32, msDbPro32, nwpsUpdate, sqlClr32, sqlClr64,
                sqlClr32, sqlClr64, sqlClr201232, sqlClr201264, SCPD4, SCPD6, SCPD6AX
            };
            List<string> custFilesClient = new List<string>()
            {
                mspClient, cadClient32, cadClient64, cadIncObs64
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
            sqlCE3532Name,sqlCE3564Name, sqlCE4064Name, nwpsGis32Name, nwpsGis64Name, sql2008Clr32Name, sql2008Clr64Name,
            SCPD6Name,SCPD4Name, fireMobileName,policeMobileName, mergeName, printerName, printerDriverName, x86COM, x64COM,tepsUpdater,
            LERMS1, CAD2, incidentObserv1, sql2012Clr32Name, sql2012Clr64Name
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