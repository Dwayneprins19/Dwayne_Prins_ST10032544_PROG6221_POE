using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RecipeManagerWPF
{

	/// <summary>
	/// Dwayne Prins
	/// Std:ST10032544
	/// Module:PRG6211
	/// </summary>
	public class Recipe
	{

		//The Properties of the recipe
		public string Name { get; set; }
		public List<Ingredient> Ingredients { get; set;}
		public List<string> Steps {  get; set;}
		public double OriginalFactor { get; set; } = 1.0;


		//Contructor to intialize a recipe with a name
		public Recipe(string name)
		{
			Name = name;
			Ingredients = new List<Ingredient>();
			Steps = new List<string>();
		}


		//The method to add an ingredient to the recipe
		public void AddIngredient(Ingredient ingredient)
		{
			Ingredients.Add(ingredient);
		}


		//The method to add a step to the recipe
		public void AddStep(string step)
		{
			Steps.Add(step);
		}


		//The metod to scale the qauntities of the ingredients by a given factor
		public void ScaleRecipe(double factor)
		{
			foreach (var ingredient in Ingredients)
			{
				ingredient.Scale(factor / OriginalFactor);
			}

			OriginalFactor = factor;
		}


		//The method to reset the quantities of the ingredients
		public void ResetQuantities()
		{
			ScaleRecipe(1.0);
		}


		//The method to calculate the total amount of calories within the recipe
		public double TotalCalories()
		{
			double total = 0.0;
			foreach (var ingredient in Ingredients)
			{
				total += ingredient.Calories * (ingredient.Quantity / OriginalFactor);
			}
			return total;
		}
	}
}
//******************************************************************END***************************************************************************************

