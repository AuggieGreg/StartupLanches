using System;
using System.Collections.Generic;
using System.Text;

namespace StartupLanches.BLL.Exception
{
    class BusinessException : System.Exception
    {
        public BusinessException(string mensagem, params object[] args) : base(string.Format(mensagem, args))
        {

        }
    }
}
