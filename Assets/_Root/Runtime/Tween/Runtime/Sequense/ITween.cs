namespace Pancake.Tween
{
    public interface ITween
    {
        float TimeScale { get; }
        int LoopCount { get; }
        
        bool IsPlaying { get; }
        bool IsCompleted { get; }
        
        ITween OnTimeScaleChanged(int value);
        ITween OnStart();
        ITween OnRestart();
        ITween OnComplete();
        ITween OnKill();
        ITween OnCompleteOrKill();
        ITween SetLoop(int count);

        float GetDuration();
        float GetElapsed();
        float GetNormalize();

        ITween SetEase(CustomizableInterpolator interpolator);
        void Complete();
        void Kill();
        void Replay();
        void Play();
    }
}