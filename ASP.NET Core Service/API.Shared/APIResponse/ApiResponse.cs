using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Shared.APIResponse
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Result { get; set; }
        public int? ErrorCode { get; set; }
        public string ErrorMessenge { get; set; }
        public ApiResponse(T result)
        {
            IsSuccess = true;
            Result = result;
        }
        public ApiResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        public ApiResponse(int errorCode, string errorMessenge)
        {
            ErrorCode = errorCode;
            ErrorMessenge = errorMessenge;
        }
    }
}
