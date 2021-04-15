using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace WEA.SharedKernel.Interfaces
{
    public interface IUnitOfWork
    {

        Task<int> CommitAsync();
        TransactionScope CreateScoppedTransaction();
    }
}
