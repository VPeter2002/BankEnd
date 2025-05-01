using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AD41HN_HFT_2022231.Models
{
    public class FoodItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public double Amount { get; set; }

        public int FoodJsonId { get; set; }  // "id" mező a JSON-ből

        public string Text { get; set; }

        public string Unit { get; set; }

        public int Weights { get; set; }

        public virtual MealDetails Details { get; set; }

        // EF kapcsolathoz:
        //public int OhioMealId { get; set; }
        //public virtual OhioMeal OhioMeal { get; set; }
    }
}