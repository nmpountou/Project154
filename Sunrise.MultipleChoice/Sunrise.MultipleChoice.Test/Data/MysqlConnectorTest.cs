using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sunrise.MultipleChoice.Data;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace Sunrise.MultipleChoice.Test.Data
{
    [TestClass]
    public class MysqlConnectorTest
    {
        private static MysqlConnector mysql;

        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) {

            mysql = new MysqlConnector("root", "Megastructures91", "127.0.0.1", "3306", "gps_zero");
            mysql.initializeConnection();
        }

        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup() { }

        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize() { }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup() { }

        [TestMethod]
        public void OpenCloseDatabase()
        {
            try
            {
                mysql.openMysqlConnection();
                mysql.closeMysqlConnection();

            }catch(MySqlException)
            {
                Assert.Fail();
            }

        }

    }
}
