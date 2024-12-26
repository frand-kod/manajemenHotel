using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Data.SQLite;
using System.Diagnostics;

namespace Sistem_Manajemen_Hotel.Model.Context
{
    public class DbContext : IDisposable
    {
        // deklarasi private variabel / field
        private SQLiteConnection _conn;
        private string connectionString;


        // deklarasi property Conn (connection), untuk menyimpan objek koneksi
        public SQLiteConnection Conn
        {
            get { return _conn ?? (_conn = GetOpenConnection()); }
        }

        // Method untuk melakukan koneksi ke database
        private SQLiteConnection GetOpenConnection()
        {
            SQLiteConnection conn = null; // deklarasi objek connection

            try // penggunaan blok try-catch untuk penanganan error
            {
                string dbPath = "C:\\Users\\user\\Documents\\kuliah\\Semester 3\\Pemrograman Lanjut\\Project dewe\\TesterMVC-SqlIite\\database.db";
                connectionString = $"Data Source={dbPath};Version=3;";
                var connection = new SQLiteConnection(connectionString);
                connection.Open();

                Debug.Print("Koneksi ke database berhasil dilakukan");
            }
            // jika terjadi error di blok try, akan ditangani langsung oleh blok catch
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("Open Connection Error: {0}", ex.Message);
            }

            return conn;
        }

        // Method ini digunakan untuk menghapus objek koneksi dari memory ketika sudah tidak digunakan
        public void Dispose()
        {
            if (_conn != null)
            {
                try
                {
                    if (_conn.State != ConnectionState.Closed) _conn.Close();
                }
                finally
                {
                    _conn.Dispose();
                }
            }
            GC.SuppressFinalize(this);
        }

    }
}
