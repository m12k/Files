using BTSS.Data.Projects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BTSS.Common;
using BTSS.Logic.Projects;

namespace TestProject1
{
    
    
    /// <summary>
    ///This is a test class for UserTest and is intended
    ///to contain all UserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserTest
    {


        private TestContext testContextInstance;

        string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=TINMatching;Integrated Security=True"; // TODO: Initialize to an appropriate value

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for IsExist
        ///</summary>
        [TestMethod()]
        public void IsExistTest()
        {
             
            BTSS.Data.Projects.User target = new BTSS.Data.Projects.User(connectionString, Core.DataProvider.SQL); // TODO: Initialize to an appropriate value
            string userName = "ltcmitc"; // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsExist(userName);
            Assert.AreEqual(expected, actual);             
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            string connectionString = string.Empty; // TODO: Initialize to an appropriate value
            Core.DataProvider provider = new Core.DataProvider(); // TODO: Initialize to an appropriate value
            BTSS.Data.Projects.User target = new BTSS.Data.Projects.User(connectionString, provider); // TODO: Initialize to an appropriate value
            BTSS.Logic.Projects.User user = null; // TODO: Initialize to an appropriate value
              
            target.Save(user);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
