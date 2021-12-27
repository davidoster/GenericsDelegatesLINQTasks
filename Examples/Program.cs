using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Examples
{
    class Program
    {
        public delegate int[] OddEvenNumbers(int howMany);

        public static int[] OddNumbers(int howMany) { return new int[] { 1, 3, 5 }; }
        public static int[] EvenNumbers(int howMany) { return new int[] { 2, 4, 6 }; }

        static void Main(string[] args)
        {
            #region Generics
            DataStore<string> store = new DataStore<string>();
            store.Data = "Hello World!";
            //store.Data = 123; // works?? in compile?

            DataStore<string> strStore = new DataStore<string>();
            strStore.Data = "Hello World!";
            //strStore.Data = 123; // compile-time error

            DataStore<int> intStore = new DataStore<int>();
            intStore.Data = 100;
            //intStore.Data = "Hello World!"; // compile-time error

            KeyValuePair<int, string> kvp1 = new KeyValuePair<int, string>();
            kvp1.Key = 100;
            kvp1.Value = "Hundred";

            KeyValuePair<string, string> kvp2 = new KeyValuePair<string, string>();
            kvp2.Key = "IT";
            kvp2.Value = "Information Technology";
            #endregion

            #region Delegates & Lambda
            OddEvenNumbers oddNumbers = new OddEvenNumbers(OddNumbers);
            OddEvenNumbers evenNumbers = new OddEvenNumbers(EvenNumbers);
            Array.ForEach(oddNumbers(0), element => { Console.Write(element); Console.Write(' '); });
            Console.WriteLine();
            Array.ForEach(evenNumbers(0), element => { Console.Write(element); Console.Write(' '); });

            Func<int, int, int, int> sumThree = (x, y, z) => x + y + z;
            Console.WriteLine("\n" + sumThree(1, 2, 3));

            Action<int, int, int> sumThreeAction = (x, y, z) => Console.WriteLine(x + y + z);
            sumThreeAction(1, 2, 3);
            #endregion

            #region Tasks
            Task t = new Task(
                () =>
                {
                    System.Threading.Thread.Sleep(5000);
                    Console.WriteLine("Huge Task Finish");
                }
                );

            //Start the Task  
            t.Start();

            //Wait for 1 second  
            bool rValue = t.Wait(1000);
            Console.WriteLine("Main Process Finished");

            Console.WriteLine($"Main Thread : {Thread.CurrentThread.ManagedThreadId} Started");
            Task task1 = new Task(PrintCounter);
            task1.Start();
            Console.WriteLine($"Main Thread : {Thread.CurrentThread.ManagedThreadId} Completed");
            Console.ReadKey();

            #endregion

            #region LINQ
            //https://www.devcurry.com/2011/08/50-linq-examples-now-in-linqpad.html
            #endregion
            Console.ReadKey();
        }

        static void PrintCounter()
        {
            Console.WriteLine($"Child Thread : {Thread.CurrentThread.ManagedThreadId} Started");
            for (int count = 1; count <= 5; count++)
            {
                Console.WriteLine($"count value: {count}");
            }
            Console.WriteLine($"Child Thread : {Thread.CurrentThread.ManagedThreadId} Completed");
        }


    }

    #region Generics
    class DataStore<T>
    {
        public T Data { get; set; }
    }

    class KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
    #endregion
}
