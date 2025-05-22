using NomenDeutsch.Models;

namespace NomenDeutsch.Pages
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLearnTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LearnPage");
        }
        private async void OnArtikelTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//QuizArtikelPage");
        }
        private async void OnMeaningTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//QuizEnglishPage");
        }
        private async void OnPluralTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//QuizPluralPage");
        }
    }
}
