using ErrorOr;
using MediatR;

namespace AdSyst.Moderation.Application.CorrectionNotes.Queries.GetAdvertismentCorrectionNotes
{
    public record GetAdvertismentCorrectionNotesQuery(Guid AdvertismentId)
        : IRequest<ErrorOr<GetAdvertismentCorrectionNotesResponse>>;
}
