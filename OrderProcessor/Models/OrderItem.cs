using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessor.Models
{
	public class OrderItem
	{
		public int Id { get; set; }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
        public int Quantity { get; set; }
		public decimal Price { get; set; } = decimal.Zero;
    }
}
