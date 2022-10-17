using System;
using System.Management;
using System.Threading.Tasks;

namespace testInstallServer.Classes
{
    public class preReqStatusClass
    {
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
            string[] preReqNames = { masterPreReqList.nwpsUpdateName, masterPreReqList.sqlCE4064Name, masterPreReqList.nwpsGis64Name,masterPreReqList.nwpsGis32Name,
            masterPreReqList.sql2008Clr64Name,masterPreReqList.sql2008Clr64Name, masterPreReqList.sqlCE3564Name, masterPreReqList.sqlCE3532Name, masterPreReqList.SCPD6Name,
            masterPreReqList.SCPD4Name };

            foreach (string preReqName in preReqNames)
            {
                if (programName == preReqName)
                {
                    if (programName.ToString() == masterPreReqList.nwpsUpdateName)
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

                    if (programName.ToString() == masterPreReqList.sqlCE4064Name)
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

                    if (programName.ToString() == masterPreReqList.nwpsGis32Name)
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

                    if (programName.ToString() == masterPreReqList.nwpsGis64Name)
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

                    if (programName.ToString() == masterPreReqList.sql2008Clr32Name)
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

                    if (programName.ToString() == masterPreReqList.sql2008Clr64Name)
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

                    if (programName.ToString() == masterPreReqList.sqlCE3532Name)
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

                    if (programName.ToString() == masterPreReqList.sqlCE3564Name)
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

                    if (programName.ToString() == masterPreReqList.SCPD6Name)
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

                    if (programName.ToString() == masterPreReqList.SCPD4Name)
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
            string[] preReqNames = { masterPreReqList.nwpsUpdate, masterPreReqList.sqlCE4064, masterPreReqList.nwpsGis64, masterPreReqList.nwpsGis32, masterPreReqList.sqlClr64,
                masterPreReqList.sqlClr32, masterPreReqList.sqlCE3564, masterPreReqList.sqlCE3532, masterPreReqList.SCPD6 , masterPreReqList.SCPD6AX , masterPreReqList.SCPD4 };

            foreach (string preReqName in preReqNames)
            {
                if (programName == preReqName)
                {
                    if (programName.ToString() == masterPreReqList.nwpsUpdate)
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

                    if (programName.ToString() == masterPreReqList.sqlCE4064)
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

                    if (programName.ToString() == masterPreReqList.nwpsGis32)
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

                    if (programName.ToString() == masterPreReqList.nwpsGis64)
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

                    if (programName.ToString() == masterPreReqList.sqlClr32)
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

                    if (programName.ToString() == masterPreReqList.sqlClr64)
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

                    if (programName.ToString() == masterPreReqList.sqlCE3532)
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

                    if (programName.ToString() == masterPreReqList.sqlCE3564)
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

                    if (programName.ToString() == masterPreReqList.SCPD6)
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

                    if (programName.ToString() == masterPreReqList.SCPD4)
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

                    if (programName.ToString() == masterPreReqList.SCPD6AX)
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