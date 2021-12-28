using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Examples
{
    class Program
    {
        // Step 1. Create a delegate with parameters and return types as expected
        public delegate void Koukou();

        public delegate int[] OddEvenNumbers(int howMany);

        public delegate string PrintFullName(string first, string last);

        // Step 2. Create AT LEAST two delegate implementations of the delegate
        // implementation means that on Step 3. we need to instantite using this 
        // method as a parameter within the delegate constructor
        public static int[] OddNumbers(int howMany) { return new int[] { 1, 3, 5 }; }
        public static int[] EvenNumbers(int howMany) { return new int[] { 2, 4, 6 }; }

        public static string PrintFirstLastName(string firstName, string lastName) { return firstName + lastName; }
        public static string PrintLastFirstName(string firstName, string lastName) { return lastName + firstName; }

        // falsy delegation - DON'T USE, wrong return type
        public static int XNumbers(int x) { return 10; }

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

            DataStore<ICollection<string>> myCollectionStore = new DataStore<ICollection<string>>();
            myCollectionStore.Data = new List<string>();

            MultipleDataStore<string, int> multipleDataStore = new MultipleDataStore<string, int>();
            multipleDataStore.Data1 = "Hello";
            multipleDataStore.Data2 = 100;



            KeyValuePair<int, string> kvp1 = new KeyValuePair<int, string>();
            kvp1.Key = 100;
            kvp1.Value = "Hundred";

            KeyValuePair<string, string> kvp2 = new KeyValuePair<string, string>();
            kvp2.Key = "IT";
            kvp2.Value = "Information Technology";
            #endregion


            //string PrintFirstLastName(string firstName, string lastName) { return firstName + lastName; }


            #region Delegates & Lambda
            //Program p = new Program();
            // Step 3. We instatiate a new variable of type delegate using the 
            // delegate implementation method as a parameter
            OddEvenNumbers oddNumbers = new OddEvenNumbers(OddNumbers);
            OddEvenNumbers evenNumbers = new OddEvenNumbers(EvenNumbers);
            //OddEvenNumbers xNumvers = new OddEvenNumbers(XNumbers);

            Console.WriteLine(new PrintFullName(PrintFirstLastName)("George", "Pasparakis"));
            Console.WriteLine(new PrintFullName(PrintLastFirstName)("George", "Pasparakis"));

            Array.ForEach(oddNumbers(5), element => { Console.Write(element); Console.Write(' '); });
            Console.WriteLine();
            Array.ForEach(evenNumbers(0), element => { Console.Write(element); Console.Write(' '); });
            
            Func<int> myNoneTest = () => 50;
            myNoneTest();

            Func<int, int> myTest = x => x + 10;
            Console.WriteLine(myTest(1));

            Func<int, int, int, int> sumThree = (x, y, z) => x + y + z;
            Console.WriteLine("\n" + sumThree(1, 2, 3));

            Func<string, string, string> PrintFirstLastFunc = (first, last) => first + " " + last;
            Console.WriteLine(new PrintFullName(PrintFirstLastFunc)("George", "PaspARakis"));

            Action<int, int, int> sumThreeAction = (x, y, z) => Console.WriteLine(x + y + z);
            sumThreeAction(1, 2, 3);
            #endregion

            #region Tasks
            Task t = new Task(
                () =>
                {
                    System.Threading.Thread.Sleep(3000);
                    Console.WriteLine("Huge Task Finish");
                }
                );

            //Start the Task  
            t.Start();

            //Wait for 1 second  
            bool rValue = t.Wait(4000);
            Console.WriteLine($"Main Process Finished, rValue={rValue}");

            Console.WriteLine($"Main Thread : {Thread.CurrentThread.ManagedThreadId} Started");
            Task task1 = new Task(PrintCounter);
            task1.Start();
            Console.WriteLine($"Main Thread : {Thread.CurrentThread.ManagedThreadId} Completed");
            //Console.ReadKey();

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

    interface MyInterface<T, V>
    {
        void MyMethod(T par1, V par2);
    }

    class MultipleDataStore<T1, T2>
    {
        public T1 Data1 { get; set; }
        public T2 Data2 { get; set; }
    }

    class KeyValuePair<TKey, TValue>: IDictionary<TKey, TValue>
    {
        public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public void Add(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(System.Collections.Generic.KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(System.Collections.Generic.KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        // methods
    }
    #endregion
}
