using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace vMe.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public ProfileViewModel()
        {
            Title = "Profile";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
        }
        public ICommand OpenWebCommand { get; }
    }
}
