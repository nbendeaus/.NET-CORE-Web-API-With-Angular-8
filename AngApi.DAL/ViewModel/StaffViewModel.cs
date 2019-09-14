using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AngApi.DAL.ViewModel
{
    public class StaffViewModel
    {
        [Key]
        public int Id { get; set; }
      //  [Required]
       // public int StaffId { get; set; }
        [Required]
        public int UnitId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string UnitName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string DepartmentName { get; set; }
    }
}
