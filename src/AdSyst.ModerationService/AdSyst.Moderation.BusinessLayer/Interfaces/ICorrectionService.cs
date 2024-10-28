using ErrorOr;
using AdSyst.Moderation.BusinessLayer.Models;
using AdSyst.Moderation.DAL.MongoDb.Models;

namespace AdSyst.Moderation.BusinessLayer.Interfaces
{
    public interface ICorrectionService
    {
        Task<ErrorOr<Advertisment>> GetAdvertismentAsync(
            Guid advertismentId,
            CancellationToken cancellationToken = default
        );

        Task<ErrorOr<Created>> CreateAdvertismentAsync(
            Guid advertismentId,
            Guid advertismentAuthorId,
            CancellationToken cancellationToken = default
        );

        Task<ErrorOr<string>> AddCorrectionNoteAsync(
            CreateCorrectionNoteDto createDto,
            CancellationToken cancellationToken = default
        );

        Task<ErrorOr<Success>> DisableCorrectionNoteAsync(
            Guid advertismentId,
            string noteId,
            CancellationToken cancellationToken = default
        );
    }
}
