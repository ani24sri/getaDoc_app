using getaDoc_app.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace getaDoc_app
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class viewAppointments : Page
    {
        public viewAppointments()
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
                    response = await client.GetStringAsync(App.baseUri);
                });
                try
                {
                    task.Wait(); // Wait 
                    listView.ItemsSource = JsonConvert.DeserializeObject<List<Appointments>>(response);
                    App._appoint = JsonConvert.DeserializeObject<List<Appointments>>(response)[0];
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
            var item = ((sender as ListView).SelectedItem as Appointments);
            App._appoint = item;

            appDate.Text = item.appDate.ToString();
            availble.Text = item.availble.ToString();
            reason.Text = string.Join(", ", item.reason);

            // Change the UI 
            viewList.Visibility = Visibility.Collapsed;
            viewSingle.Visibility = Visibility.Visible;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Content.ToString())
            {
                case "View List":
                    viewList.Visibility = Visibility.Visible;
                    viewSingle.Visibility = Visibility.Collapsed;
                    break;
                case "Update":
                    this.Frame.Navigate(typeof(bookAppoint), false);
                    break;
                case "Delete":
                    this.Frame.Navigate(typeof(removeAppointment), null);
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
