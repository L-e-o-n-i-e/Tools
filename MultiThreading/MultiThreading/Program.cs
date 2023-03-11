using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfBitsAndBytes
{
    //Keywork : BRUTE FORCE ALGORITHM 
    //All I know is the nb of char in the password
    class Program
    {
        static readonly int passwordLength = 2; //Can you solve up to 6?


        static void Main(string[] args)
        {
            BankOfBitsNBytes bbb = new BankOfBitsNBytes(passwordLength);
            int robbedamount = 0;
            int start = 0;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            

            while ( robbedamount != -1 )
            {
                //on fait des guess
                robbedamount = Guess(bbb, ref start);
                if (robbedamount != 500)
                    start++;
                if(start > 24)
                    break;
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

        private char IncrementAtIndex(char[] guess, int index, int max)
        {
            return guess[index] < max ? (char)((int)guess[index]++) : (char)97;
        }

        static int Guess(BankOfBitsNBytes bbb, ref int start)
        {
            int amount = 0;
            bool jobDone = false;
            char[] guess = new char[passwordLength];
            guess[0] = (char)(start + 97);

            //Initialize the password with char 'a' everywhere
            for (int i = passwordLength - 1; i > 0; i--)
            {
                guess[i] = (char)97;
            }


            //Increment from right to left
            for (int i = (passwordLength - 1); i > 0; i--)
            {
                for (int j = 97; j < BankOfBitsNBytes.acceptablePasswordChars.Length + 97; j++)
                {
                    guess[i] = (char)j;
                    OutputCharArray(guess);
                    amount = bbb.WithdrawMoney(guess);
                    if (amount == 500)
                    {
                        start = 0;
                        jobDone = true;
                        return 500;
                    }
                    if (amount == -1)
                    {
                        jobDone = true;
                        return -1;
                    }
                }
            }
            jobDone = true;


            return amount;
        }


        //If you fully rob the bank, you can kill the thread from the outside
        //Variables can be read and when it changes, you can tell to stop the thread
    }
}
