using System;
using System.Collections.Generic;

namespace ZooManagement
{
    public class Animal
    {
        private string _name;
        private int _age;
        private string _habitat;
        private string _foodType;

        public string Name
        {
            get { return _name; }
            private set { _name = value; }
        }

        public int Age
        {
            get { return _age; }
            private set
            {
                if (value >= MinAge)
                    _age = value;
                else
                    throw new ArgumentException("Age cannot be negative");
            }
        }

        public string Habitat
        {
            get { return _habitat; }
            private set { _habitat = value; }
        }

        public string FoodType
        {
            get { return _foodType; }
            private set { _foodType = value; }
        }

        protected const int MinAge = 0;

        protected Animal(string name, int age, string habitat, string foodType)
        {
            Name = name;
            Age = age;
            Habitat = habitat;
            FoodType = foodType;
        }

        public virtual string GetInfo()
        {
            return $"Name: {Name}, Age: {Age}, Habitat: {Habitat}, Food type: {FoodType}";
        }
    }

    public class Mammal : Animal
    {
        private bool _hasFur;

        public bool HasFur
        {
            get { return _hasFur; }
            private set { _hasFur = value; }
        }

        public Mammal(string name, int age, string habitat, string foodType, bool hasFur)
            : base(name, age, habitat, foodType)
        {
            HasFur = hasFur;
        }

        public override string GetInfo()
        {
            string furInfo = HasFur ? "yes" : "no";
            return base.GetInfo() + $", Type: Mammal, Has fur: {furInfo}";
        }
    }

    public class Bird : Animal
    {
        private double _wingSpan;

        public double WingSpan
        {
            get { return _wingSpan; }
            private set
            {
                if (value > MinWingSpan)
                    _wingSpan = value;
                else
                    throw new ArgumentException("Wing span must be positive");
            }
        }

        private const double MinWingSpan = 0.0;

        public Bird(string name, int age, string habitat, string foodType, double wingSpan)
            : base(name, age, habitat, foodType)
        {
            WingSpan = wingSpan;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $", Type: Bird, Wing span: {WingSpan} m";
        }
    }

    public class Fish : Animal
    {
        private string _waterType;

        public string WaterType
        {
            get { return _waterType; }
            private set { _waterType = value; }
        }

        public Fish(string name, int age, string habitat, string foodType, string waterType)
            : base(name, age, habitat, foodType)
        {
            WaterType = waterType;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $", Type: Fish, Water type: {WaterType}";
        }
    }

    public class Reptile : Animal
    {
        private bool _isVenomous;

        public bool IsVenomous
        {
            get { return _isVenomous; }
            private set { _isVenomous = value; }
        }

        public Reptile(string name, int age, string habitat, string foodType, bool isVenomous)
            : base(name, age, habitat, foodType)
        {
            IsVenomous = isVenomous;
        }

        public override string GetInfo()
        {
            string venomInfo = IsVenomous ? "venomous" : "non-venomous";
            return base.GetInfo() + $", Type: Reptile, Venomous: {venomInfo}";
        }
    }

    public class Amphibian : Animal
    {
        private string _skinMoisture;

        public string SkinMoisture
        {
            get { return _skinMoisture; }
            private set { _skinMoisture = value; }
        }

        public Amphibian(string name, int age, string habitat, string foodType, string skinMoisture)
            : base(name, age, habitat, foodType)
        {
            SkinMoisture = skinMoisture;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $", Type: Amphibian, Skin moisture: {SkinMoisture}";
        }
    }

    public sealed class AnimalManager
    {
        private static AnimalManager _instance = null;
        private static readonly object _lock = new object();

        private List<Animal> _animals;

        private const string MenuOptionAdd = "1";
        private const string MenuOptionDisplayAll = "2";
        private const string MenuOptionFind = "3";
        private const string MenuOptionExit = "4";

        private const string AnimalTypeMammal = "1";
        private const string AnimalTypeBird = "2";
        private const string AnimalTypeFish = "3";
        private const string AnimalTypeReptile = "4";
        private const string AnimalTypeAmphibian = "5";

        private AnimalManager()
        {
            _animals = new List<Animal>();
        }

        public static AnimalManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new AnimalManager();
                    }
                    return _instance;
                }
            }
        }

        public void AddAnimal(Animal animal)
        {
            if (animal != null)
            {
                _animals.Add(animal);
                Console.WriteLine($"Animal {animal.Name} successfully added.");
            }
            else
            {
                Console.WriteLine("Error: animal cannot be null.");
            }
        }

        public void DisplayAllAnimals()
        {
            if (_animals.Count == 0)
            {
                Console.WriteLine("The zoo has no animals yet.");
                return;
            }

            Console.WriteLine("\nList of all animals:");
            for (int animalIndex = 0; animalIndex < _animals.Count; animalIndex++)
            {
                Animal currentAnimal = _animals[animalIndex];
                Console.WriteLine($"[{animalIndex}] {currentAnimal.GetInfo()}");
            }
        }

        public void DisplayAnimalByCriterion(string userInput)
        {
            if (int.TryParse(userInput, out int index))
            {
                if (index >= 0 && index < _animals.Count)
                {
                    Console.WriteLine(_animals[index].GetInfo());
                }
                else
                {
                    Console.WriteLine("No animal with such index.");
                }
            }
            else
            {
                bool found = false;
                foreach (Animal animal in _animals)
                {
                    if (animal.Name.Equals(userInput, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine(animal.GetInfo());
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("Animal with such name not found.");
                }
            }
        }

        public void RunMenu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("\n--- Zoo Menu ---");
                Console.WriteLine("1. Add new animal");
                Console.WriteLine("2. Show all animals");
                Console.WriteLine("3. Find animal by index or name");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string choice;
                choice = Console.ReadLine();
                switch (choice)
                {
                    case MenuOptionAdd:
                        AddNewAnimalInteractive();
                        break;
                    case MenuOptionDisplayAll:
                        DisplayAllAnimals();
                        break;
                    case MenuOptionFind:
                        Console.Write("Enter index or name of the animal: ");
                        string criterion;
                        criterion = Console.ReadLine();
                        DisplayAnimalByCriterion(criterion);
                        break;
                    case MenuOptionExit:
                        keepRunning = false;
                        Console.WriteLine("Exiting program...");
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please choose 1-4.");
                        break;
                }
            }
        }

        private void AddNewAnimalInteractive()
        {
            Console.WriteLine("\nSelect animal type:");
            Console.WriteLine("1. Mammal");
            Console.WriteLine("2. Bird");
            Console.WriteLine("3. Fish");
            Console.WriteLine("4. Reptile");
            Console.WriteLine("5. Amphibian");
            Console.Write("Your choice (1-5): ");

            string typeChoice;
            typeChoice = Console.ReadLine();

            Console.Write("Enter name: ");
            string name;
            name = Console.ReadLine();

            int age;
            bool validAge = false;
            age = 0;
            while (!validAge)
            {
                Console.Write("Enter age (integer >= 0): ");
                string ageInput;
                ageInput = Console.ReadLine();
                if (int.TryParse(ageInput, out age) && age >= Animal.MinAge)
                {
                    validAge = true;
                }
                else
                {
                    Console.WriteLine("Invalid age. Try again.");
                }
            }

            Console.Write("Enter habitat (e.g., forest, water): ");
            string habitat;
            habitat = Console.ReadLine();

            Console.Write("Enter food type (predator, herbivore, omnivore): ");
            string foodType;
            foodType = Console.ReadLine();

            Animal newAnimal = null;

            switch (typeChoice)
            {
                case AnimalTypeMammal:
                    bool hasFur;
                    hasFur = AskYesNo("Does it have fur? (yes/no): ");
                    newAnimal = new Mammal(name, age, habitat, foodType, hasFur);
                    break;
                case AnimalTypeBird:
                    double wingSpan;
                    bool validWingSpan = false;
                    wingSpan = 0.0;
                    while (!validWingSpan)
                    {
                        Console.Write("Enter wing span (in meters, positive number): ");
                        string wingInput;
                        wingInput = Console.ReadLine();
                        if (double.TryParse(wingInput, out wingSpan) && wingSpan > 0.0)
                        {
                            validWingSpan = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid wing span. Enter a positive number.");
                        }
                    }
                    newAnimal = new Bird(name, age, habitat, foodType, wingSpan);
                    break;
                case AnimalTypeFish:
                    Console.Write("Enter water type (fresh/salt): ");
                    string waterType;
                    waterType = Console.ReadLine();
                    newAnimal = new Fish(name, age, habitat, foodType, waterType);
                    break;
                case AnimalTypeReptile:
                    bool isVenomous;
                    isVenomous = AskYesNo("Is it venomous? (yes/no): ");
                    newAnimal = new Reptile(name, age, habitat, foodType, isVenomous);
                    break;
                case AnimalTypeAmphibian:
                    Console.Write("Enter skin moisture (e.g., high, medium): ");
                    string skinMoisture;
                    skinMoisture = Console.ReadLine();
                    newAnimal = new Amphibian(name, age, habitat, foodType, skinMoisture);
                    break;
                default:
                    Console.WriteLine("Invalid animal type. Addition cancelled.");
                    return;
            }

            if (newAnimal != null)
            {
                AddAnimal(newAnimal);
            }
        }

        private bool AskYesNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string response;
                response = Console.ReadLine().Trim().ToLower();
                if (response == "yes" || response == "y")
                {
                    return true;
                }
                else if (response == "no" || response == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Please answer 'yes' or 'no'.");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AnimalManager zooManager;
            zooManager = AnimalManager.Instance;

            Animal lion;
            lion = new Mammal("Simba", 5, "Savanna", "predator", true);
            Animal eagle;
            eagle = new Bird("Kesha", 3, "Mountains", "predator", 2.5);
            Animal goldfish;
            goldfish = new Fish("Goldie", 1, "Aquarium", "omnivore", "fresh");
            Animal snake;
            snake = new Reptile("Kaa", 4, "Jungle", "predator", true);
            Animal frog;
            frog = new Amphibian("Croak", 2, "Swamp", "insectivore", "moist");

            zooManager.AddAnimal(lion);
            zooManager.AddAnimal(eagle);
            zooManager.AddAnimal(goldfish);
            zooManager.AddAnimal(snake);
            zooManager.AddAnimal(frog);

            zooManager.RunMenu();
        }
    }
}