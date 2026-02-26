using System;
using System.Collections.Generic;

namespace laba2
{
  public abstract class Animal
  {
    public string animalName;
    public int animalAge;
    public string animalHabitat;
    public string animalFood;
    
    public Animal(string name, int age, string habitat, string food)
    {
      animalName = name;
      animalAge = age;
      animalHabitat = habitat;
      animalFood = food;
    }
    
    public virtual string GetInfo()
    {
      string resultInfo;
      
      resultInfo = "Name: " + animalName + ", Age: " + animalAge + ", Habitat: " + animalHabitat + ", Food: " + animalFood;
      
      return resultInfo;
    }
    
    public string GetName()
    {
      return animalName;
    }
  }
  
  public class Mammal : Animal
  {
    public bool hasFur;
    
    public Mammal(string name, int age, string habitat, string food, bool fur) : base(name, age, habitat, food)
    {
      hasFur = fur;
    }
    
    public override string GetInfo()
    {
      string mainInfo;
      string furText;
      string fullInfo;
      
      mainInfo = base.GetInfo();
      
      if (hasFur == true)
      {
        furText = "Yes";
      }
      else
      {
        furText = "No";
      }
      
      fullInfo = mainInfo + ", Type: Mammal, Fur: " + furText;
      
      return fullInfo;
    }
  }
  
  public class Bird : Animal
  {
    public double wingLength;
    
    public Bird(string name, int age, string habitat, string food, double wings) : base(name, age, habitat, food)
    {
      wingLength = wings;
    }
    
    public override string GetInfo()
    {
      string mainInfo;
      string fullInfo;
      
      mainInfo = base.GetInfo();
      fullInfo = mainInfo + ", Type: Bird, Wingspan: " + wingLength + " m";
      
      return fullInfo;
    }
  }
  
  public class Fish : Animal
  {
    public string waterType;
    
    public Fish(string name, int age, string habitat, string food, string water) : base(name, age, habitat, food)
    {
      waterType = water;
    }
    
    public override string GetInfo()
    {
      string mainInfo;
      string fullInfo;
      
      mainInfo = base.GetInfo();
      fullInfo = mainInfo + ", Type: Fish, Water: " + waterType;
      
      return fullInfo;
    }
  }
  
  public class Reptile : Animal
  {
    public bool isPoison;
    
    public Reptile(string name, int age, string habitat, string food, bool poison) : base(name, age, habitat, food)
    {
      isPoison = poison;
    }
    
    public override string GetInfo()
    {
      string mainInfo;
      string poisonText;
      string fullInfo;
      
      mainInfo = base.GetInfo();
      
      if (isPoison == true)
      {
        poisonText = "Yes";
      }
      else
      {
        poisonText = "No";
      }
      
      fullInfo = mainInfo + ", Type: Reptile, Poison: " + poisonText;
      
      return fullInfo;
    }
  }
  
  public class Amphibian : Animal
  {
    public string skinType;
    
    public Amphibian(string name, int age, string habitat, string food, string skin) : base(name, age, habitat, food)
    {
      skinType = skin;
    }
    
    public override string GetInfo()
    {
      string mainInfo;
      string fullInfo;
      
      mainInfo = base.GetInfo();
      fullInfo = mainInfo + ", Type: Amphibian, Skin: " + skinType;
      
      return fullInfo;
    }
  }
  
  public class AnimalList
  {
    private List<Animal> animalCollection;
    private static AnimalList singleList;
    
    private AnimalList()
    {
      animalCollection = new List<Animal>();
    }
    
    public static AnimalList GetList()
    {
      if (singleList == null)
      {
        singleList = new AnimalList();
      }
      
      return singleList;
    }
    
    public void AddAnimal(Animal newAnimal)
    {
      if (newAnimal == null)
      {
        Console.WriteLine("Error: animal is null");
        
        return;
      }
      
      animalCollection.Add(newAnimal);
      
      Console.WriteLine("Added: " + newAnimal.GetName());
    }
    
    public void ShowAll()
    {
      int totalCount;
      int displayNumber;
      
      totalCount = animalCollection.Count;
      
      if (totalCount == 0)
      {
        Console.WriteLine("List is empty");
        
        return;
      }
      
      Console.WriteLine("");
      Console.WriteLine("=== ALL ANIMALS ===");
      
      for (int animalPosition = 0; animalPosition < totalCount; ++animalPosition)
      {
        displayNumber = animalPosition + 1;
        
        Console.WriteLine(displayNumber + ". " + animalCollection[animalPosition].GetInfo());
      }
    }
    
    public void FindByName(string searchText)
    {
      int totalCount;
      
      if (searchText == null || searchText == "")
      {
        Console.WriteLine("Error: empty name");
        
        return;
      }
      
      totalCount = animalCollection.Count;
      
      for (int animalPosition = 0; animalPosition < totalCount; ++animalPosition)
      {
        string currentName;
        
        currentName = animalCollection[animalPosition].GetName();
        
        if (currentName.ToLower() == searchText.ToLower())
        {
          Console.WriteLine(animalCollection[animalPosition].GetInfo());
          
          return;
        }
      }
      
      Console.WriteLine("Not found: " + searchText);
    }
  }
  
  public static class Program
  {
    private static void Main()
    {
      AnimalList mainList;
      string option;
      string inputName;
      
      mainList = AnimalList.GetList();
      
      mainList.AddAnimal(new Mammal("Leo", 5, "Savanna", "Meat", true));
      mainList.AddAnimal(new Bird("Aquila", 3, "Mountains", "Meat", 2.5));
      mainList.AddAnimal(new Fish("Goldie", 1, "Aquarium", "Plants", "Fresh"));
      mainList.AddAnimal(new Reptile("Slither", 2, "Desert", "Meat", true));
      mainList.AddAnimal(new Amphibian("Jumpo", 1, "Pond", "Insects", "Moist"));
      
      do
      {
        Console.WriteLine("");
        Console.WriteLine("=== MENU ===");
        Console.WriteLine("1. Show all");
        Console.WriteLine("2. Find by name");
        Console.WriteLine("3. Exit");
        Console.Write("Choice: ");
        
        option = Console.ReadLine();
        
        if (option == "1")
        {
          mainList.ShowAll();
        }
        
        if (option == "2")
        {
          Console.Write("Enter name: ");
          
          inputName = Console.ReadLine();
          
          mainList.FindByName(inputName);
        }
        
        if (option == "3")
        {
          Console.WriteLine("Bye");
        }
        
      } while (option != "3");
    }
  }
}