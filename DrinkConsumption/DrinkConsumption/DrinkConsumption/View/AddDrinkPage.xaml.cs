﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DrinkConsumption.ViewModel;

namespace DrinkConsumption.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddDrinkPage : ContentPage
	{
		public AddDrinkPage (AddDrinkViewModel viewModel)
		{
			InitializeComponent ();
            BindingContext = viewModel;
		}
	}
}