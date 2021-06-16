// Matthew Rutigliano
// 8 April 2021
// Revision History: Data members, constructor, query, isPrime, isEven written 4/3/21
// reset and revive written 4/12/21

// Class Invariant:
// dubPrime object encapsulates prime number greater than 10
// This internal value is influenced by optional seed parameter inputted by user
// Querying object with value p will return nearest prime number greater than p away from internal value when in up mode,
// less than p away from internal value when in down mode
// Objects begin in up mode, then transition to down mode after an internally determined number of queries (user tracked)
// Amount of queries possible before transitioning will be unique to each object
// Once in down mode, object remains in down mode until it is reset or revived
// Resetting object returns it to up mode, allows the same amount of queries
// Reviving object returns it to up mode, allows an updated amount of queries
// Constructor determines internal value, number of queries possible before transitioning
// Any query returning a number less than 10 will permenantly deactivate the object (tracked by user)
// Once deactivated, query will return -1, reset and revive will do nothing, Active returns false

// Interface Invariant:
// Optional seed value must be greater than 0
// Client must track Active state


using System;
using System.Collections.Generic;
using System.Text;

namespace P1
{
    public class dubPrime 
    {
        
        private int x;
        private int queryCount;
        private int queryLimit;
        private bool mode;
        private bool isActive;
        private static int dubPrimeCount = 0;
        private static int xMin = 11;
        private static int queryLimitMultiplier = 2;

        // Postcondition: Object may become inactive
        public dubPrime(int seed = 17)
        {
            mode = true;
            queryLimit = ++dubPrimeCount * queryLimitMultiplier;

            x = seed * xMin;
            if (isEven(x)) { x++; }
            while(!isPrime(x) || x < 10) { x += 2; }
            isActive = x >= xMin;
        }

        // Post Condition: Object may transition from up mode to down mode
        // Object may become inactive
        public int query(int p)
        {
            int nextPrime = -1;
            if (isActive)
            {
                int e = p + 1;
                if (!isEven(e)) { e++; }

                if (mode)
                {
                    nextPrime = x + e;
                    while (!isPrime(nextPrime))
                        nextPrime = nextPrime + 2;
                }
                else
                {
                    nextPrime = x - e;
                    while (!isPrime(nextPrime))
                        nextPrime = nextPrime - 2;
                }

                mode = ++queryCount < queryLimit;
            }
            isActive = nextPrime > xMin;
            return nextPrime;
        }

        // Post Condition: Object will stay in or transition to up mode
        public void reset()
        {
            if (isActive)
            {
                queryCount = 0;
                mode = true;
            }
        }

        // Post Condition: Object will stay in or transition to up mode
        public void revive()
        {
            if (isActive) 
            {
                mode = true;
                queryLimit = (queryLimit * dubPrimeCount) + queryCount;
            }
        }

        public bool Active
        {
            get => isActive;
        }


        private bool isEven(int n)
        {
            return ((n % 2) == 0);
        }

        private bool isPrime(int n)
        {
            bool result = !(isEven(n));
            if (result)
            {
                for (int i = 3; i < n / 2; i=i+2)
                {
                    if ((n % i) == 0)
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }
    }
}

// Implementation Invariant: 
// Internal value (x) determined by multiplying seed by minimum acceptable value (11)
// Amount of queries before transitioning modes (queryLimit) determined by multiplying amount of query objects with arbitrary scaling value
// Resetting returns counter of queries performed (queryCount) to 0
// Reviving recalculates queryLimit as the old limit multiplied by the amount of objects, summed with the current queryCount
// Active parameter isActive controls query, reset, revive