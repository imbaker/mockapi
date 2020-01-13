using System;
using WireMock.Matchers.Request;
using WireMock.Net.StandAlone;
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
            var server = FluentMockServer.Start(new FluentMockServerSettings()
            {
                Urls = new[] { "http://localhost:9095" },
                StartAdminInterface = true
            });

            server.Given(Request.Create().WithPath("/some/thing").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(201)
                    .WithHeader("Content-Type", "text/plain")
                    .WithBody("Hello world!"));

            Console.WriteLine("Press any key to stop the server");
            Console.ReadKey();
        }
    }
}
