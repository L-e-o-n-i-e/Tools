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
        static readonly int passwordLength = 3; //Can you solve up to 6?     
        public delegate void delg(BankOfBitsNBytes bbb, char[] start);
        static bool over = false;

        static void Main(string[] args)
        {
            BankOfBitsNBytes bbb = new BankOfBitsNBytes(passwordLength);
            char[] currentPassword = new char[passwordLength];
            #region Password
            //initialize current password
            for (int i = 0; i < passwordLength; i++)
            {
                currentPassword[i] = BankOfBitsNBytes.acceptablePasswordChars[0];
            }
            #endregion
            #region Delegates
            //Create a List of delegate
            List<delg> delgList = new List<delg>();
            for (int i = 0; i < BankOfBitsNBytes.acceptablePasswordChars.Length; i++)
            {
                delgList.Add(new delg(Job));
            }
            Stack<delg> toProcess = new Stack<delg>(delgList);
            #endregion

            int start = 0;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            while (toProcess.Count > 0)
            {
                delg nextToProc = toProcess.Pop();
                Console.WriteLine("Job has started for : " + (char)(start + 97));
                currentPassword[0] = BankOfBitsNBytes.acceptablePasswordChars[start];
                currentPassword = ResetPassword(currentPassword);
                //StartThread(nextToProc, bbb, currentPassword);
                nextToProc.Invoke(bbb, currentPassword);
                if (start < BankOfBitsNBytes.acceptablePasswordChars.Length -1)
                    start++;
                else
                    break;
            }         

            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            Console.WriteLine("Program finished");
            Console.ReadLine();
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
            char start = currentPassword[0];
            bool jobIsDone = false;

            while (bbb.WithdrawMoney(currentPassword) != -1)
            {
                if (!IsJobDone(currentPassword))
                {
                    currentPassword = IncrementPassword(currentPassword);
                    //OutputCharArray(currentPassword);
                }
                else
                    break;                
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
            if(currentPassword.Length == 1)
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



    //    BankOfBitsNBytes bbb = new BankOfBitsNBytes(passwordLength);
    //    bool bankRobbed = false;
    //    char[] currentPassword = new char[passwordLength];
    //    char[] startPassword = new char[passwordLength];
    //    char[] endPassword = new char[passwordLength];

    //    Queue<PswdDelg> jobs = new Queue<PswdDelg>();



    //            for (int i = 0; i<passwordLength; i++)
    //            {
    //                //Setting the current password
    //                currentPassword[i] = BankOfBitsNBytes.acceptablePasswordChars[0];
    //                //Setting the start password 
    //                startPassword[i] = BankOfBitsNBytes.acceptablePasswordChars[0];
    //                //setting the end password (where the job ends)
    //                endPassword[i] = BankOfBitsNBytes.acceptablePasswordChars[0];

    //                //Making a queue of jobs, one job for every digit
    //                jobs.Enqueue(new PswdDelg(IncrementPassword));
    //            }
    //endPassword[0] = BankOfBitsNBytes.acceptablePasswordChars[1];
    //            bool jobIsDone = false;

    //Stopwatch stopWatch = new Stopwatch();
    //stopWatch.Start();

    //            while (bbb.WithdrawMoney(currentPassword) != -1 && !jobIsDone)
    //            {
    //                //startPassword[0] = BankOfBitsNBytes.acceptablePasswordChars[start];
    //                //start += start;
    //                //endPassword[0] = BankOfBitsNBytes.acceptablePasswordChars[start];
    //                currentPassword = IncrementPassword(currentPassword, endPassword);
    //                if(currentPassword == endPassword)
    //                {
    //                    jobIsDone = true;
    //                }
    //                //Taking the first job out
    //                //PswdDelg passwordGenerator = jobs.Dequeue();
    //                //char[] newPassword = passwordGenerator(startPassword, endPassword);
    //                //OutputCharArray(currentPassword);
    //            }
    //            bankRobbed = true;

    //            TimeSpan ts = stopWatch.Elapsed;
    //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    //ts.Hours, ts.Minutes, ts.Seconds,
    //ts.Milliseconds / 10);
    //Console.WriteLine("RunTime " + elapsedTime);
    //            Console.ReadLine();
    //        }


    //        //This is very expensive and just for debugging. You do not need to output in the final test
    //        static void OutputCharArray(char[] toOut)
    //{
    //    Console.Out.WriteLine(new string(toOut));
    //}

    //static char[] IncrementPassword(char[] currentPassword, char[] endPassword)
    //{
    //    int i = passwordLength - 1;

    //    while (i >= 0)
    //    {
    //        int nextIndex = (Array.IndexOf(BankOfBitsNBytes.acceptablePasswordChars, currentPassword[i]) + 1) % BankOfBitsNBytes.acceptablePasswordChars.Length;
    //        currentPassword[i] = BankOfBitsNBytes.acceptablePasswordChars[nextIndex];
    //        OutputCharArray(currentPassword);

    //        if (nextIndex != 0)
    //        {
    //            break;
    //        }
    //        if (currentPassword == endPassword)
    //        {
    //            //endJob
    //            break;
    //        }
    //        else
    //            i--;
    //    }

    //    return currentPassword;
    //}

    ////If you fully rob the bank, you can kill the thread from the outside
    ////Variables can be read and when it changes, you can tell to stop the thread


    //#region Threads
    //public static void StartThread(delg d)
    //{
    //    //ThreadStart ts = new ThreadStart(d);
    //    //Thread t = new Thread(ts);
    //    //t.Start();
    //}

    //List<delg> delgList = new List<delg>()
    //{
    //    //BiggerNightClub,
    //    //BiggerNightClub,
    //    //NightClub,
    //    //() => someInt++,
    //    //() => BiggerNightClub(),
    //    //() => PrintOutChar('a', 50)
    //};

    //Stack<delg> toProcess = new Stack<delg>();

    //        //    while(toProcess.Count > 0)
    //        //    {
    //        //        delg nextToProc = toProcess.Pop();
    //        //StartThread(nextToProc);
    //        //Console.WriteLine("Program finished");
    //        //    Console.ReadLine();
    //        //}       
    //    }

    //#endregion
}

