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
    public partial interface IAccountViewModelService
    {
	    IQueryable<AccountViewModel> GetAllAccounts();
        AccountViewModel Find(Guid accountId);
        void CreateAccount(AccountViewModel accountViewModel);
        void SaveAccount(AccountViewModel accountViewModel);
		void DeleteAccount(AccountViewModel accountViewModel);
		void Hydrate(AccountViewModel accountViewModel);
        List<ContactViewModel> GetContacts(Guid accountId);
        List<ProjectViewModel> GetProjects(Guid accountId);
        List<NoteViewModel> GetNotes(Guid accountId);

    }
    public partial class AccountViewModelService : IAccountViewModelService
    {
        private readonly IAccountService accountService;
        private readonly IContactViewModelService contactViewModelService;
        private readonly IProjectViewModelService projectViewModelService;
        private readonly INoteViewModelService noteViewModelService;
        private readonly IContextHelper contextHelper;
        private readonly IUserService userService;
        private readonly IAuditService auditService;
        
        public IQueryable<AccountViewModel> GetAllAccounts()
        {
		    var query = (from account in accountService.GetAllAccounts()
                         			             select new AccountViewModel
						 {							AccountId = account.AccountId,
						 							Name = account.Name,
						 							Address = account.Address,
						 							Address2 = account.Address2,
						 							City = account.City,
						 							State = account.State,
						 							Zip = account.Zip,
						 							Phone = account.Phone,
						 							Fax = account.Fax,
						 							Website = account.Website,
						 							ParentAccountId = account.ParentAccountId,
						 							Lat = account.Lat,
						 							Lng = account.Lng,
						 							Description = account.Description,
						 							Active = account.Active,
						 });
            return query;
        }

        public AccountViewModelService(IAccountService accountService,IContactViewModelService contactViewModelService,IProjectViewModelService projectViewModelService,INoteViewModelService noteViewModelService, IUserService userService,
                                            IContextHelper contextHelper, IAuditService auditService )
        {
            this.accountService = accountService;
            this.contextHelper      = contextHelper;
            this.contactViewModelService = contactViewModelService;
            this.projectViewModelService = projectViewModelService;
            this.noteViewModelService = noteViewModelService;
            this.auditService = auditService;

        }

        public void CreateAccount(AccountViewModel accountViewModel)
        {
            var account = accountViewModel.Map<Account>();
            account.CreateDate = DateTime.Now;
            account.ModifyDate = DateTime.Now;
            account.CreateUserId = Current.UserId;
            account.ModifyUserId = Current.UserId;
            account.AccountId = Guid.NewGuid();

            accountService.CreateAccount(account);

            contextHelper.SaveChanges();

            // Set the new id back on the view so it can be called post-save
            accountViewModel.AccountId = account.AccountId;
        }

        public AccountViewModel Find(Guid accountId)
        {
            var account = accountService.Find(accountId)
                                                  .Map<AccountViewModel>();

            Hydrate(account);
            return account;
        }

        public void SaveAccount(AccountViewModel accountViewModel)
        {
            var account = accountViewModel.Map<Account>();
            account.ModifyDate = DateTime.Now;
            account.ModifyUserId = Current.UserId;

            var ignore = FieldMappingInspector<Account>.GetAllReadonlyFields(accountViewModel);

            accountService.UpdateAccount(account, ignore);

            contextHelper.SaveChanges();
        }

		public void DeleteAccount(AccountViewModel accountViewModel)
        {
            var account = accountViewModel.Map<Account>();

            accountService.DeleteAccount(account);

            contextHelper.SaveChanges();
        }

        public void Hydrate(AccountViewModel accountViewModel)
        {
            var id = accountViewModel.AccountId;
            accountViewModel.AuditHistory = GetAuditRecords(id);
            accountViewModel.Contacts=     GetContacts(id);
            accountViewModel.Projects=     GetProjects(id);
            accountViewModel.Notes=     GetNotes(id);
        }

        public List<ContactViewModel> GetContacts(Guid accountId)
        {
            return contactViewModelService.GetAllContacts()
                        .Where(x => x.AccountId == accountId)
                        .ToList();
        }
        public List<ProjectViewModel> GetProjects(Guid accountId)
        {
            return projectViewModelService.GetAllProjects()
                        .Where(x => x.AccountId == accountId)
                        .ToList();
        }
        public List<NoteViewModel> GetNotes(Guid accountId)
        {
            return noteViewModelService.GetAllNotes()
                        .Where(x => x.AccountId == accountId)
                        .ToList();
        }
        public List<AuditRecord> GetAuditRecords(Guid accountId)
        {
            return auditService.GetAuditRecords()
                               .Where(x => x.RecordId == accountId && x.RecordType == "Account")
                               .ToList();
        }

    }
}
