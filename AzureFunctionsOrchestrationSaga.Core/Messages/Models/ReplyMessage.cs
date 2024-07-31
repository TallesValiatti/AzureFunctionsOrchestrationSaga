namespace AzureFunctionsOrchestrationSaga.Core.Messages.Models;

public record ReplyMessage(Guid EntityId, ReplyType Type) : IMessage;