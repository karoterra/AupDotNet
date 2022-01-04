using System;

namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// トラックバー変化方法の種類
    /// </summary>
    public enum TrackbarType
    {
        /// <summary>移動無し</summary>
        Stop = 0,

        /// <summary>直線移動</summary>
        Liner = 1,

        /// <summary>曲線移動</summary>
        Curve = 2,

        /// <summary>瞬間移動</summary>
        Step = 3,

        /// <summary>中間点無視</summary>
        IgnoreKeyframe = 4,

        /// <summary>移動量指定</summary>
        Movement = 5,

        /// <summary>ランダム移動</summary>
        Random = 6,

        /// <summary>加減速移動</summary>
        AccelDecel = 7,

        /// <summary>反復移動</summary>
        Repeat = 8,

        /// <summary>スクリプト</summary>
        Script = 0xF,
    }

    /// <summary>
    /// トラックバー変化方法のフラグ
    /// </summary>
    [Flags]
    public enum TrackbarFlag
    {
        /// <summary>減速</summary>
        Deceleration = 0x20,

        /// <summary>加速</summary>
        Acceleration = 0x40,
    }

    /// <summary>
    /// 拡張編集のトラックバーを表すクラス。
    /// </summary>
    public class Trackbar
    {
        /// <summary>
        /// 現在の値(設定ダイアログにおける左側の値)
        /// </summary>
        public int Current { get; set; }

        /// <summary>
        /// 次の値(設定ダイアログにおける右側の値)
        /// </summary>
        public int Next { get; set; }

        /// <summary>
        /// トラックバー変化方法の種類
        /// </summary>
        public TrackbarType Type { get; set; }

        /// <summary>
        /// トラックバー変化方法のフラグ
        /// </summary>
        public TrackbarFlag Flag { get; set; }

        /// <summary>
        /// トラックバー変化方法スクリプトのインデックス
        /// </summary>
        public int ScriptIndex { get; set; }

        /// <summary>
        /// トラックバー変化方法のパラメータ
        /// </summary>
        public int Parameter { get; set; }

        /// <summary>
        /// トラックバー変化方法
        /// </summary>
        public uint Transition
        {
            get => (uint)Type +  (uint)Flag + (uint)(ScriptIndex << 16);
        }

        /// <summary>
        /// <see cref="Trackbar"/> のインスタンスを初期化します。
        /// </summary>
        public Trackbar() { }

        /// <summary>
        /// <see cref="Trackbar"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="current">現在の値</param>
        /// <param name="next">次の値</param>
        /// <param name="transition">変化方法</param>
        /// <param name="param">パラメータ</param>
        public Trackbar(int current, int next, uint transition, int param)
        {
            Current = current;
            Next = next;
            Type = (TrackbarType)(transition & 0xF);
            Flag = (TrackbarFlag)(transition & 0xF0);
            ScriptIndex = (int)(transition >> 16);
            Parameter = param;
        }

        /// <summary>
        /// トラックバー情報をオブジェクトファイル形式の文字列に変換します。
        /// </summary>
        /// <param name="scale">値のスケール</param>
        /// <param name="script">トラックバースクリプト</param>
        /// <returns></returns>
        public string ToString(int scale, TrackbarScript script)
        {
            string current, next;
            switch (scale)
            {
                case 100:
                    current = $"{Current / 100.0:F2}";
                    next = $",{Next / 100.0:F2}";
                    break;
                case 10:
                    current = $"{Current / 10.0:F1}";
                    next = $",{Next / 10.0:F1}";
                    break;
                default:
                    current = Current.ToString();
                    next = $",{Next}";
                    break;
            }
            if (Type == TrackbarType.Stop)
                return current;

            if (Type != TrackbarType.Script)
                script = TrackbarScript.BuiltIn[(int)Type];

            string transition = $",{(script.EnableSpeed ? ((int)Flag | (int)Type) : (int)Type)}";
            if (Type == TrackbarType.Script)
                transition += $"@{script.Name}";
            string param = (script.EnableParam && Parameter != 0) ? $",{Parameter}" : string.Empty;

            return $"{current}{next}{transition}{param}";
        }
    }
}
