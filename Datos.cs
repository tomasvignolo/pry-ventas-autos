using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MODELO_DE_PARCIAL
{
    internal class Datos
    {
        public Datos()
        {
            //Constructor.
        }

        public DataTable getData(string sql)
        {
            OleDbDataAdapter adaptador = new OleDbDataAdapter(sql, Properties.Settings.Default.Cadena);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            return tabla;
        }
    }
}
