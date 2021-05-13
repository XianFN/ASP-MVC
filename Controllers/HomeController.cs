using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lab2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lab2.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {

           
            _logger = logger;
           
        }


        public IActionResult Index()
        {
            read();
           // write();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ReloadIndex()
        {
            write();
            read();
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOrDeleteRecipe(string answer)
        {
            string number = Request.Form["select2"];
            switch (answer)
            {
                case "Edit Recipe":
                    if (number!=null)
                    {
                        return RedirectToAction("EditRecipe", "AddRecipe",new { id = number });
                    }
                    break;
                case "Delete Recipe":
                    if (number!=null)
                    {
                        int index= Int32.Parse(number);
                        RecipesList.GetInstance().Recipes.RemoveAt(index);
                        return RedirectToAction("ReloadIndex");
                        
                    }
                    break;
            }
            return Redirect("/Home");


        }
       

        

        public void read()
        {
            RecipesList del = RecipesList.GetInstance();
            del.Empty();
            using (StreamReader r = new StreamReader("recipes.json"))
            {
                string json = r.ReadToEnd();
                
             
                JsonTextReader reader = new JsonTextReader(new StringReader(json));
                JObject googleSearch = JObject.Parse(json);
                
                List<JToken> results = googleSearch.Children().ToList();

                List<Recipe> searchResults = new List<Recipe>();
                
          
                foreach (JToken result in results)
                {
                    JProperty jProperty = result.ToObject<JProperty>();
                    string propertyName = jProperty.Name;
                 
                    Recipe temp = new Recipe();
                    temp.RecName1=propertyName;
                    
                    
                    List<JToken> ingredients = result.Children().Children().ToList();
                    foreach (JToken ingredientAndRecipeDes in ingredients)
                    {
                        JProperty jProperty2 = ingredientAndRecipeDes.ToObject<JProperty>();
                        string ingName = jProperty2.Name;
                        if (ingName=="recipe")
                        {
                            string[] splitDescription = ingredientAndRecipeDes.ToString().Split(':');

                            
                            string[] unitsAndQuantity = splitDescription[1].Split('"');
                            List<string> descriptionList= new List<string>();
                            
                            foreach (var word in unitsAndQuantity)
                            {
                                if (word!="" && word!=",\n  " && word!=" [\n  " &&word!="\n]"  )
                                {
                                    
                                    descriptionList.Add(word);
                                }
                            }
                            temp.RecDescription1=descriptionList;
                            
                        }
                        else
                        {
                         
                            string[] splitUnitsAndQuantity = ingredientAndRecipeDes.ToString().Split('"');
                            string[] unitsAndQuantity = splitUnitsAndQuantity[3].Split(' ');
                            decimal quantity = decimal.Parse(unitsAndQuantity[0]);
                            string units="";
                       
                            for (int i = 1; i < unitsAndQuantity.Length; i++)
                            {
                                units += unitsAndQuantity[i];
                            }
                           
                            temp.addRecIngredient(ingName,quantity,units);
                        }
                    }
                  //  temp.printRecipe();
                  RecipesList recipes = RecipesList.GetInstance();
                   recipes.Add(temp);
                }
    
            }
        }

        public void write()
        {
            RecipesList reci = RecipesList.GetInstance();
            List<Recipe> recipes = reci.Recipes;
            
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                foreach (var recipe in recipes)
                {
                    writer.WritePropertyName(recipe.RecName1);
                    writer.WriteStartObject();
                    writer.WritePropertyName("recipe");
                    writer.WriteStartArray();
                    foreach (var desc in recipe.RecDescription1)
                    {
                        writer.WriteValue(desc);
                        
                    }
                    writer.WriteEnd();
                    foreach (var ing in recipe.RecIngredients1)
                    {
                        writer.WritePropertyName(ing.IngName1);
                        writer.WriteValue(ing.IngQuantity1+" "+ ing.IngUnits1);
                        
                    }
                    
                    writer.WriteEnd();
                }
               
                writer.WriteEnd();
             //   writer.WriteEndObject();
            }
            System.IO.File.WriteAllText("recipes.json", sb.ToString());
           
            sw.Close();
        }

       
    }
}
