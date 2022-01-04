namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// 組込みフィルタ効果のID。
    /// </summary>
    public enum EffectTypeId
    {
        /// <summary>動画ファイル</summary>
        VideoFile = 0,
        /// <summary>画像ファイル</summary>
        ImageFile,
        /// <summary>音声ファイル</summary>
        AudioFile,
        /// <summary>テキスト</summary>
        Text,
        /// <summary>図形</summary>
        Figure,
        /// <summary>フレームバッファ</summary>
        FrameBuffer,
        /// <summary>音声波形表示</summary>
        Waveform,
        /// <summary>シーン</summary>
        Scene,
        /// <summary>シーン(音声)</summary>
        SceneAudio,
        /// <summary>直前オブジェクト</summary>
        PreviousObject,
        /// <summary>標準描画</summary>
        StandardDraw,
        /// <summary>拡張描画</summary>
        ExtendedDraw,
        /// <summary>標準再生</summary>
        StandardPlayback,
        /// <summary>パーティクル出力</summary>
        Particle,
        /// <summary>シーンチェンジ</summary>
        SceneChange,
        /// <summary>色調補正</summary>
        ColorCorrection,
        /// <summary>色調補正(フィルタオブジェクト)</summary>
        ColorCorrectionFilter,
        /// <summary>クリッピング</summary>
        Clipping,
        /// <summary>ぼかし</summary>
        Blur,
        /// <summary>境界ぼかし</summary>
        BorderBlur,
        /// <summary>ぼかし(フィルタオブジェクト)</summary>
        BlurFilter,
        /// <summary>モザイク</summary>
        Mosaic,
        /// <summary>モザイク(フィルタオブジェクト)</summary>
        MosaicFilter,
        /// <summary>発光</summary>
        Emission,
        /// <summary>発光(フィルタオブジェクト)</summary>
        EmissionFilter,
        /// <summary>閃光</summary>
        Flash,
        /// <summary>拡散光</summary>
        Diffuse,
        /// <summary>拡散光(フィルタオブジェクト)</summary>
        DiffuseFilter,
        /// <summary>グロー</summary>
        Glow,
        /// <summary>グロー(フィルタオブジェクト)</summary>
        GlowFilter,
        /// <summary>クロマキー</summary>
        ChromaKey,
        /// <summary>カラーキー</summary>
        ColorKey,
        /// <summary>ルミナンスキー</summary>
        LuminanceKey,
        /// <summary>ライト</summary>
        Light,
        /// <summary>シャドー</summary>
        Shadow,
        /// <summary>縁取り</summary>
        Border,
        /// <summary>凸エッジ</summary>
        Bevel,
        /// <summary>エッジ抽出</summary>
        EdgeExtraction,
        /// <summary>シャープ</summary>
        Sharpen,
        /// <summary>フェード</summary>
        Fade,
        /// <summary>ワイプ</summary>
        Wipe,
        /// <summary>マスク</summary>
        Mask,
        /// <summary>斜めクリッピング</summary>
        DiagonalClipping,
        /// <summary>放射ブラー</summary>
        RadialBlur,
        /// <summary>放射ブラー(フィルタオブジェクト)</summary>
        RadialBlurFilter,
        /// <summary>方向ブラー</summary>
        DirectionalBlur,
        /// <summary>方向ブラー(フィルタオブジェクト)</summary>
        DirectionalBlurFilter,
        /// <summary>レンズブラー</summary>
        LensBlur,
        /// <summary>レンズブラー(フィルタオブジェクト)</summary>
        LensBlurFilter,
        /// <summary>モーションブラー</summary>
        MotionBlur,
        /// <summary>モーションブラー(フィルタオブジェクト)</summary>
        MotionBlurFilter,
        /// <summary>座標(基本効果)</summary>
        Position,
        /// <summary>拡大率(基本効果)</summary>
        Zoom,
        /// <summary>透明度(基本効果)</summary>
        Transparency,
        /// <summary>回転(基本効果)</summary>
        Rotation,
        /// <summary>領域拡張(基本効果)</summary>
        AreaExpansion,
        /// <summary>リサイズ(基本効果)</summary>
        Resize,
        /// <summary>ローテーション(基本効果)</summary>
        Rotation90,
        /// <summary>振動</summary>
        Vibration,
        /// <summary>振動(フィルタオブジェクト)</summary>
        VibrationFilter,
        /// <summary>反転(基本効果)</summary>
        Inversion,
        /// <summary>反転(フィルタオブジェクト)</summary>
        InversionFilter,
        /// <summary>ミラー</summary>
        Mirror,
        /// <summary>ラスター</summary>
        Raster,
        /// <summary>ラスター(フィルタオブジェクト)</summary>
        RasterFilter,
        /// <summary>波紋</summary>
        Ripple,
        /// <summary>画像ループ</summary>
        ImageLoop,
        /// <summary>画像ループ(フィルタオブジェクト)</summary>
        ImageLoopFilter,
        /// <summary>極座標変換</summary>
        PolarTransform,
        /// <summary>ディスプレイスメントマップ</summary>
        Displacement,
        /// <summary>ノイズ</summary>
        Noise,
        /// <summary>色ずれ</summary>
        ColorShift,
        /// <summary>色ずれ(フィルタオブジェクト)</summary>
        ColorShiftFilter,
        /// <summary>単色化</summary>
        Monochromatic,
        /// <summary>単色化(フィルタオブジェクト)</summary>
        MonochromaticFilter,
        /// <summary>グラデーション</summary>
        Gradation,
        /// <summary>拡張色設定</summary>
        ColorSettingEx,
        /// <summary>拡張色設定(フィルタオブジェクト)</summary>
        ColorSettingExFilter,
        /// <summary>特定色域変換</summary>
        GamutConversion,
        /// <summary>アニメーション効果</summary>
        AnimationEffect,
        /// <summary>カスタムオブジェクト</summary>
        CustomObject,
        /// <summary>スクリプト制御</summary>
        Script,
        /// <summary>動画ファイル合成</summary>
        VideoComposition,
        /// <summary>画像ファイル合成</summary>
        ImageComposition,
        /// <summary>インターレース解除</summary>
        Deinterlacing,
        /// <summary>カメラ制御オプション</summary>
        CameraOption,
        /// <summary>オフスクリーン描画</summary>
        OffScreen,
        /// <summary>オブジェクト描画</summary>
        Split,
        /// <summary>部分フィルタ(フィルタオブジェクト)</summary>
        PartialFilter,
        /// <summary>音声フェード</summary>
        AudioFade,
        /// <summary>音声ディレイ</summary>
        AudioDelay,
        /// <summary>音声ディレイ(フィルタオブジェクト)</summary>
        AudioDelayFilter,
        /// <summary>モノラル化</summary>
        Monaural,
        /// <summary>時間制御</summary>
        TimeControl,
        /// <summary>グループ制御</summary>
        GroupControl,
        /// <summary>カメラ制御</summary>
        CameraControl,
        /// <summary>カメラ制御(拡張描画)</summary>
        CameraControlEx,
        /// <summary>カメラ効果</summary>
        CameraEffect,
        /// <summary>シャドー(カメラ制御)</summary>
        CameraShadow,
        /// <summary>スクリプト(カメラ制御)</summary>
        CameraScript,
        /// <summary>ノイズ除去フィルタ(AviUtl組込みフィルタ)</summary>
        DenoiseFilter,
        /// <summary>シャープフィルタ(AviUtl組込みフィルタ)</summary>
        SharpenFilter,
        /// <summary>ぼかしフィルタ(AviUtl組込みフィルタ)</summary>
        BlurFilter2,
        /// <summary>クリッピング＆リサイズ(AviUtl組込みフィルタ)</summary>
        ClipResizeFilter,
        /// <summary>縁塗りつぶし(AviUtl組込みフィルタ)</summary>
        FillBorder,
        /// <summary>色調補正(AviUtl組込みフィルタ)</summary>
        ColorCorrection2,
        /// <summary>拡張色調補正(AviUtl組込みフィルタ)</summary>
        ColorCorrectionEx,
        /// <summary>音量の調整(AviUtl組込みフィルタ)</summary>
        VolumeFilter,
    }
}
