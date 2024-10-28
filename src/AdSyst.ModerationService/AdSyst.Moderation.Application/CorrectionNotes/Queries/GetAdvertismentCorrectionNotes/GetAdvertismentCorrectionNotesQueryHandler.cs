using ErrorOr;
using MediatR;
using MongoDB.Driver;
using AdSyst.Common.Application.Abstractions.Authentication;
using AdSyst.Moderation.DAL.MongoDb.Errors;
using AdSyst.Moderation.DAL.MongoDb.Models;

namespace AdSyst.Moderation.Application.CorrectionNotes.Queries.GetAdvertismentCorrectionNotes
{
    public class GetAdvertismentCorrectionNotesQueryHandler
        : IRequestHandler<
            GetAdvertismentCorrectionNotesQuery,
            ErrorOr<GetAdvertismentCorrectionNotesResponse>
        >
    {
        private readonly IMongoCollection<Advertisment> _advertisments;
        private readonly IUserContext _userContext;

        public GetAdvertismentCorrectionNotesQueryHandler(
            IMongoCollection<Advertisment> advertisments,
            IUserContext userContext
        )
        {
            _advertisments = advertisments;
            _userContext = userContext;
        }

        public async Task<ErrorOr<GetAdvertismentCorrectionNotesResponse>> Handle(
            GetAdvertismentCorrectionNotesQuery request,
            CancellationToken cancellationToken
        )
        {
            if (_userContext.UserId == null)
                return AdvertismentErrors.Unauthorized;

            var advertisment = await _advertisments
                .Find(ad => ad.AdvertismentId == request.AdvertismentId)
                .FirstOrDefaultAsync(cancellationToken);

            if (advertisment is null)
                return AdvertismentErrors.NotFound;

            if (advertisment.AdvertismentAuthorId != _userContext.UserId)
                return AdvertismentErrors.Forbidden;

            bool Filter(CorrectionNote note)
            {
                return note.IsActive;
            }

            CorrectionNoteDto Projection(CorrectionNote note)
            {
                return new CorrectionNoteDto(
                    note.Id.ToString(),
                    note.Title,
                    note.Description,
                    note.CreatedAt
                );
            }

            var notes = advertisment.CorrectionNotes.Where(Filter).Select(Projection).ToList();
            return new GetAdvertismentCorrectionNotesResponse(advertisment.AdvertismentId, notes);
        }
    }
}
