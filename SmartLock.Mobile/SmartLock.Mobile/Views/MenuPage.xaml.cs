using SmartLock.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartLock.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Login, Title = "Login"},
                new HomeMenuItem {Id = MenuItemType.Register, Title = "Register"}
            };

            ListViewMenu.ItemsSource = menuItems;
            ListViewMenu.SelectedItem = menuItems.FirstOrDefault(x => x.Id == MenuItemType.Login);

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }

        public void Login()
        {
            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem { Id = MenuItemType.Browse, Title = "Browse" },
                new HomeMenuItem { Id = MenuItemType.About, Title = "About" }
            };

            ListViewMenu.ItemsSource = menuItems;
            ListViewMenu.SelectedItem = menuItems.FirstOrDefault(x => x.Id == MenuItemType.Browse);
        }

        public void Logout()
        {
            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Login, Title = "Login"},
                new HomeMenuItem {Id = MenuItemType.Register, Title = "Register"}
            };

            ListViewMenu.ItemsSource = menuItems;
            ListViewMenu.SelectedItem = menuItems.FirstOrDefault(x => x.Id == MenuItemType.Login);
        }
    }
}