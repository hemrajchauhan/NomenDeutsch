using NomenDeutsch.Models;

namespace NomenDeutsch.Pages
{
    public partial class LearnPage : ContentPage
    {
        private List<NounNeu> _allNouns = new();
        private List<NounNeu> _currentOrder = new();
        private int _currentIndex = 0;

        public LearnPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            _allNouns = await App.Database.GetNounsAsync();

            // 1. Restore or create shuffled order
            string savedOrder = Preferences.Default.Get("NounOrder", "");
            List<int> orderedIds;
            if (string.IsNullOrEmpty(savedOrder))
            {
                orderedIds = _allNouns.Select(n => n.ID).ToList();
                Shuffle(orderedIds);
                SaveNounOrder(orderedIds);
                _currentIndex = 0;
                Preferences.Default.Set("LastNounIndex", 0);
            }
            else
            {
                orderedIds = savedOrder.Split(',').Select(int.Parse).ToList();
                _currentIndex = Preferences.Default.Get("LastNounIndex", 0);
            }

            // 2. Build the current order
            var dict = _allNouns.ToDictionary(n => n.ID);
            _currentOrder = orderedIds.Where(dict.ContainsKey).Select(id => dict[id]).ToList();

            // Clamp index if needed
            if (_currentIndex >= _currentOrder.Count) _currentIndex = 0;

            ShowNoun();
        }

        private void ShowNoun()
        {
            if (_currentOrder != null && _currentOrder.Any())
            {
                var noun = _currentOrder[_currentIndex];
                FullNounLabel.Text = noun.Full_Noun;
                PluralLabel.Text = "Plural: " + noun.Plural;
                EnglishLabel.Text = "English: " + noun.English;
                BeispielLabel.Text = noun.Beispiel;
                HintLabel.Text = noun.Hint ?? "";
                Preferences.Default.Set("LastNounIndex", _currentIndex);
            }
            else
            {
                FullNounLabel.Text = "NO DATA";
                PluralLabel.Text = "";
                EnglishLabel.Text = "";
                BeispielLabel.Text = "";
                HintLabel.Text = "";
            }
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            if (_currentOrder == null) return;

            _currentIndex++;
            if (_currentIndex >= _currentOrder.Count)
            {
                // Finished round: reshuffle!
                var orderedIds = _allNouns.Select(n => n.ID).ToList();
                Shuffle(orderedIds);
                SaveNounOrder(orderedIds);
                _currentOrder = orderedIds.Select(id => _allNouns.First(n => n.ID == id)).ToList();
                _currentIndex = 0;
            }

            ShowNoun();
        }

        private void OnPreviousClicked(object sender, EventArgs e)
        {
            if (_currentOrder == null || _currentIndex <= 0) return;
            _currentIndex--;
            ShowNoun();
        }

        // --- Shuffle and persistence ---
        private void SaveNounOrder(List<int> order)
        {
            Preferences.Default.Set("NounOrder", string.Join(",", order));
        }

        // Fisher-Yates shuffle
        private static void Shuffle<T>(IList<T> list)
        {
            var rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}
