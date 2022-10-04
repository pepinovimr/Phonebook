
using System.Net.Mail;

namespace Phonebook.DataAccessLayer
{
    internal class Contact    //VO - Value Object
    {
        public Contact() { }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }          //string bcs apparently we have like 38 thousand new genders now..:)
        public MailAddress Email { get; set; }      //might be better than string, also provides some variety
        public string PhoneNumber { get; set; }
    }
}
