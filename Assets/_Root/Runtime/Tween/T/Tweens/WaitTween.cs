namespace Pancake.Tween
{
    public class WaitTween : Tween
    {
        private readonly float _duration;
        private float _elapsed;

        protected override bool Loopable => true;

        public WaitTween(float duration) { this._duration = duration; }

        protected override void OnTweenStart(bool isCompletingInstantly) { _elapsed = 0.0f; }

        protected override void OnTweenUpdate()
        {
            float deltaTime = RuntimeUtilities.GetUnitedDeltaTime(TimeMode);
            float dt = deltaTime * TweenManager.TimeScale * TimeScale;

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

        public override void OnTimeScaleChange(float timeScale) { }
        public override void OnTimeModeChange(TimeMode timeMode) { }

        public override void OnEaseDelegateChange(EaseDelegate easeFunction) { }

        public override float OnGetDuration() { return _duration; }

        public override float OnGetElapsed() { return _elapsed; }

        public override int OnGetTweensCount() { return 1; }

        public override int OnGetPlayingTweensCount() { return IsPlaying ? 1 : 0; }
    }
}