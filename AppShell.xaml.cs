using NomenDeutsch.Pages;

namespace NomenDeutsch
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(LearnPage), typeof(LearnPage));
            Routing.RegisterRoute(nameof(QuizArtikelPage), typeof(QuizArtikelPage));
            Routing.RegisterRoute(nameof(QuizEnglishPage), typeof(QuizEnglishPage));
            Routing.RegisterRoute(nameof(QuizPluralPage), typeof(QuizPluralPage));
        }

        protected override bool OnBackButtonPressed()
        {
            var location = Shell.Current.CurrentState.Location.OriginalString;

            if (location.EndsWith("/MainPage") || location.EndsWith("//MainPage"))
                return base.OnBackButtonPressed();

            Shell.Current.GoToAsync("//MainPage");
            return true;
        }
    }
}
