﻿using Newtonsoft.Json;
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

        public async Task<string> PostPreReqUninstallAsync(int id)
        {
            List<string> response = new List<string>();
            switch (id)
            {
                case 1:
                    response.Add("None Defined, nothing uninstalled");
                    break;

                case 2:
                    var uninstall1 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server Compact 3.5 SP2 x64 ENU");
                    var uninstall2 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server Compact 3.5 SP2 ENU");

                    if (uninstall1.Equals(true))
                    {
                        response.Add("Microsoft SQL Server Compact 3.5 SP2 x64 ENU - Uninstalled");
                    }
                    if (uninstall2.Equals(true))
                    {
                        response.Add("Microsoft SQL Server Compact 3.5 SP2 x64 ENU - Uninstalled");
                    }

                    break;

                case 3:
                    var uninstall3 = await uninstallerClass.uninstallProgramAsync("New World GIS Components x64");
                    var uninstall4 = await uninstallerClass.uninstallProgramAsync("New World GIS Components x86");

                    if (uninstall3.Equals(true))
                    {
                        response.Add("New World GIS Components x64 - Uninstalled");
                    }
                    if (uninstall4.Equals(true))
                    {
                        response.Add("New World GIS Components x86 - Uninstalled");
                    }

                    break;

                case 4:
                    response.Add("None Defined, nothing uninstalled");

                    break;

                case 5:

                    var uninstall6 = await uninstallerClass.uninstallProgramAsync("New World Automatic Updater");

                    var uninstall61 = await uninstallerClass.uninstallProgramAsync("Enterprise Updater");

                    if (uninstall6.Equals(true))
                    {
                        response.Add("New World Automatic Updater - Uninstalled");
                    }
                    if (uninstall61.Equals(true))
                    {
                        response.Add("Enterprise Updater - Uninstalled");
                    }

                    break;

                case 6:

                    var uninstall7 = await uninstallerClass.uninstallProgramAsync("ScenePD 6 ActiveX Control");
                    var uninstall8 = await uninstallerClass.uninstallProgramAsync("ScenePD 6 Desktop Edition");

                    if (uninstall7.Equals(true))
                    {
                        response.Add("ScenePD 6 ActiveX Control - Uninstalled");
                    }
                    if (uninstall8.Equals(true))
                    {
                        response.Add("ScenePD 6 Desktop Edition - Uninstalled");
                    }

                    break;

                case 7:
                    var uninstall9 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server Compact 4.0 x64 ENU");

                    if (uninstall9.Equals(true))
                    {
                        response.Add("Microsoft SQL Server Compact 4.0 x64 ENU - Uninstalled");
                    }

                    break;

                case 8:
                    response.Add("None Defined, nothing uninstalled");

                    break;

                case 9:
                    var uninstall11 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server System CLR Types (x64)");
                    var uninstall12 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server System CLR Types");

                    if (uninstall11.Equals(true))
                    {
                        response.Add("Microsoft SQL Server System CLR Types (x64) - Uninstalled");
                    }
                    if (uninstall12.Equals(true))
                    {
                        response.Add("Microsoft SQL Server System CLR Types - Uninstalled");
                    }

                    break;

                case 10:

                    var uninstall13 = await uninstallerClass.uninstallProgramAsync("NWPS Enterprise Mobile PDF Printer");

                    var uninstall14 = await uninstallerClass.uninstallProgramAsync("novaPDF 8 Printer Driver");

                    var uninstall15 = await uninstallerClass.uninstallProgramAsync("novaPDF 8 SDK COM (x86)");

                    var uninstall16 = await uninstallerClass.uninstallProgramAsync("novaPDF 8 SDK COM (x64)");

                    if (uninstall13.Equals(true))
                    {
                        response.Add("NWPS Enterprise Mobile PDF Printer - Uninstalled");
                    }
                    if (uninstall14.Equals(true))
                    {
                        response.Add("novaPDF 8 Printer Driver - Uninstalled");
                    }
                    if (uninstall15.Equals(true))
                    {
                        response.Add("novaPDF 8 SDK COM (x86) - Uninstalled");
                    }
                    if (uninstall16.Equals(true))
                    {
                        response.Add("novaPDF 8 SDK COM (x64) - Uninstalled");
                    }

                    break;

                case 99:
                    var uninstall91 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server Compact 3.5 SP2 x64 ENU");
                    var uninstall92 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server Compact 3.5 SP2 ENU");

                    if (uninstall91.Equals(true))
                    {
                        response.Add("Microsoft SQL Server Compact 3.5 SP2 x64 ENU - Uninstalled");
                    }
                    if (uninstall92.Equals(true))
                    {
                        response.Add("Microsoft SQL Server Compact 3.5 SP2 x64 ENU - Uninstalled");
                    }

                    var uninstall93 = await uninstallerClass.uninstallProgramAsync("New World GIS Components x64");
                    var uninstall94 = await uninstallerClass.uninstallProgramAsync("New World GIS Components x86");

                    if (uninstall93.Equals(true))
                    {
                        response.Add("New World GIS Components x64 - Uninstalled");
                    }
                    if (uninstall94.Equals(true))
                    {
                        response.Add("New World GIS Components x86 - Uninstalled");
                    }

                    var uninstall96 = await uninstallerClass.uninstallProgramAsync("New World Automatic Updater");
                    var uninstall961 = await uninstallerClass.uninstallProgramAsync("Enterprise Updater");

                    if (uninstall96.Equals(true))
                    {
                        response.Add("New World Automatic Updater - Uninstalled");
                    }
                    if (uninstall961.Equals(true))
                    {
                        response.Add("Enterprise Updater - Uninstalled");
                    }

                    var uninstall97 = await uninstallerClass.uninstallProgramAsync("ScenePD 6 ActiveX Control");
                    var uninstall98 = await uninstallerClass.uninstallProgramAsync("ScenePD 6 Desktop Edition");

                    if (uninstall97.Equals(true))
                    {
                        response.Add("ScenePD 6 ActiveX Control - Uninstalled");
                    }
                    if (uninstall98.Equals(true))
                    {
                        response.Add("ScenePD 6 Desktop Edition - Uninstalled");
                    }

                    var uninstall99 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server Compact 4.0 x64 ENU");

                    if (uninstall99.Equals(true))
                    {
                        response.Add("Microsoft SQL Server Compact 4.0 x64 ENU - Uninstalled");
                    }

                    var uninstall911 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server System CLR Types (x64)");
                    var uninstall912 = await uninstallerClass.uninstallProgramAsync("Microsoft SQL Server System CLR Types");

                    if (uninstall911.Equals(true))
                    {
                        response.Add("Microsoft SQL Server System CLR Types (x64) - Uninstalled");
                    }
                    if (uninstall912.Equals(true))
                    {
                        response.Add("Microsoft SQL Server System CLR Types - Uninstalled");
                    }

                    var uninstall913 = await uninstallerClass.uninstallProgramAsync("NWPS Enterprise Mobile PDF Printer");

                    var uninstall914 = await uninstallerClass.uninstallProgramAsync("novaPDF 8 Printer Driver");

                    var uninstall915 = await uninstallerClass.uninstallProgramAsync("novaPDF 8 SDK COM (x86)");

                    var uninstall916 = await uninstallerClass.uninstallProgramAsync("novaPDF 8 SDK COM (x64)");

                    if (uninstall913.Equals(true))
                    {
                        response.Add("NWPS Enterprise Mobile PDF Printer - Uninstalled");
                    }
                    if (uninstall914.Equals(true))
                    {
                        response.Add("novaPDF 8 Printer Driver - Uninstalled");
                    }
                    if (uninstall915.Equals(true))
                    {
                        response.Add("novaPDF 8 SDK COM (x86) - Uninstalled");
                    }
                    if (uninstall916.Equals(true))
                    {
                        response.Add("novaPDF 8 SDK COM (x64) - Uninstalled");
                    }

                    var uninstall917 = await uninstallerClass.uninstallProgramAsync("New World MSP Client");
                    var uninstall918 = await uninstallerClass.uninstallProgramAsync("New World Aegis MSP Client");
                    var uninstall919 = await uninstallerClass.uninstallProgramAsync("New World Aegis Client");

                    if (uninstall917.Equals(true))
                    {
                        response.Add("MSP - Uninstalled");
                    }
                    if (uninstall918.Equals(true))
                    {
                        response.Add("MSP- Uninstalled");
                    }
                    if (uninstall919.Equals(true))
                    {
                        response.Add("MSP - Uninstalled");
                    }

                    var uninstall920 = await uninstallerClass.uninstallProgramAsync("New World Enterprise CAD Client");
                    var uninstall921 = await uninstallerClass.uninstallProgramAsync("Enterprise CAD Client");

                    if (uninstall920.Equals(true))
                    {
                        response.Add("CAD - Uninstalled");
                    }
                    if (uninstall921.Equals(true))
                    {
                        response.Add("CAD - Uninstalled");
                    }

                    var uninstall922 = await uninstallerClass.uninstallProgramAsync("Fire Mobile");
                    var uninstall923 = await uninstallerClass.uninstallProgramAsync("Law Enforcement Mobile");
                    var uninstall924 = await uninstallerClass.uninstallProgramAsync("Mobile Merge");

                    if (uninstall922.Equals(true))
                    {
                        response.Add("Fire Mobile - Uninstalled");
                    }
                    if (uninstall923.Equals(true))
                    {
                        response.Add("Law Enforcement Mobile - Uninstalled");
                    }
                    if (uninstall924.Equals(true))
                    {
                        response.Add("Mobile Merge - Uninstalled");
                    }

                    var uninstall925 = await uninstallerClass.uninstallProgramAsync("Enterprise CAD Incident Observer Client");
                    var uninstall926 = await uninstallerClass.uninstallProgramAsync("New World Enterprise CAD Incident Observer Client");

                    if (uninstall925.Equals(true))
                    {
                        response.Add("Enterprise CAD Incident Observer Client - Uninstalled");
                    }
                    if (uninstall926.Equals(true))
                    {
                        response.Add("New World Enterprise CAD Incident Observer Client - Uninstalled");
                    }

                    break;

                default:
                    break;
            }

            var jsonReturn = JsonConvert.SerializeObject(response);

            return jsonReturn;
        }

        public async Task<string> PostClientUninstallAsync(int ID)
        {
            List<string> response = new List<string>();

            switch (ID)
            {
                case 1:
                    var uninstall17 = await uninstallerClass.uninstallProgramAsync("New World MSP Client");
                    var uninstall18 = await uninstallerClass.uninstallProgramAsync("New World Aegis MSP Client");
                    var uninstall19 = await uninstallerClass.uninstallProgramAsync("New World Aegis Client");

                    if (uninstall17.Equals(true))
                    {
                        response.Add("MSP - Uninstalled");
                    }
                    if (uninstall18.Equals(true))
                    {
                        response.Add("MSP- Uninstalled");
                    }
                    if (uninstall19.Equals(true))
                    {
                        response.Add("MSP - Uninstalled");
                    }
                    break;

                case 2:
                    var uninstall20 = await uninstallerClass.uninstallProgramAsync("New World Enterprise CAD Client");
                    var uninstall21 = await uninstallerClass.uninstallProgramAsync("Enterprise CAD Client");

                    if (uninstall20.Equals(true))
                    {
                        response.Add("CAD - Uninstalled");
                    }
                    if (uninstall21.Equals(true))
                    {
                        response.Add("CAD - Uninstalled");
                    }

                    break;

                case 3:
                    var uninstall22 = await uninstallerClass.uninstallProgramAsync("Fire Mobile");
                    var uninstall23 = await uninstallerClass.uninstallProgramAsync("Law Enforcement Mobile");
                    var uninstall24 = await uninstallerClass.uninstallProgramAsync("Mobile Merge");

                    if (uninstall22.Equals(true))
                    {
                        response.Add("Fire Mobile - Uninstalled");
                    }
                    if (uninstall23.Equals(true))
                    {
                        response.Add("Law Enforcement Mobile - Uninstalled");
                    }
                    if (uninstall24.Equals(true))
                    {
                        response.Add("Mobile Merge - Uninstalled");
                    }

                    break;

                case 4:
                    var uninstall25 = await uninstallerClass.uninstallProgramAsync("Enterprise CAD Incident Observer Client");
                    var uninstall26 = await uninstallerClass.uninstallProgramAsync("New World Enterprise CAD Incident Observer Client");

                    if (uninstall25.Equals(true))
                    {
                        response.Add("Enterprise CAD Incident Observer Client - Uninstalled");
                    }
                    if (uninstall26.Equals(true))
                    {
                        response.Add("New World Enterprise CAD Incident Observer Client - Uninstalled");
                    }

                    break;

                case 99:

                    var uninstall917 = await uninstallerClass.uninstallProgramAsync("New World MSP Client");
                    var uninstall918 = await uninstallerClass.uninstallProgramAsync("New World Aegis MSP Client");
                    var uninstall919 = await uninstallerClass.uninstallProgramAsync("New World Aegis Client");

                    if (uninstall917.Equals(true))
                    {
                        response.Add("MSP - Uninstalled");
                    }
                    if (uninstall918.Equals(true))
                    {
                        response.Add("MSP- Uninstalled");
                    }
                    if (uninstall919.Equals(true))
                    {
                        response.Add("MSP - Uninstalled");
                    }

                    var uninstall920 = await uninstallerClass.uninstallProgramAsync("New World Enterprise CAD Client");
                    var uninstall921 = await uninstallerClass.uninstallProgramAsync("Enterprise CAD Client");

                    if (uninstall920.Equals(true))
                    {
                        response.Add("CAD - Uninstalled");
                    }
                    if (uninstall921.Equals(true))
                    {
                        response.Add("CAD - Uninstalled");
                    }

                    var uninstall922 = await uninstallerClass.uninstallProgramAsync("Fire Mobile");
                    var uninstall923 = await uninstallerClass.uninstallProgramAsync("Law Enforcement Mobile");
                    var uninstall924 = await uninstallerClass.uninstallProgramAsync("Mobile Merge");

                    if (uninstall922.Equals(true))
                    {
                        response.Add("Fire Mobile - Uninstalled");
                    }
                    if (uninstall923.Equals(true))
                    {
                        response.Add("Law Enforcement Mobile - Uninstalled");
                    }
                    if (uninstall924.Equals(true))
                    {
                        response.Add("Mobile Merge - Uninstalled");
                    }

                    var uninstall925 = await uninstallerClass.uninstallProgramAsync("Enterprise CAD Incident Observer Client");
                    var uninstall926 = await uninstallerClass.uninstallProgramAsync("New World Enterprise CAD Incident Observer Client");

                    if (uninstall925.Equals(true))
                    {
                        response.Add("Enterprise CAD Incident Observer Client - Uninstalled");
                    }
                    if (uninstall926.Equals(true))
                    {
                        response.Add("New World Enterprise CAD Incident Observer Client - Uninstalled");
                    }

                    break;

                default:
                    break;
            }

            var jsonReturn = JsonConvert.SerializeObject(response);

            return jsonReturn;
        }
    }
}