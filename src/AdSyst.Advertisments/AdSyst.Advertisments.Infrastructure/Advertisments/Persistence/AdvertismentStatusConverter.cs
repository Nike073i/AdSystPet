using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using AdSyst.Advertisments.Domain.Advertisments;
using AdSyst.Advertisments.Domain.Advertisments.AdvertismentStates;

namespace AdSyst.Advertisments.Infrastructure.Advertisments.Persistence
{
    internal class AdvertismentStatusConverter : ValueConverter<IAdvertismentState, string>
    {
        public AdvertismentStatusConverter()
            : base(state => state.Status.ToString(), status => ConvertToState(status)) { }

        public static IAdvertismentState ConvertToState(AdvertismentStatus advertismentStatus) =>
            advertismentStatus switch
            {
                AdvertismentStatus.Active => new ActiveState(),
                AdvertismentStatus.Rejected => new RejectedState(),
                AdvertismentStatus.Moderation => new ModerationState(),
                AdvertismentStatus.Archival => new ArchivalState(),
                _ => throw new NotImplementedException(),
            };

        public static IAdvertismentState ConvertToState(string status)
        {
            bool result = Enum.TryParse(typeof(AdvertismentStatus), status, out object? value);
            return result
                ? ConvertToState((AdvertismentStatus)value!)
                : throw new InvalidCastException();
        }
    }
}
