using ErrorOr;
using MediatR;

namespace AdSyst.Moderation.Application.CorrectionNotes.Queries.GetAdvertismentCorrectionNotesForModeration
{
    public record GetAdvertismentCorrectionNotesQuery(Guid AdvertismentId)
        : IRequest<ErrorOr<GetAdvertismentCorrectionNotesResponse>>;
}
