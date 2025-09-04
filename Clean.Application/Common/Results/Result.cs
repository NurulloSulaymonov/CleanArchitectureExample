namespace Clean.Application.Common.Results;

    public class Result
    {
        public bool Succeeded { get; }
        public string? Error { get; }
        public static Result Success() => new(true, null);
        public static Result Failure(string error) => new(false, error);
        protected Result(bool ok, string? err) { Succeeded = ok; Error = err; }
    }


    public class Result<T> : Result
    {
        public T? Value { get; }
        private Result(bool ok, string? err, T? value) : base(ok, err) { Value = value; }
        public static Result<T> Success(T value) => new(true, null, value);
        public static new Result<T> Failure(string error) => new(false, error, default);
    }
