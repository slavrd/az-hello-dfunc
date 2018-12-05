# Hello Azure Durable Functions

A simple project for basic Azure Durable function in C#

## Prerequisites

### Use locally

* Install .NET Core runtime - [download](https://dotnet.microsoft.com/download)
* Install Azure Functions Core Tools - [instructions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local#install-the-azure-functions-core-tools)

### Use the included Vagrant project to build a VM

* Install VirtualBox - [instructions](https://www.virtualbox.org/wiki/Downloads)
* Install vagrant - [instructions](https://www.vagrantup.com/downloads.html)
* Go to the vagrant folder - `cd vagrant`
* Start the VM - `vagrant up`
* Login to the machine - `vagrant ssh`
* Change in to the downloaded project directory - `az-hello-dfunc`

## Running the project

### Start the project locally

Note that even that the function is running locally it still needs access to Azure storage to store it's state.

* Create a JSON file  named `local.settings.json`
* Put in it the following

```JSON
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "<Your AZ Storage access string>",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet"
    }
}
```

* Run `func host start` - the project will build and start the function. In the end it will output a url
* Trigger the function - open a new terminal and run `curl <the output url>`

### Publish the function to Azure

* Crate a Function App in Azure which will host the function
* Publish the function - `func azure functionapp publish <name of the host function app>` - need to have Azure CLI or PowerShell installed and logged in.