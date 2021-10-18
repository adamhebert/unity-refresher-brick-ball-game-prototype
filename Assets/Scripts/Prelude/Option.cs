using System;

namespace Prelude
{
    using static Option;

    public sealed class Option<T>
    {
        internal Option(T value, bool hasValue)
        {
            Value = value;
            HasValue = hasValue;
        }

        public U Match<U>(Func<T, U> some, Func<U> none) => HasValue ? some(Value) : none();

        public void Apply(Action<T> some, Action none)
        {
            if (HasValue)
            {
                some(Value);
            }
            else
            {
                none();
            }
        }

        public void ForEach(Action<T> some)
        {
            if (HasValue)
            {
                some(Value);
            }
        }

        public Option<U> Select<U>(Func<T, U> tu) => HasValue ? Some(tu(Value)) : None<U>();
        public Option<U> SelectMany<U>(Func<T, Option<U>> tu) => HasValue ? tu(Value) : None<U>();
        public Option<V> SelectMany<U, V>(Func<T, Option<U>> tu, Func<T, U, V> tuv) => SelectMany(tu).Select(u => tuv(Value, u));

        public TFinal Cross<U, TFinal>(Option<U> other, Func<T, U, TFinal> forBoth, Func<T, TFinal> forOriginal, Func<U, TFinal> forOther, Func<TFinal> forNone) =>
            Match(
                x => other.Match(y => forBoth(x, y), () => forOriginal(x)),
                () => other.Match(y => forOther(y), () => forNone()));

        private readonly T Value;
        private readonly bool HasValue;
    }

    public static class Option
    {
        public static Option<T> Some<T>(T value)
        {
            // Consider removing this from runtime someday or making a safe/unsafe version.
            if (value == null)
            {
                throw new Exception($"value: {value} of type: {typeof(T).FullName} should not be null when calling {nameof(Some)}");
            }

            return new Option<T>(value, true);
        }

        public static Option<T> None<T>() => new Option<T>(default, false);

        public static Option<T> When<T>(bool isTrue, Func<T> getT) => isTrue ? Some(getT()) : None<T>();
    }
}
