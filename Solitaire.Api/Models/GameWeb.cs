using System;
using System.Collections.Generic;

namespace Solitaire.Api.Models
{
    public class GameWeb
    {
        public Guid Id { get; set; }
        public IEnumerable<CardWeb> Stock { get; set; }
        public IEnumerable<CardWeb> Wastepile { get; set; }
    }
}
