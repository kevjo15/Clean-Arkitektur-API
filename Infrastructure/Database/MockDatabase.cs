using Domain.Models;

namespace Infrastructure.Database
{
    public class MockDatabase
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
            new Bird { Id = Guid.NewGuid(), Name = "Drake"},
            new Bird { Id = Guid.NewGuid(), Name = "Simon"},
            new Bird { Id = Guid.NewGuid(), Name = "Gustav"},
            new Bird { Id = new Guid("12345678-1234-5678-1234-567812345611"), Name = "TestBirdForUnitTests"},
            new Bird { Id = new Guid("12345678-1234-5678-1234-567812345612"), Name = "TestBirdForDelete"}
        };
        private static List<Cat> allCats = new()
        {
            new Cat { Id = Guid.NewGuid(), Name = "Nemo"},
            new Cat { Id = Guid.NewGuid(), Name = "Doris"},
            new Cat { Id = Guid.NewGuid(), Name = "Simba"},
            new Cat { Id = new Guid("12345678-1234-5678-1234-567812345677"), Name = "TestCatForUnitTests"},
            new Cat { Id = new Guid("12345678-1234-5678-1234-567812345675"), Name = "TestCatForDelete"}
        };

        private static List<Dog> allDogs = new()
        {
            new Dog { Id = Guid.NewGuid(), Name = "Björn"},
            new Dog { Id = Guid.NewGuid(), Name = "Patrik"},
            new Dog { Id = Guid.NewGuid(), Name = "Alfred"},
            new Dog { Id = new Guid("12345678-1234-5678-1234-567812345678"), Name = "TestDogForUnitTests"},
            new Dog { Id = new Guid("12345678-1234-5678-1234-567812345679"), Name = "TestDogForDelete"}
        };
    }
}
