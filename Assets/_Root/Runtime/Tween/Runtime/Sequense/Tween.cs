using System;
using System.Collections.Generic;
using Pancake.Common;

namespace Pancake.Tween
{
    public class Tween : TweenBase
    {
        private const float MIN_DURATION = 0.0001f;
        private bool _registered;
        private UpdateMode _updateMode = UpdateMode.Update;
        private float _normalizedTime;
        private List<TweenAnimation> _animations = default;
        private float _duration = 1f;
        private PlayDirection _direction;
        private int _state;


        public float TimeScale { get; private set; }
        public int LoopCount { get; private set; }
        public bool IsPlaying { get; private set; }
        public bool IsCompleted { get; private set; }
        public bool Enabled { get; set; }
        public event Action onForwardArrived = default;
        public event Action onBackArrived = default;

        public UpdateMode UpdateMode
        {
            get => _updateMode;
            private set
            {
                if (_registered)
                {
                    RuntimeUtilities.RemoveUpdate(_updateMode, OnUpdate);
                    RuntimeUtilities.AddUpdate(value, OnUpdate);
                }

                _updateMode = value;
            }
        }

        public TimeMode TimeMode { get; protected set; } = TimeMode.Unscaled;
        public WrapMode WrapMode { get; protected set; } = WrapMode.Clamp;
        public ArrivedAction ArrivedAction { get; protected set; } = ArrivedAction.AlwaysStopOnArrived;

        public CustomizableInterpolator Interpolator { get; internal set; }

        public float NormalizedTime
        {
            get => _normalizedTime;
            set
            {
                _normalizedTime = M.Clamp01(value);
                Sample(_normalizedTime);
            }
        }

        /// <summary>
        /// The total duration time
        /// </summary>
        public float Duration { get => _duration; set => _duration = value > MIN_DURATION ? value : MIN_DURATION; }

        private void Sample(float normalizedTime)
        {
            if (_animations != null)
            {
                for (int i = 0; i < _animations.Count; i++)
                {
                    var item = _animations[i];
                    if (item.enabled) item.Sample(normalizedTime);
                }
            }
        }

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

        public override void OnUpdate()
        {
            if (!Enabled) return;

            float deltaTime = RuntimeUtilities.GetUnitedDeltaTime(TimeMode);

            while (deltaTime > M.Epsilon)
            {
                if (_direction == PlayDirection.Forward)
                {
                    if (_normalizedTime < 1f)
                    {
                        _state = 0;
                    }
                    else if (WrapMode == WrapMode.Loop)
                    {
                        _normalizedTime = 0f;
                        _state = 0;
                    }

                    float time = _normalizedTime * _duration + deltaTime;

                    // playing
                    if (time < _duration)
                    {
                        NormalizedTime = time / _duration;
                        return;
                    }

                    // arrived
                    NormalizedTime = 1f;
                    if (_state != +1)
                    {
                        _state = +1;

                        if ((ArrivedAction & ArrivedAction.StopOnForwardArrived) != 0)
                            Enabled = false;

                        onForwardArrived?.Invoke();
                    }

                    // wrap
                    switch (WrapMode)
                    {
                        case WrapMode.Clamp:
                            return;

                        case WrapMode.PingPong:
                            _direction = PlayDirection.Back;
                            break;
                    }

                    deltaTime = time - _duration;
                }
                else
                {
                    if (_normalizedTime > 0f)
                    {
                        _state = 0;
                    }
                    else if (WrapMode == WrapMode.Loop)
                    {
                        _normalizedTime = 1f;
                        _state = 0;
                    }

                    float time = _normalizedTime * _duration - deltaTime;

                    // playing
                    if (time > 0f)
                    {
                        NormalizedTime = time / _duration;
                        return;
                    }

                    // arrived
                    NormalizedTime = 0f;
                    if (_state != -1)
                    {
                        _state = -1;

                        if ((ArrivedAction & ArrivedAction.StopOnBackArrived) != 0)
                            Enabled = false;

                        onBackArrived?.Invoke();
                    }

                    // wrap
                    switch (WrapMode)
                    {
                        case WrapMode.Clamp:
                            return;

                        case WrapMode.PingPong:
                            _direction = PlayDirection.Forward;
                            break;
                    }

                    deltaTime = -time;
                }
            }
        }

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

        public override TweenBase Append(TweenBase tween) { throw new NotImplementedException(); }

        public override TweenBase Join(TweenBase tween) { throw new NotImplementedException(); }

        public override void AppendCallback(Action callback, bool callIfCompletingInstantly = true) { throw new NotImplementedException(); }

        public override void JoinCallback(Action callback, bool callIfCompletingInstantly = true) { throw new NotImplementedException(); }

        private Tween()
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