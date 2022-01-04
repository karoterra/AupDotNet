using System;
using System.Collections.Generic;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;

namespace AupDotNetTests.ExEdit
{
    public static class ExeditTestUtil
    {
        public static ExEditProject GetExEdit(AviUtlProject aup, IEffectFactory effectFactory = null)
        {
            var filter = aup.FilterProjects
                .Where(f => f.Name == "拡張編集")
                .First() as RawFilterProject;
            return new ExEditProject(filter, effectFactory);
        }

        public static TimelineObject GetObject(ExEditProject exedit, int scene, int layer, int frame)
        {
            return exedit.Objects
                .Where(obj => obj.SceneIndex == scene && obj.LayerIndex == layer && obj.StartFrame == frame)
                .First();
        }
    }
}
