using AutoMapper;
using t4mvc.core;
using t4mvc.data.Services;
using t4mvc.web.core;
using t4mvc.web.core.Infrastructure;
using t4mvc.web.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace t4mvc.web.core.ViewModelServices
{
    public partial interface IContactViewModelService
    {
	    IQueryable<ContactViewModel> GetAllContacts();
        ContactViewModel Find(Guid contactId);
        void CreateContact(ContactViewModel contactViewModel);
        void SaveContact(ContactViewModel contactViewModel);
		void DeleteContact(ContactViewModel contactViewModel);
		void Hydrate(ContactViewModel contactViewModel);

        List<ProjectViewModel> GetProjects(Guid contactId);
        List<NoteViewModel> GetNotes(Guid contactId);

    }
    public partial class ContactViewModelService : IContactViewModelService
    {

        private readonly IContactService contactService;
        private readonly IAccountService accountService;
        private readonly IProjectViewModelService projectViewModelService;
        private readonly INoteViewModelService noteViewModelService;
        private readonly IContextHelper contextHelper;
        private readonly IUserService userService;

        public IQueryable<ContactViewModel> GetAllContacts()
        {
		    var query = (from contact in contactService.GetAllContacts()
                         
						 join account_AccountId in accountService.GetAllAccounts() on contact.AccountId equals account_AccountId.AccountId into left_tmp_account_AccountId
						 from left_account_AccountId in left_tmp_account_AccountId.DefaultIfEmpty()
						 
			             select new ContactViewModel
						 {
							ContactId = contact.ContactId,						 
							FirstName = contact.FirstName,						 
							LastName = contact.LastName,						 
							AccountId = contact.AccountId,						 
							MiddleName = contact.MiddleName,						 
							Prefix = contact.Prefix,						 
							Suffix = contact.Suffix,						 
							EmailAddress = contact.EmailAddress,						 
							JobTitle = contact.JobTitle,						 
							Phone = contact.Phone,						 
							Fax = contact.Fax,						 
							Mobile = contact.Mobile,						 
							Address = contact.Address,						 
							Address2 = contact.Address2,						 
							City = contact.City,						 
							State = contact.State,						 
							Zip = contact.Zip,						 
							Active = contact.Active,						 
                            AccountIdName = left_account_AccountId.Name, });
            return query;
        }

        public ContactViewModelService(IContactService contactService,IAccountService accountService,IProjectViewModelService projectViewModelService,INoteViewModelService noteViewModelService, IUserService userService,
                                            IContextHelper contextHelper)
        {
            this.contactService = contactService;
            this.contextHelper      = contextHelper;

            this.accountService = accountService;

            this.projectViewModelService = projectViewModelService;
            this.noteViewModelService = noteViewModelService;
        }

        public void CreateContact(ContactViewModel contactViewModel)
        {
            var contact = contactViewModel.Map<Contact>();
            contact.CreateDate = DateTime.Now;
            contact.ModifyDate = DateTime.Now;
            contact.CreateUserId = Current.UserId;
            contact.ModifyUserId = Current.UserId;
            contact.ContactId = Guid.NewGuid();

            contactService.CreateContact(contact);

            contextHelper.SaveChanges();

            // Set the new id back on the view so it can be called post-save
            contactViewModel.ContactId = contact.ContactId;
        }

        public ContactViewModel Find(Guid contactId)
        {
            var contact = contactService.Find(contactId)
                                                  .Map<ContactViewModel>();


            if (contact.AccountId != null)
                contact.AccountIdName = accountService.Find(contact.AccountId)?.Name;
            Hydrate(contact);
            return contact;
        }

        public void SaveContact(ContactViewModel contactViewModel)
        {
            var contact = contactViewModel.Map<Contact>();
            contact.ModifyDate = DateTime.Now;
            contact.ModifyUserId = Current.UserId;

            var ignore = FieldMappingInspector<Contact>.GetAllReadonlyFields(contactViewModel);

            contactService.UpdateContact(contact, ignore);

            contextHelper.SaveChanges();
        }

		public void DeleteContact(ContactViewModel contactViewModel)
        {
            var contact = contactViewModel.Map<Contact>();

            contactService.DeleteContact(contact);

            contextHelper.SaveChanges();
        }

        public string GetAccountIdName(Guid? id)
        {
			return accountService.Find(id)?.Name;
        }

        public void Hydrate(ContactViewModel contactViewModel)
        {
            var id = contactViewModel.ContactId;
            contactViewModel.AccountIdName =     GetAccountIdName(contactViewModel.AccountId);
            contactViewModel.Projects=     GetProjects(id);
            contactViewModel.Notes=     GetNotes(id);
        
        }


        public List<ProjectViewModel> GetProjects(Guid contactId)
        {
            return projectViewModelService.GetAllProjects()
                        .Where(x => x.PrimaryContactId == contactId)
                        .ToList();
        }

        public List<NoteViewModel> GetNotes(Guid contactId)
        {
            return noteViewModelService.GetAllNotes()
                        .Where(x => x.ContactId == contactId)
                        .ToList();
        }

    }
}
