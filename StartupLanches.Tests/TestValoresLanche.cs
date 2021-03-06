using System;
using Xunit;
using StartupLanches.BLL;
using StartupLanches.Model;
using System.Collections.Generic;
using System.Linq;

namespace StartupLanches.Tests
{
    public class TestValoresLanche
    {
        private readonly LancheBLL _lancheBLL;
        private readonly IngredienteBLL _ingredienteBLL;
        public TestValoresLanche()
        {
            _lancheBLL = new LancheBLL();
            _ingredienteBLL = new IngredienteBLL();
        }

        [Fact]
        public void TesteValorSemPromocao()
        {
            //Teste para garantir que o pre�o
            var lanche = new LanchePedidoMdl();
            lanche.Ingredientes = new List<IngredienteLanchePedidoMdl>();
            lanche.Ingredientes.Add(new IngredienteLanchePedidoMdl(new IngredienteMdl() { Id = 6, Nome = "Ingrediente 1", TipoIngrediente = Model.Enumeradores.EnumTipoIngrediente.Carne, Valor = 10 }, 2));
            lanche.Ingredientes.Add(new IngredienteLanchePedidoMdl(new IngredienteMdl() { Id = 7, Nome = "Ingrediente 2", TipoIngrediente = Model.Enumeradores.EnumTipoIngrediente.Queijo, Valor = 4 }, 2));
            lanche.Ingredientes.Add(new IngredienteLanchePedidoMdl(new IngredienteMdl() { Id = 3, Nome = "Ingrediente 3", TipoIngrediente = Model.Enumeradores.EnumTipoIngrediente.Queijo, Valor = 4 }, 5));
            lanche.Ingredientes.Add(new IngredienteLanchePedidoMdl(new IngredienteMdl() { Id = 4, Nome = "Ingrediente 4", TipoIngrediente = Model.Enumeradores.EnumTipoIngrediente.Queijo, Valor = 1 }, 4));
            lanche.Ingredientes.Add(new IngredienteLanchePedidoMdl(new IngredienteMdl() { Id = 5, Nome = "Ingrediente 5", TipoIngrediente = Model.Enumeradores.EnumTipoIngrediente.Queijo, Valor = 22 }, 1));

            var valorEsperado = lanche.Ingredientes.Sum(s => s.Quantidade * s.Valor);

            _lancheBLL.CalcularValor(lanche);

            Assert.True(valorEsperado == lanche.Valor, $"Soma do lanche de testes deveria ser {valorEsperado}");
        }

        [Fact]
        public void TestePromocaoMuitaCarne()
        {
            var lanche = new LanchePedidoMdl();
            lanche.Ingredientes = new List<IngredienteLanchePedidoMdl>();
            lanche.Ingredientes.Add(new IngredienteLanchePedidoMdl(new IngredienteMdl() { Id = 3, Nome = "Ingrediente 1", TipoIngrediente = Model.Enumeradores.EnumTipoIngrediente.Carne, Valor = 10 }, 2));
            _lancheBLL.CalcularValor(lanche);
            _lancheBLL.CalcularPromocao(lanche);

            Assert.False(lanche.Promocoes.Any(x => x.TipoPromocao == Model.Enumeradores.EnumTipoPromocao.MuitaCarne), "N�o deveria possuir uma promo��o de muita carne!");

            lanche.Ingredientes.Add(new IngredienteLanchePedidoMdl(new IngredienteMdl() { Id = 3, Nome = "Ingrediente 1", TipoIngrediente = Model.Enumeradores.EnumTipoIngrediente.Carne, Valor = 10 }, 1));
            _lancheBLL.CalcularValor(lanche);
            _lancheBLL.CalcularPromocao(lanche);

            Assert.True(lanche.Promocoes.Any(x => x.TipoPromocao == Model.Enumeradores.EnumTipoPromocao.MuitaCarne), "Deveria possuir uma promo��o de muita carne!");
            Assert.True(lanche.Promocoes.Count(x => x.TipoPromocao == Model.Enumeradores.EnumTipoPromocao.MuitaCarne) == 1, "Deveria possuir apenas uma promo��o Muita Carne!");

            Assert.True(lanche.Promocoes.First(x => x.TipoPromocao == Model.Enumeradores.EnumTipoPromocao.MuitaCarne).Valor == lanche.Ingredientes.First().Valor * -1);
        }

        [Fact]
        public void TestePromocaoMuitoQueijo()
        {
            var lanche = new LanchePedidoMdl();
            lanche.Ingredientes = new List<IngredienteLanchePedidoMdl>();
            lanche.Ingredientes.Add(new IngredienteLanchePedidoMdl(new IngredienteMdl() { Id = 7, Nome = "Ingrediente 1", TipoIngrediente = Model.Enumeradores.EnumTipoIngrediente.Queijo, Valor = 10 }, 2));
            _lancheBLL.CalcularValor(lanche);
            _lancheBLL.CalcularPromocao(lanche);

            Assert.False(lanche.Promocoes.Any(x => x.TipoPromocao == Model.Enumeradores.EnumTipoPromocao.MuitoQueijo), "N�o deveria possuir nenhuma promo��o de muito queijo!");

            lanche.Ingredientes.Add(new IngredienteLanchePedidoMdl(new IngredienteMdl() { Id = 7, Nome = "Ingrediente 1", TipoIngrediente = Model.Enumeradores.EnumTipoIngrediente.Queijo, Valor = 10 }, 1));
            _lancheBLL.CalcularValor(lanche);
            _lancheBLL.CalcularPromocao(lanche);

            Assert.True(lanche.Promocoes.Any(x => x.TipoPromocao == Model.Enumeradores.EnumTipoPromocao.MuitoQueijo), "Deveria possuir uma promo��o de muito queijo!");
            Assert.True(lanche.Promocoes.Count(x => x.TipoPromocao == Model.Enumeradores.EnumTipoPromocao.MuitoQueijo) == 1, "Deveria possuir apenas uma promo��o Muito Queijo");

            Assert.True(lanche.Promocoes.First(x => x.TipoPromocao == Model.Enumeradores.EnumTipoPromocao.MuitoQueijo).Valor == lanche.Ingredientes.First().Valor * -1, "Deveria ter havido desconto de 1 ingrediente!");
        }

        [Fact]
        public void TestePromocaoLight()
        {
            var lanche = new LanchePedidoMdl();
            lanche.Ingredientes = new List<IngredienteLanchePedidoMdl>();
            lanche.Ingredientes.Add(new IngredienteLanchePedidoMdl(new IngredienteMdl() { Id = 2, Nome = "Bacon", TipoIngrediente = Model.Enumeradores.EnumTipoIngrediente.Carne, Valor = 10 }, 2));
            lanche.Ingredientes.Add(new IngredienteLanchePedidoMdl(new IngredienteMdl() { Id = 5, Nome = "Hamburguer", TipoIngrediente = Model.Enumeradores.EnumTipoIngrediente.Carne, Valor = 10 }, 2));
            lanche.Ingredientes.Add(new IngredienteLanchePedidoMdl(new IngredienteMdl() { Id = 1, Nome = "Alface", TipoIngrediente = Model.Enumeradores.EnumTipoIngrediente.Vegetal, Valor = 10 }, 2));
            _lancheBLL.CalcularValor(lanche);
            _lancheBLL.CalcularPromocao(lanche);

            Assert.False(lanche.Promocoes.Any(x => x.TipoPromocao == Model.Enumeradores.EnumTipoPromocao.Light), "N�o deveria possuir uma promo��o de Light!");

            lanche.Ingredientes.RemoveAt(0);

            _lancheBLL.CalcularValor(lanche);
            _lancheBLL.CalcularPromocao(lanche);

            Assert.True(lanche.Promocoes.Any(x => x.TipoPromocao == Model.Enumeradores.EnumTipoPromocao.Light), "Deveria possuir uma promo��o de Light!");

            Assert.True(lanche.Promocoes.First(x => x.TipoPromocao == Model.Enumeradores.EnumTipoPromocao.Light).Valor == lanche.Valor * 0.1M * -1, "O desconto deveria ser de 10%");
        }

        [Fact]
        public void TesteValorComPromocao()
        {
            var lanche = new LanchePedidoMdl();
            lanche.Ingredientes = new List<IngredienteLanchePedidoMdl>();

            lanche.Ingredientes.Add(new IngredienteLanchePedidoMdl(new IngredienteMdl() { Id = 2, Nome = "Bacon", TipoIngrediente = Model.Enumeradores.EnumTipoIngrediente.Carne, Valor = 2 }, 2));
            lanche.Ingredientes.Add(new IngredienteLanchePedidoMdl(new IngredienteMdl() { Id = 3, Nome = "Hamburguer", TipoIngrediente = Model.Enumeradores.EnumTipoIngrediente.Carne, Valor = 3 }, 2));
            lanche.Ingredientes.Add(new IngredienteLanchePedidoMdl(new IngredienteMdl() { Id = 1, Nome = "Alface", TipoIngrediente = Model.Enumeradores.EnumTipoIngrediente.Vegetal, Valor = 0.4M }, 2));
            lanche.Ingredientes.Add(new IngredienteLanchePedidoMdl(new IngredienteMdl() { Id = 4, Nome = "Ovo", TipoIngrediente = Model.Enumeradores.EnumTipoIngrediente.Outros, Valor = 0.8M }, 2));
            lanche.Ingredientes.Add(new IngredienteLanchePedidoMdl(new IngredienteMdl() { Id = 5, Nome = "Queijo", TipoIngrediente = Model.Enumeradores.EnumTipoIngrediente.Queijo, Valor = 1.5M }, 2));

            _lancheBLL.CalcularValor(lanche);
            _lancheBLL.CalcularPromocao(lanche);

            decimal valorEsperado = lanche.Ingredientes.Sum(s => s.Quantidade * s.Valor);

            Assert.True(lanche.ValorPromocional == valorEsperado, $"Lanche deveria ter um valor {valorEsperado}");

            lanche.Ingredientes.ElementAt(4).Quantidade = 4;

            _lancheBLL.CalcularValor(lanche);
            _lancheBLL.CalcularPromocao(lanche);

            valorEsperado = lanche.ValorIngredientes - lanche.Ingredientes.ElementAt(4).Valor;

            Assert.True(lanche.ValorPromocional == valorEsperado, $"Lanche deveria ter um valor {valorEsperado}");

            lanche.Ingredientes.ElementAt(1).Quantidade = 4;

            _lancheBLL.CalcularValor(lanche);
            _lancheBLL.CalcularPromocao(lanche);

            valorEsperado = lanche.ValorIngredientes - lanche.Ingredientes.ElementAt(1).Valor - lanche.Ingredientes.ElementAt(4).Valor;

            Assert.True(lanche.ValorPromocional == valorEsperado, $"Lanche deveria ter um valor {valorEsperado}");

            decimal valorPrimeiroItem = lanche.Ingredientes.ElementAt(0).Valor * lanche.Ingredientes.ElementAt(0).Quantidade;
            lanche.Ingredientes.RemoveAt(0);

            _lancheBLL.CalcularValor(lanche);
            _lancheBLL.CalcularPromocao(lanche);

            valorEsperado = (lanche.ValorIngredientes - (lanche.ValorIngredientes * 0.1M)) - lanche.Ingredientes.ElementAt(0).Valor - lanche.Ingredientes.ElementAt(3).Valor;

            Assert.True(lanche.ValorPromocional == valorEsperado, $"Lanche deveria ter um valor {valorEsperado}");
        }
    }
}
