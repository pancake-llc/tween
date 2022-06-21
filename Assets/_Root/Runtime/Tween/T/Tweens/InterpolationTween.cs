﻿using System;
using System.Collections.Generic;

namespace Pancake.Tween
{
    public class InterpolationTween : Tween
    {
        private readonly List<ITweener> _tweeners = new List<ITweener>();
        private readonly List<ITweener> _playingTweeners = new List<ITweener>();

        protected override bool Loopable => true;

        private bool _durationCalculated;
        private float _cachedCalculatedDuration;

        protected override void OnTweenStart(bool isCompletingInstantly) { StartTweeners(); }

        protected override void OnTweenUpdate()
        {
            for (int i = _playingTweeners.Count - 1; i >= 0; --i)
            {
                ITweener tweener = _playingTweeners[i];

                tweener.Update();

                if (!tweener.IsPlaying)
                {
                    _playingTweeners.RemoveAt(i);
                }
            }

            if (_playingTweeners.Count == 0)
            {
                NewMarkCompleted();
            }
        }

        protected override void OnTweenKill()
        {
            foreach (ITweener tweener in _playingTweeners)
            {
                tweener.Kill();
            }

            _playingTweeners.Clear();
        }

        protected override void OnTweenComplete()
        {
            foreach (ITweener tweener in _playingTweeners)
            {
                tweener.Complete();
            }

            _playingTweeners.Clear();

            NewMarkCompleted();
        }

        protected override void OnTweenReset(bool kill, ResetMode resetMode)
        {
            for (int i = _tweeners.Count - 1; i >= 0; --i)
            {
                ITweener tweener = _tweeners[i];

                tweener.Reset(resetMode);
            }
        }

        protected override void OnTweenStartLoop(ResetMode loopResetMode) { StartTweeners(); }

        public override void OnEaseDelegateChanges(EaseDelegate easeFunction)
        {
            foreach (ITweener tweener in _tweeners)
            {
                tweener.SetEase(easeFunction);
            }

            EaseFunction = easeFunction;
        }

        public override void OnTimeScaleChanges(float timeScale)
        {
            foreach (ITweener tweener in _tweeners)
            {
                tweener.TimeScale = timeScale;
            }
        }

        public override int OnGetTweensCount() { return 1; }

        public override int OnGetPlayingTweensCount() { return IsPlaying ? 1 : 0; }

        public override float OnGetDuration()
        {
            if (_durationCalculated)
            {
                return _cachedCalculatedDuration;
            }

            _durationCalculated = true;

            _cachedCalculatedDuration = 0.0f;

            foreach (ITweener tweener in _tweeners)
            {
                _cachedCalculatedDuration += tweener.Duration;
            }

            return _cachedCalculatedDuration;
        }

        public override float OnGetElapsed()
        {
            float totalElapsed = 0.0f;

            foreach (ITweener tweener in _tweeners)
            {
                totalElapsed += tweener.Elapsed;
            }

            return totalElapsed;
        }

        public void Add(ITweener tweener)
        {
            if (tweener == null)
            {
                throw new ArgumentNullException($"Tried to {nameof(Add)} a null {nameof(ITweener)} on {nameof(InterpolationTween)}");
            }

            if (tweener.IsPlaying)
            {
                throw new ArgumentNullException($"Tried to {nameof(Add)} a {nameof(ITweener)} on {nameof(InterpolationTween)} " + $"but it was already playing");
            }

            if (_tweeners.Contains(tweener))
            {
                throw new ArgumentNullException($"Tried to {nameof(Add)} a {nameof(ITweener)} on {nameof(InterpolationTween)} " + $"but it was already added");
            }

            _tweeners.Add(tweener);

            _durationCalculated = false;
        }

        private void StartTweeners()
        {
            _playingTweeners.Clear();
            _playingTweeners.AddRange(_tweeners);

            for (int i = _playingTweeners.Count - 1; i >= 0; --i)
            {
                ITweener tweener = _playingTweeners[i];

                tweener.TimeScale = TimeScale;
                tweener.SetEase(EaseFunction);

                tweener.Start();

                if (!tweener.IsPlaying)
                {
                    _playingTweeners.RemoveAt(i);
                }
            }

            if (_playingTweeners.Count == 0)
            {
                NewMarkCompleted();
            }
        }
    }
}