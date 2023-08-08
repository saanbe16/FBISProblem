using System;
using System.IO;
namespace FBISProblem
{
	public class GenerateFiles
	{
		public GenerateFiles()
		{
		}

        static public void Main(String[] args)
        {
            createInitialFileSetUp();
            createInputFiles();
        }

        public static void createInitialFileSetUp()
        {
            Directory.CreateDirectory("CombinedLetters");
            Directory.CreateDirectory("CombinedLetters/Input");
            Directory.CreateDirectory("CombinedLetters/Input/Admission");
            Directory.CreateDirectory("CombinedLetters/Input/Scholarship");
            Directory.CreateDirectory("CombinedLetters/Archive");
            Directory.CreateDirectory("CombinedLetters/Output");
        }

        public static void createInputFiles()
        {
            Random rnd = new Random();
            double randNum;
            int day = 20;
            string studentId;
            for (int i=0; i<5; i++) // creating test data for 5 days
            {
                string date = "202304"+ day.ToString();
                day+=1;
                for(int j=0; j < 10; j++) // assuming 10 students per day for testing
                {
                    studentId = "001"+rnd.Next(10000,99999).ToString();
                    randNum = rnd.NextDouble();
                    Console.WriteLine("StudentId ="+ studentId+ " date = "+ date+ "rand ="+ randNum);
                    Console.ReadLine();
                    if (randNum >0.35) // Assume 65% chance of admission for randomness
                    {
                        Directory.CreateDirectory("CombinedLetters/Input/Admission/"+date);
                    }
                    if (randNum > 0.65) // assume 35% chance of scholarship for randomness
                    {
                        Directory.CreateDirectory("CombinedLetters/Input/Scholarship/" + date);
                    }
                }
                
            }

        }
    }
}

