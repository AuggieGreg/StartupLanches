using System;
using System.Collections.Generic;
using System.Text;

namespace StartupLanches.Model.Interfaces
{
    interface IIngredienteLanche
    {
        int IdIngrediente { get; set; }
        string Nome { get; set; }
        decimal Valor { get; set; }
        IIngrediente Ingrediente { get; set; }
    }
}
