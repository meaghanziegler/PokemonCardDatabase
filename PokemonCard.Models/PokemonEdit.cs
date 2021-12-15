﻿using PokemonCard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonCard.Models
{
    public class PokemonEdit : CardEdit
    {
        public PokemonType PokemonType { get; set; }
        public bool Evolves { get; set; }
        public string Attack1 { get; set; }
        public string Attack2 { get; set; }
    }
}
