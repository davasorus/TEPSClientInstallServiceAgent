using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Timers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.SelfHost;

using testInstallServer.Classes;

namespace testInstallServer
{
    public partial class TEPSClientInstallService : ServiceBase
    {
        private apiClass apiClass = new apiClass();
        private loggingClass loggingClass = new loggingClass();
        private installerClass installerClass = new installerClass();

        public TEPSClientInstallService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            loggingClass.initializeNLogLogger();

            var config = new HttpSelfHostConfiguration($"http://{Environment.MachineName}:8080");

            var cors = new EnableCorsAttribute($"http://{Environment.MachineName}", "*", "*");

            config.EnableCors();

            config.Routes.MapHttpRoute(
                name: "API",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            HttpSelfHostServer server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();

            loggingClass.logEntryWriter("Service is started at " + DateTime.Now, "info");
            loggingClass.logEntryWriter($"API listening at {config.BaseAddress}", "info");

            Timer timer = new Timer
            {
                Interval = 600000
            };
            timer.Start();

            timer.Elapsed += Timer_Elapsed;

            Directory.CreateDirectory("C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Agent\\Updater");

            File.Copy(Path.Combine("C:\\Services\\Tyler-Client-Install-Agent", "TEPS Automated Agent Updater.exe"), Path.Combine("C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Agent\\Updater", "TEPS Automated Agent Updater.exe"), true);

            Process[] localbyName = Process.GetProcessesByName("TEPS Automated Agent Updater");
            if (localbyName.Length > 0)
            {
            }
            else
            {
                installerClass.openProgram("C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Agent\\Updater", "TEPS Automated Agent Updater.exe");
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!File.Exists(Path.Combine("C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Agent\\Updater", "TEPS Automated Agent Updater.exe")))
            {
                File.Move(Path.Combine("C:\\Services\\Tyler-Client-Install-Agent", "TEPS Automated Agent Updater.exe"), Path.Combine("C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Agent\\Updater", "TEPS Automated Agent Updater.exe"));
            }

            Process[] localbyName = Process.GetProcessesByName("TEPS Automated Agent Updater");
            if (localbyName.Length > 0)
            {
            }
            else
            {
                installerClass.openProgram("C:\\ProgramData\\Tyler Technologies\\Public Safety\\Tyler-Client-Install-Agent\\Updater", "TEPS Automated Agent Updater.exe");
            }
        }

        protected override void OnStop()
        {
            loggingClass.logEntryWriter("Service is stopped at " + DateTime.Now, "info");

            loggingClass.logEntryWriter($"API no longer listening", "info");
        }
    }
}

internal class configValues
{
    public static readonly string preReqRun = @"C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\PreReqs";
    public static readonly string nwsAddonLocalRun = @"C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\Addons";
    public static readonly string clientRun = @"C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\Clients";
    public static string applicationName = "TEPS Automated Client Install Agent " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
    public static readonly string logFileName = $@"C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\Logging\{applicationName}.json";
}

internal class masterPreReqList
{
    public static readonly string sqlCE3532Name = "Microsoft SQL Server Compact 3.5 SP2 ENU";
    public static readonly string sqlCE3564Name = "Microsoft SQL Server Compact 3.5 SP2 x64 ENU";
    public static readonly string sqlCE4064Name = "Microsoft SQL Server Compact 4.0 x64 ENU";
    public static readonly string nwpsGis32Name = "New World GIS Components x86";
    public static readonly string nwpsGis64Name = "New World GIS Components x64";
    public static readonly string nwpsUpdateName = "New World Automatic Updater";
    public static readonly string sql2008Clr32Name = "Microsoft SQL Server System CLR Types";
    public static readonly string sql2008Clr64Name = "Microsoft SQL Server System CLR Types (x64)";
    public static readonly string sql2012Clr32Name = "Microsoft SQL Server System CLR Types";
    public static readonly string sql2012Clr64Name = "Microsoft SQL Server System CLR Types (x64)";
    public static readonly string SCPD6Name = "ScenePD 6 Desktop Edition";
    public static readonly string SCPD4Name = "ScenePD 4";
    public static readonly string fireMobileName = "Fire Mobile";
    public static readonly string policeMobileName = "Law Enforcement Mobile";
    public static readonly string mergeName = "Mobile Merge";
    public static readonly string printerName = "NWPS Enterprise Mobile PDF Printer";
    public static readonly string printerDriverName = "novaPDF 8 Printer Driver";
    public static readonly string x86COM = "novaPDF 8 SDK COM (x86)";
    public static readonly string x64COM = "novaPDF 8 SDK COM (x64)";
    public static readonly string tepsUpdater = "Enterprise Updater";

    public static readonly string dotNet47 = "dotNetFx471_Full_setup_Offline.exe";
    public static readonly string dotNet48 = "ndp48-x86-x64-allos-enu.exe";
    public static readonly string sqlCE3532 = "SSCERuntime_x86-ENU.msi";
    public static readonly string sqlCE3564 = "SSCERuntime_x64-ENU.msi";
    public static readonly string sqlCE4032 = "SSCERuntime_x86-ENU-4.0.exe";
    public static readonly string sqlCE4064 = "SSCERuntime_x64-ENU-4.0.exe";
    public static readonly string nwpsGis32 = "NewWorld.Gis.Components.x86.msi";
    public static readonly string nwpsGis64 = "NewWorld.Gis.Components.x64.msi";
    public static readonly string msSync64 = "Synchronization-v2.1-x64-ENU.msi";
    public static readonly string msProServ64 = "ProviderServices-v2.1-x64-ENU.msi";
    public static readonly string msDbPro64 = "DatabaseProviders-v3.1-x64-ENU.msi";
    public static readonly string msSync32 = "Synchronization-v2.1-x86-ENU.msi";
    public static readonly string msProServ32 = "ProviderServices-v2.1-x86-ENU.msi";
    public static readonly string msDbPro32 = "DatabaseProviders-v3.1-x86-ENU.msi";
    public static readonly string nwpsUpdate = "NewWorld.Management.Updater.msi";
    public static readonly string sqlClr32 = "SQLSysClrTypesx86.msi";
    public static readonly string sqlClr64 = "SQLSysClrTypesx64.msi";
    public static readonly string sqlClr201232 = "SQLSysClrTypes2012.msi";
    public static readonly string sqlClr201264 = "SQLSysClrTypesx642012.msi";
    public static readonly string SCPD6 = "SPD6-4-8993.exe";
    public static readonly string SCPD6AX = "SPDX6-4-3091.exe";
    public static readonly string SCPD4 = "SPD4-0-92.exe";
}

internal class masterClientList
{
    public static readonly string mspClient = "NewWorldMSPClient.msi";
    public static readonly string cadClient64 = "NewWorld.Enterprise.CAD.Client.x64.msi";
    public static readonly string cadClient32 = "NewWorld.Enterprise.CAD.Client.x86.msi";
    public static readonly string cadIncObs64 = "NewWorld.Enterprise.CAD.IncidentObserver.x64.msi";

    public static readonly string LERMS1 = "New World MSP Client";
    public static readonly string CAD2 = "New World Enterprise CAD Client";
    public static readonly string incidentObserv1 = "Enterprise CAD Incident Observer Client";
}