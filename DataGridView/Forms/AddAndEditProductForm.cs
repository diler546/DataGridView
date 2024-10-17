using DataGridView.Contracts.Models;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DataGridView.Forms
{
    public partial class AddAndEditProductForm : Form
    {
        private Product product;

        public AddAndEditProductForm(Product oldProduct = null)
        {
            InitializeComponent();
            InitializeComboBox();

            product = oldProduct ?? DataGenerator.CreateDefaultProduct();

            textBoxName.AddBinding(control => control.Text, product, product => product.Name, errorProvider1);
            numericUpDownSize.AddBinding(control => control.Value, product, product => product.Size, errorProvider1);
            comboBoxMaterials.AddBinding(control => control.SelectedItem, product, product => product.Material);
            numericUpDownQuantity.AddBinding(control => control.Value, product, product => product.Quantity, errorProvider1);
            numericUpDownMinimumQuantity.AddBinding(control => control.Value, product, product => product.MinimumQuantity, errorProvider1);
            numericUpDownPrice.AddBinding(control => control.Value, product, product => product.Price, errorProvider1);

        }

        public Product Product => product;

        private void InitializeComboBox()
        {
            comboBoxMaterials.DataSource = EnumHelper.GetEnumDescriptions(typeof(Materials));
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (Product.IsValid())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
