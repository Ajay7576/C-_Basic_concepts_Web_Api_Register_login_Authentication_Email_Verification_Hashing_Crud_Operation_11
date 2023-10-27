using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace login_register_Encryption_Hashing__Verification_11.Models
{
    public class City
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
        public int StateId { get; set; }
        [ForeignKey("StateId")]
        public State state { get; set; }

    }
}
