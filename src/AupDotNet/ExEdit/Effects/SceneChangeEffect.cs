using System;
using System.Collections.Generic;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// シーンチェンジ
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>シーンチェンジの種類</term>
    ///         <description>説明</description>
    ///     </listheader>
    ///     <item>
    ///         <term>組み込みシーンチェンジ</term>
    ///         <description>
    ///             <see cref="ScriptFileEffect.Name"/> は空、
    ///             <see cref="ScriptFileEffect.ScriptId"/> にインデックスが入り、
    ///             <see cref="ScriptFileEffect.Params"/> は<c>null</c>。
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term>exedit.scnのスクリプト</term>
    ///         <description>
    ///             <see cref="ScriptFileEffect.Name"/> にスクリプト名、
    ///             <see cref="ScriptFileEffect.ScriptId"/> に1が入る。
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term>ユーザ定義スクリプト</term>
    ///         <description>
    ///             <see cref="ScriptFileEffect.Name"/> にスクリプト名、
    ///             <see cref="ScriptFileEffect.ScriptId"/> に2が入る。
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <term>transition画像</term>
    ///         <description>
    ///             <see cref="ScriptFileEffect.Name"/> に画像ファイル名(拡張子無し)、
    ///             <see cref="ScriptFileEffect.ScriptId"/> に0が入り、
    ///             <see cref="ScriptFileEffect.Params"/> は<c>null</c>。
    ///         </description>
    ///     </item>
    /// </list>
    /// </remarks>
    public class SceneChangeEffect : ScriptFileEffect
    {
        /// <summary>
        /// シーンチェンジのフィルタ効果定義。
        /// </summary>
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

        /// <summary>
        /// <see cref="SceneChangeEffect"/> のインスタンスを初期化します。
        /// </summary>
        public SceneChangeEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="SceneChangeEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public SceneChangeEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="SceneChangeEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
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
