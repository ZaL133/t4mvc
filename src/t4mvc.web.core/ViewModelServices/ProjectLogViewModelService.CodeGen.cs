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
    public partial interface IProjectLogViewModelService
    {
	    IQueryable<ProjectLogViewModel> GetAllProjectLogs();
        ProjectLogViewModel Find(Guid projectLogId);
        void CreateProjectLog(ProjectLogViewModel projectLogViewModel);
        void SaveProjectLog(ProjectLogViewModel projectLogViewModel);
		void DeleteProjectLog(ProjectLogViewModel projectLogViewModel);
		void Hydrate(ProjectLogViewModel projectLogViewModel);
        List<NoteViewModel> GetNotes(Guid projectLogId);

    }
    public partial class ProjectLogViewModelService : IProjectLogViewModelService
    {
        private readonly IProjectLogService projectLogService;
        private readonly IProjectService projectService;
        private readonly INoteViewModelService noteViewModelService;
        private readonly IContextHelper contextHelper;
        private readonly IUserService userService;
        private readonly IAuditService auditService;
        
        public IQueryable<ProjectLogViewModel> GetAllProjectLogs()
        {
		    var query = (from projectLog in projectLogService.GetAllProjectLogs()
                                                  join project_ProjectId in projectService.GetAllProjects() on projectLog.ProjectId equals project_ProjectId.ProjectId
                         			             select new ProjectLogViewModel
						 {							ProjectLogId = projectLog.ProjectLogId,
						 							ProjectId = projectLog.ProjectId,
						 							EntryName = projectLog.EntryName,
						 							EntryDate = projectLog.EntryDate,
						 							Hours = projectLog.Hours,
						  ProjectIdProjectName = project_ProjectId.ProjectName, });
            return query;
        }

        public ProjectLogViewModelService(IProjectLogService projectLogService,IProjectService projectService,INoteViewModelService noteViewModelService, IUserService userService,
                                            IContextHelper contextHelper, IAuditService auditService )
        {
            this.projectLogService = projectLogService;
            this.contextHelper      = contextHelper;
            this.projectService = projectService;
            this.noteViewModelService = noteViewModelService;
            this.auditService = auditService;

        }

        public void CreateProjectLog(ProjectLogViewModel projectLogViewModel)
        {
            var projectLog = projectLogViewModel.Map<ProjectLog>();
            projectLog.CreateDate = DateTime.Now;
            projectLog.ModifyDate = DateTime.Now;
            projectLog.CreateUserId = Current.UserId;
            projectLog.ModifyUserId = Current.UserId;
            projectLog.ProjectLogId = Guid.NewGuid();

            projectLogService.CreateProjectLog(projectLog);

            contextHelper.SaveChanges();

            // Set the new id back on the view so it can be called post-save
            projectLogViewModel.ProjectLogId = projectLog.ProjectLogId;
        }

        public ProjectLogViewModel Find(Guid projectLogId)
        {
            var projectLog = projectLogService.Find(projectLogId)
                                                  .Map<ProjectLogViewModel>();

            if (projectLog.ProjectId != null)
                projectLog.ProjectIdProjectName = projectService.Find(projectLog.ProjectId)?.ProjectName;
            Hydrate(projectLog);
            return projectLog;
        }

        public void SaveProjectLog(ProjectLogViewModel projectLogViewModel)
        {
            var projectLog = projectLogViewModel.Map<ProjectLog>();
            projectLog.ModifyDate = DateTime.Now;
            projectLog.ModifyUserId = Current.UserId;

            var ignore = FieldMappingInspector<ProjectLog>.GetAllReadonlyFields(projectLogViewModel);

            projectLogService.UpdateProjectLog(projectLog, ignore);

            contextHelper.SaveChanges();
        }

		public void DeleteProjectLog(ProjectLogViewModel projectLogViewModel)
        {
            var projectLog = projectLogViewModel.Map<ProjectLog>();

            projectLogService.DeleteProjectLog(projectLog);

            contextHelper.SaveChanges();
        }
        public string GetProjectIdProjectName(Guid? id)
        {
			return projectService.Find(id)?.ProjectName;
        }

        public void Hydrate(ProjectLogViewModel projectLogViewModel)
        {
            var id = projectLogViewModel.ProjectLogId;
            projectLogViewModel.AuditHistory = GetAuditRecords(id);
            projectLogViewModel.ProjectIdProjectName =     GetProjectIdProjectName(projectLogViewModel.ProjectId);
            projectLogViewModel.Notes=     GetNotes(id);
        }

        public List<NoteViewModel> GetNotes(Guid projectLogId)
        {
            return noteViewModelService.GetAllNotes()
                        .Where(x => x.ProjectLogId == projectLogId)
                        .ToList();
        }
        public List<AuditRecord> GetAuditRecords(Guid projectLogId)
        {
            return auditService.GetAuditRecords()
                               .Where(x => x.RecordId == projectLogId && x.RecordType == "ProjectLog")
                               .ToList();
        }

    }
}
