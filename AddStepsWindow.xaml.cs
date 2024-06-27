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
	/// Interaction logic for AddStepsWindow.xaml
	/// </summary>
	public partial class AddStepsWindow : Window
	{
		public List<string> Steps {  get; private set; }

		// The constructor
		public AddStepsWindow(int numIngredients)
		{
			InitializeComponent();
		}


		//The event handler for adding steps
		private void AddSteps_Click(object sender, RoutedEventArgs e)
		{

			//Split the text in the StepstextBox
			Steps = new List<string>(StepsTextBox.Text.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None));
			DialogResult = true;
			Close();
		}
	}
}

//******************************************************************END***************************************************************************************