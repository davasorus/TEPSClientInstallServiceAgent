using System;
using System.Management;
using System.Threading.Tasks;

namespace testInstallServer.Classes
{
    public class preReqStatusClass
    {
        private readonly string nwpsUpdate = "NewWorld.Management.Updater.msi";
        private readonly string sqlClr32 = "SQLSysClrTypesx86.msi";
        private readonly string sqlClr64 = "SQLSysClrTypesx64.msi";
        private readonly string SCPD6 = "SPD6-4-8993.exe";
        private readonly string SCPD6AX = "SPDX6-4-3091.exe";
        private readonly string SCPD4 = "SPD4-0-92.exe";
        private readonly string sqlCE3532 = "SSCERuntime_x86-ENU.msi";
        private readonly string sqlCE3564 = "SSCERuntime_x64-ENU.msi";
        private readonly string sqlCE4064 = "SSCERuntime_x64-ENU-4.0.exe";
        private readonly string nwpsGis32 = "NewWorld.Gis.Components.x86.msi";
        private readonly string nwpsGis64 = "NewWorld.Gis.Components.x64.msi";

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

        private bool present;
        private bool present1;

        private loggingClass loggingClass = new loggingClass();

        //checks to see if the pre req was installed
        public async Task<bool> preReqCheckerAsync(string programName)
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher(
                @"SELECT * FROM Win32_Product WHERE Name = '" + programName + "'");
            foreach (ManagementObject mo in mos.Get())
            {
                try
                {
                    if (mo["Name"].ToString() == programName)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    string logEntry2 = ex.ToString();

                    loggingClass.logEntryWriter(logEntry2, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
                }
            }

            //none found
            return false;
        }

        //used to modify the pre req checker tab from the Uninstall function
        public bool uninstallChecker(string programName)
        {
            string[] preReqNames = { nwpsUpdateName, sqlCE4064Name, nwpsGis64Name,nwpsGis32Name,sqlClr64Name,sqlClr32Name, sqlCE3564Name, sqlCE3532Name
                    , SCPD6Name,SCPD4Name };

            foreach (string preReqName in preReqNames)
            {
                if (programName == preReqName)
                {
                    if (programName.ToString() == nwpsUpdateName)
                    {
                        if (preReqStatusObj.Updater == "Uninstalled")
                        {
                            present = true;
                        }
                        else
                        {
                            preReqStatusObj.Updater = "Uninstalled";

                            present = false;
                        }
                    }

                    if (programName.ToString() == sqlCE4064Name)
                    {
                        if (preReqStatusObj.SqlCompact4 == "Uninstalled")
                        {
                            present = true;
                        }
                        else
                        {
                            preReqStatusObj.SqlCompact4 = "Uninstalled";

                            present = false;
                        }
                    }

                    if (programName.ToString() == nwpsGis32Name)
                    {
                        if (preReqStatusObj.GISComp32 == "Uninstalled")
                        {
                            present = true;
                        }
                        else
                        {
                            preReqStatusObj.GISComp32 = "Uninstalled";

                            present = false;
                        }
                    }

                    if (programName.ToString() == nwpsGis64Name)
                    {
                        if (preReqStatusObj.GISComp64 == "Uninstalled")
                        {
                            present = true;
                        }
                        else
                        {
                            preReqStatusObj.GISComp64 = "Uninstalled";

                            present = false;
                        }
                    }

                    if (programName.ToString() == sqlClr32Name)
                    {
                        if (preReqStatusObj.SQLCLR32 == "Uninstalled")
                        {
                            present = true;
                        }
                        else
                        {
                            preReqStatusObj.SQLCLR32 = "Uninstalled";

                            present = false;
                        }
                    }

                    if (programName.ToString() == sqlClr64Name)
                    {
                        if (preReqStatusObj.SQLCLR64 == "Uninstalled")
                        {
                            present = true;
                        }
                        else
                        {
                            preReqStatusObj.SQLCLR64 = "Uninstalled";

                            present = false;
                        }
                    }

                    if (programName.ToString() == sqlCE3532Name)
                    {
                        if (preReqStatusObj.SQLCompact3532 == "Uninstalled")
                        {
                            present = true;
                        }
                        else
                        {
                            preReqStatusObj.SQLCompact3532 = "Uninstalled";

                            present = false;
                        }
                    }

                    if (programName.ToString() == sqlCE3564Name)
                    {
                        if (preReqStatusObj.SQLCompact3564 == "Uninstalled")
                        {
                            present = true;
                        }
                        else
                        {
                            preReqStatusObj.SQLCompact3564 = "Uninstalled";

                            present = false;
                        }
                    }

                    if (programName.ToString() == SCPD6Name)
                    {
                        if (preReqStatusObj.ScenePD == "Uninstalled")
                        {
                            present = true;
                        }
                        else
                        {
                            preReqStatusObj.ScenePD = "Uninstalled";

                            present = false;
                        }
                    }

                    if (programName.ToString() == SCPD4Name)
                    {
                        if (preReqStatusObj.ScenePD == "Uninstalled")
                        {
                            present = true;
                        }
                        else
                        {
                            preReqStatusObj.ScenePD = "Uninstalled";

                            present = false;
                        }
                    }
                }
                else
                {
                    present = false;
                }
            }

            return present;
        }

        //designed to modify the pre req checker tab from the run/install function
        public bool installChecker(string programName)
        {
            string[] preReqNames = { nwpsUpdate, sqlCE4064, nwpsGis64, nwpsGis32, sqlClr64, sqlClr32, sqlCE3564, sqlCE3532
                    , SCPD6 , SCPD6AX , SCPD4 };

            foreach (string preReqName in preReqNames)
            {
                if (programName == preReqName)
                {
                    if (programName.ToString() == nwpsUpdate)
                    {
                        if (preReqStatusObj.Updater == "Installed")
                        {
                            present1 = true;
                        }
                        else
                        {
                            preReqStatusObj.Updater = "Installed";

                            present1 = false;
                        }
                    }

                    if (programName.ToString() == sqlCE4064)
                    {
                        if (preReqStatusObj.SqlCompact4 == "Installed")
                        {
                            present1 = true;
                        }
                        else
                        {
                            preReqStatusObj.SqlCompact4 = "Installed";

                            present1 = false;
                        }
                    }

                    if (programName.ToString() == nwpsGis32)
                    {
                        if (preReqStatusObj.GISComp32 == "Installed")
                        {
                            present1 = true;
                        }
                        else
                        {
                            preReqStatusObj.GISComp32 = "Installed";

                            present1 = false;
                        }
                    }

                    if (programName.ToString() == nwpsGis64)
                    {
                        if (preReqStatusObj.GISComp64 == "Installed")
                        {
                            present1 = true;
                        }
                        else
                        {
                            preReqStatusObj.GISComp64 = "Installed";

                            present1 = false;
                        }
                    }

                    if (programName.ToString() == sqlClr32)
                    {
                        if (preReqStatusObj.SQLCLR32 == "Installed")
                        {
                            present1 = true;
                        }
                        else
                        {
                            preReqStatusObj.SQLCLR32 = "Installed";

                            present1 = false;
                        }
                    }

                    if (programName.ToString() == sqlClr64)
                    {
                        if (preReqStatusObj.SQLCLR64 == "Installed")
                        {
                            present1 = true;
                        }
                        else
                        {
                            preReqStatusObj.SQLCLR64 = "Installed";

                            present1 = true;
                        }
                    }

                    if (programName.ToString() == sqlCE3532)
                    {
                        if (preReqStatusObj.SQLCompact3532 == "Installed")
                        {
                            present1 = true;
                        }
                        else
                        {
                            preReqStatusObj.SQLCompact3532 = "Installed";

                            present1 = false;
                        }
                    }

                    if (programName.ToString() == sqlCE3564)
                    {
                        if (preReqStatusObj.SQLCompact3564 == "Installed")
                        {
                            present1 = true;
                        }
                        else
                        {
                            preReqStatusObj.SQLCompact3564 = "Installed";

                            present1 = false;
                        }
                    }

                    if (programName.ToString() == SCPD6)
                    {
                        if (preReqStatusObj.ScenePD == "Installed")
                        {
                            present1 = true;
                        }
                        else
                        {
                            preReqStatusObj.ScenePD = "Installed";

                            present1 = false;
                        }
                    }

                    if (programName.ToString() == SCPD4)
                    {
                        if (preReqStatusObj.ScenePD == "Installed")
                        {
                            present1 = true;
                        }
                        else
                        {
                            preReqStatusObj.ScenePD = "Installed";

                            present1 = false;
                        }
                    }

                    if (programName.ToString() == SCPD6AX)
                    {
                        if (preReqStatusObj.ScenePD == "Installed")
                        {
                            present1 = true;
                        }
                        else
                        {
                            preReqStatusObj.ScenePD = "Installed";

                            present1 = false;
                        }
                    }
                }
                else
                {
                    present1 = false;
                }
            }

            return present1;
        }
    }
}

internal class preReqStatusObj
{
    public static string Updater { get; set; }

    public static string SqlCompact4 { get; set; }

    public static string GISComp32 { get; set; }

    public static string GISComp64 { get; set; }

    public static string SQLCLR32 { get; set; }

    public static string SQLCLR64 { get; set; }

    public static string SQLCompact3532 { get; set; }

    public static string SQLCompact3564 { get; set; }

    public static string ScenePD { get; set; }

    public static string ClientAdminTool { get; set; }
}