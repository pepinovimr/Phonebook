using Phonebook.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Phonebook.BusinessLogicLayer
{
    internal class ContactValidator
    {
        private readonly Contact contactToValidate;
        public ContactValidator(Contact contactToValidate)
        {
            this.contactToValidate = contactToValidate;
        }
        public bool isContactEmpty()
        {
            var properties = contactToValidate.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (string.IsNullOrEmpty((string)property.GetValue(contactToValidate)))
                    return true;
            }
            return false;
        }
        public bool isEmailValid()
        {
            return new EmailAddressAttribute().IsValid(contactToValidate.Email) ? true : false;
        }
        public bool isPhoneValid()
        {
            return Regex.IsMatch(contactToValidate.Phone, "^?([+][0-9]{3})?([0-9]{3})?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$") ? true : false;
        }
    }
}
