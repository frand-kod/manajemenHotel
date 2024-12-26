using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistem_Manajemen_Hotel.Model.Entity;
using Sistem_Manajemen_Hotel.Model.Repository;
using Sistem_Manajemen_Hotel.Model.Context;
using System.Data.Entity;
using System.Data.SQLite;
using Sistem_Manajemen_Hotel.Model.Context;

using System.Windows.Forms;

namespace Sistem_Manajemen_Hotel.Controler
{
    public class LoginController
    {
        private LoginRepository _LoginRepository;

        public LoginController(LoginRepository loginRepository)
        {
            _LoginRepository = loginRepository;
        }

        public void CreateLogin(string username, string password)
        {
            LoginEntity login_Entity = new LoginEntity
            {
                Username = username,
                Password = password
            };

            _LoginRepository.CreateUserLogin(login_Entity);
        }

        /*public List<LoginController> GetAllMasuk()
        {
            return _LoginRepository.ReadAll();
        }*/

        public void UpdateMasuk(string username, string newPassword)
        {
            LoginEntity login = new LoginEntity
            {
                Username = username,
                Password = newPassword
            };

            _LoginRepository.Update(login);
        }

        public void DeleteMasuk(string username)
        {
            _LoginRepository.Delete(username);
        }

        /*public bool AuthUserLogin(LoginEntity user)
        {
            try
            {
                if (_LoginRepository.AuthUserLogin(user))
                {
                    MessageBox.Show("Login Berhasil!!!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("Login Gagal!!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }*/

        /*public List<LoginEntity> ReadByNama(string nama)
        {
            // membuat objek collection
            List<LoginEntity> list = new List<LoginEntity>();
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _LoginRepository = new LoginRepository(context);
                list = _LoginRepository.ReadByNama();
            }
            return list;
        }*/
    }
    /*public bool AuthUserLogin(LoginEntity user)
    {

        if (_LoginRepository.AuthUserLogin(user)) {

            MessageBox.Show("Login Gagal!!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return true;
        }
        else
        {
            MessageBox.Show("Login Berhasil!!!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }
    }*/
}