using ErrorOr;
using MediatR;
using MongoDB.Driver;
using AdSyst.Moderation.DAL.MongoDb.Errors;
using AdSyst.Moderation.DAL.MongoDb.Models;

namespace AdSyst.Moderation.Application.CorrectionNotes.Queries.GetAdvertismentCorrectionNotesForModeration
{
    public class GetAdvertismentCorrectionNotesQueryHandler
        : IRequestHandler<
            GetAdvertismentCorrectionNotesQuery,
            ErrorOr<GetAdvertismentCorrectionNotesResponse>
        >
    {
        private readonly IMongoCollection<Advertisment> _advertisments;

        public GetAdvertismentCorrectionNotesQueryHandler(
            IMongoCollection<Advertisment> advertisments
        )
        {
            _advertisments = advertisments;
        }

        public async Task<ErrorOr<GetAdvertismentCorrectionNotesResponse>> Handle(
            GetAdvertismentCorrectionNotesQuery request,
            CancellationToken cancellationToken
        )
        {
            var advertisment = await _advertisments
                .Find(ad => ad.AdvertismentId == request.AdvertismentId)
                .FirstOrDefaultAsync(cancellationToken);

            if (advertisment is null)
                return AdvertismentErrors.NotFound;

            CorrectionNoteDto Projection(CorrectionNote note)
            {
                return new CorrectionNoteDto(
                    note.Id.ToString(),
                    note.Title,
                    note.Description,
                    note.ModeratorId,
                    advertisment.AdvertismentId,
                    note.IsActive,
                    note.CreatedAt
                );
            }

            var notes = advertisment.CorrectionNotes.Select(Projection).ToList();
            return new GetAdvertismentCorrectionNotesResponse(advertisment.AdvertismentId, notes);
        }
    }
}
