using System.Collections.Generic;
namespace EstrelaDaMorte
{
    public class Piloto
    {
        public int IdPiloto { get; set;  }
        public int Nome { get; set; }
        public int AnoNascimento { get; set; }
        public int IdPlaneta { get; set; }

        public Planeta Planeta{ get; set; }
        public List<Nave> Naves { get; set; }
    }
    
}
