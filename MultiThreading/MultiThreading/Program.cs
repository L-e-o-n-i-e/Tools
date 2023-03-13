using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankOfBitsAndBytes
{
    //Keywork : BRUTE FORCE ALGORITHM 
    class Program
    {
        static readonly int passwordLength = 6; //Can you solve up to 6?     
        public delegate void delg(BankOfBitsNBytes bbb, char[] start);
        static bool pswFound = false, bankRobbed = false;

        static void Main(string[] args)
        {
            BankOfBitsNBytes bbb = new BankOfBitsNBytes(passwordLength);
            #region Password
            //initialize current password
            char[] currentPassword = new char[passwordLength];
            ResetCurrentPassword(ref currentPassword);
            #endregion
            #region Delegates
            //Create a List of delegate
            List<delg> delgList = new List<delg>();
            Stack<delg> toProcess = new Stack<delg>();
            ResetJobs(ref delgList, ref toProcess);
            #endregion
            #region Threads
            Stack<Thread> threads = new Stack<Thread>();
            List<Thread> pooledThreads = new List<Thread>();

            List<Thread> activeThreads = new List<Thread>();
            #endregion

            int start = 0;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();



            while (toProcess.Count > 0 && !bankRobbed)
            {
                if (pswFound)
                {
                    ResetJobs(ref delgList, ref toProcess);
                    currentPassword[0] = 'a';
                    ResetPassword(currentPassword);
                    start = 0;
                    pswFound = false;
                }
                else
                {
                    delg nextToProc = toProcess.Pop();
                    // Console.WriteLine("Job has started for : " + (char)(start + 97));
                    currentPassword[0] = BankOfBitsNBytes.acceptablePasswordChars[start];
                    currentPassword = ResetPassword(currentPassword);
                    Thread t = StartThread(nextToProc, bbb, currentPassword);
                    t.Join();
                    //Console.WriteLine("Thread # " + start + " started. Letter : " + currentPassword[0]);
                    //nextToProc.Invoke(bbb, currentPassword);
                    if (start < BankOfBitsNBytes.acceptablePasswordChars.Length - 1)
                        start++;
                    else
                        break;


                }
            }


            #region StopWatch
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            #endregion
            Console.WriteLine("Program finished");
            Console.ReadLine();

        }

        private static void ResetCurrentPassword(ref char[] currentPassword)
        {
            for (int i = 0; i < passwordLength; i++)
            {
                currentPassword[i] = BankOfBitsNBytes.acceptablePasswordChars[0];
            }
        }

        private static char[] ResetPassword(char[] currentPassword)
        {
            char[] newPsw = currentPassword.ToArray();
            if (currentPassword.Length > 0)
            {
                for (int i = 1; i < newPsw.Length; i++)
                {
                    newPsw[i] = 'a';
                }
            }
            return newPsw;
        }
        private static void ResetJobs(ref List<delg> delgList, ref Stack<delg> toProcess)
        {
            for (int i = 0; i < BankOfBitsNBytes.acceptablePasswordChars.Length; i++)
            {
                delgList.Add(new delg(Job));
            }
            toProcess = new Stack<delg>(delgList);
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

        static void Job(BankOfBitsNBytes bbb, char[] currentPassword)
        {
            int amount = 0;
            char start = currentPassword[0];

            amount = bbb.WithdrawMoney(currentPassword);
            while (amount != -1 && amount != 500 && !pswFound && !bankRobbed)
            {
                if (!IsJobDone(currentPassword))
                {
                    currentPassword = IncrementPassword(currentPassword);
                    //OutputCharArray(currentPassword);
                    amount = bbb.WithdrawMoney(currentPassword);
                }
                else
                    break;
            }
            if (amount == 500)
            {
                pswFound = true;
            }
            else if (amount == -1)
            {
                bankRobbed = true;
            }

        }
        static bool IsJobDone(char[] currentPassword)
        {
            bool jobIsDone = false;
            if (currentPassword.Length > 1 && currentPassword[1] == (char)122)
            {
                for (int i = 1; i < currentPassword.Length; i++)
                {
                    if (currentPassword[i] == (char)122)
                        jobIsDone = true;
                    else
                    {
                        jobIsDone = false;
                        break;
                    }
                }
            }
            if (currentPassword.Length == 1)
            {
                jobIsDone = currentPassword[0] == (char)122 ? true : false;
            }
            return jobIsDone;
        }


        public static Thread StartThread(delg g, BankOfBitsNBytes bbb, char[] pswd)
        {
            var t = new Thread(() => g.Invoke(bbb, pswd));
            t.Start();
            return t;
        }

        //If you fully rob the bank, you can kill the thread from the outside

        //Variables can be read and when it changes, you can tell to stop the thread
    }

}

