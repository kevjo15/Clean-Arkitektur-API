using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UserAnimalDto
    {
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        //public Guid AnimalModelId { get; set; }

        public List<DogDto> Dogs { get; set; }
        public List<CatDto> Cats { get; set; }
        public List<BirdDto> Birds { get; set; }


    }
}
