using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHandShowdown
{
    public class Player
    {
        #region properties
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
        public Int16 HandType { get; set; }
        public Int16 KindOfCardRank { get; set; }
        public Int16 HighCard { get; set; }
        #endregion

        #region ctor
        public Player(string name, List<Card> Cards)
        {
            this.Name = name;
            this.Cards = Cards;
            SetPlayerHandType(this);
        }
        #endregion

        #region functions

        /// <summary>
        /// Set the hand type of the player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private static Player SetPlayerHandType(Player player)
        {
            int cardCount = player.Cards.Select(xCard => xCard.Rank).Distinct().Count();
            if (player.Cards.Select(xCard => xCard.Suit).Distinct().Count() == 1)
            {
                player.HandType = 5;//flush
            }
            else if (cardCount == 2)
            {
                player.HandType = 4;//four of a kind
            }
            else if (cardCount == 3)
            {
                player.HandType = 3;//three of a kind
            }
            else if (cardCount == 4)
            {
                player.HandType = 2;//pair
            }
            else
            {
                player.HandType = 1;//high card
            }

            return player;
        }

        /// <summary>
        /// Find the winnner based on the kicker card
        /// </summary>
        /// <param name="players"></param>
        /// <param name="noOfCardsToCheck"></param>
        /// <returns></returns>
        internal static IEnumerable<Player> FindWinnerWithKicker(IEnumerable<Player> players, Int16 noOfCardsToCheck)
        {
            Int16 cardCount = 1;
            var existingPlayers = players;
            do
            {
                //find the players with the highest card
                existingPlayers = FindPlayersWithHighestKickerCard(existingPlayers);

                //if only one player is there with the kicker, then that person would be the winner, and no need to proceed further
                if (existingPlayers.Count() == 1)
                {
                    return existingPlayers;
                }

                //otherwise, find the next highest kicker
                //firstly, deleting the previous kickers from the hand
                existingPlayers = RemoveCardFromHand(existingPlayers, existingPlayers.FirstOrDefault().HighCard);

                //increase the current card count
                cardCount++;

            } while (cardCount <= noOfCardsToCheck);

            return existingPlayers;
        }

        /// <summary>
        /// Finding the players with the highest kicker card
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        private static IEnumerable<Player> FindPlayersWithHighestKickerCard(IEnumerable<Player> players)
        {
            var existingPlayers = new List<Player>();
            //find the highest kicker card
            foreach (var player in players)
            {
                player.HighCard = player.Cards.Max(xCard => xCard.Rank);
                existingPlayers.Add(player);
            }

            //find the players with the highest kicker
            return existingPlayers.Where(xPlayer => xPlayer.HighCard == existingPlayers.Max(yPlayer => yPlayer.HighCard));
        }

        /// <summary>
        /// Remove card rank from the players' hand
        /// </summary>
        /// <param name="players"></param>
        /// <param name="rankToRemove"></param>
        /// <returns></returns>
        private static IEnumerable<Player> RemoveCardFromHand(IEnumerable<Player> players, Int16 rankToRemove)
        {
            var existingPlayers = new List<Player>();
            foreach (var player in players)
            {
                player.Cards = player.Cards.Where(xCard => xCard.Rank != rankToRemove).ToList();
                existingPlayers.Add(player);
            }
            return existingPlayers;
        }

        /// <summary>
        /// Getting the winner for the hand type of two of a kind(pair), three of a kind, or four of a kind
        /// </summary>
        /// <param name="players"></param>
        /// <param name="handType"></param>
        /// <returns></returns>
        internal static IEnumerable<Player> FindWinnerWithKindOfHand(IEnumerable<Player> players, Int16 handType)
        {
            //find the players with the highest kind of hand
            var existingPlayers = FindKindOfWithHighestCardRank(players, handType);

            //if only one player is there with the highest kind of a card rank, then that person would be the winner, and no need to proceed further
            if (existingPlayers.Count() == 1)
            {
                return existingPlayers;
            }

            //otherwise, deleting the kind of a card rank from the cards
            existingPlayers = RemoveCardFromHand(existingPlayers, existingPlayers.FirstOrDefault().KindOfCardRank);

            //this will find the player based on the kicker card after kind of a card
            return FindWinnerWithKicker(existingPlayers, (Int16)(GlobalVariables.NoOfCardsInHand - handType));
        }

        /// <summary>
        /// Getting the players rank of the kind of
        /// </summary>
        /// <param name="players"></param>
        /// <param name="handType"></param>
        /// <returns></returns>
        private static IEnumerable<Player> FindKindOfWithHighestCardRank(IEnumerable<Player> players, Int16 handType)
        {
            var existingPlayers = new List<Player>();
            //Set find of type for the all the players
            foreach (var player in players)
            {
                //this will set the kind of type based on hand type
                //for example, if hand type is three of a kind of 8, then it will set 8 
                player.KindOfCardRank = player.Cards.GroupBy(xCard => xCard.Rank).Where(g => g.Count() == handType).Select(g => g.Key).First();
                existingPlayers.Add(player);
            }

            //this will find the maximum kind of a players
            return existingPlayers.Where(xPlayer => xPlayer.KindOfCardRank == existingPlayers.Max(yPlayer => yPlayer.KindOfCardRank));
        }

        #endregion
    }
}