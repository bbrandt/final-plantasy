using System.Collections.ObjectModel;

namespace TRS.FinalPlantasy.Application.Abstractions.Validations;

public class ResultResponse<T> : Response
{
    public ResultResponse(
        T value,
        IEnumerable<ValidationMessage> messages) 
        : 
        base(messages)
    {
        Value = value;
    }

    public ResultResponse(
        T value)
        :
        this(value, Enumerable.Empty<ValidationMessage>())
    {
    }

    public static ResultResponse<T> WithError(T value, string message)
    {
        var messages = new Collection<ValidationMessage>
        {
            ValidationMessage.AsError(message)
        };

        return new ResultResponse<T>(value, messages);
    }

    public T Value { get; }
}
