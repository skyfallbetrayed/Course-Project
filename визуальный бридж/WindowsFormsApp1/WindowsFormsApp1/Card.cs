using System;

namespace StreetBridgeGame
{
    public class Card
    {
        // Визначення мастей карт
        public enum Suit
        {
            Черви,
            Бубни,
            Хрести,
            Піки
        }

        // Визначення значень карт
        public enum Value
        {
            Шість = 6, Сім, Вісім, Девять, Десять, Валет, Дама, Король, Туз
        }

        // Масть карти
        public Suit CardSuit { get; private set; }

        // Значення карти
        public Value CardValue { get; private set; }

        // Конструктор для створення карти з заданими мастю та значенням
        public Card(Suit suit, Value value)
        {
            CardSuit = suit;
            CardValue = value;
        }

        // Перевизначення методу ToString для зручного виведення карти
        public override string ToString()
        {
            return $"{CardValue} {CardSuit}";
        }
    }
}
