using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CofeWebApplication.Models
{
    public class BasketItemViewModel
    {
        public string CoffeeName { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}