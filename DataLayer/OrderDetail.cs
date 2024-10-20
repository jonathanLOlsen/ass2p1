﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class OrderDetail
    {
        public int OrderId { get; set; }  
        public int ProductId { get; set; }  
        public float UnitPrice { get; set; }  
        public float Quantity { get; set; }  
        public float Discount { get; set; }  

        
        public Order? Order { get; set; }

        public Product? Product { get; set; }
    }

}