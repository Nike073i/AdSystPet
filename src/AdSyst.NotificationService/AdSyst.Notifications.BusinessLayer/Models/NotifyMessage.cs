namespace AdSyst.Notifications.BusinessLayer.Models
{
    public record NotifyMessage(string Subject, string Text, bool IsHtml = true);
}
