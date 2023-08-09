using System;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;

namespace FBISProblem
{
    public class LetterService : ILetterService
    {
        public void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile)
        {
            string readFile1 = File.ReadAllText(inputFile1);
            string readFile2 = File.ReadAllText(inputFile2);
            Directory.CreateDirectory(resultFile);

            using (FileStream fs = File.Create(resultFile+"/combined-"+inputFile1.Substring(inputFile1.Length - 12)))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(readFile1+"\n\n"+readFile2);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
        }
    }
}

