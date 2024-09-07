using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using StreetBridgeGame;

namespace StreetBridgeWindowsApp
{
    public partial class Form1 : Form
    {
        // Об'єкт гри
        private StreetBridgeGame.StreetBridgeGame game;

        // Кількість гравців
        private int playerCount = 4;

        // Синглтон для доступу до форми
        private static Form1 instance;
        public static Form1 Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new Form1();
                }
                return instance;
            }
        }

        // Конструктор форми
        private Form1()
        {
            InitializeComponent();
        }

        // Метод, що викликається при завантаженні форми
        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeGame();
        }

        
        

        // Метод, що викликається при натисканні кнопки "Наступний хід"
        private void nextTurnButton_Click(object sender, EventArgs e)
        {
            var input = inputTextBox.Text;
            if (!string.IsNullOrEmpty(input))
            {
                try
                {
                    var currentPlayer = game.Players[game.CurrentPlayerIndex];
                    game.PlayRound(input);
                    LogEvent($"{currentPlayer.Name} зіграв {input}.");

                    if (game.HasWinner())
                    {
                        var winner = game.GetWinner();
                        LogEvent($"{winner.Name} виграв гру, позбувшись усіх карт!");
                        MessageBox.Show($"{winner.Name} виграв гру, позбувшись усіх карт!");
                        InitializeGame();
                    }
                    else
                    {
                        UpdateHandDisplay();
                        UpdatePlayerScores(); // Оновлення міток з інформацією про рахунки гравців
                        UpdateLastPlayedCardLabel();
                        UpdateCurrentPlayerLabel();
                        inputTextBox.Clear();
                    }
                }
                catch (Exception ex)
                {
                    LogEvent($"Сталася помилка: {ex.Message}");
                    MessageBox.Show($"Сталася помилка: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть карту.");
            }
        }

        // Метод, що викликається при натисканні кнопки "Взяти карту"
        private void drawCardButton_Click(object sender, EventArgs e)
        {
            try
            {
                var currentPlayer = game.Players[game.CurrentPlayerIndex];
                game.PlayRound("draw");
                LogEvent($"{currentPlayer.Name} взяв карту.");
                UpdateHandDisplay();
                UpdatePlayerScores(); // Оновлення міток з інформацією про рахунки гравців
                UpdateLastPlayedCardLabel();
                UpdateCurrentPlayerLabel();
                inputTextBox.Clear();
            }
            catch (Exception ex)
            {
                LogEvent($"Сталася помилка: {ex.Message}");
                MessageBox.Show($"Сталася помилка: {ex.Message}");
            }
        }

        // Ініціалізація гри
        private void InitializeGame()
        {
            // Імена гравців
            var playerNames = new List<string> { "Марійка", "Сашко", "Аня", "Артем" };

            // Створення об'єкта гри
            game = new StreetBridgeGame.StreetBridgeGame(playerNames);
            game.DealInitialCards();

            InitializePlayerLabels();
            DisplayHands();
            UpdateLastPlayedCardLabel();
            UpdateCurrentPlayerLabel();
            eventLogListBox.Items.Clear();
            eventLogListBox.Items.Add("Гру розпочато.");
        }

        // Логування події у грі
        private void LogEvent(string message)
        {
            eventLogListBox.Items.Add(message);
            eventLogListBox.TopIndex = eventLogListBox.Items.Count - 1;
        }

        // Метод для оновлення рук гравців
        private void UpdateHandDisplay()
        {
            foreach (var playerButtons in handRadioButtonGroups)
            {
                if (playerButtons != null)
                {
                    foreach (var button in playerButtons)
                    {
                        this.Controls.Remove(button);
                    }
                }
            }
            DisplayHands();
        }

        // Метод для виведення імені та рахунку гравців
        private void InitializePlayerLabels()
        {
            playerLabels = new Label[playerCount];

            for (int i = 0; i < playerCount; i++)
            {
                playerLabels[i] = new Label
                {
                    Location = new Point(10, 10 + i * 50),
                    Size = new Size(200, 20),
                    Text = $"{game.Players[i].Name} (Рахунок: {game.Players[i].Score})"
                };
                this.Controls.Add(playerLabels[i]);
            }
        }

        // Метод для виведення поточних рук гравців
        private void DisplayHands()
        {
            handRadioButtonGroups = new RadioButton[playerCount][];
            for (int i = 0; i < playerCount; i++)
            {
                var player = game.Players[i];
                handRadioButtonGroups[i] = new RadioButton[player.Hand.Count];
                for (int j = 0; j < player.Hand.Count; j++)
                {
                    handRadioButtonGroups[i][j] = new RadioButton
                    {
                        Location = new Point(220 + j * 100, 10 + i * 50),
                        Size = new Size(100, 20),
                        Text = player.Hand[j].ToString(),
                        Tag = player.Hand[j]
                    };
                    handRadioButtonGroups[i][j].CheckedChanged += new EventHandler(this.CardRadioButton_CheckedChanged);
                    handRadioButtonGroups[i][j].Enabled = (i == game.CurrentPlayerIndex);
                    this.Controls.Add(handRadioButtonGroups[i][j]);
                }
            }
        }

        // Оновлення останньої зіграної карти
        private void UpdateLastPlayedCardLabel()
        {
            lastPlayedCardLabel.Text = $"Остання грана карта: {game.GetLastPlayedCard()}";
        }

        // Оновлення імені поточного гравця
        private void UpdateCurrentPlayerLabel()
        {
            var currentPlayer = game.Players[game.CurrentPlayerIndex];
            currentPlayerLabel.Text = $"Поточний гравець: {currentPlayer.Name}";
        }

        // Виведення поточної вибраної карти у текстове поле
        private void CardRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.Checked)
            {
                var selectedCard = radioButton.Tag as Card;
                inputTextBox.Text = $"{selectedCard.CardValue} {selectedCard.CardSuit}";
            }
        }

        // Оновлення очок гравців
        private void UpdatePlayerScores()
        {
            for (int i = 0; i < playerCount; i++)
            {
                playerLabels[i].Text = $"{game.Players[i].Name} (Рахунок: {game.Players[i].Score})";
            }
        }

        // Ініціалізація форми для вибору масті
        private void InitializeSuitSelectionForm()
        {
            suitSelectionForm = new Form
            {
                Size = new Size(300, 200),
                Text = "Виберіть масть"
            };

            heartsButton = new Button
            {
                Text = "Черви",
                Location = new Point(30, 30),
                Size = new Size(75, 50)
            };
            heartsButton.Click += (sender, e) => SelectSuit(Card.Suit.Черви);

            diamondsButton = new Button
            {
                Text = "Бубни",
                Location = new Point(120, 30),
                Size = new Size(75, 50)
            };
            diamondsButton.Click += (sender, e) => SelectSuit(Card.Suit.Бубни);

            clubsButton = new Button
            {
                Text = "Хрести",
                Location = new Point(30, 100),
                Size = new Size(75, 50)
            };
            clubsButton.Click += (sender, e) => SelectSuit(Card.Suit.Хрести);

            spadesButton = new Button
            {
                Text = "Піки",
                Location = new Point(120, 100),
                Size = new Size(75, 50)
            };
            spadesButton.Click += (sender, e) => SelectSuit(Card.Suit.Піки);

            suitSelectionForm.Controls.Add(heartsButton);
            suitSelectionForm.Controls.Add(diamondsButton);
            suitSelectionForm.Controls.Add(clubsButton);
            suitSelectionForm.Controls.Add(spadesButton);
        }

        // Метод для вибору масті
        private void SelectSuit(Card.Suit suit)
        {
            game.RequestedSuit = suit;
            suitSelectionForm.Close();
        }

        // Метод для запиту нової масті
        public void RequestNewSuit()
        {
            InitializeSuitSelectionForm();
            suitSelectionForm.ShowDialog();
        }
    }
}
