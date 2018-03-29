using StartupLanches.Model.Enumeradores;
using System;

namespace StartupLanches.Model
{
    public class IngredienteMdl
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public EnumTipoIngrediente TipoIngrediente { get; set; }
    }
}