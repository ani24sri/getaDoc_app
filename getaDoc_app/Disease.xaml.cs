using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using getaDoc_app.Models;
using System.Collections.Generic;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace getaDoc_app
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Disease : Page
    {
        public Disease()
        {
            this.InitializeComponent();
        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Patients));
        }
        protected  override async void OnNavigatedTo(NavigationEventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = "";
                Task task = Task.Run(async () =>
                {
                    response = await client.GetStringAsync(App.baseUri2);
                });
                try
                {
                    task.Wait(); // Wait 
                    listView.ItemsSource = JsonConvert.DeserializeObject<List<diseaseData>>(response);
                    App._disease = JsonConvert.DeserializeObject<List<diseaseData>>(response)[0];
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
            var item = ((sender as ListView).SelectedItem as diseaseData);
            App._disease = item;
            name.Text = item.name.ToString();
            symptom1.Text = item.symptom1.ToString();
            symptom2.Text = item.symptom2.ToString();
            symptom3.Text = item.symptom3.ToString();
            symptom4.Text = item.symptom4.ToString();
            cure.Text = string.Join(", ", item.cure);
            desc.Text = string.Join(", ", item.desc);

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
                default:
                    break;
            }
        }
    }
}
