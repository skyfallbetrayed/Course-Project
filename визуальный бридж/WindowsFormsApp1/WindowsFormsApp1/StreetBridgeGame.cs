using StreetBridgeWindowsApp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StreetBridgeGame
{
    public class StreetBridgeGame
    {
        // Список гравців у грі
        public List<Player> Players { get; private set; }

        // Колода карт для гри
        private Deck deck;

        // Остання зіграна карта
        private Card lastPlayedCard;

        // Зазначена масть (якщо грається Валет)
        public Card.Suit? RequestedSuit { get; set; }

        // Індекс поточного гравця
        public int CurrentPlayerIndex { get; private set; }

        // Конструктор для ініціалізації гри з заданими іменами гравців
        public StreetBridgeGame(List<string> playerNames)
        {
            // Ініціалізація списку гравців
            Players = new List<Player>();
            foreach (var name in playerNames)
            {
                Players.Add(new Player(name));
            }

            // Створення нової колоди карт
            deck = new Deck();

            // Ініціалізація інших параметрів гри
            lastPlayedCard = null;
            RequestedSuit = null;
            CurrentPlayerIndex = 0;
        }

        // Метод для роздачі початкових карт кожному гравцеві
        public void DealInitialCards()
        {
            // Роздача 5 карт кожному гравцеві
            foreach (var player in Players)
            {
                for (int i = 0; i < 5; i++)
                {
                    player.ReceiveCard(deck.Draw());
                }
            }
        }

        // Метод для гри раунду з введеними даними
        public void PlayRound(string input)
        {
            // Перевірка наявності гравців у грі
            if (Players.Count == 0)
            {
                throw new InvalidOperationException("Немає гравців у грі.");
            }

            // Перевірка введення на порожнечу
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Введення не може бути порожнім або відсутнім.");
            }

            var player = Players[CurrentPlayerIndex];

            // Обробка команди на взяття карти
            if (input.ToLower() == "draw")
            {
                DrawCardForPlayer(player);
                return;
            }

            // Перевірка введеного значення карти
            var cardInput = input.Split(' ');
            if (cardInput.Length != 2)
            {
                throw new ArgumentException("Невірне введення. Введіть карту як 'Значення Масть'.");
            }

            // Перевірка, чи існує введена карта у руці гравця
            if (Enum.TryParse(cardInput[0], out Card.Value value) && Enum.TryParse(cardInput[1], out Card.Suit suit))
            {
                var card = player.Hand.FirstOrDefault(c => c.CardValue == value && c.CardSuit == suit);
                if (card != null)
                {
                    if (CanPlayCard(card))
                    {
                        PlayCard(player, card);
                        player.AddScore(CalculateScore(card));
                    }
                    else
                    {
                        throw new InvalidOperationException("Неможливо зіграти цю карту через невідповідність правилам гри.");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Карти немає у руці гравця.");
                }
            }
            else
            {
                throw new ArgumentException("Невірне значення карти або масть.");
            }

            // Якщо у гравця немає карт після ходу, він виграє
            if (player.Hand.Count == 0)
            {
                return;
            }

            // Переключення на наступного гравця
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
        }

        // Метод для додавання карти гравцеві
        private void DrawCardForPlayer(Player player)
        {
            if (deck.IsEmpty())
            {
                throw new InvalidOperationException("Колода порожня. Неможливо взяти карту.");
            }
            var drawnCard = deck.Draw();
            player.ReceiveCard(drawnCard);
        }

        // Метод для гри карти
        private void PlayCard(Player player, Card card)
        {
            player.PlayCard(card);
            lastPlayedCard = card;
            HandleSpecialCards(card, player);
        }

        // Метод для перевірки наявності переможця
        public bool HasWinner()
        {
            return Players.Any(player => player.Hand.Count == 0);
        }

        // Метод для отримання переможця гри
        public Player GetWinner()
        {
            return Players.FirstOrDefault(player => player.Hand.Count == 0);
        }

        // Метод для отримання останньої зіграної карти
        public string GetLastPlayedCard()
        {
            return lastPlayedCard?.ToString() ?? "Немає";
        }

        // Метод для перевірки, чи можна зіграти карту
        private bool CanPlayCard(Card card)
        {
            if (RequestedSuit.HasValue)
            {
                return card.CardSuit == RequestedSuit.Value;
            }
            if (lastPlayedCard == null)
            {
                return true;
            }
            return card.CardSuit == lastPlayedCard.CardSuit || card.CardValue == lastPlayedCard.CardValue || card.CardValue == Card.Value.Валет;
        }

        // Обробка спеціальних карт
        private void HandleSpecialCards(Card card, Player player)
        {
            switch (card.CardValue)
            {
                case Card.Value.Шість:
                    DrawUntilNoSix(player);
                    break;
                case Card.Value.Сім:
                    var nextPlayer1 = NextPlayer();
                    nextPlayer1.ReceiveCard(deck.Draw());
                    break;
                case Card.Value.Вісім:
                    var nextPlayer2 = NextPlayer();
                    nextPlayer2.ReceiveCard(deck.Draw());
                    nextPlayer2.ReceiveCard(deck.Draw());
                    SkipNextPlayer();
                    break;
                case Card.Value.Валет:
                    Form1.Instance.RequestNewSuit();
                    break;
                case Card.Value.Туз:
                    SkipNextPlayer();
                    break;
                default:
                    RequestedSuit = null;
                    break;
            }
        }

        // Алгоритм при ході картою Шість: гравець бере карти, доки не витягне щось окрім шести
        private void DrawUntilNoSix(Player player)
        {
            while (true)
            {
                if (deck.IsEmpty())
                {
                    break;
                }
                var drawnCard = deck.Draw();
                player.ReceiveCard(drawnCard);
                if (drawnCard.CardValue != Card.Value.Шість)
                {
                    break;
                }
            }
        }

        // Метод для отримання наступного гравця
        private Player NextPlayer()
        {
            return Players[(CurrentPlayerIndex + 1) % Players.Count];
        }

        // Метод для пропуску наступного гравця
        private void SkipNextPlayer()
        {
            CurrentPlayerIndex = (CurrentPlayerIndex + 2) % Players.Count;
        }

        // Метод для обчислення балів за зіграну карту
        private int CalculateScore(Card card)
        {
            return (int)card.CardValue;
        }
    }
}
