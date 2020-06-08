namespace ASB.Abstractions
{
    public interface ICommand
    {
        CommandIdentity Identity { get; }
    }
}