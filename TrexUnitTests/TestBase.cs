using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using trex;
using trex.Utils;

namespace TrexUnitTests
{
    public class IntegrationTestsBase
    {
        private TransactionScope _scope;

        [TestInitialize]
        public void Initialize()
        {
            this._scope = new TransactionScope();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this._scope.Dispose();
        }
    }

}
