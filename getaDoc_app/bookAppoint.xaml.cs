using getaDoc_app.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using Windows.Web.Http;
using System.Text;
using System.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace getaDoc_app
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class bookAppoint : Page
    {

        public  bookAppoint()
        {          
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Must be a request to update 
            // Because if the "create" is null or true,  
            // it is to denote that the request is to create a new object 
            if (e.Parameter as bool? == false)
            {
                var user = App._appoint;
                userId.Text = user.id.ToString();
                appDate.Text = user.appDate.ToString();
                availbleCh.SelectedIndex =(Int32)user.availble; // Available or Unavailable 
                reason.Text = user.reason;

                actionButton.Content = "Update";
            }
            else
            {
                actionButton.Content = "Create";
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var user = new Appointments
            {
                id = Int32.Parse(userId.Text),
                appDate = DateTime.Parse(appDate.Text),
                reason = reason.Text,
                availble =(Availibility) availbleCh.SelectedIndex
            };
            var content = JsonConvert.SerializeObject(user);
            var post = new StringContent(content, Encoding.UTF8, "application/json");
            var put = new StringContent(content, Encoding.UTF8, "application/json");

             if ((sender as Button).Content.ToString() == "Create")
            {
                  try
                    {
                    await PostData(post);
                    this.Frame.Navigate(typeof(appointments));
                    }
                    catch (Exception ex)
                    {
                        await new MessageDialog(ex.Message).ShowAsync();
                    }
           }
           else
            {
                    try
                    {
                    await PutData(put);
                    this.Frame.Navigate(typeof(Doctors));
                    }
                    catch (Exception ex)
                    {
                        await new MessageDialog(ex.Message).ShowAsync();
                    }
            }        
            
        }

        public void ClientHeaderInfo(System.Net.Http.HttpClient client)
        {
            client.BaseAddress = App.baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public  async Task<string> PostData(HttpContent data)
        {
            HttpClientHandler handler = new HttpClientHandler { UseDefaultCredentials = true };
            using (var client = new System.Net.Http.HttpClient(handler))
            {
                ClientHeaderInfo(client);
                try
                {
                    await client.PostAsync(App.baseUri.ToString(),data);

                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
            return null;
        }
        public async Task<string> PutData(HttpContent data)
        {
            HttpClientHandler handler = new HttpClientHandler { UseDefaultCredentials = true };
            using (var client = new System.Net.Http.HttpClient(handler))
            {
                ClientHeaderInfo(client);
                try
                {
                    await client.PutAsync(App.baseUri.ToString(), data);

                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
            return null;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Patients));
        }
    }
}



