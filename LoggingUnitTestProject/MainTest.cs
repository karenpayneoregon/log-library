using System;
using System.Diagnostics;
using System.IO;
using LoggingUnitTestProject.Base;
using LogLibrary.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace LoggingUnitTestProject
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        
        [TestMethod]
        [TestTraits(Trait.GeneralLogging)]
        public void UnhandledException()
        {

            try
            {
                NullException();
            }
            catch (Exception exception1)
            {
                ApplicationLog.Instance.WriteException(exception1, "Comment");
            }
            

            try
            {
                OpenNonExistingFile();
            }
            catch (Exception exception1)
            {
                ApplicationLog.Instance.WriteException(exception1, "Comment");
            }

            try
            {
                OutOfRangeException();
            }
            catch (Exception exception1)
            {
                ApplicationLog.Instance.WriteException(exception1, "Comment");
            }

            ApplicationLog.Instance.WriteException(new Exception("Test"), "Comment");
            ApplicationLog.Instance.WriteException(new Exception("Test"));
            
        }

        /// <summary>
        /// Write text to main log without an exception which is done above
        /// </summary>
        [TestMethod]
        public void WriteTextToMainLog()
        {
            ApplicationLog.Instance.WriteInformation(LoremIpsumParagraphWithDate() + "\n");
            ApplicationLog.Instance.WriteInformation(LoremIpsumParagraph() + "\n");
        }


        /// <summary>
        /// Demonstrate creating incrementing files e.g.
        /// first time Log_1.txt, next Log_2.txt etc.
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.IncrementalLogging)]
        public void IncrementLogging()
        {
            // write a simple string to a file
            ApplicationLog.Instance.Write("Some text 1");
            
            // write a paragraph of text to a file
            ApplicationLog.Instance.Write(LoremIpsumParagraph());
            
            Assert.IsTrue(File.Exists("Log_1.txt") && File.Exists("Log_2.txt"));
            
        }

        /// <summary>
        /// Test get last incremental log file name
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.IncrementalLogging)]
        public void GetLastLogFileName()
        {
            // write a simple string to a file
            ApplicationLog.Instance.Write("Some text 1");

            // write a paragraph of text to a file
            ApplicationLog.Instance.Write(LoremIpsumParagraph());

            Assert.IsTrue(Path.GetFileName(ApplicationLog.Instance.LastName()) == "Log_2.txt");

        }


    }
}
