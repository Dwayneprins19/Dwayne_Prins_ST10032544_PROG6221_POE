using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManagerWPF
{
	public class Ingredient
	{

		//The properties of the ingredient
		public string Name { get; set; }
		public double Quantity { get; set; }
		public string Unit { get; set; }
		public double Calories { get; set; }
		public string FoodGroup { get; set; }


		//The constructor to intialize an ingredient
		public Ingredient(string name, double quantity, string unit, double calories, string foodGroup)
		{
			Name = name;
			Quantity = quantity;
			Unit = unit;
			Calories = calories;
			FoodGroup = foodGroup;
		}


		//The method to scale the quantity of the ingredient by a given factor
		public void Scale(double factor)
		{
			Quantity *= factor;
		}
	}
}

//******************************************************************END***************************************************************************************
