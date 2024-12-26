using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistem_Manajemen_Hotel.Model.Entity;
using Sistem_Manajemen_Hotel.Model.Repository;
using Sistem_Manajemen_Hotel.Model.Context;
using System.Windows.Forms;

namespace Sistem_Manajemen_Hotel.Controler
{
    public class RoomController
    {
        private RoomRepository _RoomRepository;

        public int CreateRoom(RoomEntity roomEntity)
        {
            int result = 0;
            if (roomEntity.IdRoom == null)
            {
                MessageBox.Show("ID Room harus diisi !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }
            if (roomEntity.RoomNumber == null)
            {
                MessageBox.Show("Room Number harus diisi !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }
            if (string.IsNullOrEmpty(roomEntity.Availability))
            {
                MessageBox.Show("Availability Room harus diisi !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }
            if (string.IsNullOrEmpty(roomEntity.TypeRoom))
            {
                MessageBox.Show("Type Room harus diisi !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }
            using (DbContext context = new DbContext())
            {
                // membuat objek class repository
                _RoomRepository = new RoomRepository(context);
                result = _RoomRepository.CreateRoom(roomEntity);
            }
            return result;
        }
        public List<RoomEntity> ReadByNama(string room_Entity)
        {
            // membuat objek collection
            List<RoomEntity> list = new List<RoomEntity>();
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _RoomRepository = new RoomRepository(context);
                list = _RoomRepository.ReadByNama(room_Entity);
            }
            return list;
        }
        public List<RoomEntity> ReadAll()
        {
            // membuat objek collection
            List<RoomEntity> list = new List<RoomEntity>();
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _RoomRepository = new RoomRepository(context);
                list = _RoomRepository.ReadAll();
            }
            return list;
        }

        public int UpdateRoom(RoomEntity supplier)
        {
            int result = 0;

            using (DbContext context = new DbContext())
            {
                // membuat objek class repository
                _RoomRepository = new RoomRepository(context);
                result = _RoomRepository.UpdateRoom(supplier);
            }

            return result;
        }

        public int DeleteRoom(RoomEntity supplier)
        {
            int result = 0;

            using (DbContext context = new DbContext())
            {
                // membuat objek class repository
                _RoomRepository = new RoomRepository(context);
                result = _RoomRepository.DeleteRoom(supplier);
            }

            return result;
        }
    }
}
