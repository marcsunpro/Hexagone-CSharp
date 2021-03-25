using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shout.Models
{
    public class Shoutt
    {
        //J'ai du l'appeller Shoutt car j'ai nommé mon projet Shout et j'ai eu des erreurs par la suite
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DatePublication { get; set; }
        public User User { get; set; }
    }
}
