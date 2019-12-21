using System;

namespace PokerHandShowdown
{
    public class Card
    {
        #region properties
        public Int16 Rank { get; set; }
        public Int16 Suit { get; set; }
        #endregion

        #region ctor
        public Card(Int16 rank, Int16 suit)
        {
            this.Rank = rank;
            this.Suit = suit;
        }
        #endregion

        #region functions

        /// <summary>
        /// Get the card details from the input card details
        /// </summary>
        /// <param name="cardDetails"></param>
        /// <returns></returns>
        internal static Card GetCard(string cardDetails)
        {
            Int16 rank = GetRank(cardDetails.Substring(0, 1));
            Int16 suit = GetSuit(cardDetails.Substring(1, 1));

            return new Card(rank, suit);
        }

        /// <summary>
        /// Get the rank of the card from the input rank
        /// </summary>
        /// <param name="inputRank"></param>
        /// <returns></returns>
        private static Int16 GetRank(string inputRank)
        {
            Int16.TryParse(inputRank, out Int16 rank);
            if (rank == 0)
            {
                switch (inputRank)
                {
                    case "A":
                        return 14;
                    case "J":
                        return 11;
                    case "Q":
                        return 12;
                    case "K":
                        return 13;
                    default:
                        break;
                }
            }
            return rank;
        }

        /// <summary>
        /// Get the suit of the card from the input suit
        /// </summary>
        /// <param name="inputSuit"></param>
        /// <returns></returns>
        private static Int16 GetSuit(string inputSuit)
        {
            switch (inputSuit)
            {
                case "C":
                    return 1;
                case "H":
                    return 2;
                case "D":
                    return 3;
                case "S":
                    return 4;
                default:
                    return -1;
            }
        }

        #endregion
    }
}
