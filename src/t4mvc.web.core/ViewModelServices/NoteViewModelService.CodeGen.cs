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
    public partial interface INoteViewModelService
    {
	    IQueryable<NoteViewModel> GetAllNotes();
        NoteViewModel Find(Guid noteId);
        void CreateNote(NoteViewModel noteViewModel);
        void SaveNote(NoteViewModel noteViewModel);
		void DeleteNote(NoteViewModel noteViewModel);
		void Hydrate(NoteViewModel noteViewModel);

    }
    public partial class NoteViewModelService : INoteViewModelService
    {
        private readonly INoteService noteService;
        private readonly IAccountService accountService;
        private readonly IContactService contactService;
        private readonly IContextHelper contextHelper;
        private readonly IUserService userService;

        public IQueryable<NoteViewModel> GetAllNotes()
        {
		    var query = (from note in noteService.GetAllNotes()
                         						 join account_AccountId in accountService.GetAllAccounts() on note.AccountId equals account_AccountId.AccountId into left_tmp_account_AccountId
						 from left_account_AccountId in left_tmp_account_AccountId.DefaultIfEmpty()
						 						 join contact_ContactId in contactService.GetAllContacts() on note.ContactId equals contact_ContactId.ContactId into left_tmp_contact_ContactId
						 from left_contact_ContactId in left_tmp_contact_ContactId.DefaultIfEmpty()
						 			             select new NoteViewModel
						 {							NoteId = note.NoteId,
						 							ModifyUserId = note.ModifyUserId,
						 							ModifyDate = note.ModifyDate,
						 							NoteText = note.NoteText,
						 							AccountId = note.AccountId,
						 							ContactId = note.ContactId,
						  AccountIdName = left_account_AccountId.Name,  ContactIdEmailAddress = left_contact_ContactId.EmailAddress, });
            return query;
        }

        public NoteViewModelService(INoteService noteService,IAccountService accountService,IContactService contactService, IUserService userService,
                                            IContextHelper contextHelper)
        {
            this.noteService = noteService;
            this.contextHelper      = contextHelper;
            this.accountService = accountService;
            this.contactService = contactService;
        }

        public void CreateNote(NoteViewModel noteViewModel)
        {
            var note = noteViewModel.Map<Note>();
            note.CreateDate = DateTime.Now;
            note.ModifyDate = DateTime.Now;
            note.CreateUserId = Current.UserId;
            note.ModifyUserId = Current.UserId;
            note.NoteId = Guid.NewGuid();

            noteService.CreateNote(note);

            contextHelper.SaveChanges();

            // Set the new id back on the view so it can be called post-save
            noteViewModel.NoteId = note.NoteId;
        }

        public NoteViewModel Find(Guid noteId)
        {
            var note = noteService.Find(noteId)
                                                  .Map<NoteViewModel>();

            if (note.AccountId != null)
                note.AccountIdName = accountService.Find(note.AccountId)?.Name;
            if (note.ContactId != null)
                note.ContactIdEmailAddress = contactService.Find(note.ContactId)?.EmailAddress;
            Hydrate(note);
            return note;
        }

        public void SaveNote(NoteViewModel noteViewModel)
        {
            var note = noteViewModel.Map<Note>();
            note.ModifyDate = DateTime.Now;
            note.ModifyUserId = Current.UserId;

            var ignore = FieldMappingInspector<Note>.GetAllReadonlyFields(noteViewModel);

            noteService.UpdateNote(note, ignore);

            contextHelper.SaveChanges();
        }

		public void DeleteNote(NoteViewModel noteViewModel)
        {
            var note = noteViewModel.Map<Note>();

            noteService.DeleteNote(note);

            contextHelper.SaveChanges();
        }
        public string GetAccountIdName(Guid? id)
        {
			return accountService.Find(id)?.Name;
        }
        public string GetContactIdEmailAddress(Guid? id)
        {
			return contactService.Find(id)?.EmailAddress;
        }

        public void Hydrate(NoteViewModel noteViewModel)
        {
            var id = noteViewModel.NoteId;
            noteViewModel.AccountIdName =     GetAccountIdName(noteViewModel.AccountId);
            noteViewModel.ContactIdEmailAddress =     GetContactIdEmailAddress(noteViewModel.ContactId);
        }


    }
}
