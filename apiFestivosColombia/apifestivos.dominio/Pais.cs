using System.ComponentModel.DataAnnotations.Schema;

namespace apifestivos.dominio
{
    [Table("Pais")]
    public class Pais
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nombre")]
        public required string Nombre { get; set; }
    }
}