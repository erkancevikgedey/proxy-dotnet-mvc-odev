using web_proje_loginli.EFModels;

namespace web_proje_loginli.Services
{
    public interface IMessageDetailsService
    {
        bool AddMessage(string email, string mesaj);
        List<ContactMessage> GetAllMessages();
        ContactMessage GetMessage(int id);
        bool RemoveMessage(int id);
    }
}