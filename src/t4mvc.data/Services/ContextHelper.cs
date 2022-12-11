using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t4mvc.data.services
{
    public interface IContextHelper
    {
        void SaveChanges();
        void SaveAndCommit();
        void RollbackTransaction();
        void Execute(string command, params SqlParameter[] parameters);
        void BeginTransaction();
    }

    public class ContextHelper : IContextHelper
    {
        private readonly t4DbContext context;
        private IDbContextTransaction transaction;

        public ContextHelper(t4DbContext dbContext)
        {
            this.context = dbContext;
        }

        public void BeginTransaction()
        {
            if (transaction != null || this.context.Database.CurrentTransaction != null) throw new InvalidOperationException("Cannot begin another transaction");

            transaction = this.context.Database.BeginTransaction();
        }
        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
        public void SaveAndCommit()
        {
            SaveChanges();
            if (transaction != null)
            {
                transaction.Commit();
                transaction.Dispose();
                transaction = null;
            }
            if (this.context.Database.CurrentTransaction != null)
            {
                this.context.Database.CurrentTransaction.Commit();
                this.context.Database.CurrentTransaction.Dispose();
            }
        }
        public void RollbackTransaction()
        {
            if (this.context.Database.CurrentTransaction != null)
            {
                this.context.Database.CurrentTransaction.Rollback();
                this.context.Database.CurrentTransaction.Dispose();
            }
        }

        public void Execute(string command, params SqlParameter[] parameters)
        {
            this.context.Database.ExecuteSqlRaw(command, parameters);
        }
    }
}
