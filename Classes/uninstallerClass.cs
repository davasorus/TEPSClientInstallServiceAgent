using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

namespace testInstallServer.Classes
{
    public class uninstallerClass
    {
        private loggingClass loggingClass = new loggingClass();
        private preReqStatusClass preReqStatusClass = new preReqStatusClass();

        #region mobile updater config removal steps

        //will look into the updater config file and will replace any text that contains MobileUpdates with DeleteMe
        public async void fileWork64Bit()
        {
            try
            {
                if (File.Exists(@"C:\Program Files (x86)\New World Systems\New World Automatic Updater\NewWorld.Management.Updater.Service.exe.config"))
                {
                    string text = File.ReadAllText(@"C:\Program Files (x86)\New World Systems\New World Automatic Updater\NewWorld.Management.Updater.Service.exe.config");
                    text = text.Replace(@"MobileUpdates", "DeleteMe");
                    File.WriteAllText(@"C:\Program Files (x86)\New World Systems\New World Automatic Updater\NewWorld.Management.Updater.Service.exe.config", text);

                    string logEntry = "Updater config file modified";

                    loggingClass.logEntryWriter(logEntry, "info");
                }
                else
                {
                    fileWork32Bit();
                }
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "info");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
            }
        }

        //will look into the updater config file and remove any lines that contain DeleteMe
        public async void updaterWork64Bit()
        {
            try
            {
                if (File.Exists(@"C:\Program Files (x86)\New World Systems\New World Automatic Updater\NewWorld.Management.Updater.Service.exe.config"))
                {
                    string text = @"C:\Program Files (x86)\New World Systems\New World Automatic Updater\NewWorld.Management.Updater.Service.exe.config";

                    string[] Lines = File.ReadAllLines(text);
                    IEnumerable<string> newLines = Lines.Where(line => !line.Contains(@"/DeleteMe/"));
                    File.WriteAllLines(text, newLines);

                    string logEntry = "mobile updater file entries removed";

                    loggingClass.logEntryWriter(logEntry, "info");
                }
                else
                {
                    fileWork32Bit();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Could not find a part of the path"))
                {
                    loggingClass.nLogLogger(ex.Message, "Debug");
                }
                else
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
                }
            }
        }

        //Will look into the updater config file and will replace any text that contains MobileUpdates with DeleteMe
        public async void fileWork32Bit()
        {
            try
            {
                string text = File.ReadAllText(@"C:\Program Files\New World Systems\New World Automatic Updater\NewWorld.Management.Updater.Service.exe.config");
                text = text.Replace(@"MobileUpdates", "DeleteMe");
                File.WriteAllText(@"C:\Program Files\New World Systems\New World Automatic Updater\NewWorld.Management.Updater.Service.exe.config", text);

                string logEntry = "Updater config file modified";

                loggingClass.logEntryWriter(logEntry, "info");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Could not find a part of the path"))
                {
                    loggingClass.nLogLogger(ex.Message, "Debug");
                }
                else
                {
                    string logEntry = ex.ToString();

                    loggingClass.logEntryWriter(logEntry, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
                }
            }
        }

        //will look into the updater config file and remove any lines that contain DeleteMe
        public async void updaterWork32Bit()
        {
            try
            {
                string text = @"C:\Program Files\New World Systems\New World Automatic Updater\NewWorld.Management.Updater.Service.exe.config";

                var Lines = File.ReadAllLines(text);
                var newLines = Lines.Where(line => !line.Contains(@"/DeleteMe/"));
                File.WriteAllLines(text, newLines);

                string logEntry = "mobile updater file entries removed";

                loggingClass.logEntryWriter(logEntry, "info");
            }
            catch (Exception ex)
            {
                string logEntry = ex.ToString();

                loggingClass.logEntryWriter(logEntry, "error");

                //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
            }
        }

        #endregion mobile updater config removal steps

        #region program uninstaller

        //This is the method that will silently uninstall pre - reqs by name
        public async Task<bool> uninstallProgramAsync(string programName)
        {
            if (preReqStatusClass.uninstallChecker(programName) == false)
            {
                loggingClass.logEntryWriter("Searching for " + programName + " to uninstall.", "info");

                ManagementObjectSearcher mos = new ManagementObjectSearcher(
                 @"SELECT * FROM Win32_Product WHERE Name = '" + programName + "'");
                foreach (ManagementObject mo in mos.Get())
                {
                    try
                    {
                        if (mo["Name"].ToString() == programName)
                        {
                            object hr = mo.InvokeMethod("Uninstall", null);

                            //not pretty but fixes the invalid cast exception :/
                            if (hr.Equals(hr))
                            {
                                string logEntry1 = programName + " has been uninstalled";

                                loggingClass.logEntryWriter(logEntry1, "info");

                                return true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string logEntry2 = ex.ToString();

                        loggingClass.logEntryWriter(logEntry2, "error");

                        //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
                    }
                }

                string logEntry3 = programName + " was not found. It was either not installed or detected.";

                loggingClass.logEntryWriter(logEntry3, "info");

                //not found
                return false;
            }
            return true;
        }

        #endregion program uninstaller
    }
}