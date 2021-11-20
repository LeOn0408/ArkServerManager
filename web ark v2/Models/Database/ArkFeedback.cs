using System;

namespace ArkWeb.Models.Database
{
    public class ArkFeedback
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public DateTime DateFeedback { get; set; }
        public int FeebackType { get; set; }
        public bool Resolved { get; set; }

        public ArkFeedback()
        {
            DateFeedback = DateTime.Now;
            Resolved = false;
        }


    }
}
