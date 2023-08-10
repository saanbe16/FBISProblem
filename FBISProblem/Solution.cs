using System;
using System.IO;
using System.Text;

namespace FBISProblem
{
	public class Solution
	{
        static public void Main(String[] args)
        {
            FileGenerator generator = new FileGenerator();
            
            int x = 0;
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            int hour = currentTime.Hours;
            int min = currentTime.Minutes;
            while (x!=3)
            {
                if(hour == 16 && min == 50)
                {
                    Console.WriteLine("Running Automatic 10AM Program");
                    run();
                    break;
                }
                else
                {
                    Console.WriteLine("This Program is scheduled to run only at 10:00 A.M, it is currently " + currentTime);
                    Console.WriteLine("----------------------------------------------------------------------------------");
                    Console.WriteLine(" Enter 1 to run code anyways");
                    Console.WriteLine(" Enter 2 to generate starter Files");
                    Console.WriteLine(" Enter 3 to Exit program\n");

                    x = Console.Read() - 48;
                    Console.Read(); // getting rid of extra return character"
                    if (x == 2)
                    {
                        generator.generate();
                        Console.WriteLine("Files Created \n");
                    }
                    if (x == 1)
                    {
                        run();
                        Console.WriteLine("Program Completed");
                        break;
                    }
                }
            }
            Console.WriteLine("Goodbye!");
           
        }
        public static void run()
        {
            // Check for past days
            LetterService service = new LetterService();
            HashSet<string> admissionDirSet = new HashSet<string>();
            try
            {
                // get and store all date style directories in /Input/Admission
                var admissionDirectories = Directory.GetDirectories("CombinedLetters/Input/Admission");

                foreach (string dir in admissionDirectories)
                {
                    admissionDirSet.Add(dir.Substring(dir.Length - 8)); // add date folder name
                }

                var directories = Directory.GetDirectories("CombinedLetters/Input/Scholarship"); // get and store all date style directories in /Input/Scholarship

                foreach (string dir in directories)
                {
                    string date = dir.Substring(dir.Length - 8);
                    if (admissionDirSet.Contains(date)) // if the same date folders exist in Admission and Scholarship continue
                    {
                        string admissionDirectory = dir.Replace("Scholarship", "Admission");
                        HashSet<string> admissionFiles = new HashSet<string>();
                        // Get and store all file names in common date/Admission directory
                        var directory = new DirectoryInfo(admissionDirectory);
                        foreach (FileInfo fi in directory.GetFiles()) // adding to map
                        {
                            admissionFiles.Add(fi.Name.Substring(fi.Name.Length - 12));
                        }
                        // Get and store all file names in common date/Scholarship directory
                        directory = new DirectoryInfo(dir);
                        string textReport = date[4] + "" + date[5] + "/" + date[6..] + "/" + date.Substring(0, 4) + " Report\n-------------------\n\n"; // start text report file
                        List<string> reportIds = new List<string>(); // create ids list for text report file
                        foreach (FileInfo fi in directory.GetFiles()) 
                        {
                            string file = fi.Name.Substring(fi.Name.Length - 12);
                            if (admissionFiles.Contains(file)) // if the same studentId is present in the admissions folder as the scholarship folder continue and combine folders
                            {
                                // COMBINATION CODE HERE
                                reportIds.Add(file.Substring(0, file.Length - 4));
                                string scholarshipFile = dir + "/" + fi.Name;
                                string admissionFile = admissionDirectory + '/' + fi.Name.Replace("scholarship", "admission");
                                string outputFile = "CombinedLetters/Output/" + date;
                                service.CombineTwoLetters(admissionFile, scholarshipFile, outputFile); // combine files
                            }
                        }
                        textReport += "Number of combined letters: " + reportIds.Count() + "\n";
                        foreach (string id in reportIds) // create text report string
                        {
                            textReport += id + "\n";
                        }
                    Directory.CreateDirectory("CombinedLetters/Output/"+date);
                    using (FileStream fs = File.Create("CombinedLetters/Output/" + date + "/textReport.txt")) // create text report file
                        {
                            byte[] info = new UTF8Encoding(true).GetBytes(textReport);
                            // Add some information to the file.
                            fs.Write(info, 0, info.Length);
                        }
                        admissionFiles.Clear(); // clear set to start again for next loop iterration
                    }
                }
                Console.WriteLine("Files succfully combined");
                Console.WriteLine("Text Reports created");

                //Archive all input  directories
                var directory2 = new DirectoryInfo("CombinedLetters/Input/Admission"); // get all admission directiories
                foreach (DirectoryInfo fi in directory2.GetDirectories())
                {
                    Directory.Move(fi.ToString(), "CombinedLetters/Archive/Admission" + fi.Name); // move directory to Archive folder
                }
                directory2 = new DirectoryInfo("CombinedLetters/Input/Scholarship");// get all schilarship directiories
                foreach (DirectoryInfo fi in directory2.GetDirectories())
                {
                    Directory.Move(fi.ToString(), "CombinedLetters/Archive/Scolarship" + fi.Name); // move directory to Archive folder
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Directories not found. Please verify that directories exist and are in correct order, or generate directories to continue");
            }
        }

    }
}

