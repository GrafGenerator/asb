#r "Newtonsoft.Json"

using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

public static void Run(string mySbMsg, ILogger log, out Response outputSbMsg)
{
    log.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
    var data = JsonConvert.DeserializeObject<Data>(mySbMsg);
    log.LogInformation($"Message deserialized");

    var result = $"({data.OrderId},'{data.UserName}')";
    log.LogInformation($"Result: {result}");
    var response = new Response {
        Identity = "aggregator-node-post-data",
        Data = result
    };

    log.LogInformation($"Send response");
    //outputSbMsg = JsonConvert.Serialize(response);
    outputSbMsg = response;
}

public class Data {
    public string OrderId { get;set; }
    public string UserName { get;set; }
}

public class Response {
    public string Identity { get;set; }
    public string Data { get;set; }
}