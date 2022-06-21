namespace Pancake.Tween
{
    public class Tween : TweenBase
    {
        private bool _registered;


        public float TimeScale { get; private set; }
        public int LoopCount { get; private set; }
        public bool IsPlaying { get; private set; }
        public bool IsCompleted { get; private set; }

        public UpdateMode updateMode = UpdateMode.Update;

        public UpdateMode UpdateMode
        {
            get => updateMode;
            internal set
            {
                if (_registered)
                {
                    RuntimeUtilities.RemoveUpdate(updateMode, OnUpdate);
                    RuntimeUtilities.AddUpdate(value, OnUpdate);
                }

                updateMode = value;
            }
        }

        public CustomizableInterpolator Interpolator { get; internal set; }

        public override TweenBase OnTimeScaleChanged(int value) { throw new System.NotImplementedException(); }

        public override TweenBase OnStart() { throw new System.NotImplementedException(); }

        public override TweenBase OnRestart() { throw new System.NotImplementedException(); }

        public override TweenBase OnComplete() { throw new System.NotImplementedException(); }

        public override TweenBase OnKill() { throw new System.NotImplementedException(); }

        public override TweenBase OnCompleteOrKill() { throw new System.NotImplementedException(); }

        public override TweenBase SetLoop(int count)
        {
            LoopCount = count;
            return this;
        }

        public override void OnUpdate() { throw new System.NotImplementedException(); }

        public override float GetDuration() { throw new System.NotImplementedException(); }

        public override float GetElapsed() { throw new System.NotImplementedException(); }

        public override float GetNormalize() { throw new System.NotImplementedException(); }

        public override TweenBase SetEase(CustomizableInterpolator interpolator)
        {
            Interpolator = interpolator;
            return this;
        }

        public override TweenBase SetUpdateMode(UpdateMode mode)
        {
            UpdateMode = mode;
            return this;
        }

        public override void Complete() { throw new System.NotImplementedException(); }

        public override void Kill() { throw new System.NotImplementedException(); }

        public override void Replay() { throw new System.NotImplementedException(); }

        public override void Play() { throw new System.NotImplementedException(); }

        public Tween()
        {
            RuntimeUtilities.AddUpdate(UpdateMode, OnUpdate);
            _registered = true;
        }

        protected virtual void OnDestroy()
        {
            RuntimeUtilities.RemoveUpdate(UpdateMode, OnUpdate);
            _registered = false;
        }

        public static Tween Create() { return new Tween(); }
    }
}