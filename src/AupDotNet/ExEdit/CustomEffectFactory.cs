using Karoterra.AupDotNet.ExEdit.Effects;

namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// <see cref="CustomEffect"/> を生成するフィルタ効果ファクトリ。
    /// </summary>
    public class CustomEffectFactory : IEffectFactory
    {
        /// <inheritdoc/>
        public Effect Create(EffectType type, Trackbar[] trackbars, int[] checkboxes, byte[] data)
        {
            return new CustomEffect(type, trackbars, checkboxes, data);
        }
    }
}
