using ASB.Abstractions;

namespace ASB.Microservices.Actions.Client.Commands.PostProcessOrderCommand
{
    public class PostProcessOrderCommand: IPostProcessOrderCommand
    {
        public CommandIdentity Identity { get; } = C.PostProcessOrder;
        
        public int OrderId { get; set; }
    }
}