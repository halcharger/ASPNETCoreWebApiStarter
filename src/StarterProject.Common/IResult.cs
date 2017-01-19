using System;
using System.Collections.Generic;
using NExtensions.Core;

namespace StarterProject.Common
{
    public interface IResult
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }
        IEnumerable<string> Failures { get; }
        string HtmlFormattedFailures { get; }

        Exception Exception { get; set; }
        bool HasException { get; }

        void SetFailures(IEnumerable<string> failures);
    }

    public class Result : IResult
    {
        protected bool isSuccess;
        protected IEnumerable<string> failures;

        public Result() { } //DO NOT remove this default constructor
        public Result(bool isSuccess, IEnumerable<string> failures)
        {
            this.isSuccess = isSuccess;
            this.failures = failures;
        }

        public bool IsSuccess => isSuccess;
        public bool IsFailure => !IsSuccess;
        public IEnumerable<string> Failures => failures;
        public string HtmlFormattedFailures => failures.JoinWith("<br/>");

        public Exception Exception { get; set; }
        public bool HasException => Exception != null;
        public string InternalFailure { get; set; }

        public void SetFailures(IEnumerable<string> failures)
        {
            isSuccess = false;
            this.failures = failures;
        }

        public override string ToString()
        {
            return isSuccess ? "Success" : $"Failure: {Environment.NewLine}{Failures.JoinWithNewLine()}";
        }
    }

    public class SuccessResult : Result
    {
        public SuccessResult() : base(true, new List<string>()) { }
    }

    public class FailureResult : Result
    {
        public FailureResult(IEnumerable<string> failures) : base(false, failures) { }
        public FailureResult(string failure) : base(false, new[] { failure }) { }
    }

    public class Result<T> : Result
    {
        public Result() { } //DO NOT remove this default constructor
        public Result(bool isSuccess, IEnumerable<string> failures)
            : base(isSuccess, failures)
        {
        }

        public T Value { get; set; }
    }

    public class SuccessResult<T> : Result<T>
    {
        public SuccessResult(T value) : base(true, new List<string>())
        {
            Value = value;
        }
    }

    public class FailureResult<T> : Result<T>
    {
        public FailureResult(IEnumerable<string> failures) : base(false, failures)
        {
            Value = default(T);
        }
        public FailureResult(string failure) : base(false, new[] { failure })
        {
            Value = default(T);
        }
    }

}