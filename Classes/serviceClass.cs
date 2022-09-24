using System;
using System.ServiceProcess;
using testInstallServer.Classes;

namespace TEPSClientInstallService.Classes
{
    public class serviceClass
    {
        private loggingClass loggingClass = new loggingClass();

        #region Service Related Code

        //will stop the service by name
        public async void stopService(string name)
        {
            try
            {
                ServiceController sc = new ServiceController(name);
                if (sc.Status.Equals(ServiceControllerStatus.Running))
                {
                    sc.Stop();

                    string logEntry = name + " has been stopped.";

                    loggingClass.logEntryWriter(logEntry, "info");
                    //loggingClass.queEntrywriter(logEntry);
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("InvalidOperationException"))
                {
                    string logEntry = name + " could not be stopped. It likely is not installed";

                    loggingClass.logEntryWriter(logEntry, "error");
                }
                else
                {
                    string logEntry1 = ex.ToString();

                    loggingClass.logEntryWriter(logEntry1, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
                }
            }
        }

        //will start the service by name
        public async void startService(string name)
        {
            try
            {
                ServiceController sc = new ServiceController(name);
                if (sc.Status.Equals(ServiceControllerStatus.Stopped))
                {
                    sc.Start();

                    string logEntry = name + " has been started.";

                    loggingClass.logEntryWriter(logEntry, "info");
                    //loggingClass.queEntrywriter(logEntry);
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("InvalidOperationException"))
                {
                    string logEntry = name + " Could not be started. It likely is not installed";

                    loggingClass.logEntryWriter(logEntry, "error");
                }
                else
                {
                    string logEntry1 = ex.ToString();

                    loggingClass.logEntryWriter(logEntry1, "error");

                    //await loggingClass.remoteErrorReporting("Client Admin Tool", Assembly.GetExecutingAssembly().GetName().Version.ToString(), ex.ToString(), "Automated Error Reported by " + Environment.UserName);
                }
            }
        }

        //searches for a service with a specific name and returns it's status
        public string getServiceStatus(string serviceName)
        {
            try
            {
                ServiceController myservice = new ServiceController(serviceName);

                string svcStatus = myservice.Status.ToString();

                return svcStatus;
            }
            catch
            {
                return "error";
            }
        }

        #endregion Service Related Code
    }
}