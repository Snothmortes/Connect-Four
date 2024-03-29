﻿// <auto-generated />

namespace Connect_Four.Tests
{
    using System;
    using NUnit.Framework;
    using Connect_Four;
    using System.Collections.Generic;
    using Moq;

    [TestFixture()]
    public class ConnectFourTests
    {
        [TestCaseSource("TestCases")]
        public void WhoIsWinner_ShouldPassTestRuns(List<string> testCase)
        {

            var actual = ConnectFour.WhoIsWinner(testCase);
            Assert.That(actual, Is.EqualTo("Red"));
        }
        [TestCaseSource("FailingTest")]
        public void WhoIsWinner_ShouldPassFailingTest(List<string> testCase)
        {

            var actual = ConnectFour.WhoIsWinner(testCase);
            Assert.That(actual, Is.EqualTo("Yellow"));
        }

        public static List<List<string>> FailingTest()
        {
            return new List<List<string>>
            { 
                new List<string>
                {
                    "F_Yellow",
                    "A_Red",
                    "E_Yellow",
                    "F_Red",
                    "D_Yellow",
                    "F_Red",
                    "G_Yellow",
                    "B_Red",
                    "G_Yellow",
                    "A_Red",
                    "B_Yellow",
                    "F_Red",
                    "B_Yellow",
                    "F_Red",
                },
            };
        }

        public static List<List<string>> TestCases()
        {
            return new List<List<string>>
            {
                // Vertical cases
                new List<string>
                {
                    "A_Red",
                    "A_Red",
                    "A_Red",
                    "A_Red",
                },
                new List<string>
                {
                    "B_Yellow",
                    "B_Red",
                    "B_Red",
                    "B_Red",
                    "B_Red",
                },
                // Horizontal cases
                new List<string>
                {
                    "A_Red",
                    "B_Red",
                    "C_Red",
                    "D_Red",
                },
                new List<string>
                {
                    "A_Red",
                    "B_Yellow",
                    "C_Red",
                    "D_Yellow",
                    "A_Red",
                    "B_Red",
                    "C_Red",
                    "D_Red",
                },
                // Forward diagonal cases
                new List<string>
                {
                    "A_Red",
                    "B_Yellow",
                    "B_Red",
                    "C_Yellow",
                    "C_Yellow",
                    "C_Red",
                    "D_Yellow",
                    "D_Yellow",
                    "D_Yellow",
                    "D_Red",
                },
                new List<string>
                {
                    "A_Yellow",
                    "A_Red",
                    "B_Yellow",
                    "B_Yellow",
                    "B_Red",
                    "C_Red",
                    "C_Yellow",
                    "C_Yellow",
                    "C_Red",
                    "D_Yellow",
                    "D_Red",
                    "D_Yellow",
                    "D_Red",
                    "D_Red",
                },
            };
        }
    }
}