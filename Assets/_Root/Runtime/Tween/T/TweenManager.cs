using System;
using System.Collections.Generic;
using System.Diagnostics;
using Pancake.Common;

namespace Pancake.Tween
{
    public class TweenManager : AutoStartMonoSingleton<TweenManager>
    {
        private readonly List<Tween> _aliveTweens = new List<Tween>();
        private readonly List<Tween> _tweensToAdd = new List<Tween>();
        private readonly List<Tween> _tweensToRemove = new List<Tween>();

        private readonly Stopwatch _updateStopwatch = new Stopwatch();

        private float _timeScale;

        public float UpdateMilliseconds { get; private set; }

        public static float TimeScale { get => Instance._timeScale; set => Instance._timeScale = value; }

        private void Awake() { Init(); }

        private void Update() { UpdateTweens(); }

        private void OnDestroy() { Dispose(); }

        private void Init() { _timeScale = 1.0f; }

        private void Dispose()
        {
            _aliveTweens.Clear();
            _tweensToAdd.Clear();
            _tweensToRemove.Clear();
        }

        public static ISequenceTween Sequence() { return new SequenceTween(); }

        public int GetAliveTweensCounts()
        {
            int aliveTweensCount = 0;

            for (int i = 0; i < _aliveTweens.Count; ++i)
            {
                aliveTweensCount += _aliveTweens[i].GetTweensCount();
            }

            return aliveTweensCount;
        }

        public int GetPlayingTweensCounts()
        {
            int aliveTweensCount = 0;

            for (int i = 0; i < _aliveTweens.Count; ++i)
            {
                aliveTweensCount += _aliveTweens[i].GetPlayingTweensCount();
            }

            return aliveTweensCount;
        }

        internal static void Add(Tween tween)
        {
            if (IsDestroyed)
            {
                return;
            }

            if (tween == null)
            {
                throw new ArgumentNullException($"Tried to play a null {nameof(Tween)} on {nameof(TweenManager)} instance");
            }

            if (tween.IsNested)
            {
                return;
            }

            if (tween.IsAlive)
            {
                Instance.TryStartTween(tween);

                return;
            }

            tween.IsAlive = true;

            Instance._tweensToAdd.Add(tween);

            Instance.TryStartTween(tween);
        }

        private void TryStartTween(Tween tween)
        {
            if (!tween.IsPlaying)
            {
                tween.Start();
            }
        }

        private void UpdateTweens()
        {
            _updateStopwatch.Restart();

            foreach (Tween tween in _tweensToAdd)
            {
                _aliveTweens.Add(tween);
            }

            _tweensToAdd.Clear();

            foreach (Tween tween in _aliveTweens)
            {
                if (tween.IsPlaying && !tween.IsCompleted)
                {
                    tween.Update();
                }

                if (!tween.IsPlaying || tween.IsCompleted || tween.IsNested)
                {
                    _tweensToRemove.Add(tween);
                }
            }

            foreach (Tween tween in _tweensToRemove)
            {
                tween.IsAlive = false;

                _aliveTweens.Remove(tween);
                _tweensToAdd.Remove(tween);
            }

            _tweensToRemove.Clear();

            _updateStopwatch.Stop();

            UpdateMilliseconds = _updateStopwatch.ElapsedMilliseconds;
        }
    }
}