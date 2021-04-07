﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Entities
{
    public class UserInfo
    {
        [Key]
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public int Age { get; set; }

        public virtual User User { get; set; }
    }
}
