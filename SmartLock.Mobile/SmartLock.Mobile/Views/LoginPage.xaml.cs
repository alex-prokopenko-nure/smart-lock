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
		public LoginPage ()
		{
			InitializeComponent();

            var title = new Label
            {
                Text = "Welcome to CloudCakes",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            var aboutButton = new Button
            {
                Text = "About Us"
            };

            var register = new Button
            {
                Text = "Register"
            };

            // Here we are implementing a click event using lambda expressions
            // when a user clicks the `aboutButton` they will navigate to the
            // About Us page.
            aboutButton.Clicked += (object sender, EventArgs e) => {
                Navigation.PushAsync(new AboutPage());
            };

            // Navigation to the Signup Page (Note: We haven't created this page yet)
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

            // With the `PushModalAsync` method we navigate the user
            // the the orders page and do not give them an option to
            // navigate back to the Homepage by clicking the back button
            login.Clicked += (sender, e) => {
                Navigation.PushModalAsync(new ItemsPage());
            };

            Content = new StackLayout
            {
                Padding = 30,
                Spacing = 10,
                Children = { title, email, password, login, register, aboutButton }
            };
        }

	}
}