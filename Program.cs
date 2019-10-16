using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Media;
using System.Windows.Forms;

namespace Hangman
{
    class Program
    {
        public static Random rand = new Random();
        static SoundPlayer DJ = new SoundPlayer(Hangman.Properties.Resources.thefatrat_origin);
        static Thread MusicPlayer = new Thread(PlayMusic);

        public static bool isBG = false;
        public static char firstLetter, lastLetter;
        public static string[] wordsEN;
        public static string[] wordsBG;
        public static string chosenWord = "";
        public static string languageChoice = "";
        public string test = "pesho";
        public static int letterCount = 0;
       // static int index = 2;
        public static StringBuilder usedLetters = new StringBuilder();
       

        static void PlayMusic()
        {
            DJ.PlayLooping();
        }

        static int GetArrayLenght()
        {
            int counter = 0;

            if (isBG)
            {
                foreach (string word in wordsBG)
                {
                    counter++;
                }
            }
            else
            {
                foreach (string word in wordsEN)
                {
                    counter++;
                }
            }
            return counter;
        }

           
        public static bool CheckForDuplicates()
        {
            if (usedLetters.ToString().Contains(UI.chosenLetter[0])) return true;

            if (!usedLetters.ToString().Contains(UI.chosenLetter[0])) usedLetters.Append(UI.chosenLetter);

            return false;               
        }

        static int GetRandomNumber(int min, int max)
        {
            Thread.Sleep(rand.Next(1, 250));

            int cycles = rand.Next(40000, 45889);
            int finalNumber = 0;

            for (int i = 0; i < rand.Next(1, cycles); i++)
            {
                if (rand.Next(1, 5000) <= 2500)
                {
                    finalNumber = rand.Next(min, max);
                }
            }
            return finalNumber;
        }

        static void Main()
        {
			MusicChoice musicChoice = new MusicChoice();
			musicChoice.ShowDialog();
			if (MusicChoice.isMusicWanted) MusicPlayer.Start();

			int letterCount = 0;
            int randChoice;
         
            do
            {
                Console.Clear();
                UI.ShowInitMessages();
                languageChoice = Console.ReadLine();
                Console.Clear();
                UI.ShowInitMessages();
            }
            while (languageChoice != "bg" && languageChoice != "BG" && languageChoice != "en" && languageChoice != "EN");


            if (languageChoice == "bg" || languageChoice == "BG") isBG = true;
            else isBG = false;

            if (Program.isBG)
            {
                wordsBG = BG.wordList.Split(' ');
                randChoice = GetRandomNumber(0, GetArrayLenght());
                chosenWord = wordsBG[randChoice];
            }
            else
            {
                wordsEN = EN.wordList.Split(' ');
                randChoice = GetRandomNumber(0, GetArrayLenght());
                chosenWord = wordsEN[randChoice];
            }

            for (int i = 0; i < chosenWord.Length; i++)
            {
                //////////////////////// //debug
            }

            for (int i = 0; i < chosenWord.Length; i++)
            {
                if (i == 0)
                {
                    firstLetter = chosenWord[i];
                }
                if (i == chosenWord.Length - 1)
                {
                    lastLetter = chosenWord[i];
                }
            }
            usedLetters.Append(firstLetter);
            usedLetters.Append(lastLetter);
            letterCount = chosenWord.Length;

            UI.DrawUI();
        }
    }
}
