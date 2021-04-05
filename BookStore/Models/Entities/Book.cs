using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /*Navigation Properties*/
        public Category Category { get; set; }
    }
}
