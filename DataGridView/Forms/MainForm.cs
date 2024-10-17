﻿using DataGridView.Contracts;
using DataGridView.Contracts.Models;
using DataGridView.Forms;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridView
{
    public partial class MainForm : Form
    {
        private IProductManager productManager;
        private readonly BindingSource bindingSource;

        public MainForm(IProductManager manager)
        {
            InitializeComponent();

            productManager = manager;
            bindingSource = new BindingSource();

            dataGridView1.DataSource = bindingSource;
        }

        private async void ToolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var editForm = new AddAndEditProductForm();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                await productManager.AddAsync(editForm.Product);
                bindingSource.ResetBindings(false);
                await UpdateStatusStrip();
            }
        }

        private async void ToolStripButtonClose_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var oldProduct = (Product)dataGridView1.CurrentRow.DataBoundItem;

                if (MessageBox.Show(
                    $"Точно удалить товар \"{oldProduct.Name}\"?",
                    "Внимание",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                    ) == DialogResult.OK)
                {
                    await productManager.DeleteAsync(oldProduct.Id);
                    bindingSource.ResetBindings(false);
                    await UpdateStatusStrip();
                }
            }
        }

        private async void ToolStripButtonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var oldProduct = (Product)dataGridView1.CurrentRow.DataBoundItem;

                var editForm = new AddAndEditProductForm(oldProduct);

                if (MessageBox.Show(
                    $"Изменить товар \"{oldProduct.Name}\"?",
                    "Уведомление",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information
                    ) == DialogResult.OK)
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        await productManager.EditAsync(editForm.Product);
                        bindingSource.ResetBindings(false);
                        await UpdateStatusStrip();
                    }
                }
            }
        }

        private async Task UpdateStatusStrip()
        {
            var result = await productManager.GetStatsAsync();
            toolStripStatusLabelAmountProducts.Text = $"Количество строк: {result.TotalAmount}";
            toolStripStatusLabelPriceNDS.Text = $"Цена с НДС: {result.FullPriceWithNDS}";
            toolStripStatusLabelPriceNoNDS.Text = $"Цена без НДС: {result.FullPriceNoNDS}";
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "TotalPrice")
            {
                var row = (Product)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                e.Value = row.Quantity * row.Price;
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            bindingSource.DataSource = await productManager.GetAllAsync();
            await UpdateStatusStrip();
            dataGridView1.Columns.Add("TotalPrice", "Сумма");
            dataGridView1.Columns[nameof(Product.Id)].Visible = false;
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
