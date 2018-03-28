#!/bin/bash

HANDLERS_DIR=Handlers
#build handlers
dotnet restore
dotnet publish -c release $HANDLERS_DIR

#install zip
brew update >/dev/null
brew install zip >cdinstall zip

#create deployment package
pushd $HANDLERS_DIR/bin/release/netcoreapp2.0/publish
zip -r ./deploy-package.zip ./*
popd
