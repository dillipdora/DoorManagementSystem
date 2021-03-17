using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoorManagementService
{
    [Table("OpenStatusMaster")]
    public class OpenStatusMaster
    {
        [Key, Column("Id", TypeName = "integer")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Type
        [Column("Status", TypeName = "text"), MaxLength(128), Required]
        /// </summary>
        public string Status { get; set; }

    }
}
