using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstrelaDaMorte.Extensions
{
    public static class ValueExtensions 
    {
        public static T GetValueorDefault<T>(this SqlDataReader reader, string campo)
        {
            try
            {
                return (T)reader(campo);
            }
            catch
            {
                return default(T);
            }
        }
        
    }
}
