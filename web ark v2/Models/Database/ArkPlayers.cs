using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArkWeb
{
    public class ArkPlayers
    {

        [Key]
        public int Id { get; set; }
        public string User { get; set; }
        public int? ARKCoin { get; set; }
        public int? GameID { get; set; }

    }
}
