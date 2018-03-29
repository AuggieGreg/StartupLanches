using System;
using System.Collections.Generic;
using System.Text;

namespace StartupLanches.Model.Interfaces
{
    interface ILanche
    {
        int Id { get; set; }
        string Nome { get; set; }
        List<IIngredienteLanche> Ingredientes { get; set; }
        decimal Valor { get; set; }
    }
}
