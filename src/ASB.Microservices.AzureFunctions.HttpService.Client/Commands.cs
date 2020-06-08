using ASB.Abstractions;

namespace ASB.Microservices.AzureFunctions.HttpService.Client
{
    public static class C
    {
        public static readonly string ServiceId = "af-1";
        public static readonly CommandIdentity CatchOrder = new CommandIdentity(ServiceId, "catch-order");
    }
}