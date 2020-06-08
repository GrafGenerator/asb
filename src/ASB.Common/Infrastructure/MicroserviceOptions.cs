namespace ASB.Common.Infrastructure
{
    public class MicroserviceOptions<TTransportOptions>
    {
        public string ServiceId { get; set; }
        public IMicroserviceCommandsRegistry CommandsRegistry { get; set; }
        public TTransportOptions TransportOptions { get; set; }
    }
}