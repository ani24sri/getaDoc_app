using getaDoc_app.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.Calls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace getaDoc_app
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class listPatients : Page
    {
        public listPatients()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(App.baseUri3);
                });
                try
                {
                    task.Wait(); // Wait 
                    listView.ItemsSource = JsonConvert.DeserializeObject<List<PatientsModel>>(response);
                    App._patients = JsonConvert.DeserializeObject<List<PatientsModel>>(response)[0];
                }
                catch (Exception ex)
                {
                    Windows.UI.Popups.MessageDialog obj = new Windows.UI.Popups.MessageDialog(ex.Message);
                    await obj.ShowAsync();
                }
            }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ((sender as ListView).SelectedItem as PatientsModel);
            App._patients = item;
            name.Text = item.name.ToString();
            symptoms.Text = item.symptoms.ToString();
            phNo.Text = item.phNo.ToString();
            patientNo.Text = string.Join(", ", item.patientNo);

            // Change the UI 
            viewList.Visibility = Visibility.Collapsed;
            viewSingle.Visibility = Visibility.Visible;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Content.ToString())
            {
                case "Contact Patient":
                    PhoneCallStore PhoneCallStore = await PhoneCallManager.RequestStoreAsync();
                    Guid LineGuid = await PhoneCallStore.GetDefaultLineAsync();
                    var phoneCall = await PhoneLine.FromIdAsync(LineGuid);
                    phoneCall.Dial(phNo.Text, name.Text);
                    break;
                case "View List":
                    viewList.Visibility = Visibility.Visible;
                    viewSingle.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Doctors));
        }
    }
}
