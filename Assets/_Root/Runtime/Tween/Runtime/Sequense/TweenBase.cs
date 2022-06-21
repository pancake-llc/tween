﻿namespace Pancake.Tween
{
    public abstract class TweenBase
    {
        public float TimeScale { get; protected set; }
        public int LoopCount { get; protected set; }

        public bool IsPlaying { get; protected set; }
        public bool IsCompleted { get; protected set; }

        public UpdateMode UpdateMode { get; protected set; }
        public CustomizableInterpolator Interpolator { get; protected set; }

        public abstract TweenBase OnTimeScaleChanged(int value);
        public abstract TweenBase OnStart();
        public abstract TweenBase OnRestart();
        public abstract TweenBase OnComplete();
        public abstract TweenBase OnKill();
        public abstract TweenBase OnCompleteOrKill();
        public abstract TweenBase SetLoop(int count);
        public abstract void OnUpdate();

        public abstract float GetDuration();
        public abstract float GetElapsed();
        public abstract float GetNormalize();

        public abstract TweenBase SetEase(CustomizableInterpolator interpolator);
        public abstract TweenBase SetUpdateMode(UpdateMode mode);
        public abstract void Complete();
        public abstract void Kill();
        public abstract void Replay();
        public abstract void Play();
    }
}