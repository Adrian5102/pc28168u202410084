namespace pc28168u202410084.Shared.Domain.Model.ValueObjects;

public sealed record DateInterval
{
    public DateInterval(string value)
    {
        public DateOnly? ScheduledFrom { get; set; }
        public DateOnly? ScheduledTo { get; set; }
        
        if (ScheduledTo <= ScheduledFrom)
            throw new ArgumentException("ScheduledTo can't be less than ScheduledFrom.", nameof(DateOnly));

        bool HasElapsed(DateOnly referenceDate) {
                if (referenceDate > ScheduledTo)
                    return true;
        }
    }
}