namespace AdSyst.Moderation.Application.CorrectionNotes.Queries.GetAdvertismentCorrectionNotesForModeration
{
    public record CorrectionNoteDto(
        string Id,
        string Title,
        string? Description,
        Guid ModeratorId,
        Guid AdvertismentId,
        bool IsActive,
        DateTimeOffset CreatedAt
    );
}
