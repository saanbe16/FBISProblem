using System;
using System.IO;

namespace FBISProblem
{
	public class Solution
	{
        static public void Main(String[] args)
        {
            FileGenerator generator = new FileGenerator();
            LetterService service = new LetterService();
            //generator.generate();
            Console.WriteLine("Generated new files");

            // Check for past days
            HashSet<string> admissionDirSet = new HashSet<string>();
            var directories = Directory.GetDirectories("CombinedLetters/Input/Admission");
            foreach(string dir in directories)
            {
                admissionDirSet.Add(dir.Substring(dir.Length - 8)); // add date folder name

            }
            directories = Directory.GetDirectories("CombinedLetters/Input/Scholarship");
            foreach (string dir in directories)

            {
                string date = dir.Substring(dir.Length - 8);
                if (admissionDirSet.Contains(date))
                {
                    string admissionDirectory = dir.Replace("Scholarship","Admission");
                    Console.WriteLine(admissionDirectory);
                    HashSet<string> admissionFiles = new HashSet<string>();
                    var directory = new DirectoryInfo(admissionDirectory);
                    foreach (FileInfo fi in directory.GetFiles()) // adding to map
                    {
                        admissionFiles.Add(fi.Name.Substring(fi.Name.Length-12));
                    }
                    directory = new DirectoryInfo(dir);
                    foreach (FileInfo fi in directory.GetFiles()) // adding to map
                    {
                        string file = fi.Name.Substring(fi.Name.Length - 12);
                        if (admissionFiles.Contains(file))
                        {
                            // COMBINATION CODE HERE
                            string scholarshipFile = dir + "/" + fi.Name;
                            string admissionFile = admissionDirectory + '/' + fi.Name.Replace("scholarship", "admission");
                            string outputFile = "CombinedLetters/Output/"+date;
                            service.CombineTwoLetters(admissionFile,scholarshipFile,outputFile);
                        }
                    }
                }
            }


        }

    }
}

