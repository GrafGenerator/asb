using System.Linq;
using ASB.Abstractions;
using ASB.Abstractions.Validation;

namespace ASB.Common.Handlers
{
    public class CommandContractValidator<T> : ICommandValidator<T> 
        where T : ICommand
    {
        public (bool success, string[] errors) Validate(T command)
        {
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                if (propertyInfo.CustomAttributes.Any(cad => cad.AttributeType == typeof(RequiredAttribute)))
                {
                    var value = propertyInfo.GetValue(command);
                    if (propertyInfo.PropertyType.IsClass && value == null)
                    {
                        return (false, new[] {$"Property {propertyInfo.Name} requires value, '{value}' provided."});
                    }
                }
            }

            return (true, new string[0]);
        }
    }
}