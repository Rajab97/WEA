using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.SharedKernel;
using WEA.SharedKernel.Extensions;
using WEA.SharedKernel.Resources;

namespace WEA.Presentation.Services
{
    public static class Extensions
    {
        private static ILogger _logger;
        public static Result<T> HandleResponse<T>(this Result<T> actionResult)
        {
            if (actionResult == null) return null;

            _logger = _logger ?? LogManager.GetCurrentClassLogger();
            if (actionResult.IsSucceed) return actionResult;
            actionResult.Exception?.HandleException();
            _logger.ErrorEx("HandleResponse<T>.ExceptionMessage:", actionResult.ExceptionMessage);
            return actionResult;
        }

        public static void HandleException(this Exception exception)
        {
            if (exception == null) return;
            if (_logger == null) return;

            _logger.InfoEx("EXCEPTION", exception);
            var level = 0;
            while (exception != null)
            {
                _logger.Error(exception, "BSF.Admin.Exception[" + level++ + "]" + exception.Message);
                exception = exception.InnerException;
            }
        }
        public static Result HandleResponse(this Result actionResult)
        {
            if (actionResult == null) return null;
            if (actionResult.IsSucceed) return actionResult;
            actionResult.Exception?.HandleException();
            _logger.ErrorEx("HandleResponse.ExceptionMessage:", actionResult.ExceptionMessage);
            return actionResult;
        }
    }
    public class BaseServiceFacade
    {
        protected NLog.ILogger Logger { get; }

        protected BaseServiceFacade()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }

        protected Result Succeed()
        {
            return Result.Succeed();
        }
        protected Result<T> Succeed<T>(T data)
        {
            return Result<T>.Succeed(data);
        }

        protected Result<T> Failure<T>(Exception e)
        {
            e.HandleException();

            if (e is ApplicationException)
            {
                return Result<T>.Failure(e);
            }
            return Result<T>.Failure(ExceptionMessages.FatalError);
        }

        protected Result<T> Failure<T>(string errorMessage)
        {
            return Result<T>.Failure(errorMessage);
        }

        protected Result Failure(Exception e)
        {
            e.HandleException();
            if (e is ApplicationException)
            {
                return Result.Failure(e);
            }

            return Result.Failure(ExceptionMessages.FatalError);
        }
    }
}
