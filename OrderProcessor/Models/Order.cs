using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessor.Models
{
	public class Order
	{
        public int Id { get; set; }
        public string  UserName { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
