using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 拡張描画
    /// </summary>
    public class ExtendedDrawEffect : Effect
    {
        /// <summary>
        /// 拡張描画のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>X</summary>
        public Trackbar X => Trackbars[0];

        /// <summary>Y</summary>
        public Trackbar Y => Trackbars[1];

        /// <summary>Z</summary>
        public Trackbar Z => Trackbars[2];

        /// <summary>拡大率</summary>
        public Trackbar Zoom => Trackbars[3];

        /// <summary>透明度</summary>
        public Trackbar Alpha => Trackbars[4];
        
        /// <summary>縦横比</summary>
        public Trackbar AspectRatio => Trackbars[5];
        
        /// <summary>X軸回転</summary>
        public Trackbar RotateX => Trackbars[6];
        
        /// <summary>Y軸回転</summary>
        public Trackbar RotateY => Trackbars[7];
        
        /// <summary>Z軸回転</summary>
        public Trackbar RotateZ => Trackbars[8];
        
        /// <summary>中心X</summary>
        public Trackbar CenterX => Trackbars[9];
        
        /// <summary>中心Y</summary>
        public Trackbar CenterY => Trackbars[10];
        
        /// <summary>中心Z</summary>
        public Trackbar CenterZ => Trackbars[11];

        /// <summary>裏面を表示しない</summary>
        public bool BackfaceCulling
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>合成モード</summary>
        public BlendMode BlendMode { get; set; }

        /// <summary>
        /// <see cref="ExtendedDrawEffect"/> のインスタンスを初期化します。
        /// </summary>
        public ExtendedDrawEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="ExtendedDrawEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public ExtendedDrawEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="ExtendedDrawEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public ExtendedDrawEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            BlendMode = (BlendMode)data.Slice(0, 4).ToInt32();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            ((int)BlendMode).ToBytes().CopyTo(data, 0);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("blend=");
            writer.WriteLine((int)BlendMode);
        }

        static ExtendedDrawEffect()
        {
            EffectType = new EffectType(
                11, 0x440004D0, 12, 2, 4, "拡張描画",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("X", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Y", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Z", 10, -999999, 999999, 0),
                    new TrackbarDefinition("拡大率", 100, 0, 500000, 10000),
                    new TrackbarDefinition("透明度", 10, 0, 1000, 0),
                    new TrackbarDefinition("縦横比", 10, -1000, 1000, 0),
                    new TrackbarDefinition("X軸回転", 100, -360000, 360000, 0),
                    new TrackbarDefinition("Y軸回転", 100, -360000, 360000, 0),
                    new TrackbarDefinition("Z軸回転", 100, -360000, 360000, 0),
                    new TrackbarDefinition("中心X", 10, -20000, 20000, 0),
                    new TrackbarDefinition("中心Y", 10, -20000, 20000, 0),
                    new TrackbarDefinition("中心Z", 10, -20000, 20000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("通常", false, 0),
                    new CheckboxDefinition("裏面を表示しない", true, 0),
                }
            );
        }
    }
}
