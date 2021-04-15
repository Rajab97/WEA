using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace WEA.SharedKernel
{
    public class Result
    {
        public bool IsSucceed { get; private set; }

        public IEnumerable<string> FailureResult { get; private set; }

        public Exception Exception { get; private set; }

        public string ExceptionMessage
        {
            get
            {
                if (Exception != null)
                    return Exception.Message;
                else if (FailureResult?.Count() > 0)
                    return string.Join(Environment.NewLine, FailureResult);
                else
                    return String.Empty;
            }
        }

        public static Result Failure(params string[] failureResult)
        {
            var Result = new Result();

            Failure(Result, failureResult);

            return Result;
        }

        public static Result Failure(Exception exception)
        {
            var Result = new Result();

            Failure(Result, exception);

            return Result;
        }

        public static Result Succeed()
        {
            var result = new Result();

            Succeed(result);

            return result;
        }

        protected static void Succeed(Result result)
        {
            result.IsSucceed = true;
            result.FailureResult = new string[0];
        }

        protected static void Failure(Result result, params string[] failureResult)
        {
            Contract.Requires(failureResult != null);
            Contract.Requires(failureResult.Any());

            result.IsSucceed = false;
            result.FailureResult = failureResult;
        }


        protected static void Failure(Result result, Exception exception)
        {
            Contract.Requires(exception != null);
            result.IsSucceed = false;

            result.Exception = exception;

            var errorMessages = new List<string>();

            while (exception != null)
            {
                errorMessages.Add(exception.Message);
                exception = exception.InnerException;
            }

            result.FailureResult = errorMessages.ToArray();
        }
    }

    public class Result<T> : Result
    {
        public T Data { get; set; }

        public new static Result<T> Failure(params string[] failureResult)
        {
            var result = new Result<T>();
            Failure(result, failureResult);
            return result;
        }

        public new static Result<T> Failure(Exception exception)
        {
            var result = new Result<T>();
            Failure(result, exception);
            return result;
        }

        public static Result<T> Succeed(T data)
        {
            var result = new Result<T>() { Data = data };
            Succeed(result);
            return result;
        }
    }
}
