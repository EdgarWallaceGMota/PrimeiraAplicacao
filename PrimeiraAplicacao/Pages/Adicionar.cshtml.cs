using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace PrimeiraAplicacao.Pages
{
    public class AdicionarModel : PageModel
    {
        private readonly IMemoryCache _cache;
        public AdicionarModel(IMemoryCache cache)
        {
            _cache = cache;
        }
        public void OnGet()
        {

        }

        public void OnPost(string marca, string modelo, string cor, string posicionamento_cilindros, int cilindros, int litragem, string observacao)
        {
            var stringPosts = JsonConvert.SerializeObject(_cache.Get("carros"));
            var posts = JsonConvert.DeserializeObject<Models.ArrayCarros>(stringPosts);

            Models.Carro newCarro = new Models.Carro();
            Models.Motor newMotor = new Models.Motor();

            newCarro.id = posts.carros.Last().id + 1;
            newCarro.marca = marca;
            newCarro.modelo = modelo;
            newCarro.cor = cor;
            newCarro.motor_id = posts.motores.Count + 1;

            newMotor.id = posts.motores.Last().id + 1;
            newMotor.posicionamento_cilindros = posicionamento_cilindros;
            newMotor.cilindros = cilindros;
            newMotor.litragem = litragem;
            newMotor.observacao = observacao;


            posts.carros.Add(newCarro);
            posts.motores.Add(newMotor);

            _cache.Set("carros", posts);
        }
    }
}
