using AzureFunctionsOrchestrationSaga.Core.Messages.Models;

namespace AzureFunctionsOrchestrationSaga.Core.Entities;

public class BookingTravel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public IList<string> Events  { get; set; } = new List<string>();

    // other properties
    
    public void AddEvent(EventType @event)
    {
        Events.Add(@event.ToString());
    }
}

public enum EventType 
{
    BookCarCompleted = 1,
    BookHotelCompleted,
    BookFlightCompleted,
    BookCarCompensated,
    BookHotelCompensated,
    BookFlightCompensated
}

