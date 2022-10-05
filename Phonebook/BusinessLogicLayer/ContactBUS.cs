﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Phonebook.DataAccessLayer;

namespace Phonebook.BusinessLogicLayer
{
    internal class ContactBUS
    {
        ContactDAO _contactDAO = ContactDAO.Instance;
        private Contact selectedContact;
        public ContactBUS() { }

        public void AddNewContact(Contact contact)
        {
            try
            {
                _contactDAO.ContactsList.Add(contact);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool RemoveContact(Contact contact)
        {
            if (!_contactDAO.ContactsList.Contains(contact))
                return false;
            try
            {
                _contactDAO.ContactsList.Remove(contact);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void fillSelectedContact(Contact contact)
        {
            selectedContact = contact;
        }
        public void EditContact(Contact contact)
        {
            if (!ValidateContact(contact))
                return;

        }
        public bool ValidateContact(Contact contact)
        {
            //determine if all of contacts properties are null/empty
            bool isValid = contact.GetType().GetProperties()
            .Where(pi => pi.PropertyType == typeof(string))
            .Select(pi => (string)pi.GetValue(contact))
            .All(value => string.IsNullOrEmpty(value));
            if (isValid == false)
            {
                ErrorWindow("All properties are empty!");
                return false;
            }
            //validating email adress
            if (!new EmailAddressAttribute().IsValid(contact.Email))
            {
                ErrorWindow("Email address has wrong format!");
                return false;
            }
            //Validate phone number
            if (!Regex.IsMatch(contact.PhoneNumber, "^?[+]([0-9]{3})?([0-9]{3})?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$"))
            {
                ErrorWindow("Phone number has wrong format!");
                return false;
            }

            return true;
        }
        public void ErrorWindow(string errorMsg)
        {
            MessageBox.Show("Chyba", errorMsg, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
