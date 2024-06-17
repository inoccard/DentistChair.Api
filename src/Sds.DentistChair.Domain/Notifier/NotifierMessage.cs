namespace Sds.DentistChair.Domain.Notifier;

public class NotifierMessage : INotifierMessage
{
    private readonly List<string> _messages;

    public NotifierMessage()
    {
        _messages = [];
    }

    public void Add(string message) => _messages.Add(message);

    public void AddRange(List<string> messages) => _messages.AddRange(messages);

    public List<string> GetMessages() => _messages;

    public bool IsValid() => _messages.Count == 0;
}