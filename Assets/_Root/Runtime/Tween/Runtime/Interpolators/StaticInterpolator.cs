using UnityEngine;

namespace Pancake.Tween
{
    /// <summary>
    /// Predefined Interpolators
    /// </summary>
    public partial struct Interpolator
    {
        /// <summary>
        /// Linear interpolation
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <returns> Interpolation result </returns>
        public static float Linear(float t) { return t; }

        // public static float InSine(float t)
        // {
        //     return 
        // }
        // OutSine,
        // InOutSine,
        // InQuad,
        // OutQuad,
        // InOutQuad,
        // InCubic,
        // OutCubic,
        // InOutCubic,
        // InQuart,
        // OutQuart,
        // InOutQuart,
        // InQuint,
        // OutQuint,
        // InOutQuint,
        // InExpo,
        // OutExpo,
        // InOutExpo,
        // InCirc,
        // OutCirc,
        // InOutCirc,
        // InBack,
        // OutBack,
        // InOutBack,
        // InElastic,
        // OutElastic,
        // InOutElastic,
        // InBounce,
        // OutBounce,
        // InOutBounce,


        /// <summary>
        /// Speed up interpolation
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <returns> Interpolation result </returns>
        public static float Accelerate(float t) { return t * t; }


        /// <summary>
        /// Speed up interpolation Weakly
        /// </summary>
        /// <param name="t">  Unitized time, which is a value in the range [0, 1] </param>
        /// <returns> Interpolation result </returns>
        public static float AccelerateWeakly(float t) { return t * t * (2f - t); }


        /// <summary>
        /// Accelerated Interpolation Strongly
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <returns> Interpolation result </returns>
        public static float AccelerateStrongly(float t) { return t * t * t; }


        /// <summary>
        /// Speed up interpolation
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <param name="strength"> [0, 1] range strength </param>
        /// <returns> Interpolation result </returns>
        public static float Accelerate(float t, float strength) { return t * t * ((2f - t) * (1f - strength) + t * strength); }


        /// <summary>
        /// Deceleration interpolation
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <returns> Interpolation result </returns>
        public static float Decelerate(float t) { return (2f - t) * t; }


        /// <summary>
        /// Deceleration Interpolation Weakly
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <returns> Interpolation result </returns>
        public static float DecelerateWeakly(float t)
        {
            t = 1f - t;
            return 1f - t * t * (2f - t);
        }


        /// <summary>
        /// Deceleration Interpolation Strongly
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <returns> Interpolation result </returns>
        public static float DecelerateStrongly(float t)
        {
            t = 1f - t;
            return 1f - t * t * t;
        }


        /// <summary>
        /// Deceleration interpolation
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <param name="strength"> [0, 1] range strength </param>
        /// <returns> Interpolation result </returns>
        public static float Decelerate(float t, float strength)
        {
            t = 1f - t;
            return 1f - t * t * ((2f - t) * (1f - strength) + t * strength);
        }


        /// <summary>
        /// Accelerate then decelerate interpolation
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <returns> Interpolation result </returns>
        public static float AccelerateDecelerate(float t) { return (3f - t - t) * t * t; }


        /// <summary>
        /// Accelerate then decelerate interpolation Weakly
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <returns> Interpolation result </returns>
        public static float AccelerateDecelerateWeakly(float t)
        {
            float tt = t * t;
            return ((-6f * t + 15f) * tt - 14f * t + 6f) * tt;
        }


        /// <summary>
        /// Accelerate then decelerate interpolation Strongly
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <returns> Interpolation result </returns>
        public static float AccelerateDecelerateStrongly(float t) { return ((6f * t - 15f) * t + 10f) * t * t * t; }


        /// <summary>
        /// Accelerate then decelerate interpolation
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <param name="strength"> [0, 1] range strength </param>
        /// <returns> Interpolation result </returns>
        public static float AccelerateDecelerate(float t, float strength)
        {
            float tt = t * t;
            float ttt6_15tt = (6f * t - 15f) * tt;
            return ((6f - ttt6_15tt - 14f * t) * (1f - strength) + (ttt6_15tt + 10f * t) * strength) * tt;
        }


        /// <summary>
        /// Bounce acceleration interpolation
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <param name="strength"> [0, 1] range strength </param>
        /// <returns> Interpolation result </returns>
        public static float Anticipate(float t, float strength = 0.5f)
        {
            float a = 2f + strength * 2f;
            return (a * t - a + 1f) * t * t;
        }


        /// <summary>
        /// Deceleration bounce interpolation
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <param name="strength"> [0, 1] range strength </param>
        /// <returns> Interpolation result </returns>
        public static float Overshoot(float t, float strength = 0.5f)
        {
            t = 1f - t;
            float a = 2f + strength * 2f;
            return 1f - (a * t - a + 1f) * t * t;
        }


        /// <summary>
        /// First rebound acceleration and then deceleration rebound interpolation
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <param name="strength"> [0, 1] range strength </param>
        /// <returns> Interpolation result </returns>
        public static float AnticipateOvershoot(float t, float strength = 0.5f)
        {
            float d = -6f - 12f * strength;
            return ((((6f - d - d) * t + (5f * d - 15f)) * t + (10f - 4f * d)) * t + d) * t * t;
        }


        /// <summary>
        /// Bounce Interpolation
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <param name="strength"> [0, 1] range strength </param>
        /// <returns> Interpolation result </returns>
        public static float Bounce(float t, float strength = 0.5f)
        {
            float k = 0.3f + 0.4f * strength;
            float kk = k * k;
            float a = 1f + (k + k) * (1f + k + kk);

            float tmp;

            if (t < 1f / a)
            {
                tmp = a * t;
                return tmp * tmp;
            }

            if (t < (1f + k + k) / a)
            {
                tmp = a * t - 1f - k;
                return 1f - kk + tmp * tmp;
            }

            if (t < (1f + (k + kk) * 2f) / a)
            {
                tmp = a * t - 1f - k - k - kk;
                return 1f - kk * kk + tmp * tmp;
            }

            tmp = a * t - 1f - 2 * (k + kk) - kk * k;
            return 1f - kk * kk * kk + tmp * tmp;
        }


        /// <summary>
        /// Parabolic interpolation
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <returns> Interpolation result </returns>
        public static float Parabolic(float t) { return 4f * t * (1f - t); }


        /// <summary>
        /// Sine interpolation
        /// </summary>
        /// <param name="t"> Unitized time, i.e. a value in the range [0, 1] </param>
        /// <returns> Interpolation result </returns>
        public static float Sine(float t) { return Mathf.Sin((t + t + 1.5f) * Mathf.PI) * 0.5f + 0.5f; }
    } // struct Interpolator
} // namespace Pancake.Tween