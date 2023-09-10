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

    public static ValidationMessage AsError(string message)
    {
        return new ValidationMessage(ValidationType.Error, message);
    }

    public ValidationType ValidationType { get; }

    public string Message { get; }

    public bool IsError()
    {
        return ValidationType == ValidationType.Error;
    }
}
