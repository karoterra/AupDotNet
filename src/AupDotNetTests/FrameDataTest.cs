using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Karoterra.AupDotNet;

namespace AupDotNetTests
{
    [TestClass]
    public class FrameDataTest
    {
        public class EqualData
        {
            public bool Result { get; set; }
            public FrameData A { get; set; }
            public FrameData B { get; set; }
        }

        [DataTestMethod]
        [DataRow(@"TestData\FrameData\Equals_000.json")]
        [DataRow(@"TestData\FrameData\Equals_001.json")]
        [DataRow(@"TestData\FrameData\Equals_002.json")]
        [DataRow(@"TestData\FrameData\Equals_003.json")]
        [DataRow(@"TestData\FrameData\Equals_004.json")]
        [DataRow(@"TestData\FrameData\Equals_005.json")]
        [DataRow(@"TestData\FrameData\Equals_006.json")]
        [DataRow(@"TestData\FrameData\Equals_007.json")]
        [DataRow(@"TestData\FrameData\Equals_008.json")]
        [DataRow(@"TestData\FrameData\Equals_009.json")]
        [DataRow(@"TestData\FrameData\Equals_010.json")]
        [DataRow(@"TestData\FrameData\Equals_011.json")]
        [DataRow(@"TestData\FrameData\Equals_012.json")]
        public void Test_Equals(string path)
        {
            var json = File.ReadAllText(path);
            var data = JsonSerializer.Deserialize<EqualData>(json);
            bool actual = data.A.Equals(data.B);
            Assert.AreEqual(data.Result, actual);
            if (actual)
            {
                Assert.AreEqual(data.A.GetHashCode(), data.B.GetHashCode());
            }
        }
    }
}
