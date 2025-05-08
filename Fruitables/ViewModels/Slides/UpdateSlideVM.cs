using System.ComponentModel.DataAnnotations.Schema;

namespace Fruitables.ViewModels
{
    public class UpdateSlideVM
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public int Order { get; set; }
        
        public IFormFile? Photo { get; set; }
    }
}
