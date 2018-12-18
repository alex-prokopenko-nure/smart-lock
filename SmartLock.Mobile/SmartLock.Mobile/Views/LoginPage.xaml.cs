using Newtonsoft.Json;
using RestSharp;
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
	public partial class LoginPage : ContentPage
	{
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        LoginViewModel viewModel;

        public LoginPage ()
		{
			InitializeComponent();

            BindingContext = viewModel = new LoginViewModel();

            var title = new Label
            {
                Text = "Welcome to SmartLock",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            var register = new Button
            {
                Text = "Register"
            };

            register.Clicked += (object sender, EventArgs e) => {
                Navigation.PushAsync(new RegisterPage());
            };

            var email = new Entry
            {
                Placeholder = "E-Mail",
            };

            var password = new Entry
            {
                Placeholder = "Password",
                IsPassword = true
            };

            var login = new Button
            {
                Text = "Login"
            };

            login.Clicked += async (sender, e) => {
                var client = new RestClient("http://9fcc8378.ngrok.io");
                var request = new RestRequest("api/Users/login", Method.POST);
                request.AddJsonBody(new
                {
                    Email = email.Text,
                    Password = password.Text
                });
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    string jwtToken = JsonConvert.DeserializeObject<string>(response.Content);
                    Application.Current.Properties["jwt_token"] = jwtToken;
                    await RootPage.Login();
                    await Navigation.PushModalAsync(new ItemsPage());
                }
            };

            Content = new StackLayout
            {
                Padding = 30,
                Spacing = 10,
                Children = { title, email, password, login, register }
            };
        }

	}
}