using StartupLanches.Model;
using System;
using System.Collections.Generic;

namespace StartupLanches.DAL
{
    public class DataBase
    {
        public void PrepareDatabase()
        {
            this.DbIngredientes = new List<IngredienteMdl>();
            IngredienteMdl Alface = new IngredienteMdl() { Nome = "Alface", Valor = 0.4M };
            IngredienteMdl Bacon = new IngredienteMdl() { Nome = "Bacon", Valor = 2.0M };
            IngredienteMdl HamburguerCarne = new IngredienteMdl() { Nome = "Hambúrguer de carne", Valor = 3.0M };
            IngredienteMdl Ovo = new IngredienteMdl() { Nome = "Ovo", Valor = 0.8M };
            IngredienteMdl Queijo = new IngredienteMdl() { Nome = "Queijo", Valor = 1.5M };

            this.DbIngredientes.Add(Alface);
            this.DbIngredientes.Add(Bacon);
            this.DbIngredientes.Add(HamburguerCarne);
            this.DbIngredientes.Add(Ovo);
            this.DbIngredientes.Add(Queijo);

            this.DbLanches = new List<LancheMdl>();
            this.DbLanches.Add(new LancheMdl()
            {
                Nome = "X-Bacon",
                Ingredientes = new List<IngredienteLancheMdl>()
            {
                new IngredienteLancheMdl()
                { Ingrediente = Bacon, ,
                HamburguerCarne,
                Queijo
            }
            });
            this.DbLanches.Add(new LancheMdl()
            {
                Nome = "X-Burguer",
                Ingredientes = new List<IngredienteLancheMdl>()
                {
                    HamburguerCarne,
                    Queijo,
                }
            });
            this.DbLanches.Add(new LancheMdl()
            {
                Nome = "X-Egg",
                Ingredientes = new List<IngredienteLancheMdl>()
                {
                    Ovo,
                    HamburguerCarne,
                    Queijo
                }
            });
            this.DbLanches.Add(new LancheMdl()
            {
                Nome = "X-Egg Bacon",
                Ingredientes = new List<IngredienteLancheMdl>()
                {
                    Ovo,
                    Bacon,
                    HamburguerCarne,
                    Queijo
                }
            });
        }
        public List<LancheMdl> DbLanches { get; set; }
        public List<IngredienteMdl> DbIngredientes { get; set; }
    }
}
