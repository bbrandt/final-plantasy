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

    public T Value { get; }
}
