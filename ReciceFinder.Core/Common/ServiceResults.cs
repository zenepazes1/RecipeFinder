using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.Core.Common
{
    public class ServiceResult
    {
        public bool Success { get; private set; }
        public string ErrorMessage { get; private set; }

        protected ServiceResult(bool success, string errorMessage)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }

        public static ServiceResult Succeed() => new ServiceResult(true, null);
        public static ServiceResult Fail(string message) => new ServiceResult(false, message);
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; private set; }

        private ServiceResult(T data, bool success, string errorMessage)
            : base(success, errorMessage)
        {
            Data = data;
        }

        public static ServiceResult<T> Succeed(T data) => new ServiceResult<T>(data, true, null);
        public static ServiceResult<T> Fail(string message) => new ServiceResult<T>(default, false, message);
    }

    public static class ServiceError
    {
        public static string ArgumentNull(string parameter) => $"Parameter {parameter} cannot be null.";
        public static string Conflict(string message) => message;
        public static string Argument(string message) => message;
        public static string NotFound(string message) => message;
    }
}
