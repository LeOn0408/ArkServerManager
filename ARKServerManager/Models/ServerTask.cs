using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ARKServerManager.Models
{
    public class ServerTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int Result { get; set; }
        public DateTime DateJob { get; set; }
        public bool Repeating { get; set; }
        public int ServerId { get; set; }
    }
}
