using RecepieScraper.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Timers;

namespace RecepieScraper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string RecepieFileLocation = "recepies.txt";
        //Dictionary<string, Recepie> recepies = new Dictionary<string, Recepie>();
        Dictionary<string, int> recepieImageIndex = new Dictionary<string, int>();
        string orderBy = "name";
        RecepieEngine recepiesEngine = new RecepieEngine();

        System.Timers.Timer logTimer = new System.Timers.Timer();
        
        private void Form1_Load(object sender, EventArgs e)
        {
            recepiesEngine.LoadRecepies(RecepieFileLocation);
            InsertRecepiesIntoListViews(true);

            logTimer.Interval = 1000;
            logTimer.Elapsed += logTimer_Elapsed;
            logTimer.Start();
        }

        private void logTimer_Elapsed(Object source, ElapsedEventArgs e)
        {
            MethodInvoker inv = delegate
            {
                this.tbLog.Text = recepiesEngine.GetLog();
            };

            this.Invoke(inv);            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item2 in lstRecepieCatalogue.SelectedItems)
            {
                ListViewItem item = new ListViewItem();
                item.Text = item2.Text;
                item.ImageIndex = item2.ImageIndex;
                item.ToolTipText = item2.ToolTipText;

                lstSelection.Items.Add(item);
            }

            CalculateIngredients();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstSelection.SelectedItems)
            {
                lstSelection.Items.Remove(item);
            }

            CalculateIngredients();
        }

        private void CalculateIngredients()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Ingredient");
            dt.Columns.Add("Quantity", typeof(float));
            dt.Columns.Add("Measurement");

            Dictionary<string, Ingredient> ingredients = new Dictionary<string, Ingredient>();

            foreach (ListViewItem item in lstSelection.Items)
            {
                Recepie recepie = recepiesEngine.Recepies[item.ToolTipText];

                foreach (Ingredient ingredient in recepie.Ingredients.Values)
                {
                    if (ingredients.ContainsKey(ingredient.Name))
                    {
                        ingredients[ingredient.Name].Quantity += ingredient.Quantity;
                    }
                    else
                    {
                        ingredients.Add(ingredient.Name, new Ingredient() { Name = ingredient.Name, Quantity = ingredient.Quantity, MeasurmentType = ingredient.MeasurmentType });
                    }
                }
            }

            foreach (Ingredient ingredient in ingredients.Values)
            {
                dt.Rows.Add(ingredient.Name, ingredient.Quantity, ingredient.MeasurmentType);
            }

            dataGridView1.DataSource = dt;
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch.Text.Length == 0 || tbSearch.Text.Length > 2)
                InsertRecepiesIntoListViews();
        }

        private void lstRecepieCatalogue_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (lstRecepieCatalogue.FocusedItem.Bounds.Contains(e.Location) || lstSelection.FocusedItem.Bounds.Contains(e.Location))
                {
                    recepieMenu.Show(Cursor.Position);
                }
            }
        }

        private void starButton_click(object sender, EventArgs e)
        {
            ToolStripMenuItem starButton = (ToolStripMenuItem)sender;
            int selectedRating = int.Parse(starButton.Text.Replace(" ★", ""));
            if (lstRecepieCatalogue.SelectedItems.Count > 0)
                foreach (ListViewItem item in lstRecepieCatalogue.SelectedItems)
                    recepiesEngine.SetStarRating(item.ToolTipText, selectedRating);
            if (lstSelection.SelectedItems.Count > 0)
                foreach (ListViewItem item in lstSelection.SelectedItems)
                    recepiesEngine.SetStarRating(item.ToolTipText, selectedRating);


            InsertRecepiesIntoListViews();
        }

        private void orderByName_click(object sender, EventArgs e)
        {
            orderBy = "name";
            InsertRecepiesIntoListViews();
        }

        private void ratingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            orderBy = "rating";
            InsertRecepiesIntoListViews();
        }

        private void caloriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            orderBy = "calories";
            InsertRecepiesIntoListViews();
        }

        private void prepTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            orderBy = "preptime";
            InsertRecepiesIntoListViews();
        }

        private void openInRecepieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstRecepieCatalogue.SelectedItems.Count > 0)
                foreach (ListViewItem item in lstRecepieCatalogue.SelectedItems)
                    System.Diagnostics.Process.Start(item.ToolTipText);
            if (lstSelection.SelectedItems.Count > 0)
                foreach (ListViewItem item in lstSelection.SelectedItems)
                    System.Diagnostics.Process.Start(item.ToolTipText);
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void dontShow1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dontShow1ToolStripMenuItem.Checked = !dontShow1ToolStripMenuItem.Checked;
            InsertRecepiesIntoListViews();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void reScrapeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Stick in different thread to not lock up application
            recepiesEngine.RescrapeRecepieSite();
            InsertRecepiesIntoListViews(true);            
        }

        #region private methods

        private void InsertRecepiesIntoListViews(bool reloadThumbnails = false)
        {
            for (int j = 0; j < 5; j++)
            {
                try
                {
                    if (reloadThumbnails)
                    {
                        int i = 0;

                        thumbNails.Images.Clear();
                        recepieImageIndex.Clear();

                        thumbNails.ImageSize = new Size(256, 256);

                        foreach (Recepie recepie in recepiesEngine.Recepies.Values)
                        {
                            if (recepie.ThumbnailImageBytes != null && recepie.ThumbnailImageBytes.Length > 0)
                            {
                                MemoryStream ms = new MemoryStream(recepie.ThumbnailImageBytes);
                                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

                                thumbNails.Images.Add(img);
                                recepieImageIndex.Add(recepie.RecepieUrl, i);

                                i++;
                            }
                        }
                        lstRecepieCatalogue.LargeImageList = thumbNails;
                    }


                    lstRecepieCatalogue.Clear();
                    string filter = null;

                    if (tbSearch.Text.Length > 1)
                        filter = tbSearch.Text;

                    List<Recepie> orderedRecepieList = null;

                    if (orderBy == "name")
                        orderedRecepieList = recepiesEngine.Recepies.Values.OrderBy(o => o.Title).ToList();
                    else if (orderBy == "calories")
                        orderedRecepieList = recepiesEngine.Recepies.Values.OrderBy(o => o.Calories).ToList();
                    else if (orderBy == "preptime")
                        orderedRecepieList = recepiesEngine.Recepies.Values.OrderBy(o => o.PrepTime).ToList();
                    else if (orderBy == "rating")
                        orderedRecepieList = recepiesEngine.Recepies.Values.OrderByDescending(o => o.Rating).ToList();
                    else
                        orderedRecepieList = recepiesEngine.Recepies.Values.ToList();

                    if (dontShow1ToolStripMenuItem.Checked)
                        orderedRecepieList = orderedRecepieList.Where(w => w.Rating != 1).ToList();

                    if(showLowCarbOnlyToolStripMenuItem.Checked)
                        orderedRecepieList = orderedRecepieList.Where(w => w.Carbs < 7).ToList();


                    foreach (Recepie recepie in orderedRecepieList)
                    {
                        if (recepie.ThumbnailImageBytes != null && recepie.ThumbnailImageBytes.Length > 0)
                        {

                            if (!String.IsNullOrWhiteSpace(filter))
                            {
                                if (recepie.Title.ToLower().Contains(filter.ToLower()))
                                {
                                    lstRecepieCatalogue.Items.Add(ConvertRecepieToListViewItem(recepie));
                                }
                                else if (recepie.Ingredients.Any(a => a.Key.ToLower().Contains(filter.ToLower())))
                                {
                                    lstRecepieCatalogue.Items.Add(ConvertRecepieToListViewItem(recepie));
                                }
                            }
                            else
                            {
                                lstRecepieCatalogue.Items.Add(ConvertRecepieToListViewItem(recepie));
                            }
                        }
                    }
                    break;
                }
                catch
                {
                    System.Threading.Thread.Sleep(1000);
                    if (j == 4)
                        throw;
                }
            }

        }

        private ListViewItem ConvertRecepieToListViewItem(Recepie recepie)
        {
            ListViewItem item = new ListViewItem();
            item.Text = recepie.Title + System.Environment.NewLine + "[" + recepie.PrepTime + " min] - [" + recepie.Calories + " kcal]" + (recepie.Rating > 0 ? " - [" + recepiesEngine.GenerateStarString(recepie.Rating) +"]" : "");
            item.ToolTipText = recepie.RecepieUrl;
            item.ImageIndex = recepieImageIndex[recepie.RecepieUrl];
            return item;
        }

        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void showLowCarbOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showLowCarbOnlyToolStripMenuItem.Checked = !showLowCarbOnlyToolStripMenuItem.Checked;
            InsertRecepiesIntoListViews();
        }
    }
}
