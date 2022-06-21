using System;

namespace Pancake.Tween
{
    public interface ISequense
    {
        TweenBase Append(TweenBase tween);
        TweenBase Join(TweenBase tween);
        void AppendCallback(Action callback, bool callIfCompletingInstantly = true);
        void JoinCallback(Action callback, bool callIfCompletingInstantly = true);
    }
}