namespace AdSyst.Moderation.Application.CorrectionNotes.Queries.GetAdvertismentCorrectionNotes
{
    public record GetAdvertismentCorrectionNotesResponse(
        Guid AdvertisementId,
        IReadOnlyCollection<CorrectionNoteDto> CorrectionNotes
    );
}
