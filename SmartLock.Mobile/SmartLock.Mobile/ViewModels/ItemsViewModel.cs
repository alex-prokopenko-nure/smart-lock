using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using SmartLock.Mobile.Models;
using SmartLock.Mobile.Views;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartLock.Mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Lock> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel(int userId)
        {
            Title = "Your Locks";
            Items = new ObservableCollection<Lock>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(userId));
        }

        async Task ExecuteLoadItemsCommand(int userId)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var client = new RestClient("http://c6838756.ngrok.io");
                var request = new RestRequest($"/api/Locks/all-locks/{userId}", Method.GET);
                request.AddHeader("Authorization", $"Bearer {Application.Current.Properties["jwt_token"].ToString()}");
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var lockRents = JsonConvert.DeserializeObject<List<LockRent>>(response.Content);
                    foreach (var rent in lockRents)
                    {
                        Items.Add(rent.Lock);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}