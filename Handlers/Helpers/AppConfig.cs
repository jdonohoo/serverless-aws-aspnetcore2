using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Amazon;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Handlers.Helpers
{
    public class AppConfig
    {
        public string Region => Environment.GetEnvironmentVariable("region") ?? "us-east-1";
        public string Profile => Environment.GetEnvironmentVariable("profile") ?? "default";
        public string ServiceName => Environment.GetEnvironmentVariable("serviceName") ?? "serverless-aws-aspnetcore2";
        public string ParameterPath => Environment.GetEnvironmentVariable("parameterPath") ?? "/dev/serverless-aws-aspnetcore2/settings/";

        private Dictionary<string, string> Parameters { get; set; }

        [JsonIgnore]
        private static volatile AppConfig _instance;
        [JsonIgnore]
        private static readonly object _syncRoot = new Object();
        [JsonIgnore]
        public static AppConfig Instance
        {
            
            get
            {
                if (_instance != null) return _instance;
                lock (_syncRoot)
                {
                    if (_instance != null) return _instance;

                    _instance = new AppConfig();
                }

                return _instance;
            }
        }

        private AppConfig()
        {
            List<Parameter> paramList = new List<Parameter>();
            string nextToken = string.Empty;

            Parameters = new Dictionary<string, string>();
            var client = new AmazonSimpleSystemsManagementClient(RegionEndpoint.GetBySystemName(Region));

            var request = new GetParametersByPathRequest
            {
                Path = ParameterPath,
                Recursive = true,
                WithDecryption = true
            };

            var task = client.GetParametersByPathAsync(request);
            task.Wait();

            paramList.AddRange(task.Result.Parameters);
            nextToken = task.Result.NextToken;

            while(!string.IsNullOrEmpty(nextToken))
            {
                request.NextToken = nextToken;
                task = client.GetParametersByPathAsync(request);
                task.Wait();

                paramList.AddRange(task.Result.Parameters);
                nextToken = task.Result.NextToken;
            }
            

            foreach(var p in paramList)
            {
                string name = p.Name.Replace(ParameterPath, string.Empty);
                string value = p.Value;
                Parameters.Add(name, value);
            }


        }

        public string GetParameter(string name)
        {
            try
            {
                return Parameters[name];
            }
            catch (Exception)
            {
                throw new Exception($"{name} not found in parameter dictionary check: {Instance.ParameterPath}{name} is in SSM Parameter Store.");
            }

        }

    }
}
