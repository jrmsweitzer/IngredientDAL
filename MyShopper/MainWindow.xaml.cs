using System.Globalization;
using System.Windows.Controls;
using IngredientDAL.Controllers;
using System;
using System.Collections.Generic;
using System.Windows;
using IngredientDAL.Models;

namespace MyShopper
{
    public class ReceiptObject
    {
        public string IngredientName { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        // ReSharper disable InconsistentNaming
        // ReSharper disable NotAccessedField.Local
        private readonly DatabaseRemote CONTROLLER;
        // ReSharper restore NotAccessedField.Local
        // ReSharper restore InconsistentNaming

        private readonly List<ReceiptObject> _receiptBuilder;
        private List<Product> _filteredProductsList; 
        private readonly List<Product> _allProducts;

        public MainWindow()
        {
            CONTROLLER = new DatabaseRemote();
            _receiptBuilder = new List<ReceiptObject>();
            _allProducts = CONTROLLER.GetAllProducts();
        }

        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateReceiptItem())
            {
                if (ButtonAddItem.Content.Equals("+ Add Item"))
                {
                    _receiptBuilder.Add(new ReceiptObject
                    {
                        IngredientName = TextBoxItem.Text,
                        Brand = TextBoxBrand.Text,
                        Price = double.Parse(TextBoxPrice.Text),
                        Quantity = double.Parse(TextBoxQuantity.Text),
                        Unit = TextBoxUnit.Text
                    });
                }
                else if (ButtonAddItem.Content.Equals("Edit Item"))
                {
                    var index = ListBoxCurrentReceipt.SelectedIndex;
                    _receiptBuilder[index] = new ReceiptObject
                    {
                        IngredientName = TextBoxItem.Text,
                        Brand = TextBoxBrand.Text,
                        Price = double.Parse(TextBoxPrice.Text),
                        Quantity = double.Parse(TextBoxQuantity.Text),
                        Unit = TextBoxUnit.Text
                    };
                    ButtonAddItem.Content = "+ Add Item";
                }
                UnfocusListBoxes();
                ClearReceiptBuilder();
                UpdateCurrentReceipt();
            }
            if (_receiptBuilder.Count > 0)
            {
                EnableSaveButton();
                EnableClearReceiptButton();
            }
        }

        private void EnableClearReceiptButton()
        {
            ButtonClearReceipt.IsEnabled = true;
        }

        private void DisableClearReceiptButton()
        {
            ButtonClearReceipt.IsEnabled = false;
        }

        private void EnableSaveButton()
        {
            ButtonSave.IsEnabled = true;
        }

        private void DisableSaveButton()
        {
            ButtonSave.IsEnabled = false;
        }

        private void UnfocusListBoxes()
        {
            ListBoxCurrentReceipt.SelectedIndex = -1;
            ListBoxFilteredReceipts.SelectedIndex = -1;
        }

        private void UpdateCurrentReceipt()
        {
            ListBoxCurrentReceipt.Items.Clear();

            foreach (var receipt in _receiptBuilder)
            {
                //In The Raw Sugar, Two Pounds, $3.99
                //Brand Ingredient, Quantity Unit, $Price
                const string receiptLine = "{0} {1}, {2} {3}, ${4}";
                ListBoxCurrentReceipt.Items.Add(
                    string.Format(receiptLine,
                        receipt.Brand,
                        receipt.IngredientName,
                        receipt.Quantity,
                        receipt.Unit,
                        receipt.Price));
            }
            UnfocusListBoxes();
        }

        private void UpdateFilteredReceipt(IEnumerable<Product> list)
        {
            ListBoxFilteredReceipts.Items.Clear();
            foreach (var product in list)
            {
                //In The Raw Sugar, Two Pounds
                //Brand Ingredient, Quantity Unit
                const string receiptLine = "{0} {1}, {2} {3}";
                ListBoxFilteredReceipts.Items.Add(
                    string.Format(receiptLine,
                        product.BrandName,
                        product.Ingredient.IngredientName,
                        product.ProductQuantity,
                        product.ProductUnit));

            }
        }

        private bool ValidateReceiptItem()
        {
            const string message = "Please fill in the {0} field";
            var valid = true;
            if (String.IsNullOrEmpty(TextBoxBrand.Text))
            {
                valid = false;
                MessageBox.Show(string.Format(message, "Brand"));
            }
            if (String.IsNullOrEmpty(TextBoxItem.Text))
            {
                valid = false;
                MessageBox.Show(string.Format(message, "Item"));
            }
            if (String.IsNullOrEmpty(TextBoxPrice.Text))
            {
                valid = false;
                MessageBox.Show(string.Format(message, "Price"));
            }
            if (String.IsNullOrEmpty(TextBoxQuantity.Text))
            {
                valid = false;
                MessageBox.Show(string.Format(message, "Quantity"));
            }
            if (String.IsNullOrEmpty(TextBoxUnit.Text))
            {
                valid = false;
                MessageBox.Show(string.Format(message, "Unit"));
            }
            return valid;
        }

        private void FilterResults_TextChanged(object sender, TextChangedEventArgs e)
        {
            _filteredProductsList = _allProducts;
            if (!String.IsNullOrEmpty(TextBoxBrand.Text))
            {
                _filteredProductsList = CONTROLLER.FilterProductsByBrandNameContaining(
                    _filteredProductsList, TextBoxBrand.Text);
            }
            if (!String.IsNullOrEmpty(TextBoxItem.Text))
            {
                _filteredProductsList = CONTROLLER.FilterProductsByIngredientNameContaining(
                    _filteredProductsList, TextBoxItem.Text);
            }
            if (!String.IsNullOrEmpty(TextBoxQuantity.Text))
            {
                _filteredProductsList = CONTROLLER.FilterProductsByQuantityContaining(
                    _filteredProductsList, TextBoxQuantity.Text);
            }
            if (!String.IsNullOrEmpty(TextBoxUnit.Text))
            {
                _filteredProductsList = CONTROLLER.FilterProductsByUnitContaining(
                    _filteredProductsList, TextBoxUnit.Text);
            }
            UpdateFilteredReceipt(
                CONTROLLER.SortProductsByBrandName(_filteredProductsList));

        }

        private void ClearItemBtn_Click_1(object sender, RoutedEventArgs e)
        {
            ClearReceiptBuilder();
        }

        private void ClearReceiptBuilder()
        {
            TextBoxBrand.Text = "";
            TextBoxItem.Text = "";
            TextBoxPrice.Text = "";
            TextBoxQuantity.Text = "";
            TextBoxUnit.Text = "";
        }

        private void FilteredReceipts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxFilteredReceipts.SelectedIndex >= 0)
            {
                var index = ListBoxFilteredReceipts.SelectedIndex;
                var brand = _filteredProductsList[index].BrandName;
                var item = _filteredProductsList[index].Ingredient
                    .IngredientName;
                var quantity = _filteredProductsList[index]
                    .ProductQuantity.ToString(CultureInfo.InvariantCulture);
                var unit = _filteredProductsList[index].ProductUnit;
                TextBoxBrand.Text = brand;
                TextBoxItem.Text = item;
                TextBoxUnit.Text = unit;
                TextBoxQuantity.Text = quantity;
                UnfocusListBoxCurrentReceipt();

            }

        }

        private void UnfocusListBoxCurrentReceipt()
        {
            ListBoxCurrentReceipt.SelectedIndex = -1;
        }

        private void CurrentReceipt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxCurrentReceipt.SelectedIndex >= 0)
            {
                EnableButtonEditSelected();
                EnableButtonDeleteSelected();
                ClearReceiptBuilder();
                var index = ListBoxCurrentReceipt.SelectedIndex;
                TextBoxBrand.Text = _receiptBuilder[index].Brand;
                TextBoxItem.Text = _receiptBuilder[index].IngredientName;
                TextBoxQuantity.Text = _receiptBuilder[index].Quantity.ToString(
                    CultureInfo.InvariantCulture);
                TextBoxUnit.Text = _receiptBuilder[index].Unit;
                TextBoxPrice.Text = _receiptBuilder[index].Price.ToString(
                    CultureInfo.InvariantCulture);
            }
            else
            {
                DisableButtonEditSelected();
                DisableButtonDeleteSelected();
            }
        }

        private void DisableButtonDeleteSelected()
        {
            ButtonDeletedSelected.IsEnabled = false;
        }

        private void DisableButtonEditSelected()
        {
            ButtonEditSelected.IsEnabled = false;
        }

        private void EnableButtonDeleteSelected()
        {
            ButtonDeletedSelected.IsEnabled = true;
        }

        private void EnableButtonEditSelected()
        {
            ButtonEditSelected.IsEnabled = true;
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateReceipt())
            {
                foreach (var receipt in _receiptBuilder)
                {
                    CONTROLLER.AddReceiptItem(
                        receipt.IngredientName,
                        receipt.Brand,
                        (int) receipt.Quantity,
                        receipt.Unit,
                        TextBoxStore.Text,
                        DateTime.Now,
                        receipt.Price);
                }
                MessageBox.Show("Saved Successfully!");
            }
        }

        private bool ValidateReceipt()
        {
            if (!String.IsNullOrEmpty(TextBoxStore.Text)) return true;
            MessageBox.Show("Please fill in the Store field.");
            return false;
        }

        private void ClearReceiptBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearReceiptBuilder();
            ClearReceipt();
            DisableSaveButton();
            DisableClearReceiptButton();
        }

        private void ClearReceipt()
        {
            _receiptBuilder.Clear();
            ListBoxCurrentReceipt.Items.Clear();
            UpdateCurrentReceipt();
        }

        private void DeletedSelectedBtn_Click(object sender, RoutedEventArgs e)
        {
            var index = ListBoxCurrentReceipt.SelectedIndex;
            ListBoxCurrentReceipt.Items.Remove(
                ListBoxCurrentReceipt.Items[index]);
            _receiptBuilder.Remove(_receiptBuilder[index]);
            ClearReceiptBuilder();
            UpdateCurrentReceipt();
            if (_receiptBuilder.Count == 0)
            {
                DisableSaveButton();
                DisableClearReceiptButton();
            }
        }

        private void EditSelectedBtn_Click(object sender, RoutedEventArgs e)
        {
            DisableSaveButton();
            DisableClearReceiptButton();
            DisableButtonDeleteSelected();
            ButtonAddItem.Content = "Edit Item";
        }

        private void SearchBTN_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
