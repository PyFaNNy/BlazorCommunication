namespace BlazorCommunication.Client.Models.Chat;

public class ChatUserModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ImgURL { get; set; }
    
    public string LastMessage { get; set; }
    public int Notifications { get; set; }
}