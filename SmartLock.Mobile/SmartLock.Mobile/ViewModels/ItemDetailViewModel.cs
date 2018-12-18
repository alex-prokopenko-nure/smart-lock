using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using SmartLock.Mobile.Models;
using Xamarin.Forms;

namespace SmartLock.Mobile.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ObservableCollection<LockOperation> Operations { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemDetailViewModel(Lock item)
        {
            Title = $"Lock #{item.Id} operations";
            Operations = new ObservableCollection<LockOperation>();
            var userId = (int)Application.Current.Properties["user_id"];
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand(item, userId));
            ExecuteLoadItemsCommand(item, userId);
        }

        async Task ExecuteLoadItemsCommand(Lock item, int userId)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Operations.Clear();
                var client = new RestClient("http://383920c3.ngrok.io");
                var request = new RestRequest($"/api/Locks/{item.Id}/operations?userId={userId}", Method.GET);
                request.AddHeader("Authorization", $"Bearer {Application.Current.Properties["jwt_token"].ToString()}");
                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var operations = JsonConvert.DeserializeObject<List<LockOperation>>(response.Content);
                    foreach (var operation in operations)
                    {
                        Operations.Add(operation);
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
