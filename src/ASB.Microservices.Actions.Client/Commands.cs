using ASB.Abstractions;

namespace ASB.Microservices.Actions.Client
{
    public static class C
    {
        public static readonly string ServiceId = "post-process-actions";
        public static readonly CommandIdentity PostProcessOrder = new CommandIdentity(ServiceId, "process-order");
    }
}