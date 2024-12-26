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
using System.Drawing;

namespace Sistem_Manajemen_Hotel.Model.Repository
{
    public class RoomRepository
    {
        private SQLiteConnection _connection;

        //consruktor
        public RoomRepository(DbContext context)
        {
            _connection = context.Conn;
        }
        public int CreateRoom(RoomEntity room)
        {
            int result = 0;

            // Query Create RoomEntitiy
            string sqlCreate = @"INSERT INTO room (room_number,type_room, avaliabity) VALUES (@room_number,@type_room, @avaliabity)";

            using (SQLiteCommand command = new SQLiteCommand(sqlCreate, _connection))
            {
                // Daftarkan parameter
                command.Parameters.AddWithValue("@room_number", room.RoomNumber);
                command.Parameters.AddWithValue("@type_room", room.TypeRoom);
                command.Parameters.AddWithValue("@avaliabity", room.Availability);

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

        public List<RoomEntity> ReadAll()
        {
            List<RoomEntity> list = new List<RoomEntity>();
            try
            {
                string sql = @"SELECT SupplierID, NamaSupplier, AlamatSupplier, KontakSupplier FROM Supplier";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RoomEntity roomEntity = new RoomEntity();
                            roomEntity.IdRoom = int.Parse(reader["IdRoom"].ToString());
                            roomEntity.RoomNumber = int.Parse(reader["RoomNumber"].ToString());
                            roomEntity.Availability = reader["Availability"].ToString();
                            roomEntity.TypeRoom = reader["TypeRoom"].ToString();

                            list.Add(roomEntity);
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

        public List<RoomEntity> ReadByNama(string nama)
        {
            List<RoomEntity> list = new List<RoomEntity>();
            try
            {

                string sql = @"SELECT SupplierID, NamaSupplier, AlamatSupplier, KontakSupplier FROM Supplier";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, _connection))
                {
                    cmd.Parameters.AddWithValue("@nama", string.Format("%{0}%", nama));

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            while (reader.Read())
                            {
                                RoomEntity roomEntity = new RoomEntity();
                                roomEntity.IdRoom = int.Parse(reader["SupplierID"].ToString());
                                roomEntity.RoomNumber = int.Parse(reader["NamaSupplier"].ToString());
                                roomEntity.Availability = reader["AlamatSupplier"].ToString();
                                roomEntity.TypeRoom = reader["KontakSupplier"].ToString();

                                list.Add(roomEntity);
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
        public int UpdateRoom(RoomEntity room)
        {
            int result = 0;

            // Query Update RoomEntitiy
            string sqlUpdate = @"UPDATE room 
                         SET room_number = @room_number, 
                             type_room = @type_room, 
                             avaliabity = @avaliabity
                         WHERE id_room = @id_room";

            using (SQLiteCommand command = new SQLiteCommand(sqlUpdate, _connection))
            {
                // Daftarkan parameter
                command.Parameters.AddWithValue("@room_number", room.RoomNumber);
                command.Parameters.AddWithValue("@type_room", room.TypeRoom);
                command.Parameters.AddWithValue("@avaliabity", room.Availability);
                command.Parameters.AddWithValue("@id_room", room.IdRoom);

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
        public int DeleteRoom(RoomEntity room)
        {
            int result = 0;

            // Query Delete RoomEntitiy
            string sqlDelete = @"DELETE FROM room WHERE id_room = @id_room";

            using (SQLiteCommand command = new SQLiteCommand(sqlDelete, _connection))
            {
                // Daftarkan parameter
                command.Parameters.AddWithValue("@id_room", room.IdRoom);

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
    }
}