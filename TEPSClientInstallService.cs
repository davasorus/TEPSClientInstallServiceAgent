using System;
using System.ServiceProcess;
using System.Timers;
using System.Web.Http;
using System.Web.Http.SelfHost;
using testInstallServer.Classes;

namespace testInstallServer
{
    public partial class TEPSClientInstallService : ServiceBase
    {
        private Timer timer = new Timer();
        private apiClass apiClass = new apiClass();
        private loggingClass loggingClass = new loggingClass();

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
        }

        protected override void OnStop()
        {
            loggingClass.logEntryWriter("Service is stopped at " + DateTime.Now, "info");

            loggingClass.logEntryWriter($"API no longer listening", "info");
        }
    }
}