using System;
using System.Collections.Generic;
using System.Linq;
using Lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    public class AddRecipeController : Controller
    {
     
        // GET
        public IActionResult AddRecipe()
        {
            
            RecipesList temp = RecipesList.GetInstance();
            if (temp.ActualRecipe.RecName1 != "")
            {
                @ViewData["name"] = temp.ActualRecipe.RecName1;
            }

            if (temp.ActualRecipe.RecDescription1.Count != 0)
            {
                string result = string.Join("\n", temp.ActualRecipe.RecDescription1);
                @ViewData["description"] = result;
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddIngredient(RecipeIngModel aux, string answer)

        {
            
            RecipesList temp = RecipesList.GetInstance();
            switch (answer)
            {
                case "Add Ingredient":

                    Ingredient obj = aux.Ingredient;

                   
                        if (obj.IngName1 != "" && obj.IngUnits1 != "")
                        {
                            Console.WriteLine("INGREDIENT ADDED");
                            temp.ActualRecipe.addRecIngredient(obj.IngName1, obj.IngQuantity1, obj.IngUnits1);
                        }

                        if (aux.Description1 != null)
                        {
                            
                            
                            @ViewData["description"] = aux.Description1;
                            List<String> des = aux.Description1.Split("\r\n").ToList();
                            temp.ActualRecipe.RecDescription1 = des;
                        }
                        else if (temp.ActualRecipe.RecDescription1.Count != 0)
                        {
                            string result = string.Join("\n", temp.ActualRecipe.RecDescription1);
                            @ViewData["description"] = result;
                        }

                        {
                            @ViewData["description"] = "";
                        }
                        if (aux.RecName1 != null)
                        {
                            temp.ActualRecipe.RecName1 = aux.RecName1;
                            @ViewData["name"] = aux.RecName1;
                        }
                        else if (temp.ActualRecipe.RecName1 != "")
                        {
                            @ViewData["name"] = temp.ActualRecipe.RecName1;
                        }

                        {
                            @ViewData["name"] = "";
                        }


                        return RedirectToAction("AddRecipe");
        
                break;
                case "Add Recipe":
                    if (aux.RecName1 != null)
                    {
                        temp.ActualRecipe.RecName1 = aux.RecName1;
                    }

                    if (aux.Description1 != null)
                    {
                        List<String> des = aux.Description1.Split("\r\n").ToList();
                        temp.ActualRecipe.RecDescription1 = des;
                    }


                    if (temp.ActualRecipe == null || temp.ActualRecipe.RecDescription1.Count == 0 ||
                        temp.ActualRecipe.RecName1 == "" || temp.ActualRecipe.RecIngredients1.Count == 0)
                    {
                        Console.WriteLine("PLEASE ENTER ALL THE DATA");
                        return RedirectToAction("AddRecipe");
                    }
                    else
                    {
                        Console.WriteLine("RECIPE ADDED");
                        temp.Add();

                        return RedirectToAction("ReloadIndex", "Home");
                    }
                    
                    
                    break;
            }
            return RedirectToAction("AddRecipe");
        }

        public IActionResult EditRecipe(int id)
        {
            Console.WriteLine("EDIT RECIPE");
            RecipesList.GetInstance().ActualRecipe = RecipesList.GetInstance().getRecipe(id);
            RecipesList.GetInstance().Recipes.RemoveAt(id);
            return RedirectToAction("AddRecipe");
        }
        public IActionResult NewRecipe()
        {
            Console.WriteLine("NEW RECIPE");
            RecipesList.GetInstance().ActualRecipe = new Recipe();
            return RedirectToAction("AddRecipe");
        }

        public RedirectResult Index()
        {
            return Redirect("/Home");
        }

        public IActionResult AddRecipeToList()
        {
            //DEPRECATED
            RecipesList temp = RecipesList.GetInstance();
               

            if (temp.ActualRecipe == null || temp.ActualRecipe.RecDescription1.Count == 0 ||
                temp.ActualRecipe.RecName1 == "" || temp.ActualRecipe.RecIngredients1.Count == 0)
            {
                return RedirectToAction("AddRecipe");
            }
            else
            {
                temp.Add();

                return RedirectToAction("ReloadIndex", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteIngredient(RecipeIngModel aux)
        {
            
            var number = Request.Form["select1"];
            int index= Int32.Parse(number);
            RecipesList.GetInstance().ActualRecipe.deleteingredient(index);
            
            return RedirectToAction("AddRecipe");
        }
    }
    
}