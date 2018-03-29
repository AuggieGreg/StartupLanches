using StartupLanches.Model;
using System;
using System.Collections.Generic;

namespace StartupLanches.DAL
{
    public class DataBase
    {
        public void PrepareDatabase()
        {
            this.DbIngrediente = new List<IngredienteMdl>();
            IngredienteMdl Alface = new IngredienteMdl() { Id = 1, Nome = "Alface", Valor = 0.4M };
            IngredienteMdl Bacon = new IngredienteMdl() { Id = 2, Nome = "Bacon", Valor = 2.0M };
            IngredienteMdl HamburguerCarne = new IngredienteMdl() { Id = 3, Nome = "Hambúrguer de carne", Valor = 3.0M };
            IngredienteMdl Ovo = new IngredienteMdl() { Id = 4, Nome = "Ovo", Valor = 0.8M };
            IngredienteMdl Queijo = new IngredienteMdl() { Id = 5, Nome = "Queijo", Valor = 1.5M };

            this.DbIngrediente.Add(Alface);
            this.DbIngrediente.Add(Bacon);
            this.DbIngrediente.Add(HamburguerCarne);
            this.DbIngrediente.Add(Ovo);
            this.DbIngrediente.Add(Queijo);

            this.DbLanche = new List<LancheMdl>();
            this.DbLanche.Add(new LancheMdl()
            {
                Id = 1,
                Nome = "X-Bacon",
                Ingredientes = new List<IngredienteLancheMdl>()
            {
                new IngredienteLancheMdl(Bacon),
                new IngredienteLancheMdl(HamburguerCarne),
                new IngredienteLancheMdl(Queijo)
            }
            });
            this.DbLanche.Add(new LancheMdl()
            {
                Id = 2,
                Nome = "X-Burguer",
                Ingredientes = new List<IngredienteLancheMdl>()
                {
                    new IngredienteLancheMdl(HamburguerCarne),
                    new IngredienteLancheMdl(Queijo),
                }
            });
            this.DbLanche.Add(new LancheMdl()
            {
                Id = 3,
                Nome = "X-Egg",
                Ingredientes = new List<IngredienteLancheMdl>()
                {
                    new IngredienteLancheMdl(Ovo),
                    new IngredienteLancheMdl(HamburguerCarne),
                    new IngredienteLancheMdl(Queijo)
                }
            });
            this.DbLanche.Add(new LancheMdl()
            {
                Id = 4,
                Nome = "X-Egg Bacon",
                Ingredientes = new List<IngredienteLancheMdl>()
                {
                    new IngredienteLancheMdl(Ovo),
                    new IngredienteLancheMdl(Bacon),
                    new IngredienteLancheMdl(HamburguerCarne),
                    new IngredienteLancheMdl(Queijo)
                }
            });
        }
        public List<PedidoMdl> DbPedido { get; set; }
        public List<LancheMdl> DbLanche { get; set; }
        public List<IngredienteMdl> DbIngrediente { get; set; }
    }
}
