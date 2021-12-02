using System;
using System.Collections.Generic;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// シーンチェンジ
    /// <list type="definition">
    ///     <item>
    ///         <term>組み込みシーンチェンジ</term>
    ///         <description>Nameは空、ScriptIdにインデックス入り、Paramsはnull。</description>
    ///     </item>
    ///     <item>
    ///         <term>exedit.scnのスクリプト</term>
    ///         <description>Nameにスクリプト名、ScriptIdに1が入る。</description>
    ///     </item>
    ///     <item>
    ///         <term>ユーザ定義スクリプト</term>
    ///         <description>Nameにスクリプト名、ScriptIdに2が入る。</description>
    ///     </item>
    ///     <item>
    ///         <term>transition画像</term>
    ///         <description>Nameに画像ファイル名(拡張子無し)、ScriptIdに0が入り、Paramsはnull。</description>
    ///     </item>
    /// </list>
    /// </summary>
    public class SceneChangeEffect : ScriptFileEffect
    {
        public static EffectType EffectType { get; }

        /// <summary>
        /// 組み込みシーンチェンジ名の一覧。
        /// Nameが空の時にはこれのインデックスがScriptIdに入る。
        /// </summary>
        public static readonly IReadOnlyList<string> Defaults;

        /// <summary>
        /// exedit.scnで定義されているスクリプト。
        /// スクリプト名がNameに入り、ScriptIdは1になる。
        /// </summary>
        public static readonly IReadOnlyList<string> DefaultScripts;

        /// <summary>
        /// 「反転」チェックボックス
        /// </summary>
        public bool Reverse
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public SceneChangeEffect()
            : base(EffectType)
        {
        }

        public SceneChangeEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public SceneChangeEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        static SceneChangeEffect()
        {
            EffectType = new EffectType(
                14, 0x04000400, 2, 3, 516, "シーンチェンジ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("調整", 100, 0, 0, 0),
                    new TrackbarDefinition("track1", 100, 0, 0, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("反転", true, 0),
                    new CheckboxDefinition("check0", true, 0),
                    new CheckboxDefinition("クロスフェード", false, 0),
                }
            );
            Defaults = new List<string>()
            {
                "クロスフェード",
                "ワイプ(円)",
                "ワイプ(四角)",
                "ワイプ(時計)",
                "スライス",
                "スワップ",
                "スライド",
                "縮小回転",
                "押し出し(横)",
                "押し出し(縦)",
                "回転(横)",
                "回転(縦)",
                "キューブ回転(横)",
                "キューブ回転(縦)",
                "フェードアウトイン",
                "放射ブラー",
                "ぼかし",
                "ワイプ(横)",
                "ワイプ(縦)",
                "ロール(横)",
                "ロール(縦)",
                "ランダムライン",
            };
            DefaultScripts = new List<string>()
            {
                "発光",
                "レンズブラー",
                "ドア",
                "起き上がる",
                "リール回転",
                "図形ワイプ",
                "図形で隠す",
                "図形で隠す(放射)",
                "砕け散る",
                "ページめくり",
            };
        }
    }
}
