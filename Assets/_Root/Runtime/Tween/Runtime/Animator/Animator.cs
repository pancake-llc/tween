using System;
using Pancake.Common;

namespace Pancake.Tween
{
    public static class Animator
    {
    }

    #region float

    public class FloatAnimator : IAnimator<float>
    {
        private readonly Ease _ease;
        public readonly float _from;
        public readonly float _to;

        public FloatAnimator(Ease ease, float from, float to)
        {
            _ease = ease;
            _from = from;
            _to = to;
        }



        public void Interpolate(float factor) {  }
    }

    #endregion
}