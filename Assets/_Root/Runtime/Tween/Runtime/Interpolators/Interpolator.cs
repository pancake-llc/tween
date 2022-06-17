using System;
using UnityEngine;

namespace Pancake.Tween
{
    /// <summary>
    /// Interpolator
    /// </summary>
    [Serializable]
    public partial struct Interpolator
    {
        public EaseWithOutCurve ease;
        [Range(0, 1)] public float strength;


        internal static readonly Func<float, float, float>[] Interpolators =
        {
            (t, s) => t, Accelerate, Decelerate, AccelerateDecelerate, Anticipate, Overshoot, AnticipateOvershoot, Bounce, (t, s) => Parabolic(t), (t, s) => Sine(t)
        };


        /// <summary>
        /// Calculate interpolation value
        /// </summary>
        /// <param name="t"> normalized time </param>
        /// <returns> result </returns>
        public float this[float t] => Interpolators[(int) ease](t, strength);


        public Interpolator(EaseWithOutCurve ease, float strength = 0.5f)
        {
            this.ease = ease;
            this.strength = strength;
        }
    } // struct Interpolator
} // namespace Pancake.Tween