using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SmartLock.Mobile.Models;
using SmartLock.Mobile.ViewModels;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;

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

        private void ChangeLockState(object sender, EventArgs e)
        {
            var client = new RestClient("http://2d02c1bd.ngrok.io");
            RestRequest request;
            RestRequest getLockRequest = new RestRequest($"/api/Locks/{viewModel.Item.Id}", Method.GET); ;
            if (viewModel.State == "Open")
            {
                request = new RestRequest($"/api/Locks/{viewModel.Item.Id}/open", Method.POST);
            }
            else
            {
                request = new RestRequest($"/api/Locks/{viewModel.Item.Id}/close", Method.POST);
            }
            request.AddHeader("Authorization", $"Bearer {Application.Current.Properties["jwt_token"].ToString()}");
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                getLockRequest.AddHeader("Authorization", $"Bearer {Application.Current.Properties["jwt_token"].ToString()}");
                IRestResponse lockResponse = client.Execute(getLockRequest);
                var lockModel = JsonConvert.DeserializeObject<Lock>(lockResponse.Content);
                Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(lockModel)));
            }
        }
    }
}