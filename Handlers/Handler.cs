using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
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
