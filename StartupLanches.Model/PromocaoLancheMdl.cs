using StartupLanches.Model.Enumeradores;
using System;

namespace StartupLanches.Model
{
    public class PromocaoLancheMdl
    {
        public PromocaoLancheMdl(EnumTipoPromocao tipoPromocao)
        {
            this.TipoPromocao = tipoPromocao;
        }
        public string Nome { get; set; }
        public decimal Valor { get; set; }

        public EnumTipoPromocao TipoPromocao { get; set; }
    }
}