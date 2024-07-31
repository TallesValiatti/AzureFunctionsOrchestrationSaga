namespace AzureFunctionsOrchestrationSaga.Core.Messages.Models;

public record BookFlightMessage(Guid BookingTravelId) : IMessage;