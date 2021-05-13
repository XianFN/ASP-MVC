using System;
using System.Collections.Generic;
using System.Linq;
using Lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    public class ShoppingListController : Controller
    {
        // GET
        public IActionResult ShoppingList()
        {
            RecipesList.GetInstance().ShoppingList = new List<Recipe>();
            return View();
        }
        public IActionResult Index()
        {
        
            return View("ShoppingList");
        }
         [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToShoppingList(RecipeIngModel aux)

        {
            string number = Request.Form["select2"];
            int index= Int32.Parse(number);
            Recipe temp = RecipesList.GetInstance().getRecipe(index);
            RecipesList.GetInstance().AddToShopping(temp);
           
            return RedirectToAction("Index");
        }
  
        public IActionResult list()
        {
            
   
    List<string> ret= new List<string>();

    List<Recipe> shoppinglist = RecipesList.GetInstance().ShoppingList;
    List<Ingredient> vectorIngredients = new List<Ingredient>();
    for( int i = 0; i < shoppinglist.Count; i++) {
     
        List<Ingredient> auxvec= new List<Ingredient>();
       
        auxvec =  shoppinglist[i].RecIngredients1;

        for(int j = 0; j < auxvec.Count; j++){
            if (vectorIngredients.Count==0) {
                vectorIngredients.Add(auxvec[j]);
            }else{
                int existIndex =-1;
                for(int k = 0; k < vectorIngredients.Count; k++){
                    if (auxvec[j].IngName1==vectorIngredients[k].IngName1 && auxvec[j].IngUnits1==vectorIngredients[k].IngUnits1) {
                        existIndex = k;
                    }
                }
                if (existIndex!=-1) {
                    decimal newValue= Math.Round(vectorIngredients[existIndex].IngQuantity1 + auxvec[j].IngQuantity1, 3);
                    
                    vectorIngredients.RemoveAt(existIndex);

                    Ingredient aux=new Ingredient();
                    aux.IngName1= auxvec[j].IngName1;
                    aux.IngUnits1= auxvec[j].IngUnits1;
                    aux.IngQuantity1=newValue;

                    vectorIngredients.Add(aux);
                }else{
                    vectorIngredients.Add(auxvec[j]);
                }
            }
        }
    }
    for( int l = 0; l < vectorIngredients.Count; l++){
        Ingredient auxIngredient =vectorIngredients[l];
        string auxString =  auxIngredient.IngName1+": "+(float)auxIngredient.IngQuantity1+" "+auxIngredient.IngUnits1;
        ret.Add(auxString);
    }
    string result = string.Join("\n", ret);

    return Content(result);


        }

    }
}