using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace Hearts.Application
{

    public class ApiTest : BaseTest
    {
        private const int ApiTestRandomSeed = 42;
        protected HttpClient client;
        protected IntegrationTestWebApplicationFactory factory;

        [SetUp]
        public new void Init()
        {
            Environment.SetEnvironmentVariable("Database__ConnectionString", connectionString);
            Environment.SetEnvironmentVariable("App__RandomSeed", ApiTestRandomSeed.ToString());
            factory = new IntegrationTestWebApplicationFactory(loggerFactory);
            client = factory.CreateClient();
        }

        protected async Task<T> GetAsync<T>(string url, HttpStatusCode expectedStatus = HttpStatusCode.OK)
        {
            var response = await client.GetAsync(url);
            Assert.AreEqual(expectedStatus, response.StatusCode);
            return await response.Content.ReadAsAsync<T>();
        }
    }

}