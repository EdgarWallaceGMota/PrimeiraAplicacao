using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeiraAplicacao.Models
{
    public class Motor
    {
        public int id { get; set; }
        public string posicionamento_cilindros { get; set; }
        public int cilindros { get; set; }
        public int litragem { get; set; }
        public string observacao { get; set; }

    }
}
