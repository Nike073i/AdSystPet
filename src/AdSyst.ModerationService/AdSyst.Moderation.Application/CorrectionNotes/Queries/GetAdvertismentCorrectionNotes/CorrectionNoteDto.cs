namespace AdSyst.Moderation.Application.CorrectionNotes.Queries.GetAdvertismentCorrectionNotes
{
    public record CorrectionNoteDto(
        string Id,
        string Title,
        string? Description,
        DateTimeOffset CreatedAt
    );
}
