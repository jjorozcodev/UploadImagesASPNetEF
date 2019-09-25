//using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyImageManager.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }

        [Column(TypeName = "varbinary(MAX)")]
        //[MaxLength]
        public byte[] Data { get; set; }
    }
}