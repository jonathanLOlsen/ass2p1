using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Order
    {
        public int Id { get; set; }  // Maps to orderid
        public DateTime Date { get; set; }  // Maps to orderdate
        public DateTime Required { get; set; }  // Maps to requireddate
        public string? ShipName { get; set; }  // Maps to shipname
        public string? ShipCity { get; set; }  // Maps to shipcity

        public List<OrderDetail>? OrderDetails { get; set; }
    }

}
