namespace apifestivos.aplicacion
{
    public class ServicioFechas
    {
        public static DateTime ObtenerInicioSemanaSanta(int año)
        {
            int a = año % 19;
            int b = año % 4;
            int c = año % 7;
            int d = (19 * a + 24) % 30;

            int dias = d + (2 * b + 4 * c + 6 * d + 5) % 7;

            int dia = 15 + dias;
            int mes = 3;
            if (dia > 31)
            {
                dia = dia - 31;
                mes = 4;
            }
            return new DateTime(año, mes, dia);
        }

        public static DateTime AgregarDias(DateTime fecha, int dias)
        {
            return fecha.AddDays(dias);
        }

        public static DateTime ObtenerPascua(int año)
        {
            return AgregarDias(ObtenerInicioSemanaSanta(año), 7);
        }

        public static DateTime SiguienteLunes(DateTime fecha)
        {
            DayOfWeek diaSemana = fecha.DayOfWeek;
            int diasLunes = ((int)DayOfWeek.Monday - (int)diaSemana + 7) % 7;
            return AgregarDias(fecha, diasLunes);
        }

        // Ley de puente festivo de Ecuador (tipo 5): martes -> lunes anterior,
        // miércoles y jueves -> viernes, sábado -> viernes anterior, domingo -> lunes siguiente
        public static DateTime TrasladarFestivoViernes(DateTime fecha)
        {
            return fecha.DayOfWeek switch
            {
                DayOfWeek.Tuesday => AgregarDias(fecha, -1),
                DayOfWeek.Wednesday => AgregarDias(fecha, 2),
                DayOfWeek.Thursday => AgregarDias(fecha, 1),
                DayOfWeek.Saturday => AgregarDias(fecha, -1),
                DayOfWeek.Sunday => AgregarDias(fecha, 1),
                _ => fecha
            };
        }
    }
}
