using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit
{
    [TestClass]
    public class EffectTypeTest
    {
        static EffectType[] Defaults = new EffectType[]
        {
            new EffectType(0, 0x04000448, 2, 3, 284, "動画ファイル"),
            new EffectType(1, 0x04000408, 0, 1, 260, "画像ファイル"),
            new EffectType(2, 0x04200408, 2, 3, 280, "音声ファイル"),
            new EffectType(3, 0x04000408, 2, 5, 2096, "テキスト"),
            new EffectType(4, 0x04000408, 3, 2, 264, "図形"),
            new EffectType(5, 0x04000008, 0, 1, 0, "フレームバッファ"),
            new EffectType(6, 0x04000408, 4, 5, 304, "音声波形表示"),
            new EffectType(7, 0x04000408, 2, 2, 4, "シーン"),
            new EffectType(8, 0x04200408, 2, 3, 4, "シーン(音声)"),
            new EffectType(9, 0x04000008, 0, 0, 0, "直前オブジェクト"),
            new EffectType(10, 0x440004D0, 6, 1, 4, "標準描画"),
            new EffectType(11, 0x440004D0, 12, 2, 4, "拡張描画"),
            new EffectType(12, 0x04200090, 2, 0, 0, "標準再生"),
            new EffectType(13, 0x44000450, 16, 5, 4, "パーティクル出力"),
            new EffectType(14, 0x04000400, 2, 3, 516, "シーンチェンジ"),
            new EffectType(15, 0x04000020, 5, 1, 0, "色調補正"),
            new EffectType(16, 0x04000000, 5, 1, 0, "色調補正"),
            new EffectType(17, 0x04000020, 4, 1, 0, "クリッピング"),
            new EffectType(18, 0x04000020, 3, 1, 0, "ぼかし"),
            new EffectType(19, 0x04000020, 2, 1, 0, "境界ぼかし"),
            new EffectType(20, 0x04000000, 3, 0, 0, "ぼかし"),
            new EffectType(21, 0x04000020, 1, 1, 0, "モザイク"),
            new EffectType(22, 0x04000000, 1, 1, 0, "モザイク"),
            new EffectType(23, 0x04000420, 4, 2, 4, "発光"),
            new EffectType(24, 0x04000400, 4, 1, 4, "発光"),
            new EffectType(25, 0x04000420, 3, 3, 8, "閃光"),
            new EffectType(26, 0x04000020, 2, 1, 0, "拡散光"),
            new EffectType(27, 0x04000000, 2, 0, 0, "拡散光"),
            new EffectType(28, 0x04000420, 4, 3, 8, "グロー"),
            new EffectType(29, 0x04000400, 4, 2, 8, "グロー"),
            new EffectType(30, 0x04000420, 3, 3, 12, "クロマキー"),
            new EffectType(31, 0x04000420, 3, 1, 12, "カラーキー"),
            new EffectType(32, 0x04000420, 2, 1, 4, "ルミナンスキー"),
            new EffectType(33, 0x04000420, 3, 2, 4, "ライト"),
            new EffectType(34, 0x44000420, 4, 3, 260, "シャドー"),
            new EffectType(35, 0x44000420, 2, 2, 260, "縁取り"),
            new EffectType(36, 0x04000020, 3, 0, 0, "凸エッジ"),
            new EffectType(37, 0x04000420, 2, 3, 4, "エッジ抽出"),
            new EffectType(38, 0x04000020, 2, 0, 0, "シャープ"),
            new EffectType(39, 0x04000020, 2, 0, 0, "フェード"),
            new EffectType(40, 0x04000420, 3, 3, 260, "ワイプ"),
            new EffectType(41, 0x04000620, 6, 3, 264, "マスク"),
            new EffectType(42, 0x04000020, 5, 0, 0, "斜めクリッピング"),
            new EffectType(43, 0x04000020, 3, 1, 0, "放射ブラー"),
            new EffectType(44, 0x44000000, 3, 0, 0, "放射ブラー"),
            new EffectType(45, 0x04000020, 2, 1, 0, "方向ブラー"),
            new EffectType(46, 0x44000000, 2, 0, 0, "方向ブラー"),
            new EffectType(47, 0x04000020, 2, 1, 0, "レンズブラー"),
            new EffectType(48, 0x04000000, 2, 0, 0, "レンズブラー"),
            new EffectType(49, 0x04000020, 2, 3, 0, "モーションブラー"),
            new EffectType(50, 0x04000000, 2, 1, 0, "モーションブラー"),
            new EffectType(51, 0x04008020, 3, 0, 0, "座標"),
            new EffectType(52, 0x04008020, 3, 0, 0, "拡大率"),
            new EffectType(53, 0x04008020, 1, 0, 0, "透明度"),
            new EffectType(54, 0x04008020, 3, 0, 0, "回転"),
            new EffectType(55, 0x04008020, 4, 1, 0, "領域拡張"),
            new EffectType(56, 0x04008020, 3, 2, 0, "リサイズ"),
            new EffectType(57, 0x04008020, 1, 0, 0, "ローテーション"),
            new EffectType(58, 0x04000020, 4, 2, 0, "振動"),
            new EffectType(59, 0x04000000, 4, 2, 0, "振動"),
            new EffectType(60, 0x04008020, 0, 5, 0, "反転"),
            new EffectType(61, 0x04000000, 0, 4, 0, "反転"),
            new EffectType(62, 0x04000420, 3, 2, 4, "ミラー"),
            new EffectType(63, 0x04000020, 3, 2, 0, "ラスター"),
            new EffectType(64, 0x04000000, 3, 2, 0, "ラスター"),
            new EffectType(65, 0x04000420, 5, 1, 12, "波紋"),
            new EffectType(66, 0x04000020, 4, 1, 0, "画像ループ"),
            new EffectType(67, 0x04000000, 4, 0, 0, "画像ループ"),
            new EffectType(68, 0x04000020, 4, 0, 0, "極座標変換"),
            new EffectType(69, 0x04000620, 8, 3, 268, "ディスプレイスメントマップ"),
            new EffectType(70, 0x04000420, 7, 3, 16, "ノイズ"),
            new EffectType(71, 0x04000420, 3, 1, 4, "色ずれ"),
            new EffectType(72, 0x04000400, 3, 1, 4, "色ずれ"),
            new EffectType(73, 0x04000420, 1, 2, 4, "単色化"),
            new EffectType(74, 0x04000400, 1, 2, 4, "単色化"),
            new EffectType(75, 0x04000420, 5, 4, 16, "グラデーション"),
            new EffectType(76, 0x04000220, 3, 3, 0, "拡張色設定"),
            new EffectType(77, 0x04000200, 3, 3, 0, "拡張色設定"),
            new EffectType(78, 0x04000420, 3, 2, 16, "特定色域変換"),
            new EffectType(79, 0x04000420, 4, 2, 516, "アニメーション効果"),
            new EffectType(80, 0x04000408, 4, 2, 516, "カスタムオブジェクト"),
            new EffectType(81, 0x04000420, 0, 0, 2048, "スクリプト制御"),
            new EffectType(82, 0x04000420, 5, 5, 284, "動画ファイル合成"),
            new EffectType(83, 0x04000420, 3, 3, 260, "画像ファイル合成"),
            new EffectType(84, 0x04000420, 0, 1, 4, "インターレース解除"),
            new EffectType(85, 0x04000020, 0, 4, 0, "カメラ制御オプション"),
            new EffectType(86, 0x04000020, 0, 0, 0, "オフスクリーン描画"),
            new EffectType(87, 0x04000020, 2, 0, 0, "オブジェクト分割"),
            new EffectType(88, 0x44000500, 6, 2, 260, "部分フィルタ"),
            new EffectType(89, 0x04200020, 2, 0, 0, "音量フェード"),
            new EffectType(90, 0x04200020, 2, 0, 0, "音声ディレイ"),
            new EffectType(91, 0x04200000, 2, 0, 0, "音声ディレイ"),
            new EffectType(92, 0x04200020, 1, 0, 0, "モノラル化"),
            new EffectType(93, 0x05000400, 3, 1, 20, "時間制御"),
            new EffectType(94, 0x45000420, 7, 2, 20, "グループ制御"),
            new EffectType(95, 0x45800400, 10, 1, 20, "カメラ制御"),
            new EffectType(96, 0x45800400, 8, 2, 4, "カメラ制御(拡張描画)"),
            new EffectType(97, 0x05000400, 4, 2, 516, "カメラ効果"),
            new EffectType(98, 0x45000000, 5, 0, 0, "シャドー(カメラ制御)"),
            new EffectType(99, 0x05000400, 0, 0, 2048, "スクリプト(カメラ制御)"),
            new EffectType(100, 0x02000000, 3, 0, 0, "ノイズ除去フィルタ"),
            new EffectType(101, 0x02000000, 4, 0, 0, "シャープフィルタ"),
            new EffectType(102, 0x02000000, 4, 0, 0, "ぼかしフィルタ"),
            new EffectType(103, 0x02004400, 4, 0, 12, "クリッピング＆リサイズ"),
            new EffectType(104, 0x02000000, 4, 2, 0, "縁塗りつぶし"),
            new EffectType(105, 0x02001000, 6, 0, 0, "色調補正"),
            new EffectType(106, 0x02001000, 15, 3, 0, "拡張色調補正"),
            new EffectType(107, 0x02200000, 1, 0, 0, "音量の調整"),
        };

        [TestMethod]
        public void Test_EffectTypeDefaults()
        {
            Assert.AreEqual(Defaults.Length, EffectType.Defaults.Length);
            foreach (var (expected, actual) in Defaults.Zip(EffectType.Defaults, (x, y) => (x, y)))
            {
                Assert.IsTrue(expected.Equals(actual), $"{expected.Id}: {expected.Name}");
            }
        }

        public static void ValidateEffectTypeId(Effect effect, EffectTypeId id)
        {
            Assert.IsTrue(effect.Type.Equals(Defaults[(int)id]), effect.GetType().Name);
        }

        [TestMethod]
        public void Test_EffectTypeId()
        {
            ValidateEffectTypeId(new VideoFileEffect(), EffectTypeId.VideoFile);
            ValidateEffectTypeId(new ImageFileEffect(), EffectTypeId.ImageFile);
            ValidateEffectTypeId(new AudioFileEffect(), EffectTypeId.AudioFile);
            ValidateEffectTypeId(new TextEffect(), EffectTypeId.Text);
            ValidateEffectTypeId(new FigureEffect(), EffectTypeId.Figure);
            ValidateEffectTypeId(new FrameBufferEffect(), EffectTypeId.FrameBuffer);
            ValidateEffectTypeId(new WaveformEffect(), EffectTypeId.Waveform);
            ValidateEffectTypeId(new SceneEffect(), EffectTypeId.Scene);
            ValidateEffectTypeId(new SceneAudioEffect(), EffectTypeId.SceneAudio);
            ValidateEffectTypeId(new PreviousObjectEffect(), EffectTypeId.PreviousObject);
            ValidateEffectTypeId(new StandardDrawEffect(), EffectTypeId.StandardDraw);
            ValidateEffectTypeId(new ExtendedDrawEffect(), EffectTypeId.ExtendedDraw);
            ValidateEffectTypeId(new StandardPlaybackEffect(), EffectTypeId.StandardPlayback);
            ValidateEffectTypeId(new ParticleEffect(), EffectTypeId.Particle);
            ValidateEffectTypeId(new SceneChangeEffect(), EffectTypeId.SceneChange);
            ValidateEffectTypeId(new ColorCorrectionEffect(), EffectTypeId.ColorCorrection);
            ValidateEffectTypeId(new ColorCorrectionFilterEffect(), EffectTypeId.ColorCorrectionFilter);
            ValidateEffectTypeId(new ClippingEffect(), EffectTypeId.Clipping);
            ValidateEffectTypeId(new BlurEffect(), EffectTypeId.Blur);
            ValidateEffectTypeId(new BorderBlurEffect(), EffectTypeId.BorderBlur);
            ValidateEffectTypeId(new BlurFilterEffect(), EffectTypeId.BlurFilter);
            ValidateEffectTypeId(new MosaicEffect(), EffectTypeId.Mosaic);
            ValidateEffectTypeId(new MosaicFilterEffect(), EffectTypeId.MosaicFilter);
            ValidateEffectTypeId(new EmissionEffect(), EffectTypeId.Emission);
            ValidateEffectTypeId(new EmissionFilterEffect(), EffectTypeId.EmissionFilter);
            ValidateEffectTypeId(new FlashEffect(), EffectTypeId.Flash);
            ValidateEffectTypeId(new DiffuseEffect(), EffectTypeId.Diffuse);
            ValidateEffectTypeId(new DiffuseFilterEffect(), EffectTypeId.DiffuseFilter);
            ValidateEffectTypeId(new GlowEffect(), EffectTypeId.Glow);
            ValidateEffectTypeId(new GlowFilterEffect(), EffectTypeId.GlowFilter);
            ValidateEffectTypeId(new ChromaKeyEffect(), EffectTypeId.ChromaKey);
            ValidateEffectTypeId(new ColorKeyEffect(), EffectTypeId.ColorKey);
            ValidateEffectTypeId(new LuminanceKeyEffect(), EffectTypeId.LuminanceKey);
            ValidateEffectTypeId(new LightEffect(), EffectTypeId.Light);
            ValidateEffectTypeId(new ShadowEffect(), EffectTypeId.Shadow);
            ValidateEffectTypeId(new BorderEffect(), EffectTypeId.Border);
            ValidateEffectTypeId(new BevelEffect(), EffectTypeId.Bevel);
            ValidateEffectTypeId(new EdgeExtractionEffect(), EffectTypeId.EdgeExtraction);
            ValidateEffectTypeId(new SharpenEffect(), EffectTypeId.Sharpen);
            ValidateEffectTypeId(new FadeEffect(), EffectTypeId.Fade);
            ValidateEffectTypeId(new WipeEffect(), EffectTypeId.Wipe);
            ValidateEffectTypeId(new MaskEffect(), EffectTypeId.Mask);
            ValidateEffectTypeId(new DiagonalClippingEffect(), EffectTypeId.DiagonalClipping);
            ValidateEffectTypeId(new RadialBlurEffect(), EffectTypeId.RadialBlur);
            ValidateEffectTypeId(new RadialBlurFilterEffect(), EffectTypeId.RadialBlurFilter);
            ValidateEffectTypeId(new DirectionalBlurEffect(), EffectTypeId.DirectionalBlur);
            ValidateEffectTypeId(new DirectionalBlurFilterEffect(), EffectTypeId.DirectionalBlurFilter);
            ValidateEffectTypeId(new LensBlurEffect(), EffectTypeId.LensBlur);
            ValidateEffectTypeId(new LensBlurFilterEffect(), EffectTypeId.LensBlurFilter);
            ValidateEffectTypeId(new MotionBlurEffect(), EffectTypeId.MotionBlur);
            ValidateEffectTypeId(new MotionBlurFilterEffect(), EffectTypeId.MotionBlurFilter);
            ValidateEffectTypeId(new PositionEffect(), EffectTypeId.Position);
            ValidateEffectTypeId(new ZoomEffect(), EffectTypeId.Zoom);
            ValidateEffectTypeId(new TransparencyEffect(), EffectTypeId.Transparency);
            ValidateEffectTypeId(new RotationEffect(), EffectTypeId.Rotation);
            ValidateEffectTypeId(new AreaExpansionEffect(), EffectTypeId.AreaExpansion);
            ValidateEffectTypeId(new ResizeEffect(), EffectTypeId.Resize);
            ValidateEffectTypeId(new Rotation90Effect(), EffectTypeId.Rotation90);
            ValidateEffectTypeId(new VibrationEffect(), EffectTypeId.Vibration);
            ValidateEffectTypeId(new VibrationFilterEffect(), EffectTypeId.VibrationFilter);
            ValidateEffectTypeId(new InversionEffect(), EffectTypeId.Inversion);
            ValidateEffectTypeId(new InversionFilterEffect(), EffectTypeId.InversionFilter);
            ValidateEffectTypeId(new MirrorEffect(), EffectTypeId.Mirror);
            ValidateEffectTypeId(new RasterEffect(), EffectTypeId.Raster);
            ValidateEffectTypeId(new RasterFilterEffect(), EffectTypeId.RasterFilter);
            ValidateEffectTypeId(new RippleEffect(), EffectTypeId.Ripple);
            ValidateEffectTypeId(new ImageLoopEffect(), EffectTypeId.ImageLoop);
            ValidateEffectTypeId(new ImageLoopFilterEffect(), EffectTypeId.ImageLoopFilter);
            ValidateEffectTypeId(new PolarTransformEffect(), EffectTypeId.PolarTransform);
            ValidateEffectTypeId(new DisplacementEffect(), EffectTypeId.Displacement);
            ValidateEffectTypeId(new NoiseEffect(), EffectTypeId.Noise);
            ValidateEffectTypeId(new ColorShiftEffect(), EffectTypeId.ColorShift);
            ValidateEffectTypeId(new ColorShiftFilterEffect(), EffectTypeId.ColorShiftFilter);
            ValidateEffectTypeId(new MonochromaticEffect(), EffectTypeId.Monochromatic);
            ValidateEffectTypeId(new MonochromaticFilterEffect(), EffectTypeId.MonochromaticFilter);
            ValidateEffectTypeId(new GradationEffect(), EffectTypeId.Gradation);
            ValidateEffectTypeId(new ColorSettingExEffect(), EffectTypeId.ColorSettingEx);
            ValidateEffectTypeId(new ColorSettingExFilterEffect(), EffectTypeId.ColorSettingExFilter);
            ValidateEffectTypeId(new GamutConversionEffect(), EffectTypeId.GamutConversion);
            ValidateEffectTypeId(new AnimationEffect(), EffectTypeId.AnimationEffect);
            ValidateEffectTypeId(new CustomObjectEffect(), EffectTypeId.CustomObject);
            ValidateEffectTypeId(new ScriptEffect(), EffectTypeId.Script);
            ValidateEffectTypeId(new VideoCompositionEffect(), EffectTypeId.VideoComposition);
            ValidateEffectTypeId(new ImageCompositionEffect(), EffectTypeId.ImageComposition);
            ValidateEffectTypeId(new DeinterlacingEffect(), EffectTypeId.Deinterlacing);
            ValidateEffectTypeId(new CameraOptionEffect(), EffectTypeId.CameraOption);
            ValidateEffectTypeId(new OffScreenEffect(), EffectTypeId.OffScreen);
            ValidateEffectTypeId(new SplitEffect(), EffectTypeId.Split);
            ValidateEffectTypeId(new PartialFilterEffect(), EffectTypeId.PartialFilter);
            ValidateEffectTypeId(new AudioFadeEffect(), EffectTypeId.AudioFade);
            ValidateEffectTypeId(new AudioDelayEffect(), EffectTypeId.AudioDelay);
            ValidateEffectTypeId(new AudioDelayFilterEffect(), EffectTypeId.AudioDelayFilter);
            ValidateEffectTypeId(new MonauralEffect(), EffectTypeId.Monaural);
            ValidateEffectTypeId(new TimeControlEffect(), EffectTypeId.TimeControl);
            ValidateEffectTypeId(new GroupControlEffect(), EffectTypeId.GroupControl);
            ValidateEffectTypeId(new CameraControlEffect(), EffectTypeId.CameraControl);

            ValidateEffectTypeId(new CameraEffect(), EffectTypeId.CameraEffect);
            ValidateEffectTypeId(new CameraShadowEffect(), EffectTypeId.CameraShadow);
            ValidateEffectTypeId(new CameraScriptEffect(), EffectTypeId.CameraScript);
            ValidateEffectTypeId(new DenoiseFilterEffect(), EffectTypeId.DenoiseFilter);
            ValidateEffectTypeId(new SharpenFilterEffect(), EffectTypeId.SharpenFilter);
            ValidateEffectTypeId(new BlurFilter2Effect(), EffectTypeId.BlurFilter2);
            ValidateEffectTypeId(new ClipResizeFilterEffect(), EffectTypeId.ClipResizeFilter);
            ValidateEffectTypeId(new FillBorderEffect(), EffectTypeId.FillBorder);
            ValidateEffectTypeId(new ColorCorrection2Effect(), EffectTypeId.ColorCorrection2);
            ValidateEffectTypeId(new ColorCorrectionExEffect(), EffectTypeId.ColorCorrectionEx);
            ValidateEffectTypeId(new VolumeFilterEffect(), EffectTypeId.VolumeFilter);
        }
    }
}
