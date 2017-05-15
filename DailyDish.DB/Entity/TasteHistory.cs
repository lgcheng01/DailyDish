using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyDish.DB.Entity
{
   public class TasteHistory
    {
        public Guid Id { get; set; }
        public string OpenId { get; set; }
        public string UserName { get; set; }
        public string LikeFlavor { get; set; }
        public string DisLikeFlavor { get; set; }
    }
}
