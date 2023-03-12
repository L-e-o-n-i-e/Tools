using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfBitsAndBytes
{
    //Keywork : BRUTE FORCE ALGORITHM 
    class Program
    {
        static readonly int passwordLength = 6; //Can you solve up to 6?


        static void Main(string[] args)
        {
            BankOfBitsNBytes bbb = new BankOfBitsNBytes(passwordLength);
            char[] currentPassword = new char[passwordLength];

            for (int i = 0; i < passwordLength; i++)
            {
                currentPassword[i] = BankOfBitsNBytes.acceptablePasswordChars[0];
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            while (bbb.WithdrawMoney(currentPassword) != -1)
            {
                currentPassword = IncrementPassword(currentPassword);
                //OutputCharArray(currentPassword);
            }

            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            Console.ReadLine();
        }


        //This is very expensive and just for debugging. You do not need to output in the final test
        static void OutputCharArray(char[] toOut)
        {
            Console.Out.WriteLine(new string(toOut));
        }

        static char[] IncrementPassword(char[] currentPassword)
        {
            int i = passwordLength - 1;

            while (i >= 0)
            {
                int nextIndex = (Array.IndexOf(BankOfBitsNBytes.acceptablePasswordChars, currentPassword[i]) + 1) % BankOfBitsNBytes.acceptablePasswordChars.Length;
                currentPassword[i] = BankOfBitsNBytes.acceptablePasswordChars[nextIndex];

                if (nextIndex != 0)
                {
                    break;
                }
                else
                    i--;
            }

            return currentPassword;
        }

        //If you fully rob the bank, you can kill the thread from the outside
        //Variables can be read and when it changes, you can tell to stop the thread
    }
}
