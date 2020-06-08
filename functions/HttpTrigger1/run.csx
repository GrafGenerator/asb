#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static async Task<object> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody);

    var orderId = data?.orderId ?? data?.OrderId;
    var name = data?.userName ?? data?.UserName;

    return new {
        Message = $"Order with ID = {orderId} captured, congratulations {name}!"
    };
}
