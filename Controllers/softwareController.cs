using Newtonsoft.Json;
using System.Collections.Generic;
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
        private readonly string nwUpdater = "New World Automatic Updater";
        private readonly string tepsUpdater = "Enterprise Updater";
        private readonly string LERMS1 = "New World MSP Client";
        private readonly string LERMS2 = "New World Aegis MSP Client";
        private readonly string LERMS3 = "New World Aegis Client";
        private readonly string CAD1 = "New World Enterprise CAD Client";
        private readonly string CAD2 = "Enterprise CAD Client";
        private readonly string incidentObserv1 = "Enterprise CAD Incident Observer Client";
        private readonly string incidentObserv2 = "New World Enterprise CAD Incident Observer Client";

        // GET api/values
        public string GetString()
        {
            loggingClass.logEntryWriter("GetString end point hit, values returned", "info");

            List<string> knownSoftwareList = new List<string>()
        {
            sqlCE3532Name,sqlCE3564Name, sqlCE4064Name, nwpsGis32Name, nwpsGis64Name,nwpsUpdateName, sqlClr32Name, sqlClr64Name,
            SCPD6Name,SCPD4Name, fireMobileName,policeMobileName, mergeName, printerName, printerDriverName, x86COM, x64COM, nwUpdater,
            tepsUpdater, LERMS1, LERMS2, LERMS3, CAD1, CAD2, incidentObserv1, incidentObserv2
        };

            foreach (string knownSoftware in knownSoftwareList)
            {
                loggingClass.logEntryWriter($"{knownSoftware} returned in list", "info");
            }

            var jsonReturn = JsonConvert.SerializeObject(knownSoftwareList);

            return jsonReturn;
        }

        // GET api/values/5
        public string GetStringbyID(int id)
        {
            loggingClass.logEntryWriter("GetStringbyID end point hit, value returned", "info");

            List<string> knownSoftwareList = new List<string>()
        {
            sqlCE3532Name,sqlCE3564Name, sqlCE4064Name, nwpsGis32Name, nwpsGis64Name,nwpsUpdateName, sqlClr32Name, sqlClr64Name,
            SCPD6Name,SCPD4Name, fireMobileName,policeMobileName, mergeName, printerName, printerDriverName, x86COM, x64COM, nwUpdater,
            tepsUpdater, LERMS1, LERMS2, LERMS3, CAD1, CAD2, incidentObserv1, incidentObserv2
        };

            loggingClass.logEntryWriter($"{knownSoftwareList[id]} returned for int {id}", "info");

            var jsonReturn = JsonConvert.SerializeObject(knownSoftwareList[id]);

            return jsonReturn;
        }

        public string GetInstalledSoftware()
        {
            List<string> knownSoftwareList = new List<string>()
        {
            sqlCE3532Name,sqlCE3564Name, sqlCE4064Name, nwpsGis32Name, nwpsGis64Name,nwpsUpdateName, sqlClr32Name, sqlClr64Name,
            SCPD6Name,SCPD4Name, fireMobileName,policeMobileName, mergeName, printerName, printerDriverName, x86COM, x64COM, nwUpdater,
            tepsUpdater, LERMS1, LERMS2, LERMS3, CAD1, CAD2, incidentObserv1, incidentObserv2
        };

            List<string> installedSoftware = new List<string>();

            foreach (string s in knownSoftwareList)
            {
                if (preReqStatusClass.preReqCheckerAsync(s).Result.Equals(true))
                {
                    installedSoftware.Add(s);

                    loggingClass.logEntryWriter($"{s} returned in list", "info");
                }
            }

            var jsonReturn = JsonConvert.SerializeObject(installedSoftware);

            return jsonReturn;
        }
    }
}