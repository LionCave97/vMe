using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace vMe.ViewModels
{
    public class RobotViewModel :BaseViewModel
    {
        public RobotViewModel()
        {
            Title = "Robot";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
        }
        public ICommand OpenWebCommand { get; }
    }
}
