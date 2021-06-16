// Matthew Rutigliano
// 8 April 2021
// Revision History: Object initialization and modeChange written 4/7/21
// resetAll and reviveAll written 4/8/21

// Program Overview:
// An array of dubPrime objects is created, with linearly varying seeds.
// Each object is queried until it changes modes, with the resulting up mode value,
// down mode value, and amount of queries necessary for the transition being printed.
// All the objects are reset, and the process is repeated, without the query values being
// printed.
// All the objects are revived, and the process is repeated one last time.

using System;

namespace P1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int DUB_PRIME_NUM = 5;
            dubPrime[] primeArr = new dubPrime[DUB_PRIME_NUM];
            for (int i = 0; i < DUB_PRIME_NUM; i++)
                primeArr[i] = new dubPrime(i+1);

            modeChange(primeArr, true);

            resetAll(primeArr);

            modeChange(primeArr, false);

            reviveAll(primeArr);

            modeChange(primeArr, false);
        }

        // printQueries parameter determines if query results are printed
        static void modeChange(dubPrime[] arr, bool printQueries = true)
        {
            int[] queryLimits = new int[arr.Length];
            int queryNum = 2;
            int initialQuery, downQuery, count;
            if (printQueries) { Console.WriteLine($"Querying value: {queryNum}"); }
            for (int i = 0; i < arr.Length; i++)
            {
                if (!arr[i].Active) { Console.WriteLine($"Object {i} has been deactivated"); }
                else
                {
                    initialQuery = arr[i].query(queryNum);
                    if (printQueries) { Console.WriteLine($"Object {i} up mode query: {initialQuery}"); }
                    downQuery = arr[i].query(queryNum);
                    count = 0;
                    while (initialQuery == downQuery && downQuery != -1)
                    {
                        downQuery = arr[i].query(queryNum);
                        count++;
                    }
                    queryLimits[i] = count;
                    if (printQueries) { Console.WriteLine($"Object {i} down mode query: {downQuery}"); }
                }
            }
            Console.WriteLine("Amount of queries necessary to change to down mode:");
            for (int i = 0; i < queryLimits.Length; i++)
                Console.WriteLine($"Object {i} : {queryLimits[i]}");
        }
        static void resetAll(dubPrime[] arr)
        {
            Console.WriteLine("Resetting objects. Queries before transition should be the same.");
            foreach (dubPrime prime in arr)
                prime.reset();
        }
        static void reviveAll(dubPrime[] arr)
        {
            Console.WriteLine("Reviving objects. Queries before transition should change.");
            foreach (dubPrime prime in arr)
                prime.revive();
        }
    }
}
