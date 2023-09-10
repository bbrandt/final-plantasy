using System.Collections.ObjectModel;

namespace TRS.FinalPlantasy.Application.Abstractions.Validations;

public class Response
{
    public Response(IEnumerable<ValidationMessage> messages)
    {
        Messages = messages.ToList();
    }

    public static Response Empty()
    {
        return new Response(Enumerable.Empty<ValidationMessage>());
    }

    public static Response WithError(string message)
    {
        var messages = new Collection<ValidationMessage> 
        {
            ValidationMessage.AsError(message)
        };

        return new Response(messages);
    }

    public ICollection<ValidationMessage> Messages { get; }

    public bool HasErrors()
    {
        return Messages.Any(x => x.IsError());
    }
}
