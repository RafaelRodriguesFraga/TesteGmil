using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteGmil.View
{
    public class MusicDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public GenreDto Genre { get; set; }
    }
}
