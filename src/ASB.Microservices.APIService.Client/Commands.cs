using ASB.Abstractions;

namespace ASB.Microservices.APIService.Client
{
    public static class C
    {
        public static readonly string ServiceId = "web-1";
        public static readonly CommandIdentity OrderDetails = new CommandIdentity(ServiceId, "order-details");
    }
}