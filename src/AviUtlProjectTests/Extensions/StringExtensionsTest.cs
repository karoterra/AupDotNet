using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Karoterra.AviUtlProject.Extensions;

namespace AviUtlProjectTests.Extensions
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void Test_CutNull()
        {
            string x = "abcあいう";
            string y = "abcあいう";
            Assert.AreEqual(x.CutNull(), y);

            x = "abcあいう\0xxx";
            y = "abcあいう";
            Assert.AreEqual(x.CutNull(), y);

            x = "\0abcあいう";
            y = "";
            Assert.AreEqual(x.CutNull(), y);
        }
    }
}
