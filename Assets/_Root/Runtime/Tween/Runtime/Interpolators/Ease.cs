﻿namespace Pancake.Tween
{
    public enum Ease
    {
        Linear = 0,
        InSine,
        OutSine,
        InOutSine,
        InQuad, // Accelerate
        OutQuad,
        InOutQuad,
        InCubic,
        OutCubic,
        InOutCubic,
        InQuart,
        OutQuart,
        InOutQuart,
        InQuint,
        OutQuint,
        InOutQuint,
        InExpo,
        OutExpo,
        InOutExpo,
        InCirc,
        OutCirc,
        InOutCirc,
        InBack,
        OutBack,
        InOutBack,
        InElastic,
        OutElastic,
        InOutElastic,
        InBounce,
        OutBounce,
        InOutBounce,
        Accelerate,
        Decelerate,
        AccelerateDecelerate,
        Anticipate,
        Overshoot,
        AnticipateOvershoot,
        Bounce,
        Parabolic,
        Sine,
        CustomCurve = -1
    }

    public delegate float EaseDelegate(float a, float b, float t);
}