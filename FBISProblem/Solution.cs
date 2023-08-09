using System;
namespace FBISProblem
{
	public class Solution
	{
        static public void Main(String[] args)
        {
            FileGenerator generator = new FileGenerator();
            generator.generate();
            Console.WriteLine("Generated new files");
             

        }

	}
}

