using System.ComponentModel.DataAnnotations.Schema;

namespace Fruitables.Models
{
    
        public class Slide : BaseEntity
        {
            public string Title { get; set; }
           public string Image { get; set; }
            public int Order { get; set; }
          [NotMapped]
          public IFormFile Photo { get; set; }
    }
    
}
