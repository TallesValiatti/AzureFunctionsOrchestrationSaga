namespace AzureFunctionsOrchestrationSaga.Core.Messages.Models;

public record CancelBookCarMessage(Guid BookingTravelId) : IMessage;