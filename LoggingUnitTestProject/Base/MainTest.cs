using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogLibrary.Classes;


// ReSharper disable once CheckNamespace - do not change
namespace LoggingUnitTestProject
{
    public partial class MainTest
    {
        
        /// <summary>
        /// Actions to perform prior to a test method running
        /// See <see cref="Exceptions.Write()"/> method which uses the file name below
        /// which means change it here then change it in the Write method
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            if (TestContext.TestName == nameof(UnhandledException))
            {
                if (File.Exists("GeneralUnhandledException.txt"))
                {
                    File.Delete("GeneralUnhandledException.txt");
                }
            }
        }

        /// <summary>
        /// Any cleanup after running a test, use same logic as in <see cref="Init()"/>
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.TestName == nameof(IncrementLogging) || TestContext.TestName == nameof(GetLastLogFileName))
            {
                if (File.Exists("Log_1.txt"))
                {
                    File.Delete("Log_1.txt");
                }

                if (File.Exists("Log_2.txt"))
                {
                    File.Delete("Log_2.txt");
                }
            }
        }
        /// <summary>
        /// Perform any initialize for the class
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }

        #region To invoke exceptions for test method UnhandledException
        /// <summary>
        /// Attempt to open a non-existing file
        /// </summary>
        public void OpenNonExistingFile()
        {
            File.ReadAllLines("Karen.txt");
        }

        /// <summary>
        /// Cause a out of range exception
        /// </summary>
        public void OutOfRangeException()
        {
            string[] items = { "A", "B" };

            var bad = items[100];
        }

        /// <summary>
        /// Invoke a null exception
        /// </summary>
        public void NullException()
        {
            throw new NullReferenceException("Some value was null");
        }

        #endregion

        public static string LoremIpsumParagraphWithDate() => $"{DateTime.Now:G} {LoremIpsumParagraph()}";
        public static string LoremIpsumParagraph() => 
            "Pellentesque habitant morbi tristique senectus et netus et malesuada ames " + 
            "ac turpis egestas\nVestibulum tortor quam, feugiat vitae, ultricies eget, " + 
            "tempor sit amet, ante.\nDonec eu libero sit amet quam egestas semper. " + 
            "Aenean ultricies mi vitae est.";


    }

}
