namespace ASB.Abstractions.Validation
{
    public interface ICommandValidator<T>
        where T: ICommand
    {
        (bool success, string[] errors) Validate(T command);
    }
}