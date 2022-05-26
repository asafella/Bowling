using System;
using System.Text;

namespace BowlingScoring
{
    class Program
    {
        static GameManager gameManager;

        static void Main(string[] args)
        {
            gameManager = new GameManager();
            gameManager.GameEndedEvent += GameManager_GameEndedEvent;
            string input = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("Usage:\n");
            sb.Append("Enter the number of knocked down pins for each throw.\n");
            sb.Append("use '/' for spare, 'x' for strike.\n");
            sb.Append("scoring table can be presented by entering 'p'.\n");
            sb.Append("In order to display these instructions again, just time 'i'.\n");
            sb.Append("If you want to terminate this app, enter 'exit'.\n");
            sb.Append("\nJust remember to always end your entry with 'Enter'\n");

            Console.WriteLine(string.Format("Bowling Scoring App\n{0}\nLet's Go!\n\n",sb.ToString()));
            int i = 1;
            while (input.ToLower() != "exit")
            {
                Console.Write("Enter Roll Score Or Command>");
                input = Console.ReadLine();
                switch(input.ToLower())
                {
                    case "x":
                    case "/":
                        {
                            try
                            {
                                gameManager.newThrow(10);
                                i++;
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            
                            break;
                        }
                    case "p":
                        {
                            printFrames(gameManager.getFrames());
                            break;
                        }
                    case "i":
                        {
                            Console.WriteLine(sb.ToString());
                            break;
                        }
                    default:
                        {
                            if (short.TryParse(input, out short score))
                            {
                                try {
                                    gameManager.newThrow(score);
                                    i++;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                        }
                            break;
                        }
                }
            }
        }

        private static void GameManager_GameEndedEvent(object sender, EventArgs e)
        {
            Console.WriteLine("The Game has ended...\n");
            printFrames(gameManager.getFrames());
            gameManager = null;
            GC.Collect();
            Environment.Exit(0);
        }

        private static void printFrames(Frame[] frames)
        {
            if (frames == null)
            {
                Console.WriteLine("Nothing to show...\n");
                return;
            }
            int length = frames.GetLength(0);
            Console.WriteLine("1st\t2nd\tscore\n");
            for(int i=0;i<length;i++)
            {
                string line = string.Empty;
                string formatedScore = (frames[i].throwTypeEnum != ThrowTypeEnum.PartialKnockDown && i == length - 1 ? string.Empty : frames[i].frameScore.ToString());
                switch (frames[i].throwTypeEnum)
                {
                        
                    case ThrowTypeEnum.PartialKnockDown:
                        {
                            line = string.Format("{0}\t{1}\t{2}", frames[i].firstThrow, frames[i].secondThrow, formatedScore);
                            break;
                        }
                    case ThrowTypeEnum.Spare:
                        {
                            line  = string.Format("{0}\t{1}\t{2}", frames[i].firstThrow, "/", formatedScore);
                            break;
                        }
                    case ThrowTypeEnum.Strike:
                        {
                            line = string.Format("{0}\t{1}\t{2}", string.Empty, "X", formatedScore);
                            break;
                        }
                }
                Console.WriteLine(line);
            }
        }
    }
}
