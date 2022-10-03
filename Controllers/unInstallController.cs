using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace testInstallServer.Classes
{
    public class unInstallController : ApiController
    {
        private loggingClass loggingClass = new loggingClass();
        private installerClass installerClass = new installerClass();
        private uninstallerClass uninstallerClass = new uninstallerClass();

        #region pre req uninstall

        //uninstalls SQL Compact 3.5
        public async Task<IHttpActionResult> PostUninstallSQLCE35Async()
        {
            List<tupleData> tupleList = new List<tupleData>();
            var uninstall1 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server Compact 3.5 SP2 x64 ENU");
            var uninstall2 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server Compact 3.5 SP2 ENU");

            if (uninstall1.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "Microsoft SQL Server Compact 3.5 SP2 x64 ENU - Uninstalled" });
            }
            if (uninstall2.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "Microsoft SQL Server Compact 3.5 SP2 x64 ENU - Uninstalled" });
            }

            return Json(tupleList);
        }

        //uninstalls GIS components
        public async Task<IHttpActionResult> PostUninstallGISAsync()
        {
            List<tupleData> tupleList = new List<tupleData>();
            var uninstall3 = await uninstallerClass.uninstallProgramAsync("New World GIS Components x64");
            var uninstall4 = await uninstallerClass.uninstallProgramAsync("New World GIS Components x86");

            if (uninstall3.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "New World GIS Components x64 - Uninstalled" });
            }
            if (uninstall4.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "New World GIS Components x86 - Uninstalled" });
            }

            return Json(tupleList);
        }

        //uninstalls the updater
        public async Task<IHttpActionResult> PostUninstallUpdaterAsync()
        {
            List<tupleData> tupleList = new List<tupleData>();
            if (uninstallerClass.uninstallProgramAsync("Enterprise Updater").Result.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "Enterprise Updater - Uninstalled" });
            }
            else
            {
                if (uninstallerClass.uninstallProgramAsync("New World Automatic Updater").Result.Equals(true))
                {
                    tupleList.Add(new tupleData { responseCode = "200 OK", message = "New World Automatic Updater - Uninstalled" });
                }
            }

            return Json(tupleList);
        }

        //uninstalls ScenePD
        public async Task<IHttpActionResult> PostUninstallScenePDAsync()
        {
            List<tupleData> tupleList = new List<tupleData>();
            var uninstall7 = await uninstallerClass.uninstallProgramAsync("ScenePD 6 ActiveX Control");
            var uninstall8 = await uninstallerClass.uninstallProgramAsync("ScenePD 6 Desktop Edition");

            if (uninstall7.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "ScenePD 6 ActiveX Control - Uninstalled" });
            }
            if (uninstall8.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "ScenePD 6 Desktop Edition - Uninstalled" });
            }

            return Json(tupleList);
        }

        //uninstalls SQL Compact 4.0
        public async Task<IHttpActionResult> PostUninstallSQLCE40Async()
        {
            List<tupleData> tupleList = new List<tupleData>();
            var uninstall9 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server Compact 4.0 x64 ENU");

            if (uninstall9.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "Microsoft SQL Server Compact 4.0 x64 ENU - Uninstalled" });
            }

            return Json(tupleList);
        }

        //uninstalls SQL CLR 
        public async Task<IHttpActionResult> PostUninstallSQLCLRAsync()
        {
            List<tupleData> tupleList = new List<tupleData>();
            var uninstall11 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server System CLR Types (x64)");
            var uninstall12 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server System CLR Types");

            if (uninstall11.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "Microsoft SQL Server System CLR Types (x64) - Uninstalled" });
            }
            if (uninstall12.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "Microsoft SQL Server System CLR Types - Uninstalled" });
            }

            return Json(tupleList);
        }

        //for testing, remove before deployment
        public async Task<IHttpActionResult> Post99TESTUninstallAsync()
        {
            List<tupleData> tupleList = new List<tupleData>();
            var uninstall91 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server Compact 3.5 SP2 x64 ENU");
            var uninstall92 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server Compact 3.5 SP2 ENU");

            if (uninstall91.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "Microsoft SQL Server Compact 3.5 SP2 x64 ENU - Uninstalled" });
            }
            if (uninstall92.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "Microsoft SQL Server Compact 3.5 SP2 x64 ENU - Uninstalled" });
            }

            var uninstall93 = await uninstallerClass.uninstallProgramAsync("New World GIS Components x64");
            var uninstall94 = await uninstallerClass.uninstallProgramAsync("New World GIS Components x86");

            if (uninstall93.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "New World GIS Components x64 - Uninstalled" });
            }
            if (uninstall94.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "New World GIS Components x86 - Uninstalled" });
            }

            if (uninstallerClass.uninstallProgramAsync("Enterprise Updater").Result.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "Enterprise Updater - Uninstalled" });
            }
            else
            {
                if (uninstallerClass.uninstallProgramAsync("New World Automatic Updater").Result.Equals(true))
                {
                    tupleList.Add(new tupleData { responseCode = "200 OK", message = "New World Automatic Updater - Uninstalled" });
                }
            }

            var uninstall97 = await uninstallerClass.uninstallProgramAsync("ScenePD 6 ActiveX Control");
            var uninstall98 = await uninstallerClass.uninstallProgramAsync("ScenePD 6 Desktop Edition");

            if (uninstall97.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "ScenePD 6 ActiveX Control - Uninstalled" });
            }
            if (uninstall98.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "ScenePD 6 Desktop Edition - Uninstalled" });
            }

            var uninstall99 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server Compact 4.0 x64 ENU");

            if (uninstall99.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "Microsoft SQL Server Compact 4.0 x64 ENU - Uninstalled" });
            }

            var uninstall911 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server System CLR Types (x64)");
            var uninstall912 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server System CLR Types");

            if (uninstall911.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "Microsoft SQL Server System CLR Types (x64) - Uninstalled" });
            }
            if (uninstall912.Equals(true))
            {
                tupleList.Add(new tupleData { responseCode = "200 OK", message = "Microsoft SQL Server System CLR Types - Uninstalled" });
            }

            return Json(tupleList);
        }

        #endregion pre req uninstall

        public async Task<IHttpActionResult> PostClientUninstallAsync(int ID)
        {
            List<tupleData> tupleList = new List<tupleData>();

            switch (ID)
            {
                case 1:

                    if (uninstallerClass.uninstallProgramAsync("New World MSP Client").Result.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "MSP - Uninstalled" });
                    }
                    else if (uninstallerClass.uninstallProgramAsync("New World Aegis MSP Client").Result.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "MSP - Uninstalled" });
                    }
                    else
                    {
                        var uninstall19 = await uninstallerClass.uninstallProgramAsync("New World Aegis Client");
                        if (uninstall19.Equals(true))
                        {
                            tupleList.Add(new tupleData { responseCode = "200 OK", message = "MSP - Uninstalled" });
                        }
                    }

                    break;

                case 2:

                    if (uninstallerClass.uninstallProgramAsync("Enterprise CAD Client").Result.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "CAD - Uninstalled" });
                    }
                    else
                    {
                        var uninstall20 = await uninstallerClass.uninstallProgramAsync("New World Enterprise CAD Client");

                        if (uninstall20.Equals(true))
                        {
                            tupleList.Add(new tupleData { responseCode = "200 OK", message = "CAD - Uninstalled" });
                        }
                    }

                    break;

                case 3:
                    var uninstall22 = await uninstallerClass.uninstallProgramAsync("Fire Mobile");
                    var uninstall23 = await uninstallerClass.uninstallProgramAsync("Law Enforcement Mobile");
                    var uninstall24 = await uninstallerClass.uninstallProgramAsync("Mobile Merge");

                    if (uninstall22.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "Fire Mobile - Uninstalled" });
                    }
                    if (uninstall23.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "Law Enforcement Mobile - Uninstalled" });
                    }
                    if (uninstall24.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "Mobile Merge - Uninstalled" });
                    }

                    break;

                case 4:

                    if (uninstallerClass.uninstallProgramAsync("Enterprise CAD Incident Observer Client").Result.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "Enterprise CAD Incident Observer Client - Uninstalled" });
                    }
                    else
                    {
                        var uninstall26 = await uninstallerClass.uninstallProgramAsync("New World Enterprise CAD Incident Observer Client");

                        if (uninstall26.Equals(true))
                        {
                            tupleList.Add(new tupleData { responseCode = "200 OK", message = "New World Enterprise CAD Incident Observer Client - Uninstalled" });
                        }
                    }

                    break;

                case 99:

                    if (uninstallerClass.uninstallProgramAsync("New World MSP Client").Result.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "MSP - Uninstalled" });
                    }
                    else if (uninstallerClass.uninstallProgramAsync("New World Aegis MSP Client").Result.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "MSP - Uninstalled" });
                    }
                    else
                    {
                        var uninstall19 = await uninstallerClass.uninstallProgramAsync("New World Aegis Client");
                        if (uninstall19.Equals(true))
                        {
                            tupleList.Add(new tupleData { responseCode = "200 OK", message = "MSP - Uninstalled" });
                        }
                    }

                    if (uninstallerClass.uninstallProgramAsync("Enterprise CAD Client").Result.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "CAD - Uninstalled" });
                    }
                    else
                    {
                        var uninstall20 = await uninstallerClass.uninstallProgramAsync("New World Enterprise CAD Client");

                        if (uninstall20.Equals(true))
                        {
                            tupleList.Add(new tupleData { responseCode = "200 OK", message = "CAD - Uninstalled" });
                        }
                    }

                    var uninstall922 = await uninstallerClass.uninstallProgramAsync("Fire Mobile");
                    var uninstall923 = await uninstallerClass.uninstallProgramAsync("Law Enforcement Mobile");
                    var uninstall924 = await uninstallerClass.uninstallProgramAsync("Mobile Merge");

                    if (uninstall922.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "Fire Mobile - Uninstalled" });
                    }
                    if (uninstall923.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "Law Enforcement Mobile - Uninstalled" });
                    }
                    if (uninstall924.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "Mobile Merge - Uninstalled" });
                    }

                    if (uninstallerClass.uninstallProgramAsync("Enterprise CAD Incident Observer Client").Result.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "Enterprise CAD Incident Observer Client - Uninstalled" });
                    }
                    else
                    {
                        var uninstall26 = await uninstallerClass.uninstallProgramAsync("New World Enterprise CAD Incident Observer Client");

                        if (uninstall26.Equals(true))
                        {
                            tupleList.Add(new tupleData { responseCode = "200 OK", message = "New World Enterprise CAD Incident Observer Client - Uninstalled" });
                        }
                    }

                    break;

                default:
                    break;
            }

            return Json(tupleList);
        }
    }
}