using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARKServerManager.Models.Ark
{
    public class ArkLogRow
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int RecordID { get; set; }
        public string RecordRow { get; set; }
        public int ServerID { get; set; }
    }
}
