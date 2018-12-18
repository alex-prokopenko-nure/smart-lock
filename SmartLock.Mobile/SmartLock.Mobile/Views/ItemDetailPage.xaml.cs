using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SmartLock.Mobile.Models;
using SmartLock.Mobile.ViewModels;

namespace SmartLock.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}