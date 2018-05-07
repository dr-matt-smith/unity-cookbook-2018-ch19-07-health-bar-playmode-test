using System;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine;

public class HealthBarUnitTests
{
    /// <summary>
    /// Test correct values.
    /// </summary>
    public class TestCorrectValues
    {
        [Test]
        public void DefaultHealthOne()
        {
            // Arrange 
            Player player = new Player();
            float expectedResult = 1;

            // Act
            float result = player.GetHealth();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void HealthCorrectAfterReducedByPointOne()
        {
            // Arrange 
            Player player = new Player();
            float expectedResult = 0.9f;

            // Act
            player.ReduceHealth(0.1f);
            float result = player.GetHealth();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void HealthCorrectAfterReducedByHalf()
        {
            // Arrange 
            Player player = new Player();
            float expectedResult = 0.5f;

            // Act
            player.ReduceHealth(0.5f);
            float result = player.GetHealth();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }

    /// <summary>
    /// Test limit not exceeded.
    /// </summary>
    public class TestLimitNotExceeded
    {
        [Test]
        public void HealthNotExceedMaximumOfOne()
        {
            // Arrange 
            Player player = new Player();
            float expectedResult = 1;

            // Act
            player.AddHealth(1);
            player.AddHealth(1);
            player.AddHealth(0.5f);
            player.AddHealth(0.1f);
            float result = player.GetHealth();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }


    /// <summary>
    /// Test events.
    /// </summary>
    public class TestEvents
    {
        [Test]
        public void CheckEventFiredWhenAddHealth()
        {
            // Arrange
            Player player = new Player();
            bool eventFired = false;

            // register anonymous 
            // adapted from:
            // http://www.philosophicalgeek.com/2007/12/27/easily-unit-testing-event-handlers/
            Player.OnHealthChange += delegate
            {
                eventFired = true;
            };

            // Act
            player.AddHealth(0.1f);

            // Assert
            Assert.IsTrue(eventFired);
        }

        [Test]
        public void CheckEventFiredWhenReduceHealth()
        {
            // Arrange
            Player player = new Player();
            bool eventFired = false;

            // register anonymous 
            // adapted from:
            // http://www.philosophicalgeek.com/2007/12/27/easily-unit-testing-event-handlers/
            Player.OnHealthChange += delegate
            {
                eventFired = true;
            };

            // Act
            player.ReduceHealth(0.1f);

            // Assert
            Assert.IsTrue(eventFired);
        }

        [Test]
        public void No_Events_When_Health_Not_Changed()
        {
            // Arrange
            Player player = new Player();
            int eventsFiredCount = 0;
            int expectedResult = 0;

            // register anonymous 
            // adapted from:
            // http://www.philosophicalgeek.com/2007/12/27/easily-unit-testing-event-handlers/
            Player.OnHealthChange += delegate
            {
                eventsFiredCount++;
            };

            // Act

            // Assert
            Assert.AreEqual(expectedResult, eventsFiredCount);
        }

    }


    /// <summary>
    /// Test exceptions.
    /// </summary>
    public class TestExceptions
    {
        [Test]
        public void Throws_Exception_When_Add_Health_Passed_Less_Than_Zero()
        {
            // Arrange 
            Player player = new Player();

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(
                delegate
                {
                    player.AddHealth(-1);
                }
            );
        }

        [Test]
        public void Throws_Exception_When_Reduce_Health_Passed_Less_Than_Zero()
        {
            // Arrange 
            Player player = new Player();

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(
                () => player.ReduceHealth(-1)
            );
        }
    }


    /// <summary>
    /// Test logging.
    /// </summary>
    public class TestLogging
    {
        [Test]
        public void Throws_Exception_When_Add_Health_Passed_Less_Than_Zero()
        {
            Debug.unityLogger.logEnabled = true;

            // Arrange 
            Player player = new Player();
            HealthChangeLogger healthChangeLogger = new HealthChangeLogger();
            string expectedResult = "health = 0.9";

            // Act
            player.ReduceHealth(0.1f);

            // Assert
            LogAssert.Expect(LogType.Log, expectedResult);
        }
    }
}