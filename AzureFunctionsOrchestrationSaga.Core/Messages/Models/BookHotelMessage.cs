namespace AzureFunctionsOrchestrationSaga.Core.Messages.Models;

public record BookHotelMessage(Guid BookingTravelId) : IMessage;