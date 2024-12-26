using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Sistem_Manajemen_Hotel.Model.Context;
using Sistem_Manajemen_Hotel.Model.Entity;
using System.Data;
using System.Diagnostics;

namespace Sistem_Manajemen_Hotel.Model.Repository
{
    public class ClientRepository
    {
        private SQLiteConnection _connection;

        //consruktor
        public ClientRepository(DbContext context)
        {
            _connection = context.Conn;
        }
        public int CreateClient(ClientEntity client)
        {
            int result = 0;

            // Query Create ClientEntity
            string sqlCreate = @"INSERT INTO client (firstname, lastname, phone) VALUES (@firstname, @lastname, @phone)";

            using (SQLiteCommand command = new SQLiteCommand(sqlCreate, _connection))
            {
                // Daftarkan parameter
                command.Parameters.AddWithValue("@firstname", client.Firstname);
                command.Parameters.AddWithValue("@lastname", client.Lastname);
                command.Parameters.AddWithValue("@phone", client.Phone);

                // Uji coba koneksikan
                try
                {
                    // Pastikan koneksi terbuka
                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }

                    // Eksekusi query
                    result = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Logging error
                    Debug.Print($"Create Error: {ex.Message}");
                }
                finally
                {
                    // Pastikan koneksi tertutup
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
            return result;
        }
        public int UpdateClient(ClientEntity client)
        {
            int result = 0;

            // Query Update ClientEntity
            string sqlUpdate = @"UPDATE client 
                         SET firstname = @firstname, 
                             lastname = @lastname, 
                             phone = @phone 
                         WHERE id_client = @id_client";

            using (SQLiteCommand command = new SQLiteCommand(sqlUpdate, _connection))
            {
                // Daftarkan parameter
                command.Parameters.AddWithValue("@firstname", client.Firstname);
                command.Parameters.AddWithValue("@lastname", client.Lastname);
                command.Parameters.AddWithValue("@phone", client.Phone);
                command.Parameters.AddWithValue("@id_client", client.Id);

                try
                {
                    // Pastikan koneksi terbuka
                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }

                    // Eksekusi query
                    result = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Logging error
                    Debug.Print($"Update Error: {ex.Message}");
                }
                finally
                {
                    // Pastikan koneksi tertutup
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }

            return result;
        }
        public int DeleteClient(ClientEntity client)
        {
            int result = 0;

            // Query Delete ClientEntity
            string sqlDelete = @"DELETE FROM client WHERE id_client = @id_client";

            using (SQLiteCommand command = new SQLiteCommand(sqlDelete, _connection))
            {
                // Daftarkan parameter
                command.Parameters.AddWithValue("@id_client", client.Id);

                // Uji coba koneksikan
                try
                {
                    // Pastikan koneksi terbuka
                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }

                    // Eksekusi query
                    result = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Logging error

                    Debug.Print($"Delete Error: {ex.Message}");
                }
                finally
                {
                    // Pastikan koneksi tertutup
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
            return result;
        }
        public ClientEntity GetClientById(ClientEntity client)
        {
            client = null;
            string sqlQuery = @"SELECT id_client, firstname, lastname, phone FROM client WHERE id_client = @id_client";

            using (SQLiteCommand command = new SQLiteCommand(sqlQuery, _connection))
            {
                command.Parameters.AddWithValue("@id_client", client.Id);

                try
                {
                    if (_connection.State == ConnectionState.Closed)
                        _connection.Open();

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            client = new ClientEntity
                            {
                                Id = reader.GetString(0),
                                Firstname = reader.GetString(1),
                                Lastname = reader.GetString(2),
                                Phone = reader.GetInt32(3)
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Get Client Error", ex.Message);
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                        _connection.Close();
                }
            }

            return client;
        }
        public List<ClientEntity> GetAllClients()
        {
            List<ClientEntity> clients = new List<ClientEntity>();

            string sqlQuery = @"SELECT id, firstname, lastname, phone FROM client";

            using (SQLiteCommand command = new SQLiteCommand(sqlQuery, _connection))
            {
                try
                {
                    if (_connection.State == ConnectionState.Closed)
                        _connection.Open();

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clients.Add(new ClientEntity
                            {
                                Id = reader.GetString(0),
                                Firstname = reader.GetString(1),
                                Lastname = reader.GetString(2),
                                Phone = reader.GetInt32(3)
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Get All Clients Error", ex.Message);
                }
                finally
                {
                    if (_connection.State == ConnectionState.Open)
                        _connection.Close();
                }
            }

            return clients;
        }
    }
}

