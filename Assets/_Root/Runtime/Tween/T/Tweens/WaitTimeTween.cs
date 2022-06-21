using UnityEngine;

namespace Pancake.Tween
{
    public class WaitTimeTween : Tween
    {
        private readonly float _duration;
        private float _elapsed;

        protected override bool Loopable => true;

        public WaitTimeTween(float duration) { this._duration = duration; }

        protected override void OnTweenStart(bool isCompletingInstantly) { _elapsed = 0.0f; }

        protected override void OnTweenUpdate()
        {
            float dt = Time.unscaledDeltaTime * TweenManager.TimeScale * TimeScale;

            _elapsed += dt;

            if (_elapsed >= _duration)
            {
                NewMarkCompleted();
            }
        }

        protected override void OnTweenKill() { }

        protected override void OnTweenComplete()
        {
            _elapsed = _duration;

            NewMarkCompleted();
        }

        protected override void OnTweenReset(bool kill, ResetMode resetMode) { _elapsed = 0.0f; }

        protected override void OnTweenStartLoop(ResetMode loopResetMode) { _elapsed = 0.0f; }

        public override void OnTimeScaleChanges(float timeScale) { }

        public override void OnEaseDelegateChanges(EaseDelegate easeFunction) { }

        public override float OnGetDuration() { return _duration; }

        public override float OnGetElapsed() { return _elapsed; }

        public override int OnGetTweensCount() { return 1; }

        public override int OnGetPlayingTweensCount() { return IsPlaying ? 1 : 0; }
    }
}