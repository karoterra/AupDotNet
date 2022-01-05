using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace AupDotNetTests
{
    [TestClass]
    public class TestSetup
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
#if !NET472
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
#endif
        }
    }
}
