using System.ComponentModel.DataAnnotations.Schema;

namespace Fruitables.ViewModels
{
    public class CreateSlideVM
    {
      
        public string Title { get; set; }
      
        
     
        public int Order { get; set; }
        
        public IFormFile Photo { get; set; }
    }
}
