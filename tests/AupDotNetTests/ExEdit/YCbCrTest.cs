using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet.ExEdit;

namespace AupDotNetTests.ExEdit
{
    [TestClass]
    public class YCbCrTest
    {
        [DataTestMethod]
        [DataRow(0, 0, 0, "000000000000")]
        [DataRow(4660, 2748, 32767, "3412bc0aff7f")]
        [DataRow(-1, -4779, -30875, "ffff55ed6587")]
        public void Test_ToString(int y, int cb, int cr, string expected)
        {
            YCbCr color = new YCbCr(y, cb, cr);
            Assert.AreEqual(expected, color.ToString());
        }
    }
}
