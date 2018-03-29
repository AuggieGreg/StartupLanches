﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StartupLanches.Model
{
    public class LancheMdl
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<IngredienteLancheMdl> Ingredientes { get; set; }
        public decimal Valor { get; set; }
    }
}
