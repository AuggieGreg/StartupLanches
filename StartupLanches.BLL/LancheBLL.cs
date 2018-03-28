using StartupLanches.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StartupLanches.BLL
{
    public class LancheBLL : BaseBLL
    {
        public LancheBLL() : base()
        {

        }

        public List<LancheMdl> ListarLanches()
        {
            return dataBase.DbLanches.ToList();
        }
    }
}
