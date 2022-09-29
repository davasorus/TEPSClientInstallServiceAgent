using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TEPSClientInstallService.Classes;

namespace testInstallServer.Classes
{
    public class installController : ApiController
    {
        private loggingClass loggingClass = new loggingClass();
        private installerClass installerClass = new installerClass();
        private utilityClass utilityClass = new utilityClass();
        private serviceClass serviceClass = new serviceClass();
        private updaterConfigClass updaterConfigClass = new updaterConfigClass();

        private readonly string clientRun = @"C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\Clients";

        public string PostPreReqInstall(int id)
        {
            utilityClass.parseRequestBodyAsync(Request.Content.ReadAsStringAsync().Result);

            List<tupleData> tupleList = new List<tupleData>();
            switch (id)
            {
                case 1:

                    if (installerClass.dotNetAsync("").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "DotNet Installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "DotNet failed to install" });

                        loggingClass.logEntryWriter("DotNet failed to install", "error");
                    }
                    break;

                case 2:

                    if (installerClass.sqlCe35Async(true, "").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "SQL Compact 3.5 installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "SQL Compact 3.5 failed to install" });

                        loggingClass.logEntryWriter("SQL Compact 3.5 failed to install", "error");
                    }

                    break;

                case 3:

                    if (installerClass.gisAsync(true, "").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "GIS Components Installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "GIS Components failed to install" });

                        loggingClass.logEntryWriter("GIS Components failed to install", "error");
                    }
                    break;

                case 4:

                    if (installerClass.dbProviderServiceAsync(true, "").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 ok", message = "DB Providers Installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "DB Providers failed to install" });

                        loggingClass.logEntryWriter("DB Providers failed to install", "error");
                    }
                    break;

                case 5:

                    if (installerClass.updaterInstallerAsync("").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "Updater Installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "Updater failed to install" });

                        loggingClass.logEntryWriter("Updater failed to install", "error");
                    }
                    break;

                case 6:

                    //if (installerClass.scenePDAsync("").Result.Equals("true"))
                    //{
                    //    response.Add("ScenePD Installed");
                    //}
                    //else
                    //{
                    //    response.Add("ScenePD failed to install");
                    //    loggingClass.logEntryWriter("ScenePD failed to install", "error");
                    //}

                    break;

                case 7:

                    if (installerClass.sqlCe40Async(true, "").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "SQL Compact 4.0 Installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "SQL Compact 4.0 failed to install" });

                        loggingClass.logEntryWriter("SQL Compact 4.0 failed to install", "error");
                    }
                    break;

                case 8:

                    if (installerClass.vs2010Async("").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "Visual Studio 2010 Types Installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "Visual Studio 2010 Types failed to install" });

                        loggingClass.logEntryWriter("Visual Studio 2010 Types failed to install", "error");
                    }
                    break;

                case 9:

                    if (installerClass.sqlClr2008Async("").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "SQL 2008 CLR Types Installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "SQL 2008 CLR Types failed to install" });

                        loggingClass.logEntryWriter("SQL 2008 CLR Types failed to install", "error");
                    }

                    if (installerClass.sqlClr2012Async("").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "SQL 2012 CLR Types Installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "SQL 2012 CLR Types failed to install" });

                        loggingClass.logEntryWriter("SQL 2012 CLR Types failed to install", "error");
                    }

                    break;

                case 99:
                    if (installerClass.dotNetAsync("").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "DotNet Installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "DotNet failed to install" });

                        loggingClass.logEntryWriter("DotNet failed to install", "error");
                    }

                    if (installerClass.sqlCe35Async(true, "").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "SQL Compact 3.5 installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "SQL Compact 3.5 failed to install" });

                        loggingClass.logEntryWriter("SQL Compact 3.5 failed to install", "error");
                    }

                    if (installerClass.gisAsync(true, "").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "GIS Components Installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "GIS Components failed to install" });

                        loggingClass.logEntryWriter("GIS Components failed to install", "error");
                    }

                    if (installerClass.dbProviderServiceAsync(true, "").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 ok", message = "DB Providers Installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "DB Providers failed to install" });

                        loggingClass.logEntryWriter("DB Providers failed to install", "error");
                    }

                    if (installerClass.updaterInstallerAsync("").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "Updater Installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "Updater failed to install" });

                        loggingClass.logEntryWriter("Updater failed to install", "error");
                    }

                    //if (installerClass.scenePDAsync("").Result.Equals("true"))
                    //{
                    //    response.Add("ScenePD Installed");
                    // }
                    // else
                    // {
                    //     response.Add("ScenePD failed to install");
                    //     loggingClass.logEntryWriter("ScenePD failed to install", "error");
                    // }

                    if (installerClass.sqlCe40Async(true, "").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "SQL Compact 4.0 Installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "SQL Compact 4.0 failed to install" });

                        loggingClass.logEntryWriter("SQL Compact 4.0 failed to install", "error");
                    }

                    if (installerClass.vs2010Async("").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "Visual Studio 2010 Types Installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "Visual Studio 2010 Types failed to install" });

                        loggingClass.logEntryWriter("Visual Studio 2010 Types failed to install", "error");
                    }

                    if (installerClass.sqlClr2008Async("").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "SQL 2008 CLR Types Installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "SQL 2008 CLR Types failed to install" });

                        loggingClass.logEntryWriter("SQL 2008 CLR Types failed to install", "error");
                    }

                    if (installerClass.sqlClr2012Async("").Result.Equals("true"))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "SQL 2012 CLR Types Installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "SQL 2012 CLR Types failed to install" });

                        loggingClass.logEntryWriter("SQL 2012 CLR Types failed to install", "error");
                    }

                    break;

                default:
                    break;
            }

            var jsonReturn = JsonConvert.SerializeObject(tupleList);

            return jsonReturn;
        }

        public Task<string> PostClientInstall(int id, [FromBody] string bodyContent)
        {
            utilityClass.parseRequestBodyAsync(Request.Content.ReadAsStringAsync().Result);

            List<tupleData> tupleList = new List<tupleData>();

            switch (id)
            {
                case 1:

                    //installMSP

                    string command = $"msiexec /i \"{clientRun}\\NewWorldMSPClient.msi\" addlocal=\"AegisClientBase,F_VB6RedistRuntime,Maintenance,Corrections,LERMS\" MSPSERVERNAME=\"{serverConfigObj.MSPServer}\" AUTHSERVERNAME=\"{serverConfigObj.ESSServer}\" /q /L*V \"C:\\TEMP\\MSP.log\"";

                    if (installerClass.MSP(command).Result.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = $"MSP Installed successfully values passed - MSP Server: {serverConfigObj.MSPServer} | ESS Server: {serverConfigObj.ESSServer}" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = $"MSP failed to install values passed - MSP Server: {serverConfigObj.MSPServer} | ESS Server: {serverConfigObj.ESSServer}" });
                    }

                mspreset1:

                    if (utilityClass.getProcessByName("msiexec").Equals(true))
                    {
                        goto mspreset1;
                    }

                    break;

                case 2:
                    //installCAD

                    string command1 = $"msiexec /i \"{clientRun}\\NewWorld.Enterprise.CAD.Client.x64.msi\" DISPATCH_SERVER=\"{serverConfigObj.CADServer}\" MEMBERSHIP_SERVER=\"{serverConfigObj.ESSServer}\" GIS_SERVER_NAME=\"{serverConfigObj.GISServer}\" GIS_INSTANCE=\"{serverConfigObj.GISInstance}\" NWS_CHECKBOX_PICTOMETRY_ENABLE=\"0\" /q /L*V \"C:\\TEMP\\CAD.log\"";

                    if (installerClass.CAD(command1).Result.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = $"CAD Installed successfully values passed - CAD Server: {serverConfigObj.CADServer} | ESS Server: {serverConfigObj.ESSServer} | GIS Server {serverConfigObj.GISServer} | GIS Instance {serverConfigObj.GISInstance}" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = $"CAD failed to install values passed - CAD Server: {serverConfigObj.CADServer} | ESS Server: {serverConfigObj.ESSServer} | GIS Server {serverConfigObj.GISServer} | GIS Instance {serverConfigObj.GISInstance}" });
                    }

                cadReset1:

                    if (utilityClass.getProcessByName("msiexec").Equals(true))
                    {
                        goto cadReset1;
                    }

                    break;

                case 3:

                    if (installerClass.incidentObserverAsync("").Result.Equals("true"))
                    {
                        string logEntry1 = @"Incident Observer installed";

                        loggingClass.logEntryWriter(logEntry1, "info");

                        tupleList.Add(new tupleData { responseCode = "200 OK", message = "CAD Incident Observer Installed" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = "CAD Incident Observer failed to install" });

                        loggingClass.logEntryWriter("CAD Incident Observer failed to install", "error");
                    }

                    break;

                case 99:

                    string command00 = $"msiexec /i \"{clientRun}\\NewWorldMSPClient.msi\" addlocal=\"AegisClientBase,F_VB6RedistRuntime,Maintenance,Corrections,LERMS\" MSPSERVERNAME=\"{serverConfigObj.MSPServer}\" AUTHSERVERNAME=\"{serverConfigObj.ESSServer}\" /q /L*V \"C:\\TEMP\\MSP.log\"";

                    if (installerClass.MSP(command00).Result.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = $"MSP Installed successfully values passed - MSP Server: {serverConfigObj.MSPServer} | ESS Server: {serverConfigObj.ESSServer}" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = $"MSP failed to install values passed - MSP Server: {serverConfigObj.MSPServer} | ESS Server: {serverConfigObj.ESSServer}" });
                    }

                mspreset:

                    if (utilityClass.getProcessByName("msiexec").Equals(true))
                    {
                        goto mspreset;
                    }

                    string command11 = $"msiexec /i \"{clientRun}\\NewWorld.Enterprise.CAD.Client.x64.msi\" DISPATCH_SERVER=\"{serverConfigObj.CADServer}\" MEMBERSHIP_SERVER=\"{serverConfigObj.ESSServer}\" GIS_SERVER_NAME=\"{serverConfigObj.GISServer}\" GIS_INSTANCE=\"{serverConfigObj.GISInstance}\" NWS_CHECKBOX_PICTOMETRY_ENABLE=\"0\" /q /L*V \"C:\\TEMP\\CAD.log\"";

                    if (installerClass.CAD(command11).Result.Equals(true))
                    {
                        tupleList.Add(new tupleData { responseCode = "200 OK", message = $"CAD Installed successfully values passed - CAD Server: {serverConfigObj.CADServer} | ESS Server: {serverConfigObj.ESSServer} | GIS Server {serverConfigObj.GISServer} | GIS Instance {serverConfigObj.GISInstance}" });
                    }
                    else
                    {
                        tupleList.Add(new tupleData { responseCode = "400 Bad Request", message = $"CAD failed to install values passed - CAD Server: {serverConfigObj.CADServer} | ESS Server: {serverConfigObj.ESSServer} | GIS Server {serverConfigObj.GISServer} | GIS Instance {serverConfigObj.GISInstance}" });
                    }

                cadReset:

                    if (utilityClass.getProcessByName("msiexec").Equals(true))
                    {
                        goto cadReset;
                    }

                    if (serviceClass.getServiceStatus("NewWorldUpdaterService") == "Running")
                    {
                        serviceClass.stopService("NewWorldUpdaterService");
                    }

                    if (serviceClass.getServiceStatus("EnterpriseUpdaterService") == "Running")
                    {
                        serviceClass.stopService("EnterpriseUpdaterService");
                    }

                    foreach (var item in serverConfigObj.configFileORIObjs)
                    {
                        updaterConfigClass.seeIfNodesExist(item.ORI);

                        updaterConfigClass.oriSub(item.ORI, serverConfigObj.MobileServer);

                        tupleList.Add(new tupleData { responseCode = "200 ok", message = $"ORI {item.ORI} added with mobile server {serverConfigObj.MobileServer}" });
                    }

                    foreach (var item in serverConfigObj.configFileFDIDObjs)
                    {
                        updaterConfigClass.seeIfNodesExist(item.FDID);

                        updaterConfigClass.fdidSub(item.FDID, serverConfigObj.MobileServer);

                        tupleList.Add(new tupleData { responseCode = "200 OK", message = $"FDID {item.FDID} added with mobile server {serverConfigObj.MobileServer}" });
                    }

                    updaterConfigClass.policeClientSub(serverConfigObj.MobileServer);

                    updaterConfigClass.fireClientSub(serverConfigObj.MobileServer);

                    updaterConfigClass.mergeClientSub(serverConfigObj.MobileServer);

                    tupleList.Add(new tupleData { responseCode = "200 OK", message = "Police Client, Fire Client, Merge Client added with mobile server " + serverConfigObj.MobileServer });

                    Thread.Sleep(1000);

                    if (serviceClass.getServiceStatus("NewWorldUpdaterService") == "Stopped")
                    {
                        serviceClass.startService("NewWorldUpdaterService");
                    }
                    else if (serviceClass.getServiceStatus("NewWorldUpdaterService") == "Stopping")
                    {
                        Thread.Sleep(2000);
                        serviceClass.startService("NewWorldUpdaterService");
                    }
                    else if (serviceClass.getServiceStatus("EnterpriseUpdaterService") == "Stopped")
                    {
                        serviceClass.startService("EnterpriseUpdaterService");
                    }
                    else if (serviceClass.getServiceStatus("EnterpriseUpdaterService") == "Stopping")
                    {
                        Thread.Sleep(2000);
                        serviceClass.startService("EnterpriseUpdaterService");
                    }
                    else
                    {
                        loggingClass.logEntryWriter("Updater not Installed, cannot change the status of a service that is not installed", "error");

                        //updateSnackBar("Updater service not installed, but config was changed. Install updater");
                    }

                    break;

                default:
                    break;
            }

            var jsonReturn = JsonConvert.SerializeObject(tupleList);

            return Task.FromResult(jsonReturn);
        }

        public Task<string> PostMobileConfig(int id, [FromBody] string bodyContent)
        {
            utilityClass.parseRequestBodyAsync(Request.Content.ReadAsStringAsync().Result);

            List<tupleData> tupleList = new List<tupleData>();

            switch (id)
            {
                //police mobile
                case 1:

                    if (serviceClass.getServiceStatus("NewWorldUpdaterService") == "Running")
                    {
                        serviceClass.stopService("NewWorldUpdaterService");
                    }

                    if (serviceClass.getServiceStatus("EnterpriseUpdaterService") == "Running")
                    {
                        serviceClass.stopService("EnterpriseUpdaterService");
                    }

                    foreach (var item in serverConfigObj.configFileORIObjs)
                    {
                        updaterConfigClass.seeIfNodesExist(item.ORI);

                        updaterConfigClass.oriSub(item.ORI, serverConfigObj.MobileServer);

                        tupleList.Add(new tupleData { responseCode = "200 OK", message = $"ORI {item.ORI} added with mobile server " + serverConfigObj.MobileServer });
                    }

                    updaterConfigClass.policeClientSub(serverConfigObj.MobileServer);

                    tupleList.Add(new tupleData { responseCode = "200 OK", message = "Police Client added with mobile server " + serverConfigObj.MobileServer });

                    Thread.Sleep(1000);

                    if (serviceClass.getServiceStatus("NewWorldUpdaterService") == "Stopped")
                    {
                        serviceClass.startService("NewWorldUpdaterService");
                    }
                    else if (serviceClass.getServiceStatus("NewWorldUpdaterService") == "Stopping")
                    {
                        Thread.Sleep(2000);
                        serviceClass.startService("NewWorldUpdaterService");
                    }
                    else if (serviceClass.getServiceStatus("EnterpriseUpdaterService") == "Stopped")
                    {
                        serviceClass.startService("EnterpriseUpdaterService");
                    }
                    else if (serviceClass.getServiceStatus("EnterpriseUpdaterService") == "Stopping")
                    {
                        Thread.Sleep(2000);
                        serviceClass.startService("EnterpriseUpdaterService");
                    }
                    else
                    {
                        loggingClass.logEntryWriter("Updater not Installed, cannot change the status of a service that is not installed", "error");

                        //updateSnackBar("Updater service not installed, but config was changed. Install updater");
                    }

                    break;

                //Fire mobile
                case 2:

                    if (serviceClass.getServiceStatus("NewWorldUpdaterService") == "Running")
                    {
                        serviceClass.stopService("NewWorldUpdaterService");
                    }

                    if (serviceClass.getServiceStatus("EnterpriseUpdaterService") == "Running")
                    {
                        serviceClass.stopService("EnterpriseUpdaterService");
                    }

                    foreach (var item in serverConfigObj.configFileFDIDObjs)
                    {
                        updaterConfigClass.seeIfNodesExist(item.FDID);

                        updaterConfigClass.fdidSub(item.FDID, serverConfigObj.MobileServer);

                        tupleList.Add(new tupleData { responseCode = "200 ok", message = $"FDID {item.FDID} added with mobile server {serverConfigObj.MobileServer}" });
                    }

                    updaterConfigClass.fireClientSub(serverConfigObj.MobileServer);

                    tupleList.Add(new tupleData { responseCode = "200 OK", message = "Fire Client added with mobile server " + serverConfigObj.MobileServer });

                    Thread.Sleep(1000);

                    if (serviceClass.getServiceStatus("NewWorldUpdaterService") == "Stopped")
                    {
                        serviceClass.startService("NewWorldUpdaterService");
                    }
                    else if (serviceClass.getServiceStatus("NewWorldUpdaterService") == "Stopping")
                    {
                        Thread.Sleep(2000);
                        serviceClass.startService("NewWorldUpdaterService");
                    }
                    else if (serviceClass.getServiceStatus("EnterpriseUpdaterService") == "Stopped")
                    {
                        serviceClass.startService("EnterpriseUpdaterService");
                    }
                    else if (serviceClass.getServiceStatus("EnterpriseUpdaterService") == "Stopping")
                    {
                        Thread.Sleep(2000);
                        serviceClass.startService("EnterpriseUpdaterService");
                    }
                    else
                    {
                        loggingClass.logEntryWriter("Updater not Installed, cannot change the status of a service that is not installed", "error");

                        //updateSnackBar("Updater service not installed, but config was changed. Install updater");
                    }

                    break;

                //merge
                case 3:

                    if (serviceClass.getServiceStatus("NewWorldUpdaterService") == "Running")
                    {
                        serviceClass.stopService("NewWorldUpdaterService");
                    }

                    if (serviceClass.getServiceStatus("EnterpriseUpdaterService") == "Running")
                    {
                        serviceClass.stopService("EnterpriseUpdaterService");
                    }

                    foreach (var item in serverConfigObj.configFileORIObjs)
                    {
                        updaterConfigClass.seeIfNodesExist(item.ORI);

                        updaterConfigClass.oriSub(item.ORI, serverConfigObj.MobileServer);

                        tupleList.Add(new tupleData { message = "200 ok", responseCode = $"ORI {item.ORI} added with mobile server {serverConfigObj.MobileServer}" });
                    }

                    updaterConfigClass.mergeClientSub(serverConfigObj.MobileServer);

                    tupleList.Add(new tupleData { responseCode = "200 OK", message = "Merge Client added with mobile server " + serverConfigObj.MobileServer });

                    Thread.Sleep(1000);

                    if (serviceClass.getServiceStatus("NewWorldUpdaterService") == "Stopped")
                    {
                        serviceClass.startService("NewWorldUpdaterService");
                    }
                    else if (serviceClass.getServiceStatus("NewWorldUpdaterService") == "Stopping")
                    {
                        Thread.Sleep(2000);
                        serviceClass.startService("NewWorldUpdaterService");
                    }
                    else if (serviceClass.getServiceStatus("EnterpriseUpdaterService") == "Stopped")
                    {
                        serviceClass.startService("EnterpriseUpdaterService");
                    }
                    else if (serviceClass.getServiceStatus("EnterpriseUpdaterService") == "Stopping")
                    {
                        Thread.Sleep(2000);
                        serviceClass.startService("EnterpriseUpdaterService");
                    }
                    else
                    {
                        loggingClass.logEntryWriter("Updater not Installed, cannot change the status of a service that is not installed", "error");

                        //updateSnackBar("Updater service not installed, but config was changed. Install updater");
                    }

                    break;

                case 99:
                    if (serviceClass.getServiceStatus("NewWorldUpdaterService") == "Running")
                    {
                        serviceClass.stopService("NewWorldUpdaterService");
                    }

                    if (serviceClass.getServiceStatus("EnterpriseUpdaterService") == "Running")
                    {
                        serviceClass.stopService("EnterpriseUpdaterService");
                    }

                    foreach (var item in serverConfigObj.configFileORIObjs)
                    {
                        updaterConfigClass.seeIfNodesExist(item.ORI);

                        updaterConfigClass.oriSub(item.ORI, serverConfigObj.MobileServer);

                        tupleList.Add(new tupleData { responseCode = "200 OK", message = $"ORI {item.ORI} added with mobile server {serverConfigObj.MobileServer}" });
                    }

                    foreach (var item in serverConfigObj.configFileFDIDObjs)
                    {
                        updaterConfigClass.seeIfNodesExist(item.FDID);

                        updaterConfigClass.fdidSub(item.FDID, serverConfigObj.MobileServer);

                        tupleList.Add(new tupleData { message = "200 OK", responseCode = $"FDID {item.FDID} added with mobile server {serverConfigObj.MobileServer}" });
                    }

                    updaterConfigClass.policeClientSub(serverConfigObj.MobileServer);

                    updaterConfigClass.fireClientSub(serverConfigObj.MobileServer);

                    updaterConfigClass.mergeClientSub(serverConfigObj.MobileServer);

                    tupleList.Add(new tupleData { message = "200 OK", responseCode = "Police Client, Fire Client, Merge Client added with mobile server " + serverConfigObj.MobileServer });

                    Thread.Sleep(1000);

                    if (serviceClass.getServiceStatus("NewWorldUpdaterService") == "Stopped")
                    {
                        serviceClass.startService("NewWorldUpdaterService");
                    }
                    else if (serviceClass.getServiceStatus("NewWorldUpdaterService") == "Stopping")
                    {
                        Thread.Sleep(2000);
                        serviceClass.startService("NewWorldUpdaterService");
                    }
                    else if (serviceClass.getServiceStatus("EnterpriseUpdaterService") == "Stopped")
                    {
                        serviceClass.startService("EnterpriseUpdaterService");
                    }
                    else if (serviceClass.getServiceStatus("EnterpriseUpdaterService") == "Stopping")
                    {
                        Thread.Sleep(2000);
                        serviceClass.startService("EnterpriseUpdaterService");
                    }
                    else
                    {
                        loggingClass.logEntryWriter("Updater not Installed, cannot change the status of a service that is not installed", "error");

                        //updateSnackBar("Updater service not installed, but config was changed. Install updater");
                    }

                    break;

                default:
                    break;
            }

            //var jsonReturn = JsonConvert.SerializeObject(response);

            var jsonReturn = JsonConvert.SerializeObject(tupleList);

            return Task.FromResult(jsonReturn);
        }
    }
}

internal class tupleData
{
    public string responseCode { get; set; }
    public string message { get; set; }
}