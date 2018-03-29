# serverless-aws-aspnetcore2

[![serverless](https://dl.dropboxusercontent.com/s/d6opqwym91k0roz/serverless_badge_v3.svg)](http://www.serverless.com)

Serverless bootstrap for aws lambda running dotnetcore2

## Credits
Inspired by Serverless aws-csharp 1.0 template

Inspired by [PageUpPeopleOrg](https://github.com/PageUpPeopleOrg/serverless-microservice-bootstrap) Bootstrap Template
HealthCheck Endpoint / Build Scripts / ReadMe

Need help with the cloud? Check us out over at [Observian](https://www.observian.com).

[Original ReadMe](https://github.com/jdonohoo/serverless-aws-aspnetcore2/blob/master/README-original.md)

## Getting Started

### Windows
Install [Chocolatey](https://chocolatey.org/install)

Install Node
```
choco install nodejs.install
```

Install curl
```
choco install curl
```

Install [Serverless Framework](http://www.serverless.com)
```
npm install serverless -g
```

Install AWS CLI
```
choco install awscli
```
### OSX
Install [Homebrew](https://brew.sh/)

Install Node
```
brew install node
```

Install [Serverless Framework](http://www.serverless.com)
```
npm install serverless -g
```

Install AWS CLI
```
brew install awscli
```

### AWS CLI Configuration

Configure the aws-cli if you haven't already. [aws-cli](https://docs.aws.amazon.com/cli/latest/userguide/cli-chap-getting-started.html)

Install dotnet core on your machine. Instructions can be found at [dotnet website](https://www.microsoft.com/net/download)

## Build

Windows via powershell
```
build.ps1
```

OSX via bash
```
sh ./build-osx.sh
```

Linux via bash
```
./build.sh
```
## Configuration SSM
If you aren't familar with AWS SSM Parameter Store start [here](https://aws.amazon.com/blogs/mt/organize-parameters-by-hierarchy-tags-or-amazon-cloudwatch-events-with-amazon-ec2-systems-manager-parameter-store/)

### How to get there:
```
AWSConsole > EC2 > Parameter Store (Bottom left corner scroll down)
```
All functions are deployed with the environment variable: parameterPath

Because of this block in Serverless.yml:
```
  environment:
	parameterPath: /${self:provider.stage}/${self:service}/settings
```

Configure the following variables, or unit tests will fail later on:

`/dev/serverless-aws-aspnetcore2/settings/TestString` 
This can be any value


`/dev/serverless-aws-aspnetcore2/settings/TestSecure` 
This can be any value but select secure string to encrypt it.

### Lambda Role
Check original read me for specific policies, moved these to inline statements in serverless.yml

Under the provider section:
```
  iamRoleStatements:
    -  Effect: "Allow"
       Action:
         - "s3:ListBucket"
       Resource:
         Fn::Join:
           - ""
           - - "arn:aws:s3:::"
             - "${self:provider.deploymentBucket}"
    -  Effect: "Allow"
       Action:
         - "s3:PutObject"
       Resource:
         Fn::Join:
           - ""
           - - "arn:aws:s3:::"
             - "${self:provider.deploymentBucket}"
             - "/*"
    -  Effect: "Allow"
       Action:
         - "logs:*"
       Resource: "*"
    -  Effect: "Allow"
       Action:
         - "ssm:Describe*"
         - "ssm:Get*"
         - "ssm:List*"
       Resource: "*"
```

### Settings hierarchy
```
/stage/servicename/settings
```
### Accessing SSM Parameters via Code
```
AppConfig.Instance.GetParameter("TestString");
AppConfig.Instance.GetParameter("TestSecure"); 
```
Secure strings will automatically be pulled down decrypted.


### Retrieving parameters via aws-cli
```
aws ssm get-parameters-by-path --path /dev/serverless-aws-aspnetcore2/settings --recursive
```
#### Sample Output:
```
{
    "Parameters": [
        {
            "Version": 1,
            "Type": "SecureString",
            "Name": "/dev/serverless-aws-aspnetcore2/settings/TestSecure",
            "Value": "AQICAHj7GTUMLLb+voz+gUUoBAz/KGeLrbKNq+UgF9HcIvhrEAF4vJD/XTYbCpOmfJuONQn9AAAAdjB0BgkqhkiG9w0BBwagZzBlAgEAMGAGCSqGSIb3DQEHATAeBglghkgBZQMEAS4wEQQMzEPiqs2fSMS8JSKmAgEQgDNPeZzlA/ljsgxcmFni0rPIG876l7hgHlU3xJrIwwUAHKGIXs68dArewJrPGYlV3jMWV1s="
        },
        {
            "Version": 1,
            "Type": "String",
            "Name": "/dev/serverless-aws-aspnetcore2/settings/TestString",
            "Value": "Some Test String"
        }
    ]
}
```

### Retrieve secured values via aws-cli
```
aws ssm get-parameter --name /dev/serverless-aws-aspnetcore2/settings/TestSecure --with-decryption
```
#### Sample Output:
```
{
    "Parameter": {
        "Version": 1,
        "Type": "SecureString",
        "Name": "/dev/serverless-aws-aspnetcore2/settings/TestSecure",
        "Value": "Secure string test value"
    }
}
```
## Testing CommandLine
```
dotnet test .\Tests
```

#### Output:
```
Build started, please wait...
Build completed.

Test run for C:\projects\serverless-aws-aspnetcore2\Tests\bin\Debug\netcoreapp2.0\Tests.dll(.NETCoreApp,Version=v2.0)
Microsoft (R) Test Execution Command Line Tool Version 15.5.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
[xUnit.net 00:00:00.4202457]   Discovering: Tests
[xUnit.net 00:00:00.4809961]   Discovered:  Tests
[xUnit.net 00:00:00.4867238]   Starting:    Tests
[xUnit.net 00:00:00.6334110]   Finished:    Tests

Total tests: 2. Passed: 2. Failed: 0. Skipped: 0.
Test Run Successful.
Test execution time: 1.3748 Seconds
```

## Deploying CommandLine
```
serverless deploy
```

#### Output:
```
Serverless: Packaging service...
Serverless: Uploading CloudFormation file to S3...
Serverless: Uploading artifacts...
Serverless: Validating template...
Serverless: Creating Stack...
Serverless: Checking Stack create progress...
.........................................
Serverless: Stack create finished...
Service Information
service: serverless-aws-aspnetcore2
stage: dev
region: us-east-1
stack: serverless-aws-aspnetcore2-dev
api keys:
  None
endpoints:
  GET - https://xxxxxxxxxx.execute-api.us-east-1.amazonaws.com/dev/healthcheck
functions:
  hello: serverless-aws-aspnetcore2-dev-hello
  healthcheck: serverless-aws-aspnetcore2-dev-healthcheck
```

### Testing via HealthCheck Endpoint

```
curl https://xxxxxxxxxx.execute-api.us-east-1.amazonaws.com/dev/healthcheck
```

#### Output:
```
StatusCode        : 200
StatusDescription : OK
Content           : OK
RawContent        : HTTP/1.1 200 OK
                    Connection: keep-alive
                    x-amzn-RequestId: 5d9f56d7-142c-11e8-8aba-3d2ffd5e6280
                    Context-Type: text/html
                    X-Amzn-Trace-Id: sampled=0;root=1-5a88a34d-1315491996eff1c9a983409f
                    X-Cache: ...
Forms             : {}
Headers           : {[Connection, keep-alive], [x-amzn-RequestId, 5d9f56d7-142c-11e8-8aba-3d2ffd5e6280], [Context-Type, text/html], [X-Amzn-Trace-Id, sampled=0;root=1-5a88a34d-1315491996eff1c9a983409f]...}
Images            : {}
InputFields       : {}
Links             : {}
ParsedHtml        : mshtml.HTMLDocumentClass
RawContentLength  : 2
```




