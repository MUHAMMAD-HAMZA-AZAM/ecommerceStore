using System;
using System.Collections.Generic;
using System.Text;

namespace Core.PocoEntities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string ProducrName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureURL { get; set;}
        public string Brand { get; set; }
        public string Type { get; set; }
    }
}
