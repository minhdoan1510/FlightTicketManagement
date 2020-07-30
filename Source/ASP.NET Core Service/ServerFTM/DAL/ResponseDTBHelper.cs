using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ServerFTM.DAL.Helper
{
    public class ResponseDTB
    {
        public bool IsSuccess { get; set; }
        public DataTable Result { get; set; }
        public string ErrorMessenge { get; set; }
        public ResponseDTB(DataTable result)
        {
            IsSuccess = true;
            Result = result;
        }
        public ResponseDTB(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        public ResponseDTB(string errorMessenge)
        {
            ErrorMessenge = errorMessenge;
        }
    }

    public static class ResponseDTBHelper
    {
        public static ResponseDTB OkResultDB()
        {
            return new ResponseDTB(true);
        }

        public static ResponseDTB OkResultDB(DataTable result)
        {
            if (result.Rows.Count == 0)
                return FailResultDB();
            return new ResponseDTB(result);
        }

        public static ResponseDTB ErrorResultDB(string errormess)
        {
            return new ResponseDTB(errormess);
        }

        public static ResponseDTB FailResultDB()
        {
            return new ResponseDTB(false);
        }
    }

}
