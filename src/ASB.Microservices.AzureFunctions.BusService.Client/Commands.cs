using ASB.Abstractions;

namespace ASB.Microservices.AzureFunctions.BusService.Client
{
    public static class C
    {
        public static readonly string ServiceId = "af-2";
        public static readonly CommandIdentity CatchOrder = new CommandIdentity(ServiceId, "catch-order");
    }
}