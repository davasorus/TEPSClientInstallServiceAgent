using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;
using System.Web.Http;
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
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            installerClass.openProgram(Directory.GetCurrentDirectory(), "TEPS Automated Agent Updater.exe");
        }

        protected override void OnStop()
        {
            loggingClass.logEntryWriter("Service is stopped at " + DateTime.Now, "info");

            loggingClass.logEntryWriter($"API no longer listening", "info");
        }
    }
}