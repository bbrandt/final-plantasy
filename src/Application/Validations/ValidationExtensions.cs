using TRS.FinalPlantasy.Application.Abstractions.Validations;

namespace TRS.FinalPlantasy.Application.Validations;

internal static class ValidationExtensions
{
    public static Response ToValidationResponse(this FluentValidation.Results.ValidationResult result)
    {
        var messages = result.ToValidationMessages();

        return new Response(messages);
    }

    public static ResultResponse<T> ToValidationResponse<T>(this FluentValidation.Results.ValidationResult result, T value)
    {
        var messages = result.ToValidationMessages();

        return new ResultResponse<T>(value, messages);
    }

    public static IEnumerable<ValidationMessage> ToValidationMessages(this FluentValidation.Results.ValidationResult result)
    {
        return result
            .Errors
            .Distinct()
            .Select(errorResult => 
            {
                var validationType = ConvertMessageType(errorResult.Severity);

                return new ValidationMessage(validationType, errorResult.ErrorMessage);
            });
    }

    private static ValidationType ConvertMessageType(FluentValidation.Severity severity)
    {
        switch (severity)
        {
            case FluentValidation.Severity.Error:
                return ValidationType.Error;
            case FluentValidation.Severity.Warning:
                return ValidationType.Warning;
            case FluentValidation.Severity.Info:
                return ValidationType.Information;
        }

        throw new Exception("Severity is unknown");
    }
}
