using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace getaDoc_app
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class removeAppointment : Page
    {
        public removeAppointment()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {
                Action.Content = "Cancel";
                message.Text = string.Format("Are you sure you want to delete the appointment made at {0}?", App._appoint.appDate); 
            }
            else
            {
                message.Text = string.Format("Are you sure you want to delete the appointment made at {0}?", App._appoint.appDate);
                Action.Content = e.Parameter.ToString(); 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Content.ToString())
            {
                case "Cancel":
                    // Send a request to delete the user 
                    using (var client = new HttpClient())
                    {
                        Task task = Task.Run(async () =>
                        {
                            await client.DeleteAsync(App.baseUri + "/" + App._appoint.id.ToString());
                        });
                        task.Wait();
                    }
                    this.Frame.Navigate(typeof(Doctors));
                    break;
                case "Cancel2":
                    // Send a request to delete the user 
                    using (var client = new HttpClient())
                    {
                        Task task = Task.Run(async () =>
                        {
                            await client.DeleteAsync(App.baseUri + "/" + App._appoint.id.ToString());
                        });
                        task.Wait();
                    }
                    this.Frame.Navigate(typeof(Patients));
                    break;
                default:
                    break;
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if(Action.Content.ToString() == "Cancel")
            {
                this.Frame.Navigate(typeof(Doctors));
            }
            this.Frame.Navigate(typeof(Patients));
        }
    }

}

