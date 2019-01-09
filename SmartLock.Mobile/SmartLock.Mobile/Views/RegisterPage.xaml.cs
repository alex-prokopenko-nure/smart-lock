using Newtonsoft.Json;
using RestSharp;
using SmartLock.Mobile.Models;
using SmartLock.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartLock.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        RegisterViewModel viewModel;

        public RegisterPage ()
		{
            InitializeComponent();

            BindingContext = viewModel = new RegisterViewModel();

            var title = new Label
            {
                Text = "Sign up here",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            var register = new Button
            {
                Text = "Register"
            };

            var email = new Entry
            {
                Placeholder = "E-Mail",
            };

            var firstName = new Entry
            {
                Placeholder = "First Name",
            };

            var lastName = new Entry
            {
                Placeholder = "Last Name",
            };

            var username = new Entry
            {
                Placeholder = "Username",
            };

            var password = new Entry
            {
                Placeholder = "Password",
                IsPassword = true
            };

            register.Clicked += async (sender, e) => {
                var client = new RestClient("http://2d02c1bd.ngrok.io");
                var request = new RestRequest("api/Users/register", Method.POST);
                request.AddJsonBody(new
                {
                    Email = email.Text,
                    FirstName = firstName.Text,
                    LastName = lastName.Text,
                    Username = username.Text,
                    Password = password.Text
                });
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    await RootPage.NavigateFromMenu((int)MenuItemType.Login);
                }
            };

            Content = new StackLayout
            {
                Padding = 20,
                Spacing = 8,
                Children = { title, email, firstName, lastName, username, password, register }
            };
        }
	}
}