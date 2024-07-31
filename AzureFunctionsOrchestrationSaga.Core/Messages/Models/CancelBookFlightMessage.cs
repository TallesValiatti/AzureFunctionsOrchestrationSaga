namespace AzureFunctionsOrchestrationSaga.Core.Messages.Models;

public record CancelBookFlightMessage(Guid BookingTravelId): IMessage;