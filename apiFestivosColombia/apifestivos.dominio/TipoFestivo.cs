using System.ComponentModel.DataAnnotations.Schema;

namespace apifestivos.dominio
{
    [Table("TipoFestivo")]
    public class TipoFestivo
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Tipo")]
        public required string Tipo { get; set; }
    }
}