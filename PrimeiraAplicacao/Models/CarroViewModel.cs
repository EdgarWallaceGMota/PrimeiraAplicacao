using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PrimeiraAplicacao.Models
{
    public class Carro
    {
        public int id { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string cor { get; set; }
        public int motor_id { get; set; }
    }
}
