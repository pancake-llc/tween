namespace Pancake.Tween
{
    public class Sequense : ITween
    {
        public float TimeScale { get; }
        public int LoopCount { get; }
        public bool IsPlaying { get; }
        public bool IsCompleted { get; }
        public ITween OnTimeScaleChanged(int value) { throw new System.NotImplementedException(); }

        public ITween OnStart() { throw new System.NotImplementedException(); }

        public ITween OnRestart() { throw new System.NotImplementedException(); }

        public ITween OnComplete() { throw new System.NotImplementedException(); }

        public ITween OnKill() { throw new System.NotImplementedException(); }

        public ITween OnCompleteOrKill() { throw new System.NotImplementedException(); }

        public ITween SetLoop(int count) { throw new System.NotImplementedException(); }

        public float GetDuration() { throw new System.NotImplementedException(); }

        public float GetElapsed() { throw new System.NotImplementedException(); }

        public float GetNormalize() { throw new System.NotImplementedException(); }

        public ITween SetEase(CustomizableInterpolator interpolator) { throw new System.NotImplementedException(); }

        public void Complete() { throw new System.NotImplementedException(); }

        public void Kill() { throw new System.NotImplementedException(); }

        public void Replay() { throw new System.NotImplementedException(); }

        public void Play() { throw new System.NotImplementedException(); }
    }
}