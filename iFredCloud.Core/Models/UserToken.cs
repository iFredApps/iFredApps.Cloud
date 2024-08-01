using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iFredCloud.Core.Models
{
   public class UserToken
   {
      [Required]
      public string user_id { get; set; }
      [Required]
      [StringLength(145)]
      public string jwt_token { get; set; }
      [Required]
      public DateTime expiration { get; set; }
   }
}
