# serverless-aws-aspnetcore2

[![serverless](https://dl.dropboxusercontent.com/s/d6opqwym91k0roz/serverless_badge_v3.svg)](http://www.serverless.com)

Serverless bootstrap for aws lambda running dotnetcore2

Inspired by Serverless aws-csharp 1.0 template
Inspired by PageUpPeopleOrg https://github.com/PageUpPeopleOrg/serverless-microservice-bootstrap
HealthCheck Endpoint / Build Scripts / ReadMe


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

Install  [Serverless Framework](http://www.serverless.com) installed.
```
npm install serverless -g
```

Install dotnet core on your machine. Instructions can be found at (dotnet website)[https://www.microsoft.com/net/download]

## Build

Windows via powershell
```
build.ps1
```

Linux / Mac via bash
```
./build.sh
```

##Testing CommandLine
```
dotnet test .\Tests
```

Output:
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

##Deploying CommandLine
```
serverless deploy
```

Output:
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

Output:
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