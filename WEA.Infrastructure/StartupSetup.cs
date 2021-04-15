using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WEA.Infrastructure.Data;

namespace WEA.Infrastructure
{
	public static class StartupSetup
	{
		public static void AddDbContext(this IServiceCollection services, string connectionString) =>
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(connectionString));
	}
}
