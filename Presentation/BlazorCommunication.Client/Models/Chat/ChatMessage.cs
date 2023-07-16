namespace BlazorCommunication.Client.Models.Chat;

public class ChatMessageModel
{
    public string Message { get; set; }
    public bool IsSender { get; set; }
    public DateTime Time { get; set; }
    
    public ChatUserModel Sender { get; set; }
}