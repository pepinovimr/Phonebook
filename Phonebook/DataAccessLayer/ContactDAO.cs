using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Phonebook.DataAccessLayer
{
    internal class ContactDAO
    {
        //Singleton - kinda fits here bcs we dont want more instances of List, also wanted to try it...
        private ContactDAO() 
        { 
            var list = DeserializeList();  
            if(list != null) 
                ContactsList.AddRange(list);
        }
        private static ContactDAO instance = null;
        public static ContactDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContactDAO();
                }
                return instance;
            }
        }
        //Got the Json from: https://www.mockaroo.com/
        private readonly string path = 
            Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, "Resources", "FakePeople.json");
        public List<Contact> ContactsList = new List<Contact>();

        private List<Contact> DeserializeList()
        {
            try
            {
                var deserializedList = JsonConvert.DeserializeObject<List<Contact>>(File.ReadAllText(path));
                return deserializedList;
            }
            catch(Exception e)
            {
                throw new Exception("Chyba při deserializaci" + e.Message);
            }

        }
        public void SerializeList(List<Contact> list)
        {
            try
            {
                string output = JsonConvert.SerializeObject(list, Formatting.Indented);
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.Write(output);
                };
            }
            catch (Exception e)
            {
                throw new Exception("Chyba při serializaci, zapisování" + e.Message);
            }
        }
    }
}
