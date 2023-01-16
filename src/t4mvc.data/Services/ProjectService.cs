using t4mvc.core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace t4mvc.data.Services
{
    public partial interface IProjectService
    {
        Project Find(params object[] keyValues);
        IQueryable<Project> GetAllProjects();
        void CreateProject(Project project);
        void UpdateProject(Project project, IEnumerable<string> ignore);
		void DeleteProject(Project project);
    }

    public partial class ProjectService : IProjectService
    {
        private readonly t4DbContext context;
        public ProjectService(t4DbContext dbContext)
        {
            this.context = dbContext;
        }

        public void CreateProject(Project project)
        {
            this.context.Projects.Add(project);
        }

        public Project Find(params object[] keyValues)
        {
            return this.context.Projects.Find(keyValues);
        }

        public IQueryable<Project> GetAllProjects()
        {
            return this.context.Projects.AsQueryable();
        }

        public void UpdateProject(Project project, IEnumerable<string> ignore)
        {
            this.context.Projects.Attach(project);

            var entry       = this.context.Entry(project);
            entry.State     = EntityState.Modified;

            foreach(var prop in ignore)
            {
                entry.Property(prop).IsModified = false;
            }
        }

		public void DeleteProject(Project project)
        {
            this.context.Projects.Remove(project);
        }
    }

}