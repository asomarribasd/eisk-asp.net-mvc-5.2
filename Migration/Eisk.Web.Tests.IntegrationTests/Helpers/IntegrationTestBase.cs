namespace Eisk.Helpers
{
    public class IntegrationTestBase
    {
        protected IntegrationTestBase()
        {
            TestInitialize();
        }

        public void TestInitialize()
        {
            //Generate test data
            TestDataHelper.InitializeSchemaAndData(@"..\..\..\Eisk.Web\App_Data\TestData.xml");
        }
        
    }
}