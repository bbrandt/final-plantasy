namespace TRS.FinalPlantasy.Application.Abstractions.Validations;

public class ValidationMessage
{
    public ValidationMessage(
        ValidationType validationType,
        string message)
    {
        ValidationType = validationType;
        Message = message;
    }

    public ValidationType ValidationType { get; }

    public string Message { get; }

    public bool IsError()
    {
        return ValidationType == ValidationType.Error;
    }
}
