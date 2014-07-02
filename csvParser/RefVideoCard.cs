using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csvParser
{
    class RefVideoCard
    {
        private int _id;
        private string _cardName;
        private int _cardCount;
        private double _hashSoFar;
        private string _averageHash;

        public int GetId()
        {
            return _id;
        }

        public string GetCardName()
        {
            return _cardName;
        }

        public int GetCardCount()
        {
            return _cardCount;
        }

        public double GetHashSoFar()
        {
            return _hashSoFar;
        }

        public string GetAverageHash()
        {
            return _averageHash;
        }

        public void SetId(int id)
        {
            _id = id;
        }

        public void SetCardName(string cardName)
        {
            _cardName = cardName;
        }

        public void SetCardCount(int cardCount)
        {
            _cardCount = cardCount;
        }

        public void SetHashSoFar(double hashSoFar)
        {
            _hashSoFar = hashSoFar;
        }

        public void SetAverageHash(string averageHash)
        {
            _averageHash = averageHash;
        }
        


    }
}
