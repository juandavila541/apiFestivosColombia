using System.ComponentModel.DataAnnotations.Schema;

namespace apifestivos.dominio
{
    [Table("Tipo")]
    public class TipoFestivo
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Tipo")]
        public required string Tipo { get; set; }
    }
}