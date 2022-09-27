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

            List<string> response = new List<string>();
            switch (id)
            {
                case 1:

                    if (installerClass.dotNetAsync("").Result.Equals("true"))
                    {
                        response.Add("DotNet Installed");
                    }
                    else
                    {
                        response.Add("DotNet failed to install");
                        loggingClass.logEntryWriter("DotNet failed to install", "error");
                    }
                    break;

                case 2:

                    if (installerClass.sqlCe35Async(true, "").Result.Equals("true"))
                    {
                        response.Add("SQL Compact 3.5 installed");
                    }
                    else
                    {
                        response.Add("SQL Compact 3.5 failed to install");
                        loggingClass.logEntryWriter("SQL Compact 3.5 failed to install", "error");
                    }

                    break;

                case 3:

                    if (installerClass.gisAsync(true, "").Result.Equals("true"))
                    {
                        response.Add("GIS Components Installed");
                    }
                    else
                    {
                        response.Add("GIS Components failed to install");
                        loggingClass.logEntryWriter("GIS Components failed to install", "error");
                    }
                    break;

                case 4:

                    if (installerClass.dbProviderServiceAsync(true, "").Result.Equals("true"))
                    {
                        response.Add("DB Providers Installed");
                    }
                    else
                    {
                        response.Add("DB Providers failed to install");
                        loggingClass.logEntryWriter("DB Providers failed to install", "error");
                    }
                    break;

                case 5:

                    if (installerClass.updaterInstallerAsync("").Result.Equals("true"))
                    {
                        response.Add("Updater Installed");
                    }
                    else
                    {
                        response.Add("Updater failed to install");
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
                        response.Add("SQL Compact 4.0 Installed");
                    }
                    else
                    {
                        response.Add("SQL Compact 4.0 failed to install");
                        loggingClass.logEntryWriter("SQL Compact 4.0 failed to install", "error");
                    }
                    break;

                case 8:

                    if (installerClass.vs2010Async("").Result.Equals("true"))
                    {
                        response.Add("Visual Studio 2010 Types Installed");
                    }
                    else
                    {
                        response.Add("Visual Studio 2010 Types failed to install");
                        loggingClass.logEntryWriter("Visual Studio 2010 Types failed to install", "error");
                    }
                    break;

                case 9:

                    if (installerClass.sqlClr2008Async("").Result.Equals("true"))
                    {
                        response.Add("SQL 2008 CLR Types Installed");
                    }
                    else
                    {
                        response.Add("SQL 2008 CLR Types failed to install");
                        loggingClass.logEntryWriter("SQL 2008 CLR Types failed to install", "error");
                    }

                    if (installerClass.sqlClr2012Async("").Result.Equals("true"))
                    {
                        response.Add("SQL 2012 CLR Types Installed");
                    }
                    else
                    {
                        response.Add("SQL 2012 CLR Types failed to install");
                        loggingClass.logEntryWriter("SQL 2012 CLR Types failed to install", "error");
                    }

                    break;

                case 10:
                    response.Add("\"None Defined, nothing installed");
                    break;

                    break;

                case 99:
                    if (installerClass.dotNetAsync("").Result.Equals("true"))
                    {
                        response.Add("DotNet Installed");
                    }
                    else
                    {
                        response.Add("DotNet failed to install");
                        loggingClass.logEntryWriter("DotNet failed to install", "error");
                    }

                    if (installerClass.sqlCe35Async(true, "").Result.Equals("true"))
                    {
                        response.Add("SQL Compact 3.5 installed");
                    }
                    else
                    {
                        response.Add("SQL Compact 3.5 failed to install");
                        loggingClass.logEntryWriter("SQL Compact 3.5 failed to install", "error");
                    }

                    if (installerClass.gisAsync(true, "").Result.Equals("true"))
                    {
                        response.Add("GIS Components Installed");
                    }
                    else
                    {
                        response.Add("GIS Components failed to install");
                        loggingClass.logEntryWriter("GIS Components failed to install", "error");
                    }

                    if (installerClass.dbProviderServiceAsync(true, "").Result.Equals("true"))
                    {
                        response.Add("DB Providers Installed");
                    }
                    else
                    {
                        response.Add("DB Providers failed to install");
                        loggingClass.logEntryWriter("DB Providers failed to install", "error");
                    }

                    if (installerClass.updaterInstallerAsync("").Result.Equals("true"))
                    {
                        response.Add("Updater Installed");
                    }
                    else
                    {
                        response.Add("Updater failed to install");
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
                        response.Add("SQL Compact 4.0 Installed");
                    }
                    else
                    {
                        response.Add("SQL Compact 4.0 failed to install");
                        loggingClass.logEntryWriter("SQL Compact 4.0 failed to install", "error");
                    }

                    if (installerClass.vs2010Async("").Result.Equals("true"))
                    {
                        response.Add("Visual Studio 2010 Types Installed");
                    }
                    else
                    {
                        response.Add("Visual Studio 2010 Types failed to install");
                        loggingClass.logEntryWriter("Visual Studio 2010 Types failed to install", "error");
                    }

                    if (installerClass.sqlClr2008Async("").Result.Equals("true"))
                    {
                        response.Add("SQL 2008 CLR Types Installed");
                    }
                    else
                    {
                        response.Add("SQL 2008 CLR Types failed to install");
                        loggingClass.logEntryWriter("SQL 2008 CLR Types failed to install", "error");
                    }

                    if (installerClass.sqlClr2012Async("").Result.Equals("true"))
                    {
                        response.Add("SQL 2012 CLR Types Installed");
                    }
                    else
                    {
                        response.Add("SQL 2012 CLR Types failed to install");
                        loggingClass.logEntryWriter("SQL 2012 CLR Types failed to install", "error");
                    }

                    break;

                default:
                    break;
            }

            var jsonReturn = JsonConvert.SerializeObject(response);

            return jsonReturn;
        }

        public Task<string> PostClientInstall(int id, [FromBody] string bodyContent)
        {
            utilityClass.parseRequestBodyAsync(Request.Content.ReadAsStringAsync().Result);

            List<string> response = new List<string>();

            switch (id)
            {
                case 1:

                    //installMSP

                    string command = $"msiexec /i \"{clientRun}\\NewWorldMSPClient.msi\" addlocal=\"AegisClientBase,F_VB6RedistRuntime,Maintenance,Corrections,LERMS\" MSPSERVERNAME=\"{serverConfigObj.MSPServer}\" AUTHSERVERNAME=\"{serverConfigObj.ESSServer}\" /q /L*V \"C:\\TEMP\\MSP.log\"";

                    if (installerClass.MSP(command).Result.Equals(true))
                    {
                        response.Add($"MSP Installed successfully values passed - MSP Server: {serverConfigObj.MSPServer} | ESS Server: {serverConfigObj.ESSServer}");
                    }
                    else
                    {
                        response.Add($"MSP failed to install values passed - MSP Server: {serverConfigObj.MSPServer} | ESS Server: {serverConfigObj.ESSServer}");
                    }

                    break;

                case 2:
                    //installCAD

                    string command1 = $"msiexec /i \"{clientRun}\\NewWorld.Enterprise.CAD.Client.x64.msi\" DISPATCH_SERVER=\"{serverConfigObj.CADServer}\" MEMBERSHIP_SERVER=\"{serverConfigObj.ESSServer}\" GIS_SERVER_NAME=\"{serverConfigObj.GISServer}\" GIS_INSTANCE=\"{serverConfigObj.GISInstance}\" NWS_CHECKBOX_PICTOMETRY_ENABLE=\"0\" /q /L*V \"C:\\TEMP\\CAD.log\"";

                    if (installerClass.CAD(command1).Result.Equals(true))
                    {
                        response.Add($"CAD Installed successfully values passed - CAD Server: {serverConfigObj.CADServer} | ESS Server: {serverConfigObj.ESSServer} | GIS Server {serverConfigObj.GISServer} | GIS Instance {serverConfigObj.GISInstance}");
                    }
                    else
                    {
                        response.Add($"CAD failed to install values passed - CAD Server: {serverConfigObj.CADServer} | ESS Server: {serverConfigObj.ESSServer} | GIS Server {serverConfigObj.GISServer} | GIS Instance {serverConfigObj.GISInstance}");
                    }

                    break;

                case 3:

                    if (installerClass.incidentObserverAsync("").Result.Equals("true"))
                    {
                        string logEntry1 = @"Incident Observer installed";

                        loggingClass.logEntryWriter(logEntry1, "info");

                        response.Add("CAD Incident Observer Installed");
                    }
                    else
                    {
                        response.Add("CAD Incident Observer failed to install");
                        loggingClass.logEntryWriter("CAD Incident Observer failed to install", "error");
                    }

                    break;

                case 99:

                    string command00 = $"msiexec /i \"{clientRun}\\NewWorldMSPClient.msi\" addlocal=\"AegisClientBase,F_VB6RedistRuntime,Maintenance,Corrections,LERMS\" MSPSERVERNAME=\"{serverConfigObj.MSPServer}\" AUTHSERVERNAME=\"{serverConfigObj.ESSServer}\" /q /L*V \"C:\\TEMP\\MSP.log\"";

                    if (installerClass.MSP(command00).Result.Equals(true))
                    {
                        response.Add($"MSP Installed successfully values passed - MSP Server: {serverConfigObj.MSPServer} | ESS Server: {serverConfigObj.ESSServer}");
                    }
                    else
                    {
                        response.Add($"MSP failed to install values passed - MSP Server: {serverConfigObj.MSPServer} | ESS Server: {serverConfigObj.ESSServer}");
                    }

                    string command11 = $"msiexec /i \"{clientRun}\\NewWorld.Enterprise.CAD.Client.x64.msi\" DISPATCH_SERVER=\"{serverConfigObj.CADServer}\" MEMBERSHIP_SERVER=\"{serverConfigObj.ESSServer}\" GIS_SERVER_NAME=\"{serverConfigObj.GISServer}\" GIS_INSTANCE=\"{serverConfigObj.GISInstance}\" NWS_CHECKBOX_PICTOMETRY_ENABLE=\"0\" /q /L*V \"C:\\TEMP\\CAD.log\"";

                    if (installerClass.CAD(command11).Result.Equals(true))
                    {
                        response.Add($"CAD Installed successfully values passed - CAD Server: {serverConfigObj.CADServer} | ESS Server: {serverConfigObj.ESSServer} | GIS Server {serverConfigObj.GISServer} | GIS Instance {serverConfigObj.GISInstance}");
                    }
                    else
                    {
                        response.Add($"CAD failed to install values passed - CAD Server: {serverConfigObj.CADServer} | ESS Server: {serverConfigObj.ESSServer} | GIS Server {serverConfigObj.GISServer} | GIS Instance {serverConfigObj.GISInstance}");
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

                        response.Add($"ORI {item.ORI} added with mobile server {serverConfigObj.MobileServer}");
                    }

                    foreach (var item in serverConfigObj.configFileFDIDObjs)
                    {
                        updaterConfigClass.seeIfNodesExist(item.FDID);

                        updaterConfigClass.fdidSub(item.FDID, serverConfigObj.MobileServer);

                        response.Add($"FDID {item.FDID} added with mobile server {serverConfigObj.MobileServer}");
                    }

                    updaterConfigClass.policeClientSub(serverConfigObj.MobileServer);

                    updaterConfigClass.fireClientSub(serverConfigObj.MobileServer);

                    updaterConfigClass.mergeClientSub(serverConfigObj.MobileServer);

                    response.Add("Police Client, Fire Client, Merge Client added with mobile server " + serverConfigObj.MobileServer);

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

            var jsonReturn = JsonConvert.SerializeObject(response);

            return Task.FromResult(jsonReturn);
        }

        public Task<string> PostMobileConfig(int id, [FromBody] string bodyContent)
        {
            utilityClass.parseRequestBodyAsync(Request.Content.ReadAsStringAsync().Result);

            List<string> response = new List<string>();

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

                        response.Add($"ORI {item.ORI} added with mobile server {serverConfigObj.MobileServer}");
                    }

                    updaterConfigClass.policeClientSub(serverConfigObj.MobileServer);

                    response.Add("Police Client added with mobile server " + serverConfigObj.MobileServer);

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

                        response.Add($"FDID {item.FDID} added with mobile server {serverConfigObj.MobileServer}");
                    }

                    updaterConfigClass.fireClientSub(serverConfigObj.MobileServer);

                    response.Add("Fire Client added with mobile server " + serverConfigObj.MobileServer);

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

                        response.Add($"ORI {item.ORI} added with mobile server {serverConfigObj.MobileServer}");
                    }

                    updaterConfigClass.mergeClientSub(serverConfigObj.MobileServer);

                    response.Add("Merge Client added with mobile server " + serverConfigObj.MobileServer);

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

                        response.Add($"ORI {item.ORI} added with mobile server {serverConfigObj.MobileServer}");
                    }

                    foreach (var item in serverConfigObj.configFileFDIDObjs)
                    {
                        updaterConfigClass.seeIfNodesExist(item.FDID);

                        updaterConfigClass.fdidSub(item.FDID, serverConfigObj.MobileServer);

                        response.Add($"FDID {item.FDID} added with mobile server {serverConfigObj.MobileServer}");
                    }

                    updaterConfigClass.policeClientSub(serverConfigObj.MobileServer);

                    updaterConfigClass.fireClientSub(serverConfigObj.MobileServer);

                    updaterConfigClass.mergeClientSub(serverConfigObj.MobileServer);

                    response.Add("Police Client, Fire Client, Merge Client added with mobile server " + serverConfigObj.MobileServer);

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

            var jsonReturn = JsonConvert.SerializeObject(response);

            return Task.FromResult(jsonReturn);
        }
    }
}