using t4mvc.core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace t4mvc.data.Services
{
    public partial interface IProjectLogService
    {
        ProjectLog Find(params object[] keyValues);
        IQueryable<ProjectLog> GetAllProjectLogs();
        void CreateProjectLog(ProjectLog projectLog);
        void UpdateProjectLog(ProjectLog projectLog, IEnumerable<string> ignore);
		void DeleteProjectLog(ProjectLog projectLog);
    }

    public partial class ProjectLogService : IProjectLogService
    {
        private readonly t4DbContext context;
        public ProjectLogService(t4DbContext dbContext)
        {
            this.context = dbContext;
        }

        public void CreateProjectLog(ProjectLog projectLog)
        {
            // Auditing 
            var auditRecord     = projectLog.GetCreateAuditRecord(projectLog.ProjectLogId, projectLog.CreateUserId);
            this.context.AuditRecord.Add(auditRecord); 

            this.context.ProjectLogs.Add(projectLog);
        }

        public ProjectLog Find(params object[] keyValues)
        {
            return this.context.ProjectLogs.Find(keyValues);
        }

        public IQueryable<ProjectLog> GetAllProjectLogs()
        {
            return this.context.ProjectLogs.AsQueryable();
        }

        public void UpdateProjectLog(ProjectLog projectLog, IEnumerable<string> ignore)
        {
            // Auditing 
            var oldRecord       = this.context.ProjectLogs.AsNoTracking().Single(x => x.ProjectLogId == projectLog.ProjectLogId);
            var auditRecord     = oldRecord.GetUpdateAuditRecord(projectLog, ignore);
            this.context.AuditRecord.Add(auditRecord); 

            this.context.ProjectLogs.Attach(projectLog);

            var entry       = this.context.Entry(projectLog);
            entry.State     = EntityState.Modified;

            foreach(var prop in ignore)
            {
                entry.Property(prop).IsModified = false;
            }
        }

		public void DeleteProjectLog(ProjectLog projectLog)
        {
            // Auditing 
            var oldRecord       = this.context.ProjectLogs.AsNoTracking().Single(x => x.ProjectLogId == projectLog.ProjectLogId);
            var auditRecord     = oldRecord.GetDeleteAuditRecord(projectLog.ProjectLogId, projectLog.ModifyUserId);
            this.context.AuditRecord.Add(auditRecord); 

            this.context.ProjectLogs.Remove(projectLog);
        }
    }

}