using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static CRMBilling.Core.Utils.Enums;

namespace CRMBilling.Core.Utils.ErrorHandling
{
    public class ErrorHandler :IErrorHandler
    {
        private readonly ILogger<ErrorHandler> _logger;

        public ErrorHandler(ILogger<ErrorHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void HandleException(Exception ex,
          [CallerMemberName] string methodName = "",
          [CallerFilePath] string filePath = "",
          [CallerLineNumber] int lineNumber = 0)
        {
            string fileName = Path.GetFileName(filePath);
            string location = $"{fileName}:{methodName}:line {lineNumber}";

            if (ex is ApplicationException appEx)
            {
                switch (appEx.Severity)
                {
                    case ErrorSeverity.Critical:
                        _logger.LogCritical(ex, "CRITICAL ERROR [{ErrorCode}] at {Location}: {Message}",
                            appEx.ErrorCode, location, ex.Message);
                        // Potentially notify on-call team
                        break;
                    case ErrorSeverity.High:
                        _logger.LogError(ex, "ERROR [{ErrorCode}] at {Location}: {Message}",
                            appEx.ErrorCode, location, ex.Message);
                        break;
                    case ErrorSeverity.Medium:
                        _logger.LogWarning(ex, "WARNING [{ErrorCode}] at {Location}: {Message}",
                            appEx.ErrorCode, location, ex.Message);
                        break;
                    case ErrorSeverity.Low:
                        _logger.LogInformation(ex, "INFO [{ErrorCode}] at {Location}: {Message}",
                            appEx.ErrorCode, location, ex.Message);
                        break;
                }
            }
            else
            {
                _logger.LogError(ex, "UNHANDLED EXCEPTION at {Location}: {Message}", location, ex.Message);
            }

            if (ex.StackTrace != null)
            {
                _logger.LogDebug("Stack trace: {StackTrace}", ex.StackTrace);
            }

            if (ex.InnerException != null)
            {
                _logger.LogDebug("Inner exception: {InnerExceptionMessage}", ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Execute action with error handle
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="methodName"></param>
        /// <param name="filePath"></param>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public T ExecuteWithErrorHandling<T>(Func<T> action,
            [CallerMemberName] string methodName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                HandleException(ex, methodName, filePath, lineNumber);
                throw;
            }
        }

        /// <summary>
        /// Execute async action 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <param name="methodName"></param>
        /// <param name="filePath"></param>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public async Task<T> ExecuteWithErrorHandlingAsync<T>(Func<Task<T>> action,
           [CallerMemberName] string methodName = "",
           [CallerFilePath] string filePath = "",
           [CallerLineNumber] int lineNumber = 0)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                HandleException(ex, methodName, filePath, lineNumber);
                throw;
            }
        }

        /// <summary>
        /// Execute action with error handle no return value 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="methodName"></param>
        /// <param name="filePath"></param>
        /// <param name="lineNumber"></param>
        public void ExecuteWithErrorHandling(Action action,
            [CallerMemberName] string methodName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                HandleException(ex, methodName, filePath, lineNumber);
                throw;
            }
        }

        /// <summary>
        /// Execute async action with error handle return task
        /// </summary>
        /// <param name="action"></param>
        /// <param name="methodName"></param>
        /// <param name="filePath"></param>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public async Task ExecuteWithErrorHandlingAsync(Func<Task> action,
         [CallerMemberName] string methodName = "",
         [CallerFilePath] string filePath = "",
         [CallerLineNumber] int lineNumber = 0)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                HandleException(ex, methodName, filePath, lineNumber);
                throw; // Rethrow to allow higher-level handling
            }
        }

    }
}
