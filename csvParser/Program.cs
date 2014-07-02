using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csvParser
{
    class Program
    {
        static List<VideoCard> readFile(String filename)
        {
            string text = System.IO.File.ReadAllText(filename);
            System.Console.WriteLine("Contents of radeonCards.csv = {0}", text);

            string[] lines = System.IO.File.ReadAllLines(filename);
            System.Console.WriteLine("Contents of WriteLines2.txt");
            List<VideoCard> cardList = new List<VideoCard>();
            for(int i=0; i<lines.Length; i++)
            {
                string line = lines[i];
                VideoCard video = new VideoCard();
                Console.WriteLine("\t" + line);
                
                if (line.Contains('"'))
                {
                    List<string> quotes = line.Split('"').ToList<string>();
                    List<string> parsedInfo = quotes[2].Split(',').ToList<string>();
                    video.SetCardName(quotes[1]);
                    video.SetHashRate(Convert.ToInt32(parsedInfo[1]));
                }
                else
                {
                    List<string> parsedInfo = line.Split(',').ToList<string>();
                    video.SetCardName(parsedInfo[0]);
                    video.SetHashRate(Convert.ToInt32(parsedInfo[1]));
                }
                
                video.SetId(i);
                cardList.Add(video);
            }
            return cardList;

        }

        public static void FindCashPerHash(List<VideoCard> cardList)
        {
            double r6950 = 175;
            double r6970 = 200;
            double r7850 = 120;
            double r7950 = 350;
            double r7970 = 400;
            double r7990 = 900;
            double r270X = 225;
            double r280X = 425;
            double r290 = 500;
            double r290X = 585;

            foreach (VideoCard card in cardList)
            {
                if (card.GetCardName().Contains("6950"))
                {
                    card.SetCashPerHash(card.GetHashRate()/r6950);
                }
                if (card.GetCardName().Contains("6970"))
                {
                    card.SetCashPerHash(card.GetHashRate() / r6970);
                }
                if (card.GetCardName().Contains("7850"))
                {
                    card.SetCashPerHash(card.GetHashRate() / r7850);
                }
                if (card.GetCardName().Contains("7950"))
                {
                    card.SetCashPerHash(card.GetHashRate() / r7950);
                }
                if (card.GetCardName().Contains("7970"))
                {
                    card.SetCashPerHash(card.GetHashRate() / r7970);
                }
                if (card.GetCardName().Contains("7990"))
                {
                    card.SetCashPerHash(card.GetHashRate() / r7990);
                }
                if (card.GetCardName().Contains("270X"))
                {
                    card.SetCashPerHash(card.GetHashRate() / r270X);
                }
                if (card.GetCardName().Contains("280X"))
                {
                    card.SetCashPerHash(card.GetHashRate() / r280X);
                }
                if (card.GetCardName().Contains("290") && !card.GetCardName().Contains("290X"))
                {
                    card.SetCashPerHash(card.GetHashRate() / r290);
                }
                if (card.GetCardName().Contains("290X"))
                {
                    card.SetCashPerHash(card.GetHashRate() / r290X);
                }
            }
        }

        static void Main(string[] args)
        {
            List<VideoCard> basicInfoList = new List<VideoCard>();
            basicInfoList = readFile(@"C:\radeon.csv");
            //FindCashPerHash(basicInfoList); do not use. we now have realtime price data 
            //basicInfoList = basicInfoList.OrderBy(x => x.GetCashPerHash()).ToList();

            List<RefVideoCard> averagedHashCards = calculateAverageHash(basicInfoList);
            writeAverageHashRatesToJSON(averagedHashCards);
            
            Console.WriteLine("Press any key to exit.");   
            Console.ReadKey();
        }

        static void writeAverageHashRatesToJSON(List<RefVideoCard> averagedCards)
        {
            string line = "averagedHash: [ ";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"hashAverages.JSON"))
            {
                file.WriteLine(line);
                foreach (RefVideoCard card in averagedCards)
                {
                    string firstBracket = "{";
                    string cardId = '"' + "CardId" + '"' + ": " + '"' + card.GetId() + '"' + ",";
                    string cardName = '"' + "CardName" + '"' + ": " + '"' + card.GetCardName() + '"' + ",";
                    string cardAvgPrice = '"' + "CardAvgHash" + '"' + ": " + '"' + card.GetAverageHash() + '"';

                    string lastBracket = "},";
                    string lastBracketNoComma = "}";
                    file.WriteLine(firstBracket);
                    file.WriteLine(cardId);
                    file.WriteLine(cardName);
                    file.WriteLine(cardAvgPrice);
                    if (card.GetId() == averagedCards.LastIndexOf(card))
                    {
                        file.WriteLine(lastBracketNoComma);
                    }
                    else
                    {
                        file.WriteLine(lastBracket);
                    }
                }
                line = "]";
                file.WriteLine(line);
            }
        }

        static List<RefVideoCard> calculateAverageHash(List<VideoCard> inputCards)
        {
            List<RefVideoCard> averagedCards = new List<RefVideoCard>();

            RefVideoCard r5970 = new RefVideoCard();
            RefVideoCard r6850 = new RefVideoCard();
            RefVideoCard r6870 = new RefVideoCard();
            RefVideoCard r6950 = new RefVideoCard();
            RefVideoCard r6970 = new RefVideoCard();
            RefVideoCard r6990 = new RefVideoCard();
            RefVideoCard r7850 = new RefVideoCard();
            RefVideoCard r7870 = new RefVideoCard();
            RefVideoCard r7950 = new RefVideoCard();
            RefVideoCard r7970 = new RefVideoCard();
            RefVideoCard r7990 = new RefVideoCard();
            RefVideoCard r270X = new RefVideoCard();
            RefVideoCard r280X = new RefVideoCard();

            foreach (VideoCard card in inputCards)
            {
                if (card.GetCardName().Contains("5970"))
                {
                    r5970.SetHashSoFar(r5970.GetHashSoFar() + card.GetHashRate());
                    r5970.SetCardCount(r5970.GetCardCount() + 1);
                }
                if (card.GetCardName().Contains("6850"))
                {
                    r6850.SetHashSoFar(r6850.GetHashSoFar() + card.GetHashRate());
                    r6850.SetCardCount(r6850.GetCardCount() + 1);
                }
                if (card.GetCardName().Contains("6870"))
                {
                    r6870.SetHashSoFar(r6870.GetHashSoFar() + card.GetHashRate());
                    r6870.SetCardCount(r6870.GetCardCount() + 1);
                }
                if (card.GetCardName().Contains("6950"))
                {
                    r6950.SetHashSoFar(r6950.GetHashSoFar() + card.GetHashRate());
                    r6950.SetCardCount(r6950.GetCardCount() + 1);
                }
                if (card.GetCardName().Contains("6970"))
                {
                    r6970.SetHashSoFar(r6970.GetHashSoFar() + card.GetHashRate());
                    r6970.SetCardCount(r6970.GetCardCount() + 1);
                }
                if (card.GetCardName().Contains("6990"))
                {
                    r6990.SetHashSoFar(r6990.GetHashSoFar() + card.GetHashRate());
                    r6990.SetCardCount(r6990.GetCardCount() + 1);
                }
                if (card.GetCardName().Contains("7850"))
                {
                    r7850.SetHashSoFar(r7850.GetHashSoFar() + card.GetHashRate());
                    r7850.SetCardCount(r7850.GetCardCount() + 1);
                }
                if (card.GetCardName().Contains("7870"))
                {
                    r7870.SetHashSoFar(r7870.GetHashSoFar() + card.GetHashRate());
                    r7870.SetCardCount(r7870.GetCardCount() + 1);
                }
                if (card.GetCardName().Contains("7950"))
                {
                    r7950.SetHashSoFar(r7950.GetHashSoFar() + card.GetHashRate());
                    r7950.SetCardCount(r7950.GetCardCount() + 1);
                }
                if (card.GetCardName().Contains("7970"))
                {
                    r7970.SetHashSoFar(r7970.GetHashSoFar() + card.GetHashRate());
                    r7970.SetCardCount(r7970.GetCardCount() + 1);
                }
                if (card.GetCardName().Contains("7990"))
                {
                    r7990.SetHashSoFar(r7990.GetHashSoFar() + card.GetHashRate());
                    r7990.SetCardCount(r7990.GetCardCount() + 1);
                }
                if (card.GetCardName().Contains("270X"))
                {
                    r270X.SetHashSoFar(r270X.GetHashSoFar() + card.GetHashRate());
                    r270X.SetCardCount(r270X.GetCardCount() + 1);
                }
                if (card.GetCardName().Contains("280X"))
                {
                    r280X.SetHashSoFar(r280X.GetHashSoFar() + card.GetHashRate());
                    r280X.SetCardCount(r280X.GetCardCount() + 1);
                }
            }


            r5970.SetAverageHash(Convert.ToString(r5970.GetHashSoFar() / r5970.GetCardCount()));
            r5970.SetId(5970);
            r5970.SetCardName("Radeon 5970");
            averagedCards.Add(r5970);

            r6850.SetAverageHash(Convert.ToString(r6850.GetHashSoFar() / r6850.GetCardCount()));
            r6850.SetId(6850);
            r6850.SetCardName("Radeon 6850");
            averagedCards.Add(r6850);

            r6870.SetAverageHash(Convert.ToString(r6870.GetHashSoFar() / r6870.GetCardCount()));
            r6870.SetId(6870);
            r6870.SetCardName("Radeon 6870");
            averagedCards.Add(r6870);

            r6950.SetAverageHash(Convert.ToString(r6950.GetHashSoFar() / r6950.GetCardCount()));
            r6950.SetId(6950);
            r6950.SetCardName("Radeon 6950");
            averagedCards.Add(r6950);

            r6970.SetAverageHash(Convert.ToString(r6970.GetHashSoFar() / r6970.GetCardCount()));
            r6970.SetId(6970);
            r6970.SetCardName("Radeon 6970");
            averagedCards.Add(r6970);

            r6990.SetAverageHash(Convert.ToString(r6990.GetHashSoFar() / r6990.GetCardCount()));
            r6990.SetId(6990);
            r6990.SetCardName("Radeon 6990");
            averagedCards.Add(r6990);

            r7850.SetAverageHash(Convert.ToString(r7850.GetHashSoFar() / r7850.GetCardCount()));
            r7850.SetId(7850);
            r7850.SetCardName("Radeon 7850");
            averagedCards.Add(r7850);

            r7870.SetAverageHash(Convert.ToString(r7870.GetHashSoFar() / r7870.GetCardCount()));
            r7870.SetId(7870);
            r7870.SetCardName("Radeon 7870");
            averagedCards.Add(r7870);

            r7950.SetAverageHash(Convert.ToString(r7950.GetHashSoFar() / r7950.GetCardCount()));
            r7950.SetId(7950);
            r7950.SetCardName("Radeon 7950");
            averagedCards.Add(r7950);

            r7970.SetAverageHash(Convert.ToString(r7970.GetHashSoFar() / r7970.GetCardCount()));
            r7970.SetId(7970);
            r7970.SetCardName("Radeon 7970");
            averagedCards.Add(r7970);

            r7990.SetAverageHash(Convert.ToString(r7990.GetHashSoFar() / r7990.GetCardCount()));
            r7990.SetId(7990);
            r7990.SetCardName("Radeon 7990");
            averagedCards.Add(r7990);

            r270X.SetAverageHash(Convert.ToString(r270X.GetHashSoFar() / r270X.GetCardCount()));
            r270X.SetId(270);
            r270X.SetCardName("Radeon 270X");
            averagedCards.Add(r270X);

            r280X.SetAverageHash(Convert.ToString(r280X.GetHashSoFar() / r280X.GetCardCount()));
            r280X.SetId(280);
            r280X.SetCardName("Radeon 280X");
            averagedCards.Add(r280X);

            return averagedCards;
        }
    }
}
