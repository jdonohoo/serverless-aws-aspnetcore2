using Microsoft.Extensions.Configuration;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using Handlers;
using Handlers.Helpers;
using Handlers.Models;
using System;
using Xunit;

namespace Tests
{
    public class HandlerTests
    {
        [Fact]
        public void TestHealthCheck()
        {
            var request = new APIGatewayProxyRequest();
            var context = new TestLambdaContext();

            var handler = new Handler();
            var response = handler.HealthCheck(request, context);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal("OK", response.Body);
        }

        [Fact]
        public void TestHello()
        {
            var context = new TestLambdaContext();
            var handler = new Handler();

            var response = handler.Hello(new Request(), context);

            Assert.Equal("Hello World, serverless-aws-aspnetcore2!",response.Message);
        }
                
        [Theory]
        [InlineData("dev")]
        public void ConfigurationTest(string stage)
        {
            Environment.SetEnvironmentVariable("region", "us-east-1");
            Environment.SetEnvironmentVariable("serviceName", "serverless-aws-aspnetcore2");
            Environment.SetEnvironmentVariable("parameterPath", $"/{stage}/serverless-aws-aspnetcore2/settings/");
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

            var context = new TestLambdaContext();
            var handler = new Handler();

            Assert.Equal("serverless-aws-aspnetcore2", AppConfig.Instance.ServiceName);
            Assert.Equal($"/{stage}/serverless-aws-aspnetcore2/settings/", AppConfig.Instance.ParameterPath);
            Assert.Equal("Secure string test value", AppConfig.Instance.GetParameter("TestSecure"));
            Assert.Equal("Some Test String", AppConfig.Instance.GetParameter("TestString"));
        }
    }
}
