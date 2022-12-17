using t4mvc.core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace t4mvc.data.Services
{
    public partial interface INoteService
    {
        Note Find(params object[] keyValues);
        IQueryable<Note> GetAllNotes();
        void CreateNote(Note note);
        void UpdateNote(Note note, IEnumerable<string> ignore);
		void DeleteNote(Note note);
    }

    public partial class NoteService : INoteService
    {
        private readonly t4DbContext context;
        public NoteService(t4DbContext dbContext)
        {
            this.context = dbContext;
        }

        public void CreateNote(Note note)
        {
            this.context.Notes.Add(note);
        }

        public Note Find(params object[] keyValues)
        {
            return this.context.Notes.Find(keyValues);
        }

        public IQueryable<Note> GetAllNotes()
        {
            return this.context.Notes.AsQueryable();
        }

        public void UpdateNote(Note note, IEnumerable<string> ignore)
        {
            this.context.Notes.Attach(note);

            var entry       = this.context.Entry(note);
            entry.State     = EntityState.Modified;

            foreach(var prop in ignore)
            {
                entry.Property(prop).IsModified = false;
            }
        }

		public void DeleteNote(Note note)
        {
            this.context.Notes.Remove(note);
        }
    }

}