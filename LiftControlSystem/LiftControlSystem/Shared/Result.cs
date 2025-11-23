namespace LiftControlSystem.Shared
{
    public class Result
    {
        public bool Success => Errors.Count == 0;
        public List<string> Errors { get; } = [];
        public List<string> Warnings { get; } = [];

        public static Result Ok()
        {
            return new Result();
        }

        public Result AddWarning(string warning)
        {
            Warnings.Add(warning);
            return this;
        }

        public Result AddError(string error)
        {
            Errors.Add(error);
            return this;
        }
    }
}