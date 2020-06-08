using ASB.Abstractions;

namespace ASB.Microservices.Actions.Action3
{
    public static class C
    {
        public static readonly string ServiceId = "aggregator-node";
        public static readonly CommandIdentity PostData = new CommandIdentity(ServiceId, "post-data");
    }
}