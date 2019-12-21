using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHandShowdown
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endPoker = false;
            Console.WriteLine("WELCOME TO POKER HAND SHOWDOWN!");
            Console.WriteLine("========================================\n");
            
            while (!endPoker)
            {
                try
                {
                    bool isValid = true;
                    List<Player> players = new List<Player>();

                    #region Input Logic

                    //getting no of player
                    Console.WriteLine("How many players? (Minimum 2 and  maximum 9 players are allowed.)");
                    string strPlayers = Console.ReadLine();
                    Int32.TryParse(strPlayers, out int noOfPlayers);

                    if (noOfPlayers < 2 || noOfPlayers > 9)
                    {
                        Console.WriteLine("ERROR: Minimum 2 and  maximum 9 players are allowed.");
                        isValid = false;
                    }
                    else
                    {
                        //getting each player details
                        for (int i = 1; i <= noOfPlayers; i++)
                        {
                            //it should show for the first hand request
                            if (i == 1)
                            {
                                Console.WriteLine("\n----------------------------------------------------------------------------------------------------");
                                Console.WriteLine("TIPS:");
                                Console.WriteLine("For example, input hand details: 'AD KD 8C 5H JS'");
                                Console.WriteLine("In each card, the first character is Card Rank and the second one is Card Suit.");
                                Console.WriteLine("In AD, A is Card Rank and J is Card Suit.");
                                Console.WriteLine("For Card Suit, C stands for Club, H stands for Heart, D stands for Diamonds, S stands for Spades.");
                                Console.WriteLine("----------------------------------------------------------------------------------------------------");
                            }

                            Console.WriteLine($"\nPlease enter the player {i} name:");
                            string playerName = Console.ReadLine();

                            Console.WriteLine($"Please enter the hand details:");
                            string inputHandDetails = Console.ReadLine();

                            string[] inputCardDetails = inputHandDetails.Split(' ');

                            if (inputCardDetails.Length != GlobalVariables.NoOfCardsInHand)
                            {
                                Console.WriteLine("ERROR: the hand details are incorrect.");
                                isValid = false;
                                break;
                            }
                            else
                            {
                                List<Card> inputCards = new List<Card>();
                                foreach (string cardDetails in inputCardDetails)
                                {
                                    inputCards.Add(Card.GetCard(cardDetails));
                                }
                                players.Add(new Player(playerName, inputCards));
                            }
                        }
                    }

                    #endregion

                    if (isValid)
                    {
                        #region Finding the winner(s)

                        //finding the player with the highest hand type
                        var possibleWinners = players.Where(iPlayer => iPlayer.HandType == players.Max(yPlayer => yPlayer.HandType));

                        //if only one player is there with the highest hand, then that person would be the winner, and no need to proceed further
                        if (possibleWinners.Count() == 1)
                        {
                            DisplayWinner($"{possibleWinners.First().Name} won!");
                        }
                        else
                        {
                            //finding the highest hand type
                            var winnerHandType = possibleWinners.First().HandType;

                            //if the winners have flush or high card, then the winner will be decided based on the highest kicker
                            if (winnerHandType == 1 || winnerHandType == 5)
                            {
                                var winners = Player.FindWinnerWithKicker(possibleWinners, GlobalVariables.NoOfCardsInHand);
                                DisplayWinner($"{string.Join(", ", winners.Select(x => x.Name))} won!");
                            }
                            //otherwise, it requires to process further to determine the winner(s)
                            else
                            {
                                var winners = Player.FindWinnerWithKindOfHand(possibleWinners, winnerHandType);
                                DisplayWinner($"{string.Join(", ", winners.Select(x => x.Name))} won!");
                            }
                        }
                        #endregion
                    }
                }
                catch
                {
                    //we can also write error logs if we want
                    Console.WriteLine("\nSomething went wrong!");
                }

                Console.WriteLine("\nPress 'x' and Enter to close the poker, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "x") endPoker = true;
            }
        }

        #region functions  
        private static void DisplayWinner(string message)
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine($"{message}");
            Console.WriteLine("-----------------------------------------------");
        }
        #endregion
    }
}