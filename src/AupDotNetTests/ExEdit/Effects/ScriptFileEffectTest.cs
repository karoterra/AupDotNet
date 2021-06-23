using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    using Params = Dictionary<string, string>;

    [TestClass]
    public class ScriptFileEffectTest
    {

        [TestMethod]
        public void Test_ParseParams()
        {
            var strs = new string[]
            {
                null,
                "",
                "*",
                "a=0;",
                "local p0=\"abc\";local p1=123;",
                @"file=""C:\\path\\to\\file.jpg""",
                "s=\"abc;xyz\"",
                "s1=\"abc;xyz\";s2=\"a;b\\\"c\";",
            };
            var dics = new Params[]
            {
                new Params(),
                new Params(),
                null,
                new Params() { { "a", "0" } },
                new Params() { { "local p0", "\"abc\"" }, { "local p1", "123" } },
                new Params() { { "file", @"""C:\\path\\to\\file.jpg""" } },
                new Params() { { "s", "\"abc;xyz\"" } },
                new Params() { { "s1", "\"abc;xyz\"" }, { "s2", "\"a;b\\\"c\"" } },
            };

            for (int i = 0; i < strs.Length; i++)
            {
                var p = ScriptFileEffect.ParseParams(strs[i]);
                CollectionAssert.AreEqual(dics[i], p);
            }
        }
    }
}
