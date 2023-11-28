using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MODELO_DE_PARCIAL
{
    public partial class frmGraficar : Form
    {
        //Instancio la clase Datos. 
        Datos datos = new Datos();
        //Creo un datatable el cual usare para cargar el listview.
        DataTable tbVende = new DataTable();
        DataTable tbVenta = new DataTable();

        public frmGraficar()
        {
            InitializeComponent();
        }

        private void frmGraficar_Load(object sender, EventArgs e)
        {
            //Limpio las series.
            chrGrafico.Series.Clear();
            
            //Instruccion sql.
            String SQL1 = "SELECT * FROM Vendedores";

            //Lleno las tablas necesarias.
            DataTable tbVende = datos.getData(SQL1);

            //Recorro la tabla 
            foreach (DataRow fila in tbVende.Rows)
            {
                //Cargo las casillas con los vendedores y su id en la propiedad tag.
                ListViewItem casillas = lstVendedores.Items.Add(fila["nombre"].ToString());
                casillas.Tag = fila["vendedor"].ToString();
            }
        }

        private void btnGraficar_Click(object sender, EventArgs e)
        {
            String SQL2 = "SELECT vendedor, aa, cantidad FROM Ventas";
            DataTable tbVenta = datos.getData(SQL2);

            chrGrafico.Series.Clear();

            //Variables de Años.
            Int32 Desde = Convert.ToInt32(txtDesde.Text);
            Int32 Hasta = Convert.ToInt32(txtHasta.Text);
            Int32 Recorrido = Desde;

            //Variable de cantidad de autos vendidos
            Int32 cuenta = 0;

            //Variable de total general.
            Int32 General = 0;

            //Hará lo que haya dentro hasta que recorra todo el espectro de los años
            while (Recorrido <= Hasta)
            {
                Series serie = chrGrafico.Series.Add(Recorrido.ToString());

                //Recorro la listview para obtener el tag del checkbox
                foreach (ListViewItem casilla in lstVendedores.CheckedItems)
                {   
                    Int32 tagVendedor = Convert.ToInt32(casilla.Tag);

                    //Recorro los registros de la tabla de ventas
                    foreach (DataRow filaVenta in tbVenta.Rows)
                    {
                        Int32 idVendedor = Convert.ToInt32(filaVenta["vendedor"].ToString());
                        Int32 Anio = Convert.ToInt32(filaVenta["aa"].ToString());
                        Int32 cantidad = Convert.ToInt32(filaVenta["cantidad"].ToString());

                        if (idVendedor == tagVendedor)
                        {
                            if (Anio == Recorrido)
                            {
                                cuenta = cuenta + cantidad;
                            }
                        }
                    }
                    General = General + cuenta;
                    serie.Points.AddXY(casilla.Text, cuenta);
                    cuenta = 0;
                }
                Recorrido++;
            }

            tslblTotal.Text = "Cantidad total de autos vendidos: " + General.ToString();
        }

        private void chrGrafico_Click(object sender, EventArgs e)
        {

        }
    }
}
