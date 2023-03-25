using System.ComponentModel.DataAnnotations;

namespace NewsPage.Models
{
    public class Theme
    {
        [Key] public int ThemeId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
