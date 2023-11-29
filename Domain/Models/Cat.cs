using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Animal;

namespace Domain.Models
{
    public class Cat : AnimalModel
    {
        public bool LikesToPlay { get; set; }
    }
}
