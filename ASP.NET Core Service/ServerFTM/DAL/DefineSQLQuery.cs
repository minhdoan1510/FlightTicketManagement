using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerFTM.DAL.Query
{
    class DefineSQLQuery
    {


        public const string ProcSignUp = "EXECUTE ProcSignup @id , @user , @pass , @name , @acctype";
        public const string ProcLogin = "EXECUTE ProcLogin @user , @pass";
        public const string ProcGetAllFlight = "EXECUTE GetAllFlight";
        public const string ProcGetTransits = "EXECUTE GetTransit @flightId";
        public const string ProcGetRestrictions = "EXECUTE GetRestrictions";
    }
}
