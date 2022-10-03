﻿using System.Collections.Generic;
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
        private readonly string sqlClr32Name = "Microsoft SQL Server System CLR Types";
        private readonly string sqlClr64Name = "Microsoft SQL Server System CLR Types (x64)";
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
        private readonly string LERMS2 = "New World Aegis MSP Client";
        private readonly string LERMS3 = "New World Aegis Client";
        private readonly string CAD1 = "New World Enterprise CAD Client";
        private readonly string CAD2 = "Enterprise CAD Client";
        private readonly string incidentObserv1 = "Enterprise CAD Incident Observer Client";
        private readonly string incidentObserv2 = "New World Enterprise CAD Incident Observer Client";

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
        // TODO #2 this needs to be replaced with something more useful -- change the name to GetPresentFiles() returns the files present locally in the sub directories
        // C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\Clients AND \PreReqs AND \Addons Folders
        public async Task<IHttpActionResult> GetStringbyID()
        {
            List<tupleData> tupleList = new List<tupleData>();

            return Json(tupleList);
        }

        //GET
        //this searches through all of TEPS software (pre reqs, and Clients) to see what is installed/not installed
        public async Task<IHttpActionResult> GetInstalledSoftware()
        {
            List<tupleData> tupleList = new List<tupleData>();

            List<string> knownSoftwareList = new List<string>()
        {
            sqlCE3532Name,sqlCE3564Name, sqlCE4064Name, nwpsGis32Name, nwpsGis64Name,nwpsUpdateName, sqlClr32Name, sqlClr64Name,
            SCPD6Name,SCPD4Name, fireMobileName,policeMobileName, mergeName, printerName, printerDriverName, x86COM, x64COM,tepsUpdater, 
            LERMS1, LERMS2, LERMS3, CAD1, CAD2, incidentObserv1, incidentObserv2
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
                    loggingClass.logEntryWriter($"{s} not found on machine", "Error");

                    tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = $"{s} not found on machine" });
                }
            }

            return Json(tupleList);
        }
    }
}