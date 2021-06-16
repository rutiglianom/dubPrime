// Matthew Rutigliano
// 8 April 2021
// Revision History: query_up_mode, query_down_mode, query_mode_shift written 4/5/21
// query_mode_shift_varies, invalid_number_deactivates_object, reset_mode_change, 
// reset_query_count, revive_mode_change, revive_query_count written 4/6/21

using P1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dubPrimeTests
{
    [TestClass]
    public class dubPrimeTests
    {
        [TestMethod]
        public void query_up_mode()
        {
            int queryVal = 3;
            int queryExpectedUp = 197;
            dubPrime prime = new dubPrime();
            int queryResultUp = prime.query(queryVal);
            Assert.AreEqual(queryExpectedUp, queryResultUp, "Query calculation is incorrect in up mode");
        }

        [TestMethod]
        public void query_down_mode()
        {
            int queryVal = 3;
            int queryExpectedDown = 181;
            int queryResultDown = 0;
            int modeChange = 5;
            dubPrime prime = new dubPrime();
            for(int i=0; i<modeChange; i++)
                queryResultDown = prime.query(queryVal);
            Assert.AreEqual(queryExpectedDown, queryResultDown, "Query calculation is incorrect in down mode");
        }

        [TestMethod]
        public void query_mode_shift()
        {
            int queryVal = 3;
            dubPrime prime = new dubPrime();
            int queryResultUp = prime.query(queryVal);
            int queryResultDown = prime.query(queryVal);
            int i = 0;
            int testLength = 100;
            while (queryResultUp == queryResultDown && i++ < testLength)
            {
                queryResultDown = prime.query(queryVal);
            }
            Assert.AreNotEqual(queryResultUp, queryResultDown, "Object doesn't transition to down mode");
        }

        [TestMethod]
        public void query_mode_shift_varies()
        {
            int queryVal = 3;
            dubPrime prime1 = new dubPrime();
            dubPrime prime2 = new dubPrime();
            int queryResultUp = prime1.query(queryVal);
            int queryResultDown = prime1.query(queryVal);
            int change1 = 0;
            int testLength = 100;
            while (queryResultUp == queryResultDown && change1++ < testLength)
            {
                queryResultDown = prime1.query(queryVal);
            }
            queryResultUp = prime2.query(queryVal);
            queryResultDown = prime2.query(queryVal);
            int change2 = 0;
            while (queryResultUp == queryResultDown && change2++ < testLength)
            {
                queryResultDown = prime2.query(queryVal);
            }
            Assert.AreNotEqual(change1, change2, "Multiple objects transition after the same amount of queries");
        }

        [TestMethod]
        public void invalid_number_deactivates_object()
        {
            dubPrime invalid = new dubPrime(1); 
            int i = 0;
            int testLength = 100;
            while (invalid.Active && i++ < testLength)
            {
                invalid.query(1);
            }
            Assert.IsFalse(invalid.Active, "Object doesn't deactivate");
        }

        [TestMethod]
        public void reset_mode_change()
        {
            dubPrime prime = new dubPrime();
            int modeSwitch = 5;
            int queryVal = 3;
            int i = 0;
            int queryResultUp1 = prime.query(queryVal);
            int queryResultDown = prime.query(queryVal);

            while (queryResultUp1 == queryResultDown && i++ < modeSwitch)
                queryResultDown = prime.query(queryVal);

            prime.reset();

            int queryResultUp2 = prime.query(queryVal);
            Assert.AreEqual(queryResultUp1, queryResultUp2, "Resetting doesn't restore up mode");
        }

        [TestMethod]
        public void reset_query_count()
        {
            dubPrime prime = new dubPrime();
            int modeSwitch = 5;
            int queryVal = 3;
            int i = 0;
            int j = 0;
            int queryResultUp = prime.query(queryVal);
            int queryResultDown = prime.query(queryVal);

            while (queryResultUp == queryResultDown && i++ < modeSwitch)
                queryResultDown = prime.query(queryVal);

            prime.reset(); 

            queryResultUp = prime.query(queryVal);
            queryResultDown = prime.query(queryVal);

            while (queryResultUp == queryResultDown && j++ < modeSwitch)
                queryResultDown = prime.query(queryVal);

            Assert.AreEqual(i, j, "Resetting changes amount of queries before mode change");
        }

        [TestMethod]
        public void revive_mode_change()
        {
            dubPrime prime = new dubPrime();
            int modeSwitch = 5;
            int queryVal = 3;
            int i = 0;
            int queryResultUp1 = prime.query(queryVal);
            int queryResultDown = prime.query(queryVal);

            while (queryResultUp1 == queryResultDown && i++ < modeSwitch)
                queryResultDown = prime.query(queryVal);

            prime.revive();

            int queryResultUp2 = prime.query(queryVal);
            Assert.AreEqual(queryResultUp1, queryResultUp2, "Reviving doesn't restore up mode");
        }

        [TestMethod]
        public void revive_query_count()
        {
            dubPrime prime = new dubPrime();
            int testLength = 100;
            int queryVal = 3;
            int i = 0;
            int j = 0;
            int queryResultUp = prime.query(queryVal);
            int queryResultDown = prime.query(queryVal);

            while (queryResultUp == queryResultDown && i++ < testLength)
                queryResultDown = prime.query(queryVal);

            prime.revive();

            queryResultUp = prime.query(queryVal);
            queryResultDown = prime.query(queryVal);

            while (queryResultUp == queryResultDown && j++ < testLength)
                queryResultDown = prime.query(queryVal);

            Assert.AreNotEqual(i, j, "Reviving doesn't change amount of queries before mode change");
        }
    }
}
