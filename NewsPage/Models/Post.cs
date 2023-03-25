using System.ComponentModel.DataAnnotations;

namespace NewsPage.Models;

public class Post
{
    [Key] public int PostId { get; set; } // № поста
    public string Title { get; set; } = string.Empty; // Заголовок поста
    public string MainImage { get; set; } = string.Empty; // Главное изображение поста
    public string Text { get; set; } = string.Empty; // Текст поста
    public List<Image>? Images{ get; set; } // Изображения внутри текста поста (от 0 до 20 штук)
   
    public int ThemeId { get; set; }
    public Theme? Theme { get; set; } // Тема поста





}
