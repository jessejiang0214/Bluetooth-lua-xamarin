using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BluetoothLua
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel();
        }
        private double width;
        private double height;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (width > height)
                {
                    //Landscape
                    mainGrid.RowDefinitions.Clear();
                    mainGrid.ColumnDefinitions.Clear();
                    mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40, GridUnitType.Absolute) });
                    mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    mainGrid.Children.Remove(CodeView);
                    mainGrid.Children.Remove(RunButton);
                    mainGrid.Children.Remove(LogView);
                    mainGrid.Children.Add(CodeView, 0, 0);
                    mainGrid.Children.Add(RunButton, 1, 0);
                    mainGrid.Children.Add(LogView, 2, 0);
                    RunButton.HorizontalOptions = LayoutOptions.Center;
                    RunButton.VerticalOptions = LayoutOptions.Center;
                    this.Padding = new Thickness(0);
                }
                else
                {
                    //Portrait
                    mainGrid.ColumnDefinitions.Clear();
                    mainGrid.RowDefinitions.Clear();
                    mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20, GridUnitType.Absolute) });
                    mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    mainGrid.Children.Remove(CodeView);
                    mainGrid.Children.Remove(RunButton);
                    mainGrid.Children.Remove(LogView);
                    mainGrid.Children.Add(CodeView, 0, 0);
                    mainGrid.Children.Add(RunButton, 0, 1);
                    mainGrid.Children.Add(LogView, 0, 2);
                    RunButton.HorizontalOptions = LayoutOptions.End;
                    RunButton.VerticalOptions = LayoutOptions.Center;
                    this.Padding = new Thickness(0, 40, 0, 0);
                }
            }
        }
    }
}
