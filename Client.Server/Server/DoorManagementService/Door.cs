using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoorManagementService
{
    [Table("Door")]
    public class Door
    {
        [Key, Column("Id", TypeName = "integer")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        [Column("Name", TypeName = "text"), MaxLength(128), Required]
        public string Name { get; set; }

        
        [Column("LockStatus", TypeName = "integer"), Required]
        public int LockStatus { get; set; }

        
        [Column("OpenStatus", TypeName = "integer"), Required]
        public int OpenStatus { get; set; }

        
        #region Forign Keys
        
        [ForeignKey("LockStatus")]
        public virtual LockStatusMaster LockStatusMaster { get; set; }

        [ForeignKey("OpenStatus")]
        public virtual LockStatusMaster OpenStatusMaster { get; set; }
        
        #endregion
    }
}
