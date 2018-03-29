using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Handlers.Helpers;
using Handlers.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Handlers
{
    public class Handler
    {
        public Response Hello(Request request, ILambdaContext context)
        {
            context.Logger.LogLine($"{context.FunctionName} execution started");
            context.Logger.LogLine($"TestString: {AppConfig.Instance.GetParameter("TestString")}");
            context.Logger.LogLine($"TestSecure: {AppConfig.Instance.GetParameter("TestSecure")}");
            context.Logger.LogLine($"Region: {Environment.GetEnvironmentVariable("region")}");
            return new Response { Message = "Hello World, serverless-aws-aspnetcore2!" };
        }

        public APIGatewayProxyResponse HealthCheck(APIGatewayProxyRequest request, ILambdaContext context)
        {
            context.Logger.LogLine($"{context.FunctionName} execution started");

            return new APIGatewayProxyResponse()
            {
                StatusCode = 200,
                Headers = new Dictionary<string, string>() { { "Context-Type", "text/html" } },
                Body = "OK"
            };
        }

    }
}
