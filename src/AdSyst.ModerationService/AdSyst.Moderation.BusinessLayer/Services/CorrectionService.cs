using ErrorOr;
using MongoDB.Bson;
using AdSyst.Moderation.BusinessLayer.Interfaces;
using AdSyst.Moderation.BusinessLayer.Models;
using AdSyst.Moderation.DAL.MongoDb.Errors;
using AdSyst.Moderation.DAL.MongoDb.Interfaces;
using AdSyst.Moderation.DAL.MongoDb.Models;

namespace AdSyst.Moderation.BusinessLayer.Services
{
    public class CorrectionService : ICorrectionService
    {
        private readonly IAdvertismentRepository _advertisments;

        public CorrectionService(IAdvertismentRepository correctionNotes)
        {
            _advertisments = correctionNotes;
        }

        public async Task<ErrorOr<Created>> CreateAdvertismentAsync(
            Guid advertismentId,
            Guid advertismentAuthorId,
            CancellationToken cancellationToken
        )
        {
            var advertisment = await _advertisments.GetByIdAsync(advertismentId, cancellationToken);
            if (advertisment != null)
                return AdvertismentErrors.AlreadyExists;

            advertisment = new Advertisment(advertismentId, advertismentAuthorId);
            await _advertisments.CreateAsync(advertisment, cancellationToken);
            return Result.Created;
        }

        public async Task<ErrorOr<Advertisment>> GetAdvertismentAsync(
            Guid advertismentId,
            CancellationToken cancellationToken
        )
        {
            var advertisment = await _advertisments.GetByIdAsync(advertismentId, cancellationToken);
            return advertisment is null ? AdvertismentErrors.NotFound : advertisment;
        }

        public async Task<ErrorOr<string>> AddCorrectionNoteAsync(
            CreateCorrectionNoteDto createDto,
            CancellationToken cancellationToken
        )
        {
            var advertisment = await _advertisments.GetByIdAsync(
                createDto.AdvertismentId,
                cancellationToken
            );
            if (advertisment is null)
                return AdvertismentErrors.NotFound;

            var correctionNote = new CorrectionNote(createDto.Title, createDto.ModeratorId)
            {
                Description = createDto.Description
            };

            advertisment.CorrectionNotes.Add(correctionNote);

            await _advertisments.UpdateAsync(advertisment, cancellationToken);
            return correctionNote.Id.ToString();
        }

        public async Task<ErrorOr<Success>> DisableCorrectionNoteAsync(
            Guid advertismentId,
            string noteId,
            CancellationToken cancellationToken
        )
        {
            var advertisment = await _advertisments.GetByIdAsync(advertismentId, cancellationToken);
            if (advertisment is null)
                return AdvertismentErrors.NotFound;

            var correctionNote = advertisment
                .CorrectionNotes
                .FirstOrDefault(x => x.Id == new ObjectId(noteId));

            if (correctionNote is null)
                return AdvertismentErrors.NoteNotFound;

            if (!correctionNote.IsActive)
                return AdvertismentErrors.NoteAlreadyDisabled;

            correctionNote.IsActive = false;
            await _advertisments.UpdateAsync(advertisment, cancellationToken);
            return Result.Success;
        }
    }
}
