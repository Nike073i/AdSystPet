namespace AdSyst.Moderation.Application.CorrectionNotes.Queries.GetAdvertismentCorrectionNotesForModeration
{
    public record GetAdvertismentCorrectionNotesResponse(
        Guid AdvertisementId,
        IReadOnlyCollection<CorrectionNoteDto> CorrectionNotes
    );
}
