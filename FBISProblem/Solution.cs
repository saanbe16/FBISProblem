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
            LetterService service = new LetterService();
            generator.generate();
            Console.WriteLine();
            Console.WriteLine("Generated new files");

            // Check for past days
            HashSet<string> admissionDirSet = new HashSet<string>();
            var admissionDirectories = Directory.GetDirectories("CombinedLetters/Input/Admission");
            foreach(string dir in admissionDirectories)
            {
                admissionDirSet.Add(dir.Substring(dir.Length - 8)); // add date folder name
            }
            var directories = Directory.GetDirectories("CombinedLetters/Input/Scholarship");
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
                    Console.WriteLine(dir+" "+ admissionDirectory);
                    string textReport= date[4] +""+ date[5] +"/"+ date[6..] +"/"+ date.Substring(0,4) + " Report\n-------------------\n\n";
                    List<string> reportIds = new List<string>();
                    foreach (FileInfo fi in directory.GetFiles()) // adding to map
                    {
                        string file = fi.Name.Substring(fi.Name.Length - 12);
                        if (admissionFiles.Contains(file))
                        {
                            // COMBINATION CODE HERE
                            reportIds.Add(file.Substring(0,file.Length-4));

                            string scholarshipFile = dir + "/" + fi.Name;
                            string admissionFile = admissionDirectory + '/' + fi.Name.Replace("scholarship", "admission");
                            string outputFile = "CombinedLetters/Output/"+date;
                            service.CombineTwoLetters(admissionFile,scholarshipFile,outputFile);
                        }
                    }
                    textReport += "Number of combined letters: " + reportIds.Count()+"\n";
                    foreach(string id in reportIds)
                    {
                        textReport += id+"\n";
                    }
                    using (FileStream fs = File.Create("CombinedLetters/Output/" + date+"/textReport.txt"))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(textReport);
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                    }
                    admissionFiles.Clear();
                }
            }
            //Archive all directories

           // Directory.Move("CombinedLShow potential fixesetters/Input/Admission", "CombinedLetters/Archive/Admission");
            var directory2 = new DirectoryInfo("CombinedLetters/Input/Admission");
            foreach (DirectoryInfo fi in directory2.GetDirectories())
            {
                Console.WriteLine(fi.Name.ToString());
                Directory.Move(fi.ToString(), "CombinedLetters/Archive/Admission" + fi.Name);
            }
            directory2 = new DirectoryInfo("CombinedLetters/Input/Scholarship");
            foreach (DirectoryInfo fi in directory2.GetDirectories())
            {
                Console.WriteLine(fi.ToString());
                Directory.Move(fi.ToString(), "CombinedLetters/Archive/Scolarship" + fi.Name);
            }
            Console.Read();
        }

    }
}

