using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EquipoQ22.Domino;
using EquipoQ22.Servicio.Interfaz;
using EquipoQ22.Servicio;

//COMPLETAR --> Curso:  1W4    Legajo:113825         Apellido y Nombre:Benito sofia

//CadenaDeConexion: "Data Source=sqlgabineteinformatico.frc.utn.edu.ar;Initial Catalog=Qatar2022;User ID=alumnoprog22;Password=SQL+Alu22"

namespace EquipoQ22
{
    public partial class FrmAlta : Form
    {
        private Equipo nuevo;
        private IServicio servicio;
        public FrmAlta()
        {
            InitializeComponent();
            nuevo = new Equipo();
            servicio = new ServicioFactoryImpl().CrearServicio();
        }
        private void FrmAlta_Load(object sender, EventArgs e)
        {
            CargarCombo();
            LimpiarCampos();
            CalcularTotal();
        }

        private void CargarCombo()
        {
            cboPersona.DataSource = servicio.CargarPersonas();
            cboPersona.ValueMember = "IdPersona";
            cboPersona.DisplayMember = "NombreCompleto";
        }
        
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(ValidarAgregar())
            {
                Jugador jugador = new Jugador();
                jugador.Persona = (Persona)cboPersona.SelectedItem;
                jugador.Camiseta = (int)nudCamiseta.Value;
                jugador.Posicion =cboPosicion.Text;
                Equipo equipo = new Equipo();
                nuevo.Agregar(jugador);
                dgvDetalles.Rows.Add(new object[] { jugador.Persona.IdPersona,jugador.Persona.NombreCompleto, jugador.Camiseta, jugador.Posicion });
                CalcularTotal();

            }
        }

        private void CalcularTotal()
        {
            lblTotal.Text = "Total de jugadores: " + dgvDetalles.Rows.Count;
        }
        private bool ValidarAgregar()
        {
            if(cboPersona.SelectedIndex==-1)
            {
                MessageBox.Show("Ingrese campo valido en persona", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboPersona.Focus();
                return false;

            }

            if(nudCamiseta.Value<1 || nudCamiseta.Value>23 )
            {
                MessageBox.Show("Ingrese camiseta valida entre 1 y 23 ", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                nudCamiseta.Focus();
                return false;
            }
            
            if (cboPosicion.SelectedIndex == -1)
            {
                MessageBox.Show("Ingrese campo valido en posicion", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboPosicion.Focus();
                return false;

            }
            foreach(DataGridViewRow data in dgvDetalles.Rows)
            {
                if(data.Cells["jugador"].Value.ToString().Equals(cboPersona.Text))
                {
                    MessageBox.Show("La persona  "+ cboPersona.Text+ " Ya cubre una posicion", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboPersona.Focus();
                    return false;
                }
            }
            foreach (DataGridViewRow data in dgvDetalles.Rows)
            {
                if (data.Cells["camiseta"].Value.ToString().Equals(nudCamiseta.Text))
                {
                    MessageBox.Show("La camiseta " + nudCamiseta.Text + " Ya se usa", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    nudCamiseta.Focus();
                    return false;
                }
            }

            return true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(ValidarAceptar())
            {
                nuevo.Pais = txtPais.Text;
                nuevo.DirectorTecnico = txtDT.Text;
                if(servicio.ConfirmarPresupuesto(nuevo))
                {
                    MessageBox.Show("Se agregaron los jugador al equipo Exitosamente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CalcularTotal();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el equipo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidarAceptar()
        {
            if (txtPais.Text == string.Empty)
            {
                MessageBox.Show("Ingrese campo valido en Pais", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPais.Focus();
                return false;
            }
            if (txtDT.Text == string.Empty)
            {
                MessageBox.Show("Ingrese campo valido en director tecnico", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDT.Focus();
                return false;
            }
            if (dgvDetalles.Rows.Count < 1)
            {
                MessageBox.Show("Ingrese por lo menos un jugador", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboPersona.Focus();
                return false;

            }
            return true;
        }

        private void LimpiarCampos()
        {
            txtPais.Text = "";
            txtDT.Text = "";
            cboPersona.SelectedIndex = -1;
            cboPosicion.SelectedIndex = -1;
            nudCamiseta.Value = 1;
            dgvDetalles.Rows.Clear();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(MessageBox.Show("Seguro que quiere eliminar este jugador","Control",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)==DialogResult.Yes)
            {
                if(dgvDetalles.CurrentCell.ColumnIndex==4)
                {
                    nuevo.Quitar(dgvDetalles.CurrentCell.RowIndex);
                    dgvDetalles.Rows.Remove(dgvDetalles.CurrentRow);
                    CalcularTotal();
                }
            }
        }
    }
}
