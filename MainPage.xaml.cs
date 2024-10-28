namespace AnimalMatchingGame
{
    public partial class MainPage : ContentPage
    {
        private Button lastClicked;
        private int matchesFound = 0;
        private int tenthsOfSecondsElapsed = 0;

        public MainPage()
        {
            InitializeComponent();
            AnimalButtons.IsVisible = false;
        }

        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {
            AnimalButtons.IsVisible = true;
            PlayAgainButton.IsVisible = false;

            List<string> animalEmoji = new List<string>()
            {
                "🐙", "🐙",
                "🐡", "🐡",
                "🐘", "🐘",
                "🐳", "🐳",
                "🐪", "🐪",
                "🦕", "🦕",
                "🦘", "🦘",
                "🦔", "🦔",
            };

            foreach (var button in AnimalButtons.Children.OfType<Button>())
            {
                int index = Random.Shared.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                button.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }

            Dispatcher.StartTimer(TimeSpan.FromSeconds(0.1), TimerTick);
            lastClicked = null;
            matchesFound = 0;
            tenthsOfSecondsElapsed = 0;
        }

        private bool TimerTick()
        {
            tenthsOfSecondsElapsed++;
            TimeElapsed.Text = $"Time elapsed: {(tenthsOfSecondsElapsed / 10F):0.0} seconds";

            
            if (matchesFound == 8) 
            {
                PlayAgainButton.IsVisible = true;
                AnimalButtons.IsVisible = false;
                return false; 
            }

            return !PlayAgainButton.IsVisible; 
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var buttonClicked = (Button)sender;

            if (buttonClicked == lastClicked || buttonClicked.Text == "")
                return;

            if (lastClicked == null)
            {
                lastClicked = buttonClicked;
            }
            else
            {
                if (buttonClicked.Text == lastClicked.Text)
                {
                    matchesFound++;
                    lastClicked.Text = "";
                    buttonClicked.Text = "";
                }

                lastClicked = null;
            }
        }
    }
}

