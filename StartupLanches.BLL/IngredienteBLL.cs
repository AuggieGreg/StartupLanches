using StartupLanches.BLL.Exception;
using StartupLanches.Model;
using StartupLanches.Model.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StartupLanches.BLL
{
    public class IngredienteBLL : BaseBLL
    {
        public IngredienteBLL() : base()
        {

        }

        public List<IngredienteMdl> ListarPorTermoETipo(string termo, EnumTipoIngrediente? tipoIngrediente = null, int? take = null)
        {
            var query = dataBase.DbIngrediente.Where(x => x.Nome.Contains(termo) && (tipoIngrediente == null || x.TipoIngrediente == tipoIngrediente));
            query = take.HasValue ? query.Take(take.Value) : query;
            return query.ToList();
        }
    }
}
