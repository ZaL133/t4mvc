using t4mvc.core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace t4mvc.data.Services
{
    public partial interface IContactService
    {
        Contact Find(params object[] keyValues);
        IQueryable<Contact> GetAllContacts();
        void CreateContact(Contact contact);
        void UpdateContact(Contact contact, IEnumerable<string> ignore);
		void DeleteContact(Contact contact);
    }

    public partial class ContactService : IContactService
    {
        private readonly t4DbContext context;
        public ContactService(t4DbContext dbContext)
        {
            this.context = dbContext;
        }

        public void CreateContact(Contact contact)
        {

            this.context.Contacts.Add(contact);
        }

        public Contact Find(params object[] keyValues)
        {
            return this.context.Contacts.Find(keyValues);
        }

        public IQueryable<Contact> GetAllContacts()
        {
            return this.context.Contacts.AsQueryable();
        }

        public void UpdateContact(Contact contact, IEnumerable<string> ignore)
        {

            this.context.Contacts.Attach(contact);

            var entry       = this.context.Entry(contact);
            entry.State     = EntityState.Modified;

            foreach(var prop in ignore)
            {
                entry.Property(prop).IsModified = false;
            }
        }

		public void DeleteContact(Contact contact)
        {

            this.context.Contacts.Remove(contact);
        }
    }

}