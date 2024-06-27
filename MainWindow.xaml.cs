using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecipeManagerWPF
{
	/// <summary>
	/// Dwayne Prins
	/// Std:ST10032544
	/// Module:PRG6211
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		// This is the list to store recipes 
		private List<Recipe> recipes = new List<Recipe>();
		public MainWindow()
		{
			InitializeComponent();
		}

		//The event handler for adding a recipe
		private void AddRecipe_Click(object sender, RoutedEventArgs e)
		{

			// This is to get the recipe from the text box
			string name = RecipeNameTextBox.Text;
			//Parse the amount of ingredients from the text box
			if (int.TryParse(NumIngredientsTextBox.Text, out int numIngredients))
			{
				// This is to create a new recipe with the specified name
				Recipe recipe = new Recipe(name);

				//This is a Loop to add ingredients
				for (int i = 0; i < numIngredients; i++)
				{
					IngredientWindow ingredientWindow = new IngredientWindow();
					if (ingredientWindow.ShowDialog() == true)
					{
						recipe.AddIngredient(ingredientWindow.Ingredient);
					}
				}


				//This is a window creted to add steps
				AddStepsWindow addStepsWindow = new AddStepsWindow(numIngredients); 
				if (addStepsWindow.ShowDialog() == true)
				{

					//Add the steps to the recipe
					foreach (var step in addStepsWindow.Steps)
					{
						recipe.AddStep(step);
					}
				}

				//Add the new recipe to the list and update the recipe liat
				recipes.Add(recipe);
				UpdateRecipeList();
			}
		}


		//The event handler for clearing all the recipes
		private void ClearRecipes_Click(object sender, RoutedEventArgs e)
		{
			recipes.Clear();
			UpdateRecipeList();
			RecipeDetailsTextBox.Text = "";
		}


		//The event handler for scaling the recipe
		private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
		{
			if (RecipesListBox.SelectedItem is Recipe selectedRecipe)
			{
				if (double.TryParse(((Button)sender).Tag.ToString(), out double factor))
				{
					selectedRecipe.ScaleRecipe(factor);
					DisplayRecipeDetails(selectedRecipe);
				}
			}
		}


		//The event handler for resetting the quantities of the recipe
		private void ResetRecipe_Click(object sender, RoutedEventArgs e)
		{
			if (RecipesListBox.SelectedItem is Recipe selectedRecipe)
			{
				selectedRecipe.ResetQuantities();
				DisplayRecipeDetails(selectedRecipe);
			}
		}


		//The event handler for selection change in the recipe list box
		private void RecipeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (RecipesListBox.SelectedItem is Recipe selectedRecipe)
			{
				DisplayRecipeDetails(selectedRecipe);
			}
		}


		//The event handler for the filtering of recipes
		private void FilterRecipes(object sender, System.Windows.Input.KeyEventArgs e)
		{
			UpdateRecipeList();
		}


		//The method to update the recipe list based on dilters
		private void UpdateRecipeList()
		{
			string ingredientFilter = IngredientFilterTextBox.Text.ToLower(); 
			string foodGroupFilter = FoodGroupFilterTextBox.Text.ToLower();

			//Parse the maximum amount of calories filter
			if (!double.TryParse(MaxCaloriesFilterTextBox.Text, out double maxCalories))
			{
				maxCalories = double.MaxValue;
			}


			//Filter the recipes based on the specified criteria
			var filteredRecipes = recipes.Where(recipe =>
				(string.IsNullOrWhiteSpace(ingredientFilter) || recipe.Ingredients.Any(i => i.Name.ToLower().Contains(ingredientFilter))) &&
				(string.IsNullOrWhiteSpace(foodGroupFilter) || recipe.Ingredients.Any(i => i.FoodGroup.ToLower().Contains(foodGroupFilter))) &&
				recipe.TotalCalories() <= maxCalories)
				.OrderBy(recipe => recipe.Name)
				.ToList();

			RecipesListBox.ItemsSource = filteredRecipes;
		}


		// The method to display the details of the selected recipe
		private void DisplayRecipeDetails(Recipe recipe)
		{
			RecipeDetailsTextBox.Text = $"Recipe: {recipe.Name}\nIngredients:\n"; 

			//Display the ingredients
			foreach (var ingredient in recipe.Ingredients)
			{
				RecipeDetailsTextBox.Text += $"{ingredient.Quantity} {ingredient.Unit} {ingredient.Name} ({ingredient.Calories} calories, {ingredient.FoodGroup})\n"; //(In this line RecipeDetailsTextBox is picked up as an error.)
			}
			RecipeDetailsTextBox.Text += "Steps:\n"; 

			//Display Steps
			for (int i = 0; i < recipe.Steps.Count; i++)
			{
				RecipeDetailsTextBox.Text += $"{i + 1}. {recipe.Steps[i]}\n"; 
			}
			RecipeDetailsTextBox.Text += $"Total Calories: {recipe.TotalCalories()}\n"; 


			//Warning if the tatol calories exceed 300
			if (recipe.TotalCalories() > 300)
			{
				RecipeDetailsTextBox.Text += "Warning: The total calories exceed 300!\n"; 
			}
		}
	}
}
//******************************************************************END***************************************************************************************
