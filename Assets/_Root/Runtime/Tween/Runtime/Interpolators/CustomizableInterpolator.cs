using System;
using UnityEngine;

namespace Pancake.Tween
{
    /// <summary>
    /// Customizable Interpolator
    /// </summary>
    [Serializable]
    public struct CustomizableInterpolator
    {
        public Ease ease;
        [Range(0, 1)] public float strength;
        public AnimationCurve customCurve;


        /// <summary>
        /// Calculate interpolation value
        /// </summary>
        /// <param name="t"> normalized time </param>
        /// <returns> result </returns>
        public float this[float t] => ease == Ease.CustomCurve ? customCurve.Evaluate(t) : Interpolator.Interpolators[(int) ease](t, strength);


        public CustomizableInterpolator(Ease ease, float strength = 0.5f, AnimationCurve customCurve = null)
        {
            this.ease = ease;
            this.strength = strength;
            this.customCurve = customCurve;
        }
    } // struct CustomizableInterpolator
} // namespace Pancake.Tween