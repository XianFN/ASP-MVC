using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab2.Models

{
    
    public class Recipe
    
    {
        
        string RecName;
 
        private   List<string> RecDescription;
        private  List<Ingredient> RecIngredients;
        
        
        public Recipe(string RecName,    List<string> RecDescription)
        {
            this.RecName=RecName;
            this.RecDescription=RecDescription;
            this.RecIngredients = new  List<Ingredient>();

        }
        public Recipe(){
            this.RecName="";
            this.RecDescription=  new  List<string>();
            this.RecIngredients=  new  List<Ingredient>();
        }
        

        public string RecName1
        {
            get => RecName;
            set => RecName = value;
        }

        public List<string> RecDescription1
        {
            get => RecDescription;
            set => RecDescription = value;
        }

        public List<Ingredient> RecIngredients1
        {
            get => RecIngredients;
            set => RecIngredients = value;
        }


        //DELETE INGREDIENT FROM THIS RECIPE WITH THE ING NAME
        public void  deleteingredient(int index){
       
                this.RecIngredients.RemoveAt(index);
            

        }

//ADD INGREDIENT FOR THIS RECIPE
        public void  addRecIngredient(string NewName, decimal NewQuantity, string NewUnits){
         
            RecIngredients.Add(new Ingredient(NewName,NewQuantity,NewUnits));

        }
        public bool  isEmpty(){
            Console.WriteLine("     : "+(RecName1=="") +""+ (RecDescription1.Count==0)+"" + (RecIngredients1.Count==0));
         return (RecName1=="" && RecDescription1.Count==0 && RecIngredients1.Count==0 );

        }





        public void  printRecipe()
        {
            Console.WriteLine("      RECIPE        ");
            Console.WriteLine(this.RecName);
         

            for(  int a = 0; a < this.RecDescription.Count ; a++) {

                Console.WriteLine(this.RecDescription[a]);
            }
            for(  int a = 0; a < this.RecIngredients.Count ; a++) {

                Console.WriteLine("Ing Name: {0} Ing Quantity: {1} Ing Units: {2}", this.RecIngredients[a].IngName1,this.RecIngredients[a].IngQuantity1,this.RecIngredients[a].IngUnits1);
            }
        }

    }
}