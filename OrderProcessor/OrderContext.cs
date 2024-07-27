using Microsoft.EntityFrameworkCore;
using OrderProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessor
{
	public class OrderContext : DbContext
	{
		public OrderContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<Order> Orders { get; set; }	
		public DbSet<OrderItem> OrderItems { get; set; }
	}
}
