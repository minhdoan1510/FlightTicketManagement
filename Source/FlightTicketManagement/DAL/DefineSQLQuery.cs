﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicketManagement.DAL.Query
{
    class DefineSQLQuery
    {
        private static DefineSQLQuery query;
        public static DefineSQLQuery Query
        {
            get
            {
                if (query == null)
                    query = new DefineSQLQuery();
                return query;
            }
            set => query = value;
        }

        public string ProcSignUp = "EXECUTE ProcSignup @id , @user , @pass , @name , @acctype";
        public string ProcLogin = "EXECUTE ProcLogin @user , @pass";
    }
}
