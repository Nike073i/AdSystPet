namespace AdSyst.Moderation.BusinessLayer.Models
{
    public record CreateCorrectionNoteDto(
        string Title,
        string? Description,
        Guid ModeratorId,
        Guid AdvertismentId
    );
}
