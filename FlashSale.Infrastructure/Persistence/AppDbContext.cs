using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FlashSale.Infrastructure.Persistence
{
	internal class AppDbContext:DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
		{
			
		}
	}
}
