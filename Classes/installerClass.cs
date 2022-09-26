using System;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace testInstallServer.Classes
{
    internal class installerClass
    {
        private readonly string preReqRun = @"C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\PreReqs";
        private readonly string nwsAddonLocalRun = @"C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\Addons";
        private readonly string clientRun = @"C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\Clients";
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
        private readonly string sqlClr201232 = "SQLSysClrTypesx2012.msi";
        private readonly string sqlClr201264 = "SQLSysClrTypesx642012.msi";
        private readonly string SCPD6 = "SPD6-4-8993.exe";
        private readonly string SCPD6AX = "SPDX6-4-3091.exe";
        private readonly string SCPD4 = "SPD4-0-92.exe";
        private readonly string mspClient = "NewWorldMSPClient.msi";
        private readonly string cadClient64 = "NewWorld.Enterprise.CAD.Client.x64.msi";
        private readonly string cadClient32 = "NewWorld.Enterprise.CAD.Client.x86.msi";
        private readonly string cadIncObs64 = "NewWorld.Enterprise.CAD.IncidentObserver.x64.msi";
        private string returnedValue = "";

        private loggingClass loggingClass = new loggingClass();
        private copyClass copyClass = new copyClass();
        private preReqStatusClass preReqStatusClass = new preReqStatusClass();

        #region pre req install

        //dot net 4.7 or 4.8 installer
        public async Task<string> dotNetAsync(string installerPath)
        {
            loggingClass.logEntryWriter("Checking for .Net 3.5", "info");

            if (cmdScriptRun(@"/C Dism /online /Enable-Feature /FeatureName:""NetFx3""").Equals(true))
            {
                loggingClass.logEntryWriter(".Net 3.5 is installed", "info");
            }
            else
            {
                loggingClass.logEntryWriter(".Net 3.5 failed to install", "error");
            }

            if (Directory.Exists(preReqRun))
            {
                if (File.Exists(Path.Combine(preReqRun, dotNet47)))
                {
                    loggingClass.logEntryWriter("Running 4.7.1 .Net", "info");

                    try
                    {
                        returnedValue = installProgramAsync(dotNet47, preReqRun).Result;
                    }
                    catch (Exception ex)
                    {
                        if (ex.ToString().Contains("The system cannot find the file specified"))
                        {
                            string logEntry = $"unable to find {dotNet47}, could not install. Verify that it was downloaded.";
                            loggingClass.logEntryWriter(logEntry, "error");

                            returnedValue = "false";
                        }
                        if (ex.Message.Contains("Value cannot be null"))
                        {
                            string logEntry = ex.ToString();

                            loggingClass.logEntryWriter(logEntry, "error");
                            loggingClass.logEntryWriter($"The path on the primary tab is blank. Please put {nwpsUpdate} in {preReqRun} OR fill in the path. ", "debug");

                            returnedValue = "false";
                        }
                        else
                        {
                            string logEntry = ex.ToString();

                            loggingClass.logEntryWriter(logEntry, "error");

                            //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                            returnedValue = "false";
                        }
                    }
                }
                //install .net 4.8 if 4.7.1 is not present
                else
                {
                    loggingClass.logEntryWriter("Running 4.8 .Net", "info");

                    try
                    {
                        returnedValue = installProgramAsync(dotNet48, preReqRun).Result;
                    }
                    catch (Exception ex)
                    {
                        if (ex.ToString().Contains("The system cannot find the file specified"))
                        {
                            string logEntry = $"unable to find {dotNet48}, could not install. Verify that it was downloaded.";
                            loggingClass.logEntryWriter(logEntry, "error");

                            returnedValue = "false";
                        }
                        if (ex.Message.Contains("Value cannot be null"))
                        {
                            string logEntry = ex.ToString();

                            loggingClass.logEntryWriter(logEntry, "error");
                            loggingClass.logEntryWriter($"The path on the primary tab is blank. Please put {nwpsUpdate} in {preReqRun} OR fill in the path. ", "debug");

                            returnedValue = "false";
                        }
                        else
                        {
                            string logEntry = ex.ToString();

                            loggingClass.logEntryWriter(logEntry, "error");

                            //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                            returnedValue = "false";
                        }
                    }
                }
            }
            else
            {
                if (copyClass.preReqSearch(Path.Combine(installerPath, "_Client-Installation"), dotNet48) == 1)
                {
                    try
                    {
                        loggingClass.logEntryWriter("Running 4.8 .Net", "info");

                        returnedValue = preReqRunAsync(Path.Combine(installerPath, "_Client-Installation"), dotNet48).Result;
                    }
                    catch (Exception ex)
                    {
                        if (ex.ToString().Contains("The system cannot find the file specified"))
                        {
                            string logEntry = $"unable to find {dotNet48}, could not install. Verify that the network path is correct.";
                            loggingClass.logEntryWriter(logEntry, "error");

                            returnedValue = "false";
                        }
                        else
                        {
                            string logEntry = ex.ToString();

                            loggingClass.logEntryWriter(logEntry, "error");

                            //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                            returnedValue = "false";
                        }
                    }
                }
                else
                {
                    try
                    {
                        loggingClass.logEntryWriter("Running 4.7.1 .Net", "info");

                        returnedValue = preReqRunAsync(Path.Combine(installerPath, "_Client-Installation"), dotNet47).Result;
                    }
                    catch (Exception ex)
                    {
                        if (ex.ToString().Contains("The system cannot find the file specified"))
                        {
                            string logEntry = $"unable to find {dotNet47}, could not install. Verify that the network path is correct.";
                            loggingClass.logEntryWriter(logEntry, "error");

                            returnedValue = "false";
                        }
                        else
                        {
                            string logEntry = ex.ToString();

                            loggingClass.logEntryWriter(logEntry, "error");

                            //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                            returnedValue = "false";
                        }
                    }
                }
            }

            return returnedValue;
        }

        //SQL Compact 3.5 installer
        public async Task<string> sqlCe35Async(bool Is64Bit, string installerPath)
        {
            if (Is64Bit == true)
            {
                loggingClass.logEntryWriter("Running 32bit SQL Runtime", "info");

                try
                {
                    if (File.Exists(Path.Combine(preReqRun, sqlCE3532)))
                    {
                        returnedValue = installProgramAsync(sqlCE3532, preReqRun).Result;
                    }
                    else
                    {
                        returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), sqlCE3532);
                    }
                }
                catch (Exception ex)
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                    returnedValue = "false";
                }

                loggingClass.logEntryWriter("Running 64 bit SQL Runtime", "info");

                try
                {
                    if (File.Exists(Path.Combine(preReqRun, sqlCE3532)))
                    {
                        returnedValue = await installProgramAsync(sqlCE3564, preReqRun);
                    }
                    else
                    {
                        returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), sqlCE3564);
                    }
                }
                catch (Exception ex)
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                    returnedValue = "false";
                }
            }
            else
            {
                loggingClass.logEntryWriter("Running 32bit SQL Runtime", "info");

                try
                {
                    if (File.Exists(Path.Combine(preReqRun, sqlCE3532)))
                    {
                        returnedValue = await installProgramAsync(sqlCE3532, preReqRun);
                    }
                    else
                    {
                        returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), sqlCE3532);
                    }
                }
                catch (Exception ex)
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                    returnedValue = "false";
                }
            }

            return returnedValue;
        }

        //32bit and 64bit GIS installer
        public async Task<string> gisAsync(bool Is64Bit, string installerPath)
        {
            if (Is64Bit == true)
            {
                loggingClass.logEntryWriter("Running 32 bit GIS Components", "info");

                try
                {
                    if (File.Exists(Path.Combine(preReqRun, (nwpsGis32))))
                    {
                        returnedValue = await installProgramAsync(nwpsGis32, preReqRun);
                    }
                    else
                    {
                        returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), (nwpsGis32));
                    }
                }
                catch (Exception ex)
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                    returnedValue = "false";
                }

                loggingClass.logEntryWriter("Running 64 bit GIS Components", "info");

                try
                {
                    if (File.Exists(Path.Combine(preReqRun, nwpsGis64)))
                    {
                        returnedValue = await installProgramAsync(nwpsGis64, preReqRun);
                    }
                    else
                    {
                        returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), nwpsGis64);
                    }
                }
                catch (Exception ex)
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                    returnedValue = "false";
                }
            }
            else
            {
                loggingClass.logEntryWriter("Running 32 bit GIS Components", "info");

                try
                {
                    if (File.Exists(Path.Combine(preReqRun, (nwpsGis32))))
                    {
                        returnedValue = await installProgramAsync(nwpsGis32, preReqRun);
                    }
                    else
                    {
                        returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), nwpsGis32);
                    }
                }
                catch (Exception ex)
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                    returnedValue = "false";
                }
            }

            return returnedValue;
        }

        //MS Sync Service provider installer
        public async Task<string> dbProviderServiceAsync(bool Is64Bit, string installerPath)
        {
            if (Is64Bit == true)
            {
                loggingClass.logEntryWriter("Running 64 bit Synchronization", "info");

                try
                {
                    if (File.Exists(Path.Combine(preReqRun, msSync64)))
                    {
                        returnedValue = await installProgramAsync(msSync64, preReqRun);
                    }
                    else
                    {
                        returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), msSync64);
                    }
                }
                catch (Exception ex)
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                    returnedValue = "false";
                }

                loggingClass.logEntryWriter("Running 64 bit Provider Services", "info");

                try
                {
                    if (File.Exists(Path.Combine(preReqRun, msProServ64)))
                    {
                        returnedValue = await installProgramAsync(msProServ64, preReqRun);
                    }
                    else
                    {
                        returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), msProServ64);
                    }
                }
                catch (Exception ex)
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                    returnedValue = "false";
                }

                loggingClass.logEntryWriter("Running 64 bit DB Providers", "info");

                try
                {
                    if (File.Exists(Path.Combine(preReqRun, msDbPro64)))
                    {
                        returnedValue = await installProgramAsync(msDbPro64, preReqRun);
                    }
                    else
                    {
                        returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), msDbPro64);
                    }
                }
                catch (Exception ex)
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                    returnedValue = "false";
                }
            }
            else
            {
                loggingClass.logEntryWriter("Running 32 bit Synchronization", "info");

                try
                {
                    if (File.Exists(Path.Combine(preReqRun, msSync32)))
                    {
                        returnedValue = await installProgramAsync(msSync32, preReqRun);
                    }
                    else
                    {
                        returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), msSync32);
                    }
                }
                catch (Exception ex)
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                    returnedValue = "false";
                }

                loggingClass.logEntryWriter("Running 32 bit Provider Services", "info");

                try
                {
                    if (File.Exists(Path.Combine(preReqRun, msProServ32)))
                    {
                        returnedValue = await installProgramAsync(msProServ32, preReqRun);
                    }
                    else
                    {
                        returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), msProServ32);
                    }
                }
                catch (Exception ex)
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                    returnedValue = "false";
                }

                loggingClass.logEntryWriter("Running 32 bit DB Providers", "info");

                try
                {
                    if (File.Exists(Path.Combine(preReqRun, msProServ32)))
                    {
                        returnedValue = await installProgramAsync(msDbPro32, preReqRun);
                    }
                    else
                    {
                        returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), msProServ32);
                    }
                }
                catch (Exception ex)
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                    returnedValue = "false";
                }
            }

            return returnedValue;
        }

        //NWPS Updater installer
        public async Task<string> updaterInstallerAsync(string installerPath)
        {
            try
            {
                loggingClass.logEntryWriter("Running Updater Installer", "info");

                if (File.Exists(Path.Combine(preReqRun, nwpsUpdate)))
                {
                    returnedValue = await installProgramAsync(nwpsUpdate, preReqRun);
                }
                else
                {
                    returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), nwpsUpdate);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Value cannot be null"))
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");
                    loggingClass.logEntryWriter($"The path on the primary tab is blank. Please put {nwpsUpdate} in {preReqRun} OR fill in the path. ", "debug");

                    returnedValue = "false";
                }
                else
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                    returnedValue = "false";
                }
            }

            return returnedValue;
        }

        //ScenePD installer
        public async Task<string> scenePDAsync(string installerPath)
        {
            try
            {
                //will check for scene pd 6 before displaying install prompt
                // If scenepd 6 install is denied this will check for scene pd 4 before displaying install prompt
                //if scene pd 4 is not located and that is the desired scene pd version a message is displayed with paths to move folders
                if (File.Exists(preReqRun + @"\SPD6-4-8993.exe"))
                {
                    string command111 = $"msiexec /i \"{clientRun}\\{SCPD6}\" /q /L*V \"C:\\TEMP\\ScenePD.log\"";

                    if (powerShellScriptRun(command111).Equals(true))
                    {
                        returnedValue = "true";
                    }
                    else
                    {
                        returnedValue = "true";
                    }
                }

                //if scene pd is not found at all it must be moved by the user to a displayed location
                else
                {
                    string logEntry = @"ERROR: COULD NOT LOCATE SCENE PD 6.";

                    loggingClass.logEntryWriter(logEntry, "error");

                    //nwpsAddonThreadWorker();

                    returnedValue = "false";
                }
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "error");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                returnedValue = "false";
            }

            return returnedValue;
        }

        //SQL Compact 4.0 installer
        public async Task<string> sqlCe40Async(bool Is64Bit, string installerPath)
        {
            if (Is64Bit == true)
            {
                loggingClass.logEntryWriter("Running 64 bit SQL 4.0 Runtime", "info");

                try
                {
                    if (File.Exists(Path.Combine(preReqRun, sqlCE4064)))
                    {
                        returnedValue = await runProgramAsync(sqlCE4064, preReqRun);
                    }
                    else
                    {
                        try
                        {
                            //copyClass.preReqRename("SSCERuntime_x64-ENU.exe", sqlCE4064, "SQL Compact Edition 4.0", uiObject.FolderPath);
                            //copyClass.preReqRename("SSCERuntime_x86-ENU.exe", sqlCE4032, "SQL Compact Edition 4.0", uiObject.FolderPath);

                            returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), sqlCE4064);
                        }
                        catch (Exception ex)
                        {
                            string logEntry = ex.Message.ToString();
                            loggingClass.logEntryWriter(logEntry, "error");

                            //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                            returnedValue = "false";
                        }
                    }
                }
                catch (Exception ex)
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                    returnedValue = "false";
                }
            }
            else
            {
                loggingClass.logEntryWriter("Running 32bit SQL Compact 4.0 Runtime", "info");

                try
                {
                    if (File.Exists(Path.Combine(preReqRun, sqlCE4032)))
                    {
                        returnedValue = await runProgramAsync(sqlCE4032, preReqRun);
                    }
                    else
                    {
                        copyClass.preReqRename("SSCERuntime_x86-ENU.exe", sqlCE4032, "SQL Compact Edition 4.0", installerPath);

                        returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), sqlCE4064);
                    }
                }
                catch (Exception ex)
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                    returnedValue = "false";
                }
            }

            return returnedValue;
        }

        //visual studio 2010 installer
        public async Task<string> vs2010Async(string installerPath)
        {
            try
            {
                if (File.Exists(Path.Combine(preReqRun, "vstor_redist.exe")))
                {
                    loggingClass.logEntryWriter("Running Primary Interop Assemblies for Office", "info");

                    returnedValue = await installProgramAsync(@"vstor_redist.exe", preReqRun);
                }
                else
                {
                    loggingClass.logEntryWriter("Running Primary Interop Assemblies for Office", "info");

                    returnedValue = preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), "vstor_redist.exe").Result;
                }
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "error");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                returnedValue = "false";
            }

            return returnedValue;
        }

        //SQL Server CLR Types 64bit and 32bit
        public async Task<string> sqlClr2008Async(string installerPath)
        {
            loggingClass.logEntryWriter("Installing SQL Server CLR Types", "info");

            try
            {
                if (File.Exists(Path.Combine(preReqRun, sqlClr32)))
                {
                    returnedValue = await installProgramAsync(sqlClr32, preReqRun);
                }
                else
                {
                    returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), sqlClr32);
                }

                if (File.Exists(Path.Combine(preReqRun, sqlClr64)))
                {
                    returnedValue = await installProgramAsync(sqlClr64, preReqRun);
                }
                else
                {
                    returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), sqlClr64);
                }
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "error");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                returnedValue = "false";
            }

            return returnedValue;
        }

        public async Task<string> sqlClr2012Async(string installerPath)
        {
            try
            {
                if (File.Exists(Path.Combine(preReqRun, sqlClr201232)))
                {
                    returnedValue = await installProgramAsync(sqlClr201232, preReqRun);
                }
                else
                {
                    returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), sqlClr201232);
                }

                if (File.Exists(Path.Combine(preReqRun, sqlClr201264)))
                {
                    returnedValue = await installProgramAsync(sqlClr201264, preReqRun);
                }
                else
                {
                    returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), sqlClr201264);
                }
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "error");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                returnedValue = "false";
            }

            return returnedValue;
        }

        public async Task<bool> MSP(string command)
        {
            bool returnedValue1 = false;

            try
            {
                if (File.Exists(Path.Combine(clientRun, mspClient)))
                {
                    returnedValue1 = powerShellScriptRun(command).Equals(true);
                }
                else
                {
                    throw new Exception($"MSP Installer is not in {clientRun}");
                }
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "error");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                returnedValue1 = false;
            }

            return returnedValue1;
        }

        public async Task<bool> CAD(string command)
        {
            bool returnedValue12 = false;

            try
            {
                if (File.Exists(Path.Combine(clientRun, cadClient64)))
                {
                    returnedValue12 = powerShellScriptRun(command).Equals(true);
                }
                else
                {
                    throw new Exception($"CAD Installer is not in {clientRun}");
                }
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "error");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                returnedValue12 = false;
            }

            return returnedValue12;
        }

        //CAD Incident Observer Installer
        //used to view/create AVL history packages
        public async Task<string> incidentObserverAsync(string installerPath)
        {
            try
            {
                string logEntry = @" Attempting to install Incident Observer";

                loggingClass.logEntryWriter(logEntry, "info");

                if (File.Exists(Path.Combine(clientRun, cadIncObs64)))
                {
                    returnedValue = await installProgramAsync(cadIncObs64, clientRun);
                }
                else
                {
                    returnedValue = await preReqRunAsync(Path.Combine(installerPath, @"_Client-Installation\"), cadIncObs64);
                }
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "error");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                returnedValue = "false";
            }

            return returnedValue;
        }

        #endregion pre req install

        #region install/uninstall/run set permission

        //This is will run any/all programs that need user interaction by name
        public async Task<string> runProgramAsync(string programName, string location)
        {
            if (preReqStatusClass.installChecker(programName) == false)
            {
                try
                {
                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        UseShellExecute = true,
                        FileName = Path.Combine(location, programName)
                    };
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        string logEntry2 = programName + " was ran successfully.";

                        loggingClass.logEntryWriter(logEntry2, "info");

                        returnedValue = "True";
                    }
                    else
                    {
                        string errorcode = process.ExitCode.ToString();
                        string logEntry2 = programName + " failed to run. Error code: " + errorcode;

                        loggingClass.logEntryWriter(logEntry2, "error");

                        returnedValue = "false";
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("The system cannot find the file specified"))
                    {
                        string logEntry = "Could not find " + programName + " install failed";

                        loggingClass.logEntryWriter(logEntry, "error");

                        returnedValue = "false";
                    }
                    else
                    {
                        string logEntry = ex.ToString();

                        loggingClass.logEntryWriter(logEntry, "error");

                        //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                        returnedValue = "false";
                    }
                }
            }
            else
            {
                returnedValue = "True";
            }

            return returnedValue;
        }

        //This is the actual method to silently install pre-reqs by name
        public async Task<string> installProgramAsync(string preReqName, string installLocation)
        {
            try
            {
                if (preReqStatusClass.installChecker(preReqName) == false)
                {
                    string logEntry1 = "Attempting to install " + preReqName;

                    loggingClass.logEntryWriter(logEntry1, "info");

                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        UseShellExecute = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = Path.Combine(installLocation, preReqName),
                        Arguments = "/q"
                    };
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        string logEntry2 = preReqName + " has been installed successfully";

                        loggingClass.logEntryWriter(logEntry2, "info");

                        returnedValue = "true";
                    }
                    else if (process.ExitCode == 3010)
                    {
                        string errorcode = process.ExitCode.ToString();
                        string logEntry2 = preReqName + " has exited with code: " + errorcode +
                            " which means the machine needs to restart to finish the install process. You will need to restart the install process after the restart.";

                        loggingClass.logEntryWriter(logEntry2, "debug");

                        returnedValue = "true";
                    }
                    else if (process.ExitCode == 1603)
                    {
                        string errorcode = process.ExitCode.ToString();
                        string logEntry2 = preReqName + " has exited with code: " + errorcode +
                            " which means that there was a generic error that caused the install to fail. This could be that a newer version is installed, or a different install is current running.";

                        loggingClass.logEntryWriter(logEntry2, "error");

                        returnedValue = "false";
                    }
                    else if (process.ExitCode == 1625)
                    {
                        string errorcode = process.ExitCode.ToString();
                        string logEntry2 = preReqName + " has exited with code: " + errorcode +
                            " which means that the current user does not system permissions to install software. I would recommend running the application as a domain admin account and trying again.";

                        loggingClass.logEntryWriter(logEntry2, "error");

                        returnedValue = "false";
                    }
                    else if (process.ExitCode == 1307)
                    {
                        string errorcode = process.ExitCode.ToString();
                        string logEntry2 = preReqName + " has exited with code: " + errorcode +
                            $" which means that the default OS Drive (the C drive most likely) does not have enough space for {preReqName}. I would make space and try installing again.";

                        loggingClass.logEntryWriter(logEntry2, "error");

                        returnedValue = "false";
                    }
                    else
                    {
                        string errorcode = process.ExitCode.ToString();
                        string logEntry2 = preReqName + " failed to install. Error code: " + errorcode;

                        loggingClass.logEntryWriter(logEntry2, "error");

                        returnedValue = "false";
                    }
                }
                else
                {
                    returnedValue = "True";
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The system cannot find the file specified"))
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //loggingClass.queEntrywriter($"{preReqName} not found, unable to run");

                    returnedValue = "false";
                }
                else if (ex.Message.Contains("The operation was canceled by the user"))
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //loggingClass.queEntrywriter($"error running {preReqName}. Please try running again.");

                    returnedValue = "false";
                }
                else
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                    returnedValue = "false";
                }
            }

            return returnedValue;
        }

        //this will open files using whatever is the default program associated with the file type.
        //IF there is not a default file processor associated it will display a windows prompt to open the file type
        public async void openProgram(string location, string process)
        {
            try
            {
                string logEntry1 = "Accessing file at " + location; ;

                loggingClass.logEntryWriter(logEntry1, "info");

                var proc = Process.Start(process, location);

                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "error");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
            }
        }

        //this will iterate through folders to find specific pre reqs
        public async Task<string> preReqRunAsync(string sDir, string preReqName)
        {
            try
            {
                if (sDir.StartsWith("https"))
                {
                    foreach (var filename in Directory.GetFiles(preReqRun))
                    {
                        //if the file name of a found file matches what we are looking for code is run
                        if (Path.GetFileName(filename) == preReqName)
                        {
                            //if the file we found(that matches what we are looking for) matches specific pre reqs
                            //then specific UI code is ran for the install
                            //otherwise it is silently installed
                            if (Path.GetFileName(filename) == sqlCE4064)
                            {
                                return returnedValue = runProgramAsync(preReqName, Path.GetDirectoryName(filename)).Result;
                            }
                            else if (Path.GetFileName(filename) == sqlCE4032)
                            {
                                return returnedValue = runProgramAsync(preReqName, Path.GetDirectoryName(filename)).Result;
                            }
                            else if (Path.GetFileName(filename) == mspClient)
                            {
                                return returnedValue = runProgramAsync(preReqName, Path.GetDirectoryName(filename)).Result;
                            }
                            else if (Path.GetFileName(filename) == cadClient64)
                            {
                                return returnedValue = runProgramAsync(preReqName, Path.GetDirectoryName(filename)).Result;
                            }
                            else if (Path.GetFileName(filename) == cadClient32)
                            {
                                return returnedValue = runProgramAsync(preReqName, Path.GetDirectoryName(filename)).Result;
                            }
                            else
                            {
                                return returnedValue = installProgramAsync(preReqName, Path.GetDirectoryName(filename)).Result;
                            }
                        }
                    }
                    await preReqRunAsync(preReqRun, preReqName);
                }
                else
                {
                    foreach (var directory in Directory.GetDirectories(sDir))
                    {
                        foreach (var filename in Directory.GetFiles(directory))
                        {
                            //if the file name of a found file matches what we are looking for code is run
                            if (Path.GetFileName(filename) == preReqName)
                            {
                                //if the file we found(that matches what we are looking for) matches specific pre reqs
                                //then specific UI code is ran for the install
                                //otherwise it is silently installed
                                if (Path.GetFileName(filename) == sqlCE4064)
                                {
                                    return returnedValue = runProgramAsync(preReqName, Path.GetDirectoryName(filename)).Result;
                                }
                                else if (Path.GetFileName(filename) == sqlCE4032)
                                {
                                    return returnedValue = runProgramAsync(preReqName, Path.GetDirectoryName(filename)).Result;
                                }
                                else if (Path.GetFileName(filename) == mspClient)
                                {
                                    return returnedValue = runProgramAsync(preReqName, Path.GetDirectoryName(filename)).Result;
                                }
                                else if (Path.GetFileName(filename) == cadClient64)
                                {
                                    return returnedValue = runProgramAsync(preReqName, Path.GetDirectoryName(filename)).Result;
                                }
                                else if (Path.GetFileName(filename) == cadClient32)
                                {
                                    return returnedValue = runProgramAsync(preReqName, Path.GetDirectoryName(filename)).Result;
                                }
                                else
                                {
                                    return returnedValue = installProgramAsync(preReqName, Path.GetDirectoryName(filename)).Result;
                                }
                            }
                        }
                        await preReqRunAsync(directory, preReqName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Could not find a part of the path"))
                {
                    loggingClass.logEntryWriter("could not find pre req path, is it correct? " + sDir, "error");

                    returnedValue = "false";
                }
                else if (ex.Message.Contains("The user name or password is incorrect"))
                {
                    loggingClass.logEntryWriter("There was an error running " + preReqName + " over the network. Please verify your user can, or download pre reqs locally.", "error");

                    loggingClass.logEntryWriter(ex.ToString(), "error");

                    returnedValue = "false";
                }
                else if (ex.Message.Contains("The network name cannot be found"))
                {
                    loggingClass.logEntryWriter("There was an error running " + preReqName + " over the network. Please verify your network path is correct, or download pre reqs locally.", "error");

                    loggingClass.logEntryWriter(ex.ToString(), "error");

                    returnedValue = "false";
                }
                else if (ex.Message.Contains("The network path was not found."))
                {
                    loggingClass.logEntryWriter("There was an error running " + preReqName + " over the network. Please verify your network path is correct, or download pre reqs locally.", "error");

                    loggingClass.logEntryWriter(ex.ToString(), "error");

                    returnedValue = "false";
                }
                else if (ex.Message.Contains("The given path's format is not supported."))
                {
                    loggingClass.logEntryWriter(ex.ToString(), "error");

                    returnedValue = "false";
                }
                else if (ex.Message.Contains("The device is not ready"))
                {
                    loggingClass.logEntryWriter(ex.ToString(), "error");

                    returnedValue = "false";
                }
                else if (ex.Message.Contains("The specified network name is no longer available."))
                {
                    loggingClass.logEntryWriter(ex.ToString(), "error");

                    returnedValue = "false";
                }
                else
                {
                    string logEntry = ex.Message + " " + ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);

                    returnedValue = "false";
                }
            }

            return returnedValue;
        }

        //this will run command prompt scripts within the application.
        public bool cmdScriptRun(string Command)
        {
            bool value = false;

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = Command
            };
            process.StartInfo = startInfo;

            loggingClass.logEntryWriter("running script =>" + Command, "info");
            process.Start();
            process.WaitForExit();

            if (process.ExitCode == 0)
            {
                string logEntry2 = Command + " has been installed successfully";

                loggingClass.logEntryWriter(logEntry2, "info");

                value = true;
            }
            else
            {
                string errorcode = process.ExitCode.ToString();
                string logEntry2 = Command + " failed to install. Error code: " + errorcode;

                loggingClass.logEntryWriter(logEntry2, "error");

                value = false;
            }

            return value;
        }

        public bool powerShellScriptRun(string command)
        {
            bool value = false;

            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();

            RunspaceInvoke runSpaceInvoker = new RunspaceInvoke(runspace);
            runSpaceInvoker.Invoke("Set-ExecutionPolicy Unrestricted");

            Pipeline pipeline = runspace.CreatePipeline();
            Command psCommand = new Command(command);

            pipeline.Commands.AddScript(psCommand.CommandText);

            try
            {
                loggingClass.logEntryWriter("running script =>" + psCommand.CommandText, "info");

                pipeline.Invoke();

                runspace.Close();

                value = true;

                string logEntry3 = $"{psCommand.CommandText} was run successfully";

                loggingClass.logEntryWriter(logEntry3, "info");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Mobile Services does not exist or is not running"))
                {
                    loggingClass.logEntryWriter($"{command} was not run successfully", "error");
                }
                else
                {
                    loggingClass.logEntryWriter($"{command} was not run successfully", "error");

                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    value = false;

                    //await loggingClass.remoteErrorReporting("Server Migration Utility", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
                }
            }

            return value;
        }

        //this will give the user role full control for folder permissions
        //this also will modify the all files and sub directories with full control to the user role
        public static bool setAcl(string destinationDirectory)
        {
            try
            {
                FileSystemRights Rights = (FileSystemRights)0;
                Rights = FileSystemRights.FullControl;

                // *** Add Access Rule to the actual directory itself
                FileSystemAccessRule AccessRule = new FileSystemAccessRule("Users", Rights,
                                            InheritanceFlags.None,
                                            PropagationFlags.NoPropagateInherit,
                                            AccessControlType.Allow);

                DirectoryInfo Info = new DirectoryInfo(destinationDirectory);
                DirectorySecurity Security = Info.GetAccessControl(AccessControlSections.Access);

                Security.ModifyAccessRule(AccessControlModification.Set, AccessRule, out bool Result);

                if (!Result)
                    return false;

                // *** Always allow objects to inherit on a directory
                InheritanceFlags iFlags = InheritanceFlags.ObjectInherit;
                iFlags = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;

                // *** Add Access rule for the inheritance
                AccessRule = new FileSystemAccessRule("Users", Rights,
                                            iFlags,
                                            PropagationFlags.InheritOnly,
                                            AccessControlType.Allow);
                Result = false;
                Security.ModifyAccessRule(AccessControlModification.Add, AccessRule, out Result);

                if (!Result)
                    return false;

                Info.SetAccessControl(Security);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //restarts PC
        public void restartMachine()
        {
            Process.Start("Shutdown", "/r");

            string logEntry = "Restart Initiated";

            loggingClass.logEntryWriter(logEntry, "info");

            for (int i = 0; i < 60; i++)
            {
                if (i % 10 == 0)
                {
                    int j = 60 - i;

                    loggingClass.nLogLogger("Restart is in " + j + " seconds", "info");
                }
            }
        }

        #endregion install/uninstall/run set permission
    }
}