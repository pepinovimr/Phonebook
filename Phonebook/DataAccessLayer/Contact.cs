
using System;

namespace Phonebook.DataAccessLayer
{
    internal class Contact : IEquatable<Contact>   //VO - Value Object|| Needs to implement IEquitable and define Equals so that the Contains method works
    {
        public Contact() { }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }          //string bcs apparently we have like 38 thousand new genders now..:)
        public string Email { get; set; }
        public string Phone { get; set; }

        public bool Equals(Contact otherContact)
        {
            if (this.FirstName.Equals(otherContact.FirstName) && this.LastName.Equals(otherContact.LastName)
                && this.Gender.Equals(otherContact.Gender) && this.Email.Equals(otherContact.Email)
                && this.Phone.Equals(otherContact.Phone))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
