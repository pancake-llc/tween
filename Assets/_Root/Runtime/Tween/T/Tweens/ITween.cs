using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Pancake.Tween
{
    public interface ITween
    {
        float TimeScale { get; }

        int Loops { get; }
        ResetMode LoopResetMode { get; }
        TimeMode TimeMode { get; }
        UpdateMode UpdateMode { get; }

        bool IsPlaying { get; }
        bool IsCompleted { get; }

        event Action<float> OnTimeScaleChangedEvent;
        event Action OnStart;
        event Action OnLoop;
        event Action OnReset;
        event Action OnComplete;
        event Action OnKill;
        event Action OnCompleteOrKill;

        float GetDuration();
        float GetElapsed();
        float GetNormalizedProgress();
        int GetTweensCount();
        int GetPlayingTweensCount();

        ITween SetTimeScale(float timeScale = 1f, TimeMode timeMode = TimeMode.Unscaled);
        ITween SetUpdateMode(UpdateMode updateMode);
        ITween SetEase(Ease ease);
        ITween SetEase(AnimationCurve animationCurve);
        ITween SetLoops(int loops, ResetMode resetMode);
        

        void Complete();
        void Kill();
        void Reset(bool kill, ResetMode resetMode = ResetMode.InitialValues);

        void Replay();
        void Play();

        Task AwaitCompleteOrKill(CancellationToken cancellationToken);
    }
}