using System;
using System.Collections.Generic;

namespace StreetBridgeGame
{
    public class Player
    {
        // Ім'я гравця
        public string Name { get; private set; }

        // Карти в руці гравця
        public List<Card> Hand { get; private set; }

        // Очки гравця
        public int Score { get; private set; }

        // Конструктор для створення гравця з заданим ім'ям
        public Player(string name)
        {
            Name = name;
            Hand = new List<Card>();
            Score = 0;
        }

        // Метод для отримання карти
        public void ReceiveCard(Card card)
        {
            Hand.Add(card);
        }

        // Метод для гри карти
        public void PlayCard(Card card)
        {
            Hand.Remove(card);
        }

        // Метод для додавання очок
        public void AddScore(int score)
        {
            Score += score;
            if (Score == 125)
            {
                Score = 0;
            }
        }
    }
}
