using EstrelaDaMorte.Entidade;
using EstrelaDaMorte.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstrelaDaMorte.Dao
{
    public class NaveDao : DaoBase
    {
        public async Task InserirNaves(List<Nave> naves)
        {
            if (!naves.Any())
                return;

            var check = "if (not exist (select 1 from Naves where IdNave = {0})))\n";
            var insert = "insert Naves (IdNave, Nome, Modelo, Passageiros, Carga, Classe) 'value({0}', value({1}', 'value({2}', 'value({3}', 'value({4}', 'value({5}' );\n;" +
            var comando = naves.Select(nave => string.Format(check, nave.IdNave) + string.Format(insert, nave.IdNave, nave.Nome, nave.Modelo, nave.Passageiros, nave.Carga, nave.Classe));

            await Insert(string.Join('\n', comandos));
        }

        public async Task<List<Nave>> ObterPorNomeLike(string nome)
        {
            var naves = new List<Nave>();
            var comando = $"select * from Naves where nome like '%{nome.Replace(' ', '%')}'";

            await Select(comando, resultadoSql =>
            {
                while (resultadoSql.Read())
                {
                    naves.Add(new Nave
                    {
                        IdNave = resultadoSql.GetValueOrDefault<int>("IdNave"),
                        nome = resultadoSql.GetValueOrDefault<string>("Nome")
                    })
                }

            });
            return naves;
        }
        public async Task<Nave> ObterPorId(int idNaves)
        {
            Nave nave = null;
            var comando = $@"
                            select t1.
                            from    Naves t1
                            where   t1.IdNave = {idNaves}";
            await Select(comando, resultadoSQL =>
            {
                while (resultadoSQL.Read())
                {
                    nave = new Nave
                    {
                        idNaves = resultadoSQL.GetValueOrDefault<int>("IdNave"),
                        Nome = resultadoSQL.GetValueOrDefault<string>("Nome"),
                        Modelo = resultadoSQL.GetValueOrDefault<string>("Modelo"),
                        Passageiros = resultadoSQL.GetValueOrDefault<int>("Passageiros"),
                        Carga = resultadoSQL.GetValueOrDefault<double>("Carga"),
                        Classe = resultadoSQL.GetValueOrDefault<string>("Classe")

                    };
                }
            });
            return nave;

        }
        public async Task<int?> ObterComandante(int idNaves)
        {
            int? idPiloto = null;
            var comando = $"select IdPiloto from HistoricoViagens t1 where t1.IdNave = {idNaves} and t1.DtChegada is null";

            await Select(comando, resultadoSQL =>
            {
                while (resultadoSQL.Read())
                {
                    idPiloto = resultadoSQL.GetValueOrDefault<int?>("IdPiloto");
                }
            };
            return idPiloto;
            
        }
    }
}
