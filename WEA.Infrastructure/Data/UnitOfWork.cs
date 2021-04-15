using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WEA.SharedKernel.Interfaces;

namespace WEA.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork , IDisposable
    {
        private DbFactory _dbFactory;
        private TransactionScope transactionScope;
        public UnitOfWork(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public TransactionScope CreateScoppedTransaction()
        {
            if (transactionScope == null)
            {
                transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            }
            return transactionScope;
        }
        public Task<int> CommitAsync()
        {
            var result = _dbFactory.DbContext.SaveChangesAsync();
            if (transactionScope != null)
            {
                transactionScope.Complete();
            }
            return result;
        }

        public void Dispose()
        {
            if (transactionScope != null)
            {
                transactionScope.Dispose();
            }
        }
    }
}
