using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistem_Manajemen_Hotel.Model.Entity;
using Sistem_Manajemen_Hotel.Model.Context;
using Sistem_Manajemen_Hotel.Model.Repository;

namespace Sistem_Manajemen_Hotel.Controller
{
    public class ClientController
    {
        private ClientRepository _clientRepository;

        public int Create (ClientEntity client)
        {
            int result = 0;
            if (string.IsNullOrEmpty(client.Id))
            {
                MessageBox.Show("Id client harus diisi !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }
            if (string.IsNullOrEmpty(client.Firstname))
            {
                MessageBox.Show("Nama client harus diisi !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }
            if (string.IsNullOrEmpty(client.Lastname))
            {
                MessageBox.Show("Lastname client harus diisi !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }
            if (client.Phone == null)
            {
                MessageBox.Show("Phone No harus diisi !!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }
            
            using (DbContext context = new DbContext())
            {
                // membuat objek class repository
                _clientRepository = new ClientRepository(context);
                result = _clientRepository.CreateClient(client);
            }
            return result;
        }
        public List<ClientEntity> ReadAll()
        {   
            // membuat objek collection
            List<ClientEntity> list = new List<ClientEntity>();
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _clientRepository = new ClientRepository(context);
                list = _clientRepository.GetAllClients();
            }
            return list;
        }
    }
}
