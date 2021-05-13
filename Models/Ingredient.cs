namespace Lab2.Models
{
    public class Ingredient
    {
        private string IngName;
        private decimal IngQuantity;
        private string IngUnits;

      public Ingredient(string NewName, decimal NewQuantity, string NewUnits)
        {


            this.IngName= NewName;
            this.IngQuantity=NewQuantity;
            this.IngUnits=NewUnits;

        }
      public Ingredient(){
            this.IngName="";
            this.IngQuantity=0;
            this.IngUnits="";
        }

      public string IngName1
      {
          get => IngName;
          set => IngName = value;
      }

      public decimal IngQuantity1
      {
          get => IngQuantity;
          set => IngQuantity = value;
      }

      public string IngUnits1
      {
          get => IngUnits;
          set => IngUnits = value;
      }




    }
}