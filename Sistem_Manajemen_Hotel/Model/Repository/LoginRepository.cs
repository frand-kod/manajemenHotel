using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Sistem_Manajemen_Hotel.Model.Context;
using Sistem_Manajemen_Hotel.Model.Entity;
using System.Data;
using System.Drawing;

namespace Sistem_Manajemen_Hotel.Model.Repository
{
    public class LoginRepository
    {
            private SQLiteConnection _connection;

            //consruktor
            public LoginRepository(DbContext context)
            {
                _connection = context.Conn;
            }

            // Login Auththentication => non hashing
            // in future, PLEASE DO WITH HASHING ALGORITHM eg. MD5 or SHA256

            //maybe do with SHA256 Helper in future to make it easily
            public bool AuthUserLogin(LoginEntity user)
            {
                try
                {
                    string query = "SELECT COUNT(1) FROM login WHERE username = @Username AND password = @Password";
                    using (SQLiteCommand command = new SQLiteCommand(query, _connection))
                    {
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@Password", user.Password);

                        if (_connection.State == ConnectionState.Closed)
                        {
                            _connection.Open();
                        }

                        int result = (int)command.ExecuteScalar();
                        return result > 0;
                    }
                }
                catch (Exception ex)
                {
                    // Log error here
                    throw new Exception("Error during authentication: " + ex.Message);
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }

            //membuat userbaru
            public int CreateUserLogin(LoginEntity user)
            {
                try
                {
                    // Query SQL untuk memasukkan data login baru
                    string query = "INSERT INTO login (username, password) VALUES (@Username, @Password)";

                    using (SQLiteCommand command = new SQLiteCommand(query, _connection))
                    {
                        // Tambahkan parameter untuk query
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@Password", user.Password);

                        // Buka koneksi jika belum terbuka
                        if (_connection.State == ConnectionState.Closed)
                        {
                            _connection.Open();
                        }

                        // Eksekusi query dan kembalikan jumlah baris yang terpengaruh
                        return command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Log error jika diperlukan
                    throw new Exception("Error while creating user login: " + ex.Message);
                }
                finally
                {
                    // Tutup koneksi jika masih terbuka
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }

            //mencatat jumlah kegagalan login
            public void IncrementFailedAttempts(LoginEntity user)
            {
                string query = "UPDATE login SET failed_attempts = failed_attempts + 1 WHERE username = @Username";

                using (SQLiteCommand command = new SQLiteCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@Username", user.Username);

                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }

                    command.ExecuteNonQuery();
                }
            }

            // edit username password
            public bool EditUsernamePassword(LoginEntity user)
            {
                try
                {
                    // Query untuk mengupdate username 
                    string query = "UPDATE login SET username = @NewUsername WHERE id_login = @UserId;";

                    using (SQLiteCommand command = new SQLiteCommand(query, _connection))
                    {
                        command.Parameters.AddWithValue("@NewUsername", user.Username);
                        command.Parameters.AddWithValue("@UserId", user.idLogin);

                        // Buka koneksi jika belum terbuka
                        if (_connection.State == ConnectionState.Closed)
                        {
                            _connection.Open();
                        }

                        // Eksekusi query dan periksa apakah ada baris yang diubah
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
                catch (Exception ex)
                {
                    // Log error jika diperlukan
                    throw new Exception("Error while updating username: " + ex.Message);
                }
                finally
                {
                    // Tutup koneksi jika masih terbuka
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        public List<LoginEntity> ReadAll()
        {
            List<LoginEntity> list = new List<LoginEntity>(); // Ganti List<Login> menjadi List<Masuk>
            try
            {
                string sql = @"SELECT Username, Password FROM Login";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LoginEntity login = new LoginEntity(); // Ganti Masuk login = new Masuk
                            login.Username = reader["Username"].ToString();
                            login.Password = reader["Password"].ToString();


                            list.Add(login); // Ubah Masuk menjadi login
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
            }
            return list;
        }

        public List<LoginEntity> ReadByNama(string nama)
        {
            List<LoginEntity> list = new List<LoginEntity>();
            try
            {

                string sql = @"SELECT Username, Password FROM Login";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _connection))
                {
                    cmd.Parameters.AddWithValue("@nama", string.Format("%{0}%", nama));

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            while (reader.Read())
                            {
                                LoginEntity login = new LoginEntity(); // Ganti Masuk login = new Masuk
                                login.Username = reader["Username"].ToString();
                                login.Password = reader["Password"].ToString();


                                list.Add(login); // Ubah Masuk menjadi login
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByNama error: {0}", ex.Message);
            }
            return list;
        }

        public int Update(LoginEntity login)
        {
            int result = 0;
            string sql = @"UPDATE Login SET Password = @password WHERE Username = @username";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@password", login.Password);
                cmd.Parameters.AddWithValue("@username", login.Username);

                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Update error: {0}", ex.Message);
                }

            }
            return result;
        }

        public int Delete(string username)
        {
            int result = 0;

            string sql = @"DELETE FROM Login WHERE Username = @username";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@username", username);

                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Update error: {0}", ex.Message);
                }
            }
            return result;
        }
    }
}