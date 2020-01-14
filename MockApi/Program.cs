using System;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace MockApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new FluentMockServerSettings
            {
                Urls = new[] { "https://localhost:9095/" },
                StartAdminInterface = true,
                ProxyAndRecordSettings = new ProxyAndRecordSettings
                {
                    Url = "https://samples.openweathermap.org",
                    SaveMapping = true,
                    SaveMappingToFile = true,
                    SaveMappingForStatusCodePattern = "2xx"
                }
            };

            var proxyServer = FluentMockServer.Start(settings);

            var server = FluentMockServer.Start(new FluentMockServerSettings()
            {
                Urls = new[] { "http://localhost:9096/" },
                StartAdminInterface = true
            });

            server.Given(Request.Create().WithPath("/some/thing").WithParam("name"))
                .RespondWith(Response.Create().WithStatusCode(201)
                    .WithHeader("Content-Type", "text/plain")
                    .WithBody($"Hello {{{{request.query.name}}}} it's {DateTime.Now}!")
                    .WithTransformer());

            Console.WriteLine("Press any key to stop the server");
            Console.ReadKey();
        }
    }
}
