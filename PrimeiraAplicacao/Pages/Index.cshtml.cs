using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace PrimeiraAplicacao.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMemoryCache _cache;

        public IndexModel(ILogger<IndexModel> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public async Task OnGet()
        {
            var url = "http://apiintranet.kryptonbpo.com.br/test-dev/exercise-1";

            using (var http = new HttpClient())
            {
                var response = await http.GetStringAsync(url);
                var posts = JsonConvert.DeserializeObject<Models.ArrayCarros>(response);

                if (_cache.Get("carros") != null)
                {
                    var stringPosts = JsonConvert.SerializeObject(_cache.Get("carros"));
                    posts = JsonConvert.DeserializeObject<Models.ArrayCarros>(stringPosts);
                }                              
               
                string imprimir = "";


                imprimir += "<section class='container'>";
                for (int i = 0; i < posts.carros.Count; i++)
                {
                        
                    imprimir += "<div class='columns features'>";

                    for (int p = 0; p < 3 && i < posts.carros.Count; p++)
                    {
                        imprimir += "    <div class='column is-4'>" + "\r\n";
                        imprimir += "        <div class='card'>" + "\r\n";
                        imprimir += "            <div class='card-content has-text-centered'>" + "\r\n";
                        imprimir += "                <div class='content'>" + "\r\n";
                        imprimir += "                <h4 class='has-text-centered'>" + posts.carros[i].modelo + "</h4>" + "\r\n";
                        imprimir += "                    <ul>" + "\r\n";

                        imprimir += "id: " + posts.carros[i].id + "<br>\r\n";
                        imprimir += "marca: " + posts.carros[i].marca + "<br>\r\n";
                        imprimir += "cor: " + posts.carros[i].cor + "<br>\r\n";

                        for (int x = 0; x < posts.motores.Count; x++)
                        {
                            if (posts.carros[i].motor_id == posts.motores[x].id)
                            {
                                imprimir += "                posicionamento cilindros: " + posts.motores[x].posicionamento_cilindros + "<br>\r\n";
                                imprimir += "                cilindros: " + posts.motores[x].cilindros + "<br>\r\n";
                                imprimir += "                litragem: " + posts.motores[x].litragem + "<br>\r\n";
                                imprimir += "                observacao: " + posts.motores[x].observacao + "<br>\r\n";
                            }
                        }

                        imprimir += "                    </ul>" + "\r\n";
                        imprimir += "                </div>" + "\r\n";
                        imprimir += "            </div>" + "\r\n";
                        imprimir += "        </div>" + "\r\n";
                        imprimir += "    </div>" + "\r\n";

                        i++;
                    }

                    i--;
                    imprimir += "</div>" + "\r\n";
                   
                }

                imprimir += "</section>" + "\r\n";

                _cache.Set("carros", posts);
                HttpContext.Session.SetString("Carros", imprimir);
            }
        }
    }
}
