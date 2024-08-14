using AzureFunctionsOrchestrationSaga.Core.Entities;

namespace AzureFunctionsOrchestrationSaga.Core.Messages.Models;

public record ReplyMessage(Guid BookingTravelId, EventType Type) : IMessage;