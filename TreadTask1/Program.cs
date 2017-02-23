using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TreadTask1
{
    class Program
    {
        static List<int[]> myValues = new List<int[]>();
        static int[] sumVals = new int[3];
        static List<Thread> workerThreads = new List<Thread>();
        static void Main(string[] args)
        {
            String line;
            StreamReader sr = new StreamReader("C:\\Temp\\Task2.txt");
            
            line = sr.ReadLine();
            while (line != null)
            {
                myValues.Add(Array.ConvertAll<string, int>(line.Split(','), int.Parse));
                line = sr.ReadLine();
            }
            sr.Close();
            for (int i = 0; i < 3; i++)
            {
                workerThreads.Add(new Thread(SumArray));
                workerThreads[i].Start(i);
            }
            foreach (Thread thread in workerThreads)
            {
                thread.Join();
            }

            Array.Sort(sumVals);
            StreamWriter sw = new StreamWriter("C:\\Temp\\AnswerTask2.txt");
            for (int i = 0; i < sumVals.Count(); i++)
            {
                sw.WriteLine(sumVals[i].ToString());
            }
            sw.Close();

        }
        static void SumArray(object treadID)
        {          
            int ID = (int) treadID;
            sumVals[ID] = 0;
            for (int x = 0; x < myValues[ID].Length; x++)
            {
                sumVals[ID] = sumVals[ID] + Convert.ToInt32(myValues[ID].GetValue(x));
            }
        }
    }
}
