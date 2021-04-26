using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace FirstWebService
{
    public class Program
    {
        private static readonly ActivitySource ConsoleActivitySource = new ActivitySource(
            "Program.FirstWebService");
        public static void Main(string[] args)
        {
            ////Using console exporter for tracing
            //using var tracerProvider = Sdk.CreateTracerProviderBuilder()
            //    .SetSampler(new AlwaysOnSampler())
            //    .AddSource("Program.FirstWebService")
            //    .AddConsoleExporter()
            //    .Build();

            ////Using activity with tags
            //using (var activity = ConsoleActivitySource.StartActivity("ConsoleDisplayNameActivity"))
            //{
            //    //custom tags
            //    activity?.SetTag("foo-int", 1);
            //    activity?.SetTag("foo-string", "Hello, World from console!");
            //    activity?.SetTag("foo-array", new int[] { 1, 2, 3 });
            //    activity?.SetStatus(Status.Ok);//Error,Unset
            //    activity?.SetCustomProperty("customName","Volha");
            //}

            // Enable OpenTelemetry for the sources "Samples.SampleServer" and "Samples.SampleClient"
            // and use OTLP exporter.
            using var openTelemetry = Sdk.CreateTracerProviderBuilder()
                .AddSource("Program.FirstWebService")
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("otlp-test"))
                .AddOtlpExporter(opt => opt.Endpoint = new Uri("https://localhost:55678"))
                .Build();
            //Using activity with tags
            using (var activity = ConsoleActivitySource.StartActivity("ConsoleDisplayNameActivity"))
            {
                //custom tags
                activity?.SetTag("foo-int", 1);
                activity?.SetTag("foo-string", "Hello, World from console!");
                activity?.SetTag("foo-array", new int[] { 1, 2, 3 });
                activity?.SetStatus(Status.Ok);//Error,Unset
                activity?.SetCustomProperty("customName", "Volha");
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
