using ASB.Abstractions;

namespace ASB.Microservices.Actions.Action3.Handlers
{
    public class PostDataCommand: IAggregatorNodeCommand
    {
        public CommandIdentity Identity { get; } = C.PostData;
        
        public string Data { get; set; }
    }
}