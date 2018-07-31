namespace DutchTreat.Services
{
    public interface IMailServices
    {
        void SendMessage(string to, string subject, string body);
    }
}