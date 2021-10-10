using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstrelaDaMorte.Dao
{
    public abstract class DaoBase: IDisposable
    {
        protected readonly SqlConnection con;

        protected DaoBase()
        {
            con = new SqlConnection(@"Data Source=DESKTOP-4A7HK8C\SQLEXPRESS;Initial Catalog=EstrelaDaMorte;Integrated Security=Treu; Connect Timeout=30");
        }
        protected async Task Insert(string comando)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(comando, con);
            await cmd.ExecuteNonQueryAsync();
            con.Close();

        }
        protected async Task Select(string comando, Action<SqlDataReader> tratamentoLeitura)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(comando, con);
            SqlDataReader dr = await cmd.ExecuteReaderAsync();
            tratamentoLeitura(dr);
            con.Close();

        }
    }
}
