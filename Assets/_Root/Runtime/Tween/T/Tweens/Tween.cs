﻿using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Pancake.Tween
{
    public abstract partial class Tween : ITween
    {
        private int _loopsRemaining;

        protected EaseDelegate EaseFunction { get; set; }
        public float TimeScale { get; private set; }

        public int Loops { get; private set; }
        public ResetMode LoopResetMode { get; private set; }

        public bool IsNested { get; set; }

        public bool IsPlaying { get; protected set; }
        public bool IsCompleted { get; protected set; }

        public bool IsAlive { get; set; }

        public event Action<float> OnTimeScaleChanged;
        public event Action OnStart;
        public event Action OnLoop;
        public event Action OnReset;
        public event Action OnComplete;
        public event Action OnKill;
        public event Action OnCompleteOrKill;

        internal Tween()
        {
            SetEase(Ease.Linear);
            SetTimeScale(1.0f);
        }

        public void Start(bool isCompletingInstantly = false)
        {
            if (IsPlaying)
            {
                Kill();
            }

            IsPlaying = true;
            IsCompleted = false;

            _loopsRemaining = Loops;

            OnStart?.Invoke();

            OnTweenStart(isCompletingInstantly);
        }

        public void Update()
        {
            if (!IsPlaying)
            {
                return;
            }

            OnTweenUpdate();
        }

        public void Complete()
        {
            if (!IsPlaying && !IsCompleted)
            {
                Start(isCompletingInstantly: true);
            }

            OnTweenComplete();

            _loopsRemaining = 0;

            NewMarkCompleted();
        }

        public void Kill()
        {
            if (!IsPlaying)
            {
                return;
            }

            IsPlaying = false;
            IsCompleted = true;

            OnTweenKill();

            OnKill?.Invoke();
            OnCompleteOrKill?.Invoke();
        }

        public void Reset(bool kill, ResetMode resetMode = ResetMode.InitialValues)
        {
            if (kill)
            {
                Kill();

                IsPlaying = false;
            }

            IsCompleted = false;

            OnTweenReset(kill, resetMode);

            OnReset?.Invoke();
        }

        public float GetDuration() { return OnGetDuration(); }

        public float GetElapsed()
        {
            if (!IsPlaying && !IsCompleted)
            {
                return 0.0f;
            }

            if (!IsPlaying && IsCompleted)
            {
                return GetDuration();
            }

            return OnGetElapsed();
        }

        public float GetNormalizedProgress()
        {
            float duration = GetDuration();

            if (duration <= 0)
            {
                return 0.0f;
            }

            float elapsed = GetElapsed();

            return (1.0f / duration) * elapsed;
        }

        public int GetTweensCount() { return OnGetTweensCount(); }

        public int GetPlayingTweensCount() { return OnGetPlayingTweensCount(); }

        public void SetTimeScale(float timeScale)
        {
            if (TimeScale == timeScale)
            {
                return;
            }

            TimeScale = timeScale;

            OnTimeScaleChanges(timeScale);

            OnTimeScaleChanged?.Invoke(timeScale);
        }

        public void SetEase(EaseDelegate easeFunction)
        {
            EaseFunction = easeFunction;

            OnEaseDelegateChanges(EaseFunction);
        }

        public void SetEase(Ease ease) { SetEase(Interpolator.Get(ease)); }

        public void SetEase(AnimationCurve animationCurve)
        {
            if (animationCurve == null)
            {
                throw new ArgumentNullException($"Tried to {nameof(SetEase)} " + $"with a null {nameof(AnimationCurve)} on {nameof(Tween)}");
            }

            SetEase(Interpolator.Get(animationCurve));
        }

        public void SetLoops(int loops, ResetMode resetMode)
        {
            Loops = Math.Max(loops, 0);
            LoopResetMode = resetMode;
        }

        public void Replay()
        {
            Reset(kill: true);
            Play();
        }

        public void Play() { TweenManager.Add(this); }

        private bool NewLoop(ResetMode loopResetMode)
        {
            bool needsToLoop = _loopsRemaining > 0;

            if (!needsToLoop || !Loopable)
            {
                return false;
            }

            --_loopsRemaining;

            Reset(kill: false, loopResetMode);

            Start();

            OnLoop?.Invoke();

            return true;
        }

        protected void NewMarkCompleted()
        {
            if (!IsPlaying)
            {
                return;
            }

            bool loops = NewLoop(LoopResetMode);

            if (loops)
            {
                return;
            }

            IsPlaying = false;
            IsCompleted = true;

            OnTweenComplete();

            OnComplete?.Invoke();
            OnCompleteOrKill?.Invoke();
        }

        public async Task AwaitCompleteOrKill(CancellationToken cancellationToken)
        {
            if (!IsPlaying)
            {
                return;
            }

            TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();

            Action onComplete = () => taskCompletionSource.TrySetResult(default);

            OnCompleteOrKill += onComplete;

            cancellationToken.Register(Kill);

            await taskCompletionSource.Task;

            OnCompleteOrKill -= onComplete;
        }

        protected abstract bool Loopable { get; }

        protected abstract void OnTweenStart(bool isCompletingInstantly);
        protected abstract void OnTweenUpdate();
        protected abstract void OnTweenKill();
        protected abstract void OnTweenComplete();
        protected abstract void OnTweenReset(bool kill, ResetMode loopResetMode);
        protected abstract void OnTweenStartLoop(ResetMode loopResetMode);

        public abstract void OnEaseDelegateChanges(EaseDelegate easeFunction);
        public abstract void OnTimeScaleChanges(float timeScale);

        public abstract float OnGetDuration();
        public abstract float OnGetElapsed();
        public abstract int OnGetTweensCount();
        public abstract int OnGetPlayingTweensCount();
    }
}