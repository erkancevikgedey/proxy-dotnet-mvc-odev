using web_proje_loginli.Data;
using web_proje_loginli.EFModels;

namespace web_proje_loginli.Services
{
    public class MessageDetailsService : IMessageDetailsService
    {
        private readonly ApplicationDbContext veriContext;

        public MessageDetailsService(ApplicationDbContext veriContext)
        {
            this.veriContext = veriContext;
        }
        public List<ContactMessage> GetAllMessages()
        {
            return veriContext.ContactMessage.ToList();
        }

        public ContactMessage GetMessage(int id)
        {
            return veriContext.ContactMessage.Where(x => x.MessageId == id).FirstOrDefault();
        }

        public bool AddMessage(string email, string mesaj)
        {
            ContactMessage eklenecek = new ContactMessage()
            {
                Email = email,
                Message = mesaj,
                Time = DateTime.Now,
            };

            try
            {
                veriContext.ContactMessage.Add(eklenecek);
                veriContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveMessage(int id)
        {
            var silinecek = this.GetMessage(id);
            if (silinecek != null)
            {
                try
                {
                    veriContext.Remove(silinecek);
                    veriContext.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
