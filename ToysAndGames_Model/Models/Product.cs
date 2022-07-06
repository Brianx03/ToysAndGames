using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToysAndGames_Model.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AgeRestriction { get; set; }
        public string Company { get; set; }
        public decimal Price { get; set; }  
    }
}
