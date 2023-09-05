namespace TRS.FinalPlantasy.Application.Abstractions.Validations;

public class Response
{
    public Response(IEnumerable<ValidationMessage> messages)
    {
        Messages = messages.ToList();
    }

    public ICollection<ValidationMessage> Messages { get; }

    public bool HasErrors()
    {
        return Messages.Any(x => x.IsError());
    }
}
