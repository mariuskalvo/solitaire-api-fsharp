using System;
using System.Collections.Generic;

namespace Solitaire.Api.Models
{
    public class GameWeb
    {
        public Guid Id { get; set; }
        public List<CardWeb> Stock { get; set; }
        public List<CardWeb> Active { get; set; }
        public List<CardWeb> Wastepile { get; set; }
        public List<List<CardWeb>> Tableau { get; set; }
        public List<List<CardWeb>> Foundations { get; set; }
    }
}
