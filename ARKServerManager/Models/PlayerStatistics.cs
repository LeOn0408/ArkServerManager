using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARKServerManager.Models
{
    public class PlayerStatistics
    {
        [Required]
        public int Id {  get; set; }
        public string PlayerId {  get; set; }
        public int ServerId { get; set; }
        public string PlayerName {  get; set; }
        public DateTime LastGame {  get; set; }
        public TimeSpan TimeGame {  get; set; }

        
    }
}
