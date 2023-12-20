using Domain.Models.Animal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserAnimal
    {
        public Guid UserId { get; set; }
        public Guid AnimalModelId { get; set; }
        public User User { get; set; }
        public AnimalModel AnimalModel { get; set; }
    }
}
