namespace Pancake.Tween
{
    public interface IConcatableAnimator<T>
    {
    }

    public interface IAnimator<T> : IConcatableAnimator<T>
    {
        (T, float) Update(float time);
    }

    public interface IAnimatorWithStartValue<T> : IConcatableAnimator<T>
    {
        IAnimator<T> Start(T startValue);
    }
}