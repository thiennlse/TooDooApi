﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Tags : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
