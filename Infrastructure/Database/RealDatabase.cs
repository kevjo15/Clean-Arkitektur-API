using Domain.Models;

namespace Infrastructure.Database
{
    public class RealDatabase
    {
        public List<Dog> Dogs
        {
            get { return allDogs; }
            set { allDogs = value; }
        }
        public List<Cat> Cats
        {
            get { return allCats; }
            set { allCats = value; }
        }
        public List<Bird> Birds
        {
            get { return allBirds; }
            set { allBirds = value; }
        }
        private static List<Bird> allBirds = new()
        {
            new Bird { Id = Guid.NewGuid(), Name = "Drake", CanFly = false },
            new Bird { Id = Guid.NewGuid(), Name = "Simon", CanFly = false },
            new Bird { Id = Guid.NewGuid(), Name = "Gustav", CanFly = false },
            new Bird { Id = new Guid("12345678-1234-5678-1234-567812345611"), Name = "TestBirdForUnitTests", CanFly = false },
            new Bird { Id = new Guid("12345678-1234-5678-1234-567812345614"), Name = "UpdatedBirdName", CanFly = false },
            new Bird { Id = new Guid("12345678-1234-5678-1234-567812345612"), Name = "TestBirdForDelete", CanFly = false }
        };
        private static List<Cat> allCats = new()
        {
            new Cat { Id = Guid.NewGuid(), Name = "Nemo", LikesToPlay = false },
            new Cat { Id = Guid.NewGuid(), Name = "Doris", LikesToPlay = false},
            new Cat { Id = Guid.NewGuid(), Name = "Simba", LikesToPlay = false},
            new Cat { Id = new Guid("12345678-1234-5678-1234-567812345677"), Name = "TestCatForUnitTests", LikesToPlay = false},
            new Cat { Id = new Guid("12345678-1234-5678-1234-567812345613"), Name = "UpdatedCatName", LikesToPlay = false },
            new Cat { Id = new Guid("12345678-1234-5678-1234-567812345675"), Name = "TestCatForDelete", LikesToPlay = false}
        };

        private static List<Dog> allDogs = new()
        {
            new Dog { Id = Guid.NewGuid(), Name = "Björn"},
            new Dog { Id = Guid.NewGuid(), Name = "Patrik"},
            new Dog { Id = Guid.NewGuid(), Name = "Alfred"},
            new Dog { Id = new Guid("12345678-1234-5678-1234-567812345678"), Name = "TestDogForUnitTests"},
            new Dog { Id = new Guid("12345678-1234-5678-1234-567812345615"), Name = "UpdatedDogName",},
            new Dog { Id = new Guid("12345678-1234-5678-1234-567812345679"), Name = "TestDogForDelete"}
        };
    }
}
