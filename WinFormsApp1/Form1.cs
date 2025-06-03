using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        
        private AppDbContext db = new AppDbContext();
        private BindingList<Pizza> pizzaList;
        private bool isClosing = false;

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            backToMenuButton.Click += backToMenuButton_Click;
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // KROK 1: Najpierw ustaw flagę, żeby zablokować nowe operacje
            isClosing = true;

            try
            {
                // KROK 2: Anuluj wszystkie oczekujące edycje PRZED zamknięciem kontekstu
                if (dataGridView1 != null)
                {
                    // Zakończ tryb edycji bez zapisywania zmian
                    dataGridView1.CancelEdit();

                    // Odłącz źródło danych, żeby uniknąć dalszych zdarzeń
                    dataGridView1.DataSource = null;
                }

                // KROK 3: Teraz bezpiecznie zamknij kontekst bazy danych
                if (db != null)
                {
                    db.Dispose();
                    db = null; // Ustaw na null, żeby uniknąć przypadkowego użycia
                }
            }
            catch (Exception ex)
            {
                // Loguj błąd, ale pozwól na zamknięcie formularza
                System.Diagnostics.Debug.WriteLine($"Błąd przy zamykaniu: {ex.Message}");

                // W przypadku błędu, wymuś zamknięcie kontekstu
                try
                {
                    db?.Dispose();
                    db = null;
                }
                catch
                {
                    // Ignoruj błędy przy wymuszonym zamykaniu
                }
            }
        }

    

        private bool isLoading = false;

        private void Form1_Load_1(object sender, EventArgs e)
        {
            isLoading = true;

            // Wyczyść wszystkie ślady poprzednich operacji
            db.ChangeTracker.Clear();

            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;

            db.Database.EnsureCreated();

            // Załaduj dane BEZ trackingu
            var pizze = db.pizze.AsNoTracking().ToList();
            pizzaList = new BindingList<Pizza>(pizze);
            dataGridView1.DataSource = pizzaList;

            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 13F);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);

            if (dataGridView1.Columns.Contains("Id"))
            {
                dataGridView1.Columns["Id"].Visible = false;
            }

            isLoading = false;
        }



        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

            if (dataGridView1.Rows[e.RowIndex].IsNewRow)
                return;

            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            string value = e.FormattedValue?.ToString();


            if (columnName == "Nazwa")
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Nazwa musi mieć co najmniej 3 znaki!";
                    e.Cancel = true;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "";
                }
            }


            if (columnName == "cena")
            {
                if (!decimal.TryParse(value, out decimal cena) || cena <= 0)
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Cena musi być liczbą dodatnią!";
                    e.Cancel = true;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "";
                }
            }
        }
        private bool IsDbContextAvailable()
        {
            return db != null && !isClosing;
        }

        private void dataGridView1_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (isClosing || !IsDbContextAvailable())
                return;

            try
            {
                if (dataGridView1.Rows == null || dataGridView1.Rows.Count == 0)
                    return;
                if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
                    return;

                DataGridViewRow row;
                try
                {
                    row = dataGridView1.Rows[e.RowIndex];
                }
                catch (ArgumentOutOfRangeException)
                {
                    return;
                }

                // DODATKOWE ZABEZPIECZENIE przed sprawdzaniem IsNewRow
                if (row == null || row.Cells == null || row.Cells.Count == 0)
                    return;

                // Sprawdź IsNewRow w try-catch
                bool isNewRow;
                try
                {
                    isNewRow = row.IsNewRow;
                }
                catch
                {
                    return; // Jeśli IsNewRow rzuca wyjątek, wyjdź
                }

                if (isNewRow)
                    return;

                // Sprawdź DataBoundItem w try-catch
                object dataBoundItem;
                try
                {
                    dataBoundItem = row.DataBoundItem;
                }
                catch
                {
                    return; // Jeśli DataBoundItem rzuca wyjątek, wyjdź
                }

                if (dataBoundItem == null)
                    return;

                var pizza = dataBoundItem as Pizza;
                if (pizza == null)
                    return;

                if (string.IsNullOrWhiteSpace(pizza.nazwa) || pizza.cena <= 0)
                {
                    MessageBox.Show("Uzupełnij poprawnie wszystkie wymagane pola!");
                    e.Cancel = true;
                    return;
                }

                if (pizza.id == 0)
                    db.pizze.Add(pizza);

                try
                {
                    if (IsDbContextAvailable())
                        db.SaveChanges();
                }
                catch (ObjectDisposedException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    if (ex.InnerException != null) msg += "\n" + ex.InnerException.Message;

                    if (msg.Contains("numeric field overflow"))
                        MessageBox.Show("Podana liczba jest za duża! Wprowadź mniejszą wartość.", "Błąd danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else if (msg.Contains("FOREIGN KEY"))
                        MessageBox.Show("Nie można usunąć powiązanej pozycji.", "Błąd bazy danych", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Błąd zapisu: " + msg, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    e.Cancel = true;
                }
            }
            catch (Exception)
            {
                // Globalny catch dla całej funkcji - jeśli cokolwiek pójdzie nie tak, po prostu wyjdź
                return;
            }
        }




        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }
        private void usunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                var pizza = row.DataBoundItem as Pizza;

                if (pizza != null && pizza.id != 0)
                {
                    try
                    {
                       
                        using (var checkContext = new AppDbContext())
                        {
                            var zamowieniaCount = checkContext.zamowienia.Count(z => z.pizzaid == pizza.id);

                            if (zamowieniaCount > 0)
                            {
                                MessageBox.Show(
                                    $"Nie można usunąć tej pizzy, ponieważ ma {zamowieniaCount} powiązanych zamówień.\n" +
                                    "Najpierw usuń wszystkie zamówienia tej pizzy.",
                                    "Nie można usunąć",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                return; // Nie usuwaj pizzy
                            }
                        }

                        // Jeśli pizza nie ma zamówień, usuń normalnie
                        using (var deleteContext = new AppDbContext())
                        {
                            var pizzaToDelete = deleteContext.pizze.Find(pizza.id);
                            if (pizzaToDelete != null)
                            {
                                deleteContext.pizze.Remove(pizzaToDelete);
                                deleteContext.SaveChanges();
                                pizzaList.Remove(pizza);
                                MessageBox.Show("Pizza została usunięta pomyślnie.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Błąd podczas usuwania: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Jeśli pizza nie ma ID (nowy wiersz), usuń tylko z listy
                    if (pizza != null)
                        pizzaList.Remove(pizza);
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void backToMenuButton_Click(object sender, EventArgs e)
        {

           
            Form2 existingForm2 = Application.OpenForms.OfType<Form2>().FirstOrDefault();

            if (existingForm2 != null)
            {
                
                existingForm2.Show();
                existingForm2.BringToFront();
            }
            else
            {
                
                Form2 form2 = new Form2();
                form2.Show();
            }

            this.Close();
        }
    }
}
