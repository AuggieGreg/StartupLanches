using StartupLanches.DAL;
using System;

namespace StartupLanches.BLL
{
    public abstract class BaseBLL
    {
        protected static DataBase dataBase { get; set; }
        public bool IsConfigured
        {
            get
            {
                return dataBase != null;
            }
        }
        protected BaseBLL()
        {
            if (!IsConfigured)
            {
                dataBase = new DataBase();
                dataBase.PrepareDatabase();
            }
        }
    }
}
