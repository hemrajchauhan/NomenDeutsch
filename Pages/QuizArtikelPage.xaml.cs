using NomenDeutsch.Models;

namespace NomenDeutsch.Pages
{
    [QueryProperty(nameof(QuizMode), "QuizMode")]
    public partial class QuizArtikelPage : ContentPage
    {
        string quizMode;
        public string QuizMode
        {
            get => quizMode;
            set
            {
                quizMode = value;
                // Use the quizMode to set up your quiz logic here
            }
        }

        public QuizArtikelPage()
	    {
		    InitializeComponent();
        }
    }
}