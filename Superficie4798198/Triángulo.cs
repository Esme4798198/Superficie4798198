using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Superficie4798198
{
    [Table("resultado")]

    public class Triángulo
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        [Column("altura")]
        public string Altura { get; set; }
        [Column("base")]
        public string Base { get; set; }
        [Column("superficie")]
        public string Superficie { get; set; }
    }
}
