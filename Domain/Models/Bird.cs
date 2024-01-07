﻿using Domain.Models.Animal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Bird : AnimalModel
    {
        public bool CanFly { get; set; }
        public string Color { get; set; }
    }
}
