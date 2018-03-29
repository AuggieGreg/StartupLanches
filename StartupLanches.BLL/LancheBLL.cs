using StartupLanches.BLL.Exception;
using StartupLanches.Model;
using StartupLanches.Model.Enumeradores;
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

        public LancheMdl Obter(int id)
        {
            return dataBase.DbLanche.SingleOrDefault(x => x.Id == id);
        }

        public List<LancheMdl> ListarLanches()
        {
            return dataBase.DbLanche.ToList();
        }

        public void CalcularValor(LanchePedidoMdl Lanche)
        {
            if (Lanche == null)
            {
                throw new BusinessException("Erro ao calcular preço do lanche. Lanche não informado.");
            }

            if (Lanche.Ingredientes == null || !Lanche.Ingredientes.Any())
            {
                throw new BusinessException("Lanches devem possuir ingredientes para composição de seus preços.");
            }

            Lanche.Valor = Lanche.ValorIngredientes;
        }

        public void CalcularPromocao(LanchePedidoMdl Lanche)
        {
            if (Lanche == null)
            {
                throw new BusinessException("Erro ao calcular promoções do lanche. Lanche não informado.");
            }

            if (Lanche.Ingredientes == null || !Lanche.Ingredientes.Any())
            {
                throw new BusinessException("Lanches devem possuir ingredientes para composição de suas promoções.");
            }

            if (Lanche.Valor <= 0)
            {
                throw new BusinessException("Lanches devem possuir seus valores calculados antes de validar promoções.");
            }

            Lanche.Promocoes = new List<PromocaoLancheMdl>();

            //Estes IDs poderiam ser oferecidos através de um cadastro de promoções ou parametros globais. Não os implementei para economizar tempo.
            int idAlface = 1;
            int idBacon = 2;

            if (Lanche.Ingredientes.Any(x => x.IdIngrediente == idAlface)
            && !Lanche.Ingredientes.Any(x => x.IdIngrediente == idBacon))
            {
                //Promoção Light
                var promocaoLight = new PromocaoLancheMdl(EnumTipoPromocao.Light);
                promocaoLight.Nome = "Light";

                promocaoLight.Valor = Lanche.Valor * 0.1M * -1;
                Lanche.Promocoes.Add(promocaoLight);
            }

            foreach (var group in Lanche.Ingredientes
                .Where(x => x.Ingrediente.TipoIngrediente == Model.Enumeradores.EnumTipoIngrediente.Carne)
                .GroupBy(g => new { g.IdIngrediente, g.Ingrediente.TipoIngrediente }))
            {
                //Promoção muita carne
                int carnesParaDescontar = group.Sum(s => s.Quantidade) / 3;
                if (carnesParaDescontar > 0)
                {
                    var ingrediente = group.First().Ingrediente;
                    var promocaoMuitaCarne = new PromocaoLancheMdl(EnumTipoPromocao.MuitaCarne);
                    promocaoMuitaCarne.Nome = string.Format("Muita Carne! - {0}", ingrediente.Nome);
                    promocaoMuitaCarne.Valor = carnesParaDescontar * ingrediente.Valor * -1;
                    Lanche.Promocoes.Add(promocaoMuitaCarne);
                }
            }

            foreach (var group in Lanche.Ingredientes
                .Where(x => x.Ingrediente.TipoIngrediente == Model.Enumeradores.EnumTipoIngrediente.Queijo)
                .GroupBy(g => new { g.IdIngrediente, g.Ingrediente.TipoIngrediente }))
            {
                //Promoção muito queijo
                int queijosParaDescontar = group.Sum(s => s.Quantidade) / 3;
                if (queijosParaDescontar > 0)
                {
                    var ingrediente = group.First().Ingrediente;
                    var promocaoMuitoQueijo = new PromocaoLancheMdl(EnumTipoPromocao.MuitoQueijo);
                    promocaoMuitoQueijo.Nome = string.Format("Muito Queijo! - {0}", ingrediente.Nome);
                    promocaoMuitoQueijo.Valor = queijosParaDescontar * ingrediente.Valor * -1;
                    Lanche.Promocoes.Add(promocaoMuitoQueijo);
                }
            }
        }
    }
}
