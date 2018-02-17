using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using Handlers;
using Handlers.Models;
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
    }
}
