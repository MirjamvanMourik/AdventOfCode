using AdventOfCode2023.Input;
using AdventOfCode2023.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    public class Day4 : IDay
    {
        long IDay.Day => 4;

        public long GetFirstAnswer()
        {
            var input = InputSplitter.SplitPerLine(Day4Cards.Input);
            var cards = CreateCards(input);
            return CalculateScore(cards);
        }

        public long GetSecondAnswer()
        {
            var input = InputSplitter.SplitPerLine(Day4Cards.Input);
            var cards = CreateCards(input);
            return CalculateAmountOfCards(cards);
        }

        private static long CalculateScore(List<Card> cards)
        {
            var totalScore = 0;

            foreach (var card in cards)
            {
                var cardScore = 0;

                foreach (var winningNumber in card.winningNumbers)
                {
                    if (card.myNumbers.Contains(winningNumber))
                    {
                        if (cardScore == 0)
                        {
                            cardScore = 1;
                        } 
                        else
                        {
                            cardScore *= 2;
                        }
                    }
                }

                totalScore += cardScore;
            }

            return totalScore;
        }

        private static List<Card> CreateCards(string[] input)
        {
            var list = new List<Card>();

            foreach (var card in input)
            {
                var currentCard = new Card
                {
                    cardNumer = int.Parse(card.Split(":")[0].ToLower().Replace("card ", ""))
                };

                var numbers = card.Split(":")[1];
                var winningNumbers = numbers.Split("|")[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var myNumbers = numbers.Split("|")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                foreach (var wNr in winningNumbers)
                {
                    currentCard.winningNumbers.Add(int.Parse(wNr));
                }

                foreach (var mNr in myNumbers)
                {
                    currentCard.myNumbers.Add(int.Parse(mNr));
                }

                list.Add(currentCard);
            }

            return list;
        }

        private long CalculateAmountOfCards(List<Card> cards)
        {
            foreach (var card in cards)
            {
                var winningNumbersAmount = 0;

                foreach (var winningNumber in card.winningNumbers)
                {
                    if (card.myNumbers.Contains(winningNumber))
                    {
                        winningNumbersAmount++;
                    }
                }

                var originalCardNumber = card.cardNumer;

                for (int i = 0; i < winningNumbersAmount; i++)
                {
                    if ((originalCardNumber + i + 1) <= cards.Count)
                    {
                        var nextCard = cards.First(card => card.cardNumer == originalCardNumber + i + 1);
                        nextCard.copies.Add(nextCard);
                    }
                }

                foreach (var copy in card.copies)
                {
                    var winningNumbersCopyAmount = 0;

                    foreach (var winningNumber in copy.winningNumbers)
                    {
                        if (copy.myNumbers.Contains(winningNumber))
                        {
                            winningNumbersCopyAmount++;
                        }
                    }

                    var originalCardCopyNumber = copy.cardNumer;

                    for (int i = 0; i < winningNumbersCopyAmount; i++)
                    {
                        if ((originalCardCopyNumber + i + 1) <= cards.Count)
                        {
                            var nextCard = cards.First(card => card.cardNumer == originalCardCopyNumber + i + 1);
                            nextCard.copies.Add(nextCard);
                        }
                    }
                }
            }

            var amountOfCards = 0;

            foreach (var card in cards)
            {
                //Console.WriteLine($"Card {card.cardNumer} - Copies: {card.copies.Count} - Total {card.copies.Count + 1}");

                amountOfCards += card.copies.Count + 1;
            }

            return amountOfCards;
        }
    }

    internal class Card
    {
        public int cardNumer = 0;
        public List<Card> copies = new();
        public List<int> winningNumbers = new();
        public List<int> myNumbers = new();

    }
}
