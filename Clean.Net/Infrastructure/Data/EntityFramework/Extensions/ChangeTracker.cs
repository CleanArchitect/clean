using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Clean.Net;

internal static class ChangeTrackerExtensions
{
    public static IEvent[] GetRaisedEvents(this ChangeTracker changeTracker) =>
        changeTracker
            .Entries()
            .Where(entityEntry => entityEntry.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .Select(entityEntry => entityEntry.Entity)
            .OfType<Entity>()
            .SelectMany(entity => entity.RaisedEvents)
            .ToArray();
}