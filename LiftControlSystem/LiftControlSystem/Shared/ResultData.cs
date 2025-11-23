using LiftControlSystem.Domain.Models;

namespace LiftControlSystem.Shared
{
    public class ResultData<T> : Result
    {
        public T? Value { get; private set; }

        public ResultData(T? value = default)
        {
            Value = value;
        }

        public ResultData<T> SetData(T value)
        {
            Value = value;
            return this;
        }

        public static ResultData<T> Ok(T value)
        {
            return new ResultData<T>(value);
        }

        public ResultData<T> WithWarning(string warning)
        {
            AddWarning(warning);
            return this;
        }

        public ResultData<T> WithError(string error)
        {
            AddError(error);
            return this;
        }
    }
}
