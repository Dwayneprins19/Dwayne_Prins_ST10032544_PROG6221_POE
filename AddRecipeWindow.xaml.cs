using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
using System.Windows.Shapes;

namespace RecipeManagerWPF
{
	/// <summary>
	/// Dwayne Prins
	/// Std:ST10032544
	/// Module:PRG6211
	/// </summary>
	public partial class AddRecipeWindow : Window
	{

		//This property is to retrieve the newly created recipe
		public Recipe NewRecipe { get; private set; } 
		public AddRecipeWindow()
		{
			InitializeComponent();
		}


		//The event handler for adding the ingredients dynamically
		private void AddIngredients_Click(object sender, RoutedEventArgs e)
		{

			//Clear existing ingredients
			IngredientsPanel.Children.Clear();

			//Parse the amount of ingredients from the text box input
			if (int.TryParse(NumIngredientsTextBox.Text, out int numIngredients))
			{

				//This is Ui created for each ingredient
				for (int i = 0; i < numIngredients; i++)
				{
					var ingredientPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };
					ingredientPanel.Children.Add(new TextBox { Text = "Name", Width = 150, Margin = new Thickness(5) });
					ingredientPanel.Children.Add(new TextBox { Text = "Quantity", Width = 100, Margin = new Thickness(5) });
					ingredientPanel.Children.Add(new TextBox { Text = "Unit", Width = 100, Margin = new Thickness(5) });
					ingredientPanel.Children.Add(new TextBox { Text = "Calories", Width = 100, Margin = new Thickness(5) });
					ingredientPanel.Children.Add(new TextBox { Text = "Food Group", Width = 100, Margin = new Thickness(5) });
					IngredientsPanel.Children.Add(ingredientPanel);
				}
			}
		}


		//The event handler for adding steps dynamically
		private void AddSteps_Click(object sender, RoutedEventArgs e)
		{
			StepsPanel.Children.Clear(); 

			//Parse the amount of steps from the text box input
			if (int.TryParse(NumStepsTextBox.Text, out int numSteps))
			{

				//To create a UI for each step
				for (int i = 0; i < numSteps; i++)
				{
					StepsPanel.Children.Add(new TextBox { Text = "Step", Margin = new Thickness(5), Width = 400 }); 
				}
			}
		}


		//The event handler for saving recipe
		private void SaveRecipe_Click(object sender, RoutedEventArgs e)
		{
			string recipeName = RecipeNameTextBox.Text;
			if (string.IsNullOrWhiteSpace(recipeName))
			{
				MessageBox.Show("Please enter a recipe name.");
				return;
			}

			Recipe recipe = new Recipe(recipeName);
			
			foreach (StackPanel ingredientPanel in IngredientsPanel.Children) 
			{
				var textBoxes = ingredientPanel.Children.OfType<TextBox>().ToList();
				string name = textBoxes[0].Text ;
				if (double.TryParse(textBoxes[1].Text, out double quantity) &&
					double.TryParse(textBoxes[3].Text, out double calories))
				{
					string unit = textBoxes[2].Text;
					string foodGroup = textBoxes[4].Text;
					recipe.AddIngredient(new Ingredient(name, quantity, unit, calories, foodGroup)); 
				}
				else
				{
					MessageBox.Show("Please enter valid quantities and calories.");
					return;
				}
			}


			//Iterate through each step text box to retrieve recipe steps
			foreach (TextBox stepTextBox in StepsPanel.Children)
			{
				recipe.AddStep(stepTextBox.Text);
			}

			NewRecipe = recipe;
			DialogResult = true;
			Close();
		}
	}
}

//******************************************************************END***************************************************************************************
