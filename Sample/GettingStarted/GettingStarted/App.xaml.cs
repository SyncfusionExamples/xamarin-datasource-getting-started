using Syncfusion.DataSource;
using Syncfusion.DataSource.Extensions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GettingStarted
{
    public partial class App : Application
    {
        #region Field 

        ListView listView;

        #endregion
        public App()
        {
            DataSource dataSource = new DataSource();
            dataSource.Source = new ContactsList();
            dataSource.SortDescriptors.Add(new SortDescriptor("ContactName"));
            dataSource.GroupDescriptors.Add(new GroupDescriptor()
            {
                PropertyName = "ContactName",
                KeySelector = (object obj1) =>
                {
                    var item = (obj1 as Contacts);
                    return item.ContactName[0].ToString();
                }
            });

            StackLayout stack = new StackLayout();
            stack.Children.Add(new Label()
            {
                TextColor = Color.Black,
                FontSize = 14,
                HeightRequest = 50,
                Text = "Contact List",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.Gray
            });

            listView = new ListView() { RowHeight = 30 };
            listView.ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label()
                {
                    TextColor = Color.Black,
                    FontSize = 12,
                    VerticalTextAlignment = TextAlignment.Center,
                    BackgroundColor = Color.White,
                };
                label.SetBinding(Label.TextProperty, new Binding("ContactName"));
                var viewCell = new ViewCell() { View = label };
                viewCell.BindingContextChanged += ViewCell_BindingContextChanged;
                return viewCell;
            });
            listView.ItemsSource = dataSource.DisplayItems;
            stack.Children.Add(listView);
            MainPage = new ContentPage { Content = stack };
            Device.OnPlatform(iOS: () => MainPage.Padding = new Thickness(0, 20, 0, 0));
        }

        private void ViewCell_BindingContextChanged(object sender, EventArgs e)
        {
            var viewCell = sender as ViewCell;
            if (viewCell.BindingContext is GroupResult)
            {
                var label = new Label()
                {
                    TextColor = Color.Black,
                    FontSize = 14,
                    HeightRequest = 30,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    BackgroundColor = Color.Gray
                };
                label.SetBinding(Label.TextProperty, new Binding("Key"));
                viewCell.View = label;
            }
        }



        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
