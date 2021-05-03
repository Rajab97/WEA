using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WEA.Infrastructure.Data
{
    public class DbFactory : IDisposable
    {
        private bool _disposed;
        private Func<AppDbContext> _instanceFunc;
        private Func<ViewsDbContext> _viewsFunc;
        private AppDbContext _dbContext;
        private ViewsDbContext _viewsDbContext;
        public AppDbContext DbContext => _dbContext ?? (_dbContext = _instanceFunc.Invoke());
        public ViewsDbContext ViewsDbContext => _viewsDbContext ?? (_viewsDbContext = _viewsFunc.Invoke());
        public DbFactory(Func<AppDbContext> dbContextFactory , Func<ViewsDbContext> dbViewContextFactory)
        {
            _instanceFunc = dbContextFactory;
            _viewsFunc = dbViewContextFactory;
        }

        public void Dispose()
        {
            if (!_disposed && _dbContext != null)
            {
                _disposed = true;
                _dbContext.Dispose();
            }
        }
    }
}
