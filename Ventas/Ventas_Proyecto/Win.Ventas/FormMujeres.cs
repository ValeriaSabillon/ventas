using BL.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win.Ventas
{
    public partial class FormMujeres : Form
    {
        MujeresBL _productos;

        public FormMujeres()
        {
            InitializeComponent();

            _productos = new MujeresBL();//Inicializar
            listaProdMujeresBindingSource.DataSource = _productos.obtenerProductos();
        }

        private void listaProdMujeresBindingNavigator_RefreshItems(object sender, EventArgs e)
        {

        }

        private void listaProdMujeresBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            listaProdMujeresBindingSource.EndEdit();
            var mujer =(Mujer)listaProdMujeresBindingSource.Current;

            var resultado = _productos.GuardarProdMujeres(mujer);

            if (resultado.Exitoso==true)
            {
                listaProdMujeresBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotones(true);
            }
            else
            {
                MessageBox.Show(resultado.Mensaje);
            }
     
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _productos.AgregarProdMujeres();
            listaProdMujeresBindingSource.MoveLast();

            DeshabilitarHabilitarBotones(false);
        }

        private void DeshabilitarHabilitarBotones(bool valor)
        {
            bindingNavigatorMoveFirstItem.Enabled = valor;
            bindingNavigatorMoveLastItem.Enabled = valor;
            bindingNavigatorMovePreviousItem.Enabled = valor;
            bindingNavigatorMoveNextItem.Enabled = valor;
            bindingNavigatorPositionItem.Enabled = valor;

            bindingNavigatorAddNewItem.Enabled = valor;
            bindingNavigatorDeleteItem.Enabled = valor;
            toolStripButtonCancelar.Visible = !valor;
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
           
            if (codigoTextBox.Text != "")//validacion del texto no este vacio
            {
                var resultado = MessageBox.Show("Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    var codigo = Convert.ToInt32(codigoTextBox.Text);
                    Eliminar(codigo);
                }
             }
        }

        private void Eliminar(int codigo)
        {
          
            var resultado = _productos.EliminarProdMujeres(codigo);
            if (resultado == true)
            {
                listaProdMujeresBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al eliminar el producto");
            }
        }

        private void toolStripButtonCancelar_Click(object sender, EventArgs e)
        {
            DeshabilitarHabilitarBotones(true);
            Eliminar(0);
        }
    }
}
