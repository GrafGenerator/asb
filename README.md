## Microsoft Azure Service Bus and Azure Functions architecture example
This solution is an example of (micro)services architecture with services communicating through Azure Service Bus and HTTP APIs.

Main features:
* Simplified and unified declaration of microservices
* Common way to declare service contract and reuse it
* Different types of services integrations
* DI included
* Reader tolerance for incoming messages

### Overall solution structure
* **Framework** - contains common code which is used to create concrete services.
  * **ASB.Abstractions** - set of required abstractions without any dependency.
  * **ASB.Common** - common/base implementations of abstractions, built on top of Service Bus and Http dependencies.  
* **Info** - readme  
* **Microservices** - set of microservices of application
  * **Actions** - console-app based microservices.
    * **ASB.Microservices.Actions.Action1** - Action microservice. Make calls to WebAPI service and then Azure Functions HTTP triggered function.
    * **ASB.Microservices.Actions.Action2** - Action microservice. Make calls to WebAPI service and then Azure Functions Service Bus triggered function.
    * **ASB.Microservices.Actions.Action3** - Aggregator microservice. Listens to own topic/subscription and simply logs everything that comes.
    * **ASB.Microservices.Actions.Client** - Post-process order services contract and client (action-1 and action-2).
  * **AzureFunctions** - Azure Functions microservices
    * **Functions** - functions files
      * **HttpTrigger1** - HTTP triggered function microservice (af1)
      * **ServiceBusTrigger1** - Service Bus triggered microservice (af2)
    * **ASB.Microservices.AzureFunctions.BusService.Client** - client library for Service Bus triggered Azure Function.  
    * **ASB.Microservices.AzureFunctions.HttpService.Client** - client library for Http triggered Azure Function.  
  * **WebApi1** - set of microservices of application
    * **ASB.Microservices.APIService** - WebAPI based microservice (web1).
    * **ASB.Microservices.APIService.Client** - WebAPI service contract and client.  
* **TestApp** - test application, generating initial request into system.

### Service Bus structure
Create your Service Bus namespace and following entitities in it.
* af2 - topic
  * function1 - subscription
* aggregatornode - topic
  * action3 - subscription
* orderpostprocess - topic
  * action1 - subscription
  * action2 - subscription

### Azure Functions structure
* HttpTrigger1
  * Default settings
  * Code is in the solution
* ServiceBusTriggered1
  * Input binding
    * Namespace: your Service Bus namespace
    * Variable: mySbMsg
    * Topic: af2
    * Subscription: function1
  * Output binding: Azure Service Bus
    * Namespace: your Service Bus namespace
    * Variable: outputSbMsg
    * Topic: aggregatornode
    
### Microservices
* TestApp - sends command to system (order id to post-process)
* web1 - receives order id (http) and returns generated order details (http).
* action-1 - receives order id (bus), gets order details (http) and calls af-1 (http) and shows the response.
* action-2 - receives order id (bus), gets order details (http) and sends message to af-2 (bus).
* action-3 - receives messages in on topic/subscription (bus) and outputs them (topic - aggregator node).
* af-1 - receives order details (http) and returns string result (http).
* af-2 - receives order details (bus) and posts message to aggregatornode topic.

### Steps to run
1. Setup Service Bus
   1. Create SB namespace
   1. Create topics and subscriptions. Parameters are default.
1. Setup functions
   1. Create Functions App
   1. Create HttpTrigger1 function, description above
   1. Create ServiceBusTrigger1 function, description above
1. Get SB namespace connection string (in Shared Access Policies)
1. Get URL for HTTP triggered Azure Function (af-1)
1. Run WebAPI service, ensure it's running on https://localhost:5001. If not, either make it run on this address or remember address on which it runs. 
1. Go through all appsettings.json in solution:
   1. Set connection string where it's required
   1. Ensure correct address of WebAPI service in action-1 and action-2 services.
   1. Ensure correct function URL in action-1 service.
1. Run all projects.
1. In TestApp, enter random order number and see how the system is interacting.

### Visual structure
![System diagram](https://github.com/GrafGenerator/asb/blob/master/structure.png)