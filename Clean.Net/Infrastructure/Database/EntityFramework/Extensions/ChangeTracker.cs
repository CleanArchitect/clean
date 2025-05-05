using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Clean.Net;

internal static class ChangeTrackerExtensions
{
    public static IEvent[] GetAndClearRaisedEvents(this ChangeTracker changeTracker)
    {
        var modifiedEntities = changeTracker
            .Entities()
            .ToList();

        var events = modifiedEntities
            .SelectMany(entity => entity.RaisedEvents)
            .ToArray();

        modifiedEntities
            .ForEach(entity => entity.ClearEvents());

        return events;
    }

    private static IEnumerable<Entity> Entities(this ChangeTracker changeTracker) =>
        changeTracker
            .Entries()
            .Where(entityEntry => entityEntry.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .Select(entityEntry => entityEntry.Entity)
            .OfType<Entity>();
}