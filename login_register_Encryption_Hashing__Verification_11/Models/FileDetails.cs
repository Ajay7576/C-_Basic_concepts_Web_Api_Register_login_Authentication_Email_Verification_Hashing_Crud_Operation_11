using Microsoft.VisualBasic.FileIO;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace login_register_Encryption_Hashing__Verification_11.Models
{
    public class FileDetails
    {
       
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ID { get; set; }
            public string FileName { get; set; }
            public byte[] FileData { get; set; }
      
    }
}
