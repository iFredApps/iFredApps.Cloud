using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iFredCloud.Core.Models
{
   public class User
   {
      public int id { get; set; }
      [Required]
      [StringLength(60)]
      public string? username { get; set; }
      [Required]
      [StringLength(120)]
      public string? password { get; set; }
      [Required]
      [StringLength(150)]
      public string? email { get; set; }
   }
}
