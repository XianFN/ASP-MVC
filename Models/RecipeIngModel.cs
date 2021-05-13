using System.Collections.Generic;

namespace Lab2.Models
{
    public class RecipeIngModel
    {
        public  Ingredient Ingredient{ get; set; }
        public string Description = "";
        public string RecName ="";
    
        public string Description1
        {
            get => Description;
            set => Description = value;
        }

        public string RecName1
        {
            get => RecName;
            set => RecName = value;
        }
    }
}