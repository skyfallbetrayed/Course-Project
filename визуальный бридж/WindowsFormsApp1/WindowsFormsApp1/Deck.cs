using System;
using System.Collections.Generic;

namespace StreetBridgeGame
{
    public class Deck
    {
        // Колода карт
        private List<Card> cards;

        // Генератор випадкових чисел для перемішування карт
        private Random random = new Random();

        // Конструктор для створення нової колоди карт
        public Deck()
        {
            // Ініціалізація списку карт
            cards = new List<Card>();

            // Прохід по всім мастям і значенням, щоб створити повну колоду
            foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
            {
                foreach (Card.Value value in Enum.GetValues(typeof(Card.Value)))
                {
                    // Додавання нової карти в колоду
                    cards.Add(new Card(suit, value));
                }
            }

            // Перемішування карт після створення
            Shuffle();
        }

        // Метод для перемішування карт у колоді
        public void Shuffle()
        {
            for (int i = cards.Count - 1; i > 0; i--)
            {
                // Вибір випадкового індексу для перемішування
                int j = random.Next(i + 1);

                // Обмін значеннями двох карт
                var temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        // Метод для взяття карти з колоди
        public Card Draw()
        {
            // Перевірка, чи не порожня колода
            if (cards.Count == 0)
                throw new InvalidOperationException("Колода порожня.");

            // Витягування верхньої карти
            var card = cards[0];
            cards.RemoveAt(0);
            return card;
        }

        // Метод для перевірки, чи порожня колода
        public bool IsEmpty()
        {
            return cards.Count == 0;
        }
    }
}
