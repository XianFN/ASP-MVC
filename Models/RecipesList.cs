using System;
using System.Collections.Generic;

namespace Lab2.Models
{
    public class RecipesList
    {
         List<Recipe> recipes= new List<Recipe>();
         List<Recipe> shoppingList= new List<Recipe>();
         private Recipe actualRecipe = new Recipe();

         public List<Recipe> ShoppingList
         {
             get => shoppingList;
             set => shoppingList = value;
         }

         public Recipe ActualRecipe
         {
             get => actualRecipe;
             set => actualRecipe = value;
         }

         private RecipesList()
        {
            List<Recipe> recipes = new List<Recipe>();
        
        }

        public List<Recipe> Recipes
        {
            get => recipes;
            set => recipes = value;
        }

        public void Add()
        {
            recipes.Add(actualRecipe);
            EmptyRecipe();
        }
        public void Add(Recipe aux)
        {
            recipes.Add(aux);
        }
        public void AddToShopping(Recipe aux)
        {
            shoppingList.Add(aux);
        }
        public Recipe getRecipe(int index)
        {
            return recipes[index];
        }

        public void Empty()
        {
            recipes.Clear();
            
        }

        public void EmptyRecipe()
        {
            actualRecipe = new Recipe();
        } 

        // The Singleton's instance is stored in a static field. There there are
        // multiple ways to initialize this field, all of them have various pros
        // and cons. In this example we'll show the simplest of these ways,
        // which, however, doesn't work really well in multithreaded program.
        private static RecipesList _instance;

        // This is the static method that controls the access to the singleton
        // instance. On the first run, it creates a singleton object and places
        // it into the static field. On subsequent runs, it returns the client
        // existing object stored in the static field.
        public static RecipesList GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RecipesList();
            }
            return _instance;
        }

        // Finally, any singleton should define some business logic, which can
        // be executed on its instance.
        public static void someBusinessLogic()
        {
            // ...
        }
    }
}