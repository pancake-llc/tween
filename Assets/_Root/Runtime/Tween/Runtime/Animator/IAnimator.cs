namespace Pancake.Tween
{
    public interface IConcatableAnimator<T>
    {
    }

    public interface IAnimator<T> : IConcatableAnimator<T>
    {
        void Interpolate(float factor);
    }

    public interface IAnimatorWithStartValue<T> : IConcatableAnimator<T>
    {
        IAnimator<T> Start(T startValue);
    }
}