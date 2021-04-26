using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net.Http;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace FirstWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfoController : ControllerBase
    {
        private readonly ILogger<InfoController> _logger;

        private static HttpClient httpClient = new HttpClient();

        public InfoController()
        {
            //Logger example
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddOpenTelemetry(options => options
                    .AddConsoleExporter());
            });

            _logger = loggerFactory.CreateLogger<InfoController>();
        }

        private static readonly ActivitySource JaegerActivitySource = new ActivitySource(
            "Samples.InfoController");

        [HttpGet]
        public string Get()
        {
            //// Enable OpenTelemetry for the sources "Samples.InfoController"
            //using var openTelemetry = Sdk.CreateTracerProviderBuilder()
            //    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("jaeger-info-controller"))
            //    .AddSource("Samples.InfoController")
            //    .AddJaegerExporter()
            //    .Build();

            ////activity
            //using (var activity = JaegerActivitySource.StartActivity("MyCustomActivityFromInfoController"))
            //{
            //    activity?.SetTag("foo-int", 1);
            //    activity?.SetTag("foo-string", "Hello, World!");
            //    activity?.SetTag("foo-array", new int[] {1, 2, 3});
            //    activity?.SetCustomProperty("customName", "Volha");

            //    // Making an http call here to serve as an example of
            //    // how dependency calls will be captured and treated
            //    // automatically as child of incoming request.
            //    var res = httpClient.GetStringAsync("http://google.com").Result;
            //}

            ////span
            //using (var startActiveSpan =
            //    openTelemetry.GetTracer("Samples.InfoController").StartActiveSpan("CustomSpan"))
            //{
            //    startActiveSpan.SetStatus(Status.Error);
            //    startActiveSpan.SetAttribute("attr1", "11");
            //    startActiveSpan.SetAttribute("attr2", "22");
            //}

            var res = httpClient.GetStringAsync("http://localhost:5555").Result;
            _logger.LogInformation("Hello from logger {name} {price}.", "tomato", 2.99);
            var rng = new Random();
            _logger.LogWarning("Hello from logger {name} {price}.", "potato", 3.55);
            return $"First message {rng.Next()}";
        }
    }
}
