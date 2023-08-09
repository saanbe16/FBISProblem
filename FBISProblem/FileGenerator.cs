using System;
using System.IO;
using System.Reflection.Emit;
using System.Text;

namespace FBISProblem
{
	public class FileGenerator
	{

        private void createInitialFileSetUp()
        {
            Directory.CreateDirectory("CombinedLetters");
            Directory.CreateDirectory("CombinedLetters/Input");
            Directory.CreateDirectory("CombinedLetters/Input/Admission");
            Directory.CreateDirectory("CombinedLetters/Input/Scholarship");
            Directory.CreateDirectory("CombinedLetters/Archive");
            Directory.CreateDirectory("CombinedLetters/Output");
        }
    

        private void createInputFiles()
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
                    studentId = "01"+rnd.Next(100000,999999).ToString();
                    randNum = rnd.NextDouble();
                    if (randNum>0.35) // Assume 65% chance of admission for randomness
                    {
                        Directory.CreateDirectory("CombinedLetters/Input/Admission/"+date);
                        // Creating admission file
                        using (FileStream fs = File.Create("CombinedLetters/Input/Admission/" + date+"/"+"admission-"+studentId+".txt"))
                        {
                            byte[] info = new UTF8Encoding(true).GetBytes("CONGRATULATIONS! \n--------------------------\nCongrats student "+ studentId+", you have been admitted to the university of Iowa\n\nGo Hawks!");
                            // Add some information to the file.
                            fs.Write(info, 0, info.Length);
                        }
                    }
                    if (randNum > 0.65) // assume 35% chance of scholarship for randomness
                    {
                        Directory.CreateDirectory("CombinedLetters/Input/Scholarship/" + date);
                        // Creating scholarship file
                        using (FileStream fs = File.Create("CombinedLetters/Input/Scholarship/" + date + "/" + "scholarship-" + studentId + ".txt"))
                        {
                            byte[] info = new UTF8Encoding(true).GetBytes("CONGRATULATIONS! \n--------------------------\nCongrats student "+ studentId+", you have been awarded a scholarship to the University of Iowa\n\nWe hope top see you soon!");
                            // Add some information to the file.
                            fs.Write(info, 0, info.Length);
                        }
                    }
                }

            }

        }
        public void generate()
        {
            createInitialFileSetUp();
            createInputFiles();
        }
    }
}

