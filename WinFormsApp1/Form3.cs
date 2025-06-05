using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form3 : Form
    {
        private AppDbContext db = new AppDbContext();
        private BindingList<Zamowienie> zamowienieList;
        private bool isClosing = false;
        private bool isLoading = false;

        public Form3()
        {
            InitializeComponent(); // Usuń duplikat - tylko jeden raz!
            this.FormClosing += Form3_FormClosing;
            this.Load += Form3_Load; // Podłącz zdarzenie Load

            // NIE konfiguruj DataGridView w konstruktorze - zamowienieList jest null!
            // Konfiguracja zostanie wykonana w Form3_Load
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;

            try
            {
                if (dataGridView1 != null)
                {
                    dataGridView1.CancelEdit();
                    dataGridView1.DataSource = null;
                }

                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Błąd przy zamykaniu: {ex.Message}");
                try
                {
                    db?.Dispose();
                    db = null;
                }
                catch { }
            }
        }


        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Obsłuż błąd ComboBox
            if (e.Exception is ArgumentException && e.Exception.Message.Contains("DataGridViewComboBoxCell"))
            {
                // Ustaw wartość na pierwszą dostępną pizzę
                if (dataGridView1.Columns[e.ColumnIndex].Name == "nazwapizzy")
                {
                    var pizze = db.pizze.AsNoTracking().ToList();
                    if (pizze.Count > 0)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = pizze[0].id;
                    }
                }
                e.ThrowException = false; // Nie przerywaj działania
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            isLoading = true;

            db.ChangeTracker.Clear();
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            db.Database.EnsureCreated();
            dataGridView1.AutoGenerateColumns = false;

            // Dodaj obsługę błędów
            dataGridView1.DataError += dataGridView1_DataError;

            // Konfiguruj ComboBox dla pizzy
            if (dataGridView1.Columns["nazwapizzy"] != null)
                dataGridView1.Columns.Remove("nazwapizzy");

            var pizzaColumn = new DataGridViewComboBoxColumn();
            pizzaColumn.Name = "nazwapizzy";
            pizzaColumn.HeaderText = "Pizza";
            pizzaColumn.DataPropertyName = "pizzaid";
            pizzaColumn.ValueMember = "id";
            pizzaColumn.DisplayMember = "nazwa";

            // Załaduj pizze z pustą opcją
            var pizze = db.pizze.AsNoTracking().ToList();
            var pizzeList = new List<Pizza>
    {
        new Pizza { id = 0, nazwa = "-- Wybierz pizzę --" }
    };
            pizzeList.AddRange(pizze);
            pizzaColumn.DataSource = pizzeList;

            dataGridView1.Columns.Insert(2, pizzaColumn);

            // Mapowanie kolumn
            if (dataGridView1.Columns["imie"] != null)
                dataGridView1.Columns["imie"].DataPropertyName = "imieklienta";

            if (dataGridView1.Columns["numertelefonu"] != null)
                dataGridView1.Columns["numertelefonu"].DataPropertyName = "numertelefonu";

            if (dataGridView1.Columns["datazamowienia"] != null)
                dataGridView1.Columns["datazamowienia"].DataPropertyName = "datazamowienia";

            if (dataGridView1.Columns["status"] != null)
                dataGridView1.Columns["status"].DataPropertyName = "status";

            // Załaduj i sprawdź dane
            var zamowienia = db.zamowienia
                .Include(z => z.Pizza)
                .AsNoTracking()
                .ToList();

            // Napraw nieistniejące powiązania
            foreach (var zamowienie in zamowienia)
            {
                if (!pizze.Any(p => p.id == zamowienie.pizzaid))
                {
                    zamowienie.pizzaid = 0; // Ustaw na pustą opcję
                }
            }

            zamowienieList = new BindingList<Zamowienie>(zamowienia);
            dataGridView1.DataSource = zamowienieList;

            // Ukryj niepotrzebne kolumny
            if (dataGridView1.Columns.Contains("id"))
                dataGridView1.Columns["id"].Visible = false;
            if (dataGridView1.Columns.Contains("pizzaid"))
                dataGridView1.Columns["pizzaid"].Visible = false;

            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 13F);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);

            isLoading = false;
        }


        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].IsNewRow)
                return;

            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            string value = e.FormattedValue?.ToString();

            // Walidacja imienia klienta - nie puste, minimum 2 znaki, tylko litery
            if (columnName == "imie")
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Imię nie może być puste!";
                    e.Cancel = true;
                }
                else if (value.Length < 2)
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Imię musi mieć co najmniej 2 znaki!";
                    e.Cancel = true;
                }
                else if (value.Any(char.IsDigit))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Imię nie może zawierać cyfr!";
                    e.Cancel = true;
                }
                else if (value.Length > 50)
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Imię nie może być dłuższe niż 50 znaków!";
                    e.Cancel = true;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "";
                }
            }

            // Walidacja numeru telefonu - nie pusty, dokładnie 9 cyfr
            if (columnName == "numertelefonu")
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Numer telefonu nie może być pusty!";
                    e.Cancel = true;
                }
                else if (!value.All(char.IsDigit))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Numer telefonu może zawierać tylko cyfry!";
                    e.Cancel = true;
                }
                else if (value.Length != 9)
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Numer telefonu musi mieć dokładnie 9 cyfr!";
                    e.Cancel = true;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "";
                }
            }

            // Walidacja daty zamówienia - nie pusta, poprawny format
            if (columnName == "datazamowienia")
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Data zamówienia nie może być pusta!";
                    e.Cancel = true;
                }
                else if (!DateTime.TryParse(value, out DateTime data))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Wprowadź poprawną datę (np. dd.mm.yyyy hh:mm)!";
                    e.Cancel = true;
                }
                else if (data < DateTime.Now.AddYears(-1))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Data zamówienia nie może być starsza niż rok!";
                    e.Cancel = true;
                }
                else if (data > DateTime.Now.AddDays(30))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Data zamówienia nie może być dalej niż 30 dni w przyszłość!";
                    e.Cancel = true;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "";
                }
            }

            // Walidacja statusu - nie pusty, z dozwolonych wartości
            if (columnName == "status")
            {
                var dozwoloneStatusy = new[] { "nowe", "w realizacji", "gotowe", "dostarczone" };

                if (string.IsNullOrWhiteSpace(value))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Status nie może być pusty!";
                    e.Cancel = true;
                }
                else if (value.Any(char.IsDigit))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Status nie może zawierać cyfr!";
                    e.Cancel = true;
                }
                else if (!dozwoloneStatusy.Contains(value.ToLower()))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Status musi być: nowe, w realizacji, gotowe lub dostarczone!";
                    e.Cancel = true;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "";
                }
            }

            // Walidacja ComboBox pizzy - nie pusta opcja
            if (columnName == "nazwapizzy")
            {
                if (string.IsNullOrWhiteSpace(value) || value == "-- Wybierz pizzę --")
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Musisz wybrać pizzę dla zamówienia!";
                    e.Cancel = true;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "";
                }
            }
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

                // Dodatkowe zabezpieczenie przed sprawdzaniem IsNewRow
                if (row == null || row.Cells == null || row.Cells.Count == 0)
                    return;

                // IsNewRow w try-catch
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

                // DataBoundItem w try-catch
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

                var zamowienie = dataBoundItem as Zamowienie;
                if (zamowienie == null)
                    return;

                // Walidacja wymaganych pól zamówienia
                if (string.IsNullOrWhiteSpace(zamowienie.imieklienta) ||
                    string.IsNullOrWhiteSpace(zamowienie.numertelefonu) ||
                    string.IsNullOrWhiteSpace(zamowienie.status))
                {
                    MessageBox.Show("Uzupełnij poprawnie wszystkie wymagane pola!");
                    e.Cancel = true;
                    return;
                }

                // Walidacja statusu
                var dozwoloneStatusy = new[] { "nowe", "w realizacji", "gotowe", "dostarczone" };
                if (!dozwoloneStatusy.Contains(zamowienie.status.ToLower()))
                {
                    MessageBox.Show("Status musi być: nowe, w realizacji, gotowe lub dostarczone!", "Błędny status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }

                // Sprawdź, czy pizzaid jest poprawne
                if (zamowienie.pizzaid <= 0)
                {
                    MessageBox.Show("Wybierz poprawną pizzę dla zamówienia!");
                    e.Cancel = true;
                    return;
                }

                // Sprawdź, czy pizza o podanym ID istnieje w bazie
                if (!db.pizze.Any(p => p.id == zamowienie.pizzaid))
                {
                    MessageBox.Show("Pizza o podanym ID nie istnieje w bazie danych!");
                    e.Cancel = true;
                    return;
                }

                // Obsługa daty zamówienia
                if (zamowienie.datazamowienia == default(DateTime))
                {
                    zamowienie.datazamowienia = DateTime.Now;
                }

                // Konwertuj datę na format odpowiedni dla PostgreSQL timestamp without time zone
                zamowienie.datazamowienia = DateTime.SpecifyKind(zamowienie.datazamowienia, DateTimeKind.Unspecified);

                // Dodaj nowe zamówienie do bazy lub zaktualizuj istniejące
                if (zamowienie.id == 0)
                {
                    db.zamowienia.Add(zamowienie);
                }
                else
                {
                    // Dla istniejących zamówień - zaktualizuj w bazie
                    var existingZamowienie = db.zamowienia.Find(zamowienie.id);
                    if (existingZamowienie != null)
                    {
                        existingZamowienie.imieklienta = zamowienie.imieklienta;
                        existingZamowienie.numertelefonu = zamowienie.numertelefonu;
                        existingZamowienie.pizzaid = zamowienie.pizzaid;
                        existingZamowienie.datazamowienia = DateTime.SpecifyKind(zamowienie.datazamowienia, DateTimeKind.Unspecified);
                        existingZamowienie.status = zamowienie.status;
                    }
                }

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

                    if (msg.Contains("DateTime") || msg.Contains("timestamp"))
                        MessageBox.Show("Błąd formatu daty. Sprawdź czy data jest poprawna.", "Błąd daty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else if (msg.Contains("numeric field overflow"))
                        MessageBox.Show("Podana liczba jest za duża! Wprowadź mniejszą wartość.", "Błąd danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else if (msg.Contains("FOREIGN KEY"))
                        MessageBox.Show("Nie można dodać zamówienia - pizza o podanym ID nie istnieje.", "Błąd bazy danych", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (msg.Contains("duplicate key"))
                        MessageBox.Show("Zamówienie o takich danych już istnieje.", "Błąd bazy danych", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Błąd zapisu: " + msg, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    e.Cancel = true;
                }
            }
            catch (Exception)
            {
                // Globalny catch dla całej funkcji - jeśli błąd, po prostu wyjdź
                return;
            }
        }

        private bool IsDbContextAvailable()
        {
            return db != null && !isClosing;
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
                var zamowienie = row.DataBoundItem as Zamowienie;

                if (zamowienie != null && zamowienie.id != 0)
                {
                    try
                    {
                        // Potwierdzenie usunięcia zamówienia
                        var result = MessageBox.Show(
                            $"Czy na pewno chcesz usunąć zamówienie klienta '{zamowienie.imieklienta}'?\n" +
                            $"Data zamówienia: {zamowienie.datazamowienia:dd.MM.yyyy HH:mm}\n" +
                            $"Status: {zamowienie.status}",
                            "Potwierdzenie usunięcia",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result != DialogResult.Yes)
                            return;

                        // Użyj głównego kontekstu zamiast nowego
                        var zamowienieToDelete = db.zamowienia.Find(zamowienie.id);
                        if (zamowienieToDelete != null)
                        {
                            db.zamowienia.Remove(zamowienieToDelete);
                            db.SaveChanges();

                            // Usuń z listy po pomyślnym usunięciu z bazy
                            zamowienieList.Remove(zamowienie);
                            MessageBox.Show("Zamówienie zostało usunięte pomyślnie.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Zamówienie nie zostało znalezione w bazie danych.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        var msg = ex.Message;
                        if (ex.InnerException != null) msg += "\n" + ex.InnerException.Message;

                        if (msg.Contains("FOREIGN KEY"))
                            MessageBox.Show("Nie można usunąć zamówienia - jest powiązane z innymi danymi.", "Błąd bazy danych", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else if (msg.Contains("DELETE"))
                            MessageBox.Show("Błąd podczas usuwania zamówienia z bazy danych.", "Błąd bazy danych", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                            MessageBox.Show($"Błąd podczas usuwania: {msg}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Jeśli zamówienie nie ma ID (nowy wiersz), usuń tylko z listy
                    if (zamowienie != null)
                    {
                        zamowienieList.Remove(zamowienie);
                        MessageBox.Show("Nowe zamówienie zostało usunięte z listy.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano żadnego zamówienia do usunięcia.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void exit_Click_Click(object sender, EventArgs e)
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