using AdSyst.Advertisments.Domain.Abstractions;

namespace AdSyst.Advertisments.Domain.Tests.Infrastructure
{
    public abstract class BaseTest
    {
        public static T? GetDomainEvent<T>(IHasEvents entity)
            where T : IDomainEvent => entity.Events.OfType<T>().FirstOrDefault();

        public static T ShouldBeDomainEvent<T>(IHasEvents entity)
            where T : IDomainEvent =>
            GetDomainEvent<T>(entity)
            ?? throw new ShouldAssertException($"{typeof(T).Name} was not published");
    }
}
