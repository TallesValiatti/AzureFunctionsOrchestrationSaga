using AzureFunctionsOrchestrationSaga.Core.Entities;

namespace AzureFunctionsOrchestrationSaga.Core.Repositories;

public interface IBookingTravelRepository
{
    Task AddAsync(BookingTravel entity);
    Task<BookingTravel?> GetByIdAsync(Guid id);
}