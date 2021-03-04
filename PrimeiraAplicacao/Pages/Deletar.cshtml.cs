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
    public class DeletarModel : PageModel
    {
        private readonly IMemoryCache _cache;
        public DeletarModel(IMemoryCache cache)
        {
            _cache = cache;
        }
        public void OnGet()
        {

        }

        public void OnPost(int id)
        {
            var stringPosts = JsonConvert.SerializeObject(_cache.Get("carros"));
            var posts = JsonConvert.DeserializeObject<Models.ArrayCarros>(stringPosts);
            int motorID = 0;

            foreach (Models.Carro post in posts.carros)
            {
                if (post.id == id)
                {
                    posts.carros.Remove(post);
                    break;
                }
            }

            foreach (Models.Motor postMotor in posts.motores)
            {
                if (postMotor.id == motorID)
                {
                    posts.motores.Remove(postMotor);
                    break;
                }
            }

            _cache.Set("carros", posts);
        }
    }
}
