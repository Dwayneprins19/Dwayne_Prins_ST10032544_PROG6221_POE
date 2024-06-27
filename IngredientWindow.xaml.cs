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
using System.Windows.Shapes;

namespace RecipeManagerWPF
{
	/// <summary>
	/// Dwayne Prins
	/// Std:ST10032544
	/// Module:PRG6211
	/// Interaction logic for IngredientWindow.xaml
	/// </summary>
	public partial class IngredientWindow : Window
	{
		public Ingredient Ingredient { get; private set; }
		public IngredientWindow()
		{
			InitializeComponent();
		}


		//The event handler for adding an ingredient
		public void AddIngredient_Click(object sender, RoutedEventArgs e)
		{
			string name = IngredientNameTextBox.Text;
			if (double.TryParse(QuantityTextBox.Text, out double quantity) &&
				double.TryParse(CaloriesTextBox.Text, out double calories))
			{
				string unit = UnitTextBox.Text;
				string foodGroup = FoodGroupTextBox.Text;

				//The is to create a new Ingredient object with the entered data
				Ingredient = new Ingredient(name, quantity, unit, calories, foodGroup);
				DialogResult = true;
				Close();
			}
			else 
			{
				// To show a warnig message
				MessageBox.Show("Please enter valid values for quantity and calories.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}
	}
}
//******************************************************************END***************************************************************************************