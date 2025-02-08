using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;

namespace AupDotNetTests.ExEdit
{
    [TestClass]
    public class TrackbarTest
    {
        [DataTestMethod]
        [DataRow(0x1234006FU, TrackbarType.Script, TrackbarFlag.Acceleration | TrackbarFlag.Deceleration, 0x1234)]
        [DataRow(0x00010040U, TrackbarType.Stop, TrackbarFlag.Acceleration, 1)]
        [DataRow(0x00000021U, TrackbarType.Liner, TrackbarFlag.Deceleration, 0)]
        [DataRow(0x00100002U, TrackbarType.Curve, (TrackbarFlag)0, 0x10)]
        public void Test_Transition(uint transition, TrackbarType type, TrackbarFlag flag, int script)
        {
            Trackbar trackbar = new Trackbar(0, 0, transition, 0);
            Assert.AreEqual(type, trackbar.Type);
            Assert.AreEqual(flag, trackbar.Flag);
            Assert.AreEqual(script, trackbar.ScriptIndex);
            Assert.AreEqual(transition, trackbar.Transition);
        }

        [TestMethod]
        public void Test_ToString()
        {
            Trackbar trackbar = new Trackbar(1, 2, 0, 100);
            Assert.AreEqual("1", trackbar.ToString(1, null));

            trackbar.Type = TrackbarType.Liner;
            trackbar.Flag = 0;
            Assert.AreEqual("1,2,1", trackbar.ToString(1, null));
            Assert.AreEqual("0.1,0.2,1", trackbar.ToString(10, null));
            Assert.AreEqual("0.01,0.02,1", trackbar.ToString(100, null));
            trackbar.Flag = TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,65", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Deceleration;
            Assert.AreEqual("1,2,33", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Deceleration | TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,97", trackbar.ToString(1, null));

            trackbar.Type = TrackbarType.Curve;
            trackbar.Flag = 0;
            Assert.AreEqual("1,2,2", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,66", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Deceleration;
            Assert.AreEqual("1,2,34", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Deceleration | TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,98", trackbar.ToString(1, null));

            trackbar.Type = TrackbarType.Step;
            trackbar.Flag = 0;
            Assert.AreEqual("1,2,3", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,3", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Deceleration;
            Assert.AreEqual("1,2,3", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Deceleration | TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,3", trackbar.ToString(1, null));

            trackbar.Type = TrackbarType.IgnoreKeyframe;
            trackbar.Flag = 0;
            Assert.AreEqual("1,2,4", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,68", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Deceleration;
            Assert.AreEqual("1,2,36", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Deceleration | TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,100", trackbar.ToString(1, null));

            trackbar.Type = TrackbarType.Movement;
            trackbar.Flag = 0;
            Assert.AreEqual("1,2,5", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,5", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Deceleration;
            Assert.AreEqual("1,2,5", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Deceleration | TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,5", trackbar.ToString(1, null));

            trackbar.Type = TrackbarType.Random;
            trackbar.Flag = 0;
            Assert.AreEqual("1,2,6,100", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,6,100", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Deceleration;
            Assert.AreEqual("1,2,6,100", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Deceleration | TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,6,100", trackbar.ToString(1, null));

            trackbar.Type = TrackbarType.AccelDecel;
            trackbar.Flag = 0;
            Assert.AreEqual("1,2,7", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,71", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Deceleration;
            Assert.AreEqual("1,2,39", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Deceleration | TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,103", trackbar.ToString(1, null));

            trackbar.Type = TrackbarType.Repeat;
            trackbar.Flag = 0;
            Assert.AreEqual("1,2,8,100", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,72,100", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Deceleration;
            Assert.AreEqual("1,2,40,100", trackbar.ToString(1, null));
            trackbar.Flag = TrackbarFlag.Deceleration | TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,104,100", trackbar.ToString(1, null));

            trackbar.Type = TrackbarType.Script;
            trackbar.Flag = 0;
            TrackbarScript script = TrackbarScript.Defaults[0];
            Assert.AreEqual("1,2,15@補間移動", trackbar.ToString(1, script));
            trackbar.Flag = TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,79@補間移動", trackbar.ToString(1, script));
            trackbar.Flag = TrackbarFlag.Deceleration;
            Assert.AreEqual("1,2,47@補間移動", trackbar.ToString(1, script));
            trackbar.Flag = TrackbarFlag.Deceleration | TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,111@補間移動", trackbar.ToString(1, script));

            trackbar.Type = TrackbarType.Script;
            trackbar.Flag = 0;
            script = TrackbarScript.Defaults[1];
            Assert.AreEqual("1,2,15@回転,100", trackbar.ToString(1, script));
            trackbar.Flag = TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,15@回転,100", trackbar.ToString(1, script));
            trackbar.Flag = TrackbarFlag.Deceleration;
            Assert.AreEqual("1,2,15@回転,100", trackbar.ToString(1, script));
            trackbar.Flag = TrackbarFlag.Deceleration | TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,15@回転,100", trackbar.ToString(1, script));

            trackbar.Type = TrackbarType.Script;
            trackbar.Flag = 0;
            script = new TrackbarScript("track@bar") { EnableParam = false, EnableSpeed = false };
            Assert.AreEqual("1,2,15@track@bar", trackbar.ToString(1, script));
            trackbar.Flag = TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,15@track@bar", trackbar.ToString(1, script));
            trackbar.Flag = TrackbarFlag.Deceleration;
            Assert.AreEqual("1,2,15@track@bar", trackbar.ToString(1, script));
            trackbar.Flag = TrackbarFlag.Deceleration | TrackbarFlag.Acceleration;
            Assert.AreEqual("1,2,15@track@bar", trackbar.ToString(1, script));
        }

        [DataTestMethod]
        [DataRow(@"TestData\Exedit\Trackbar.aup")]
        public void Test_Read(string filename)
        {
            AviUtlProject aup = new AviUtlProject(filename);
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);
            string jsonPath = Path.Combine(
                Path.GetDirectoryName(filename),
                Path.GetFileNameWithoutExtension(filename) + "_trackbar.json");
            string jsonText = File.ReadAllText(jsonPath);
            List<Trackbar> expected = JsonSerializer.Deserialize<List<Trackbar>>(jsonText);
            List<Trackbar> actual = exedit.Objects.SelectMany(
                obj => obj.Effects.SelectMany(
                    effect => effect.Trackbars)).ToList();

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Current, actual[i].Current, $"{i}: Current");
                Assert.AreEqual(expected[i].Next, actual[i].Next, $"{i}: Next");
                Assert.AreEqual(expected[i].Type, actual[i].Type, $"{i}: Type");
                Assert.AreEqual(expected[i].Flag, actual[i].Flag, $"{i}: Flag");
                Assert.AreEqual(expected[i].ScriptIndex, actual[i].ScriptIndex, $"{i}: ScriptIndex");
                Assert.AreEqual(expected[i].Parameter, actual[i].Parameter, $"{i}: Parameter");
            }
        }
    }
}
