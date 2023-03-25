namespace NewsPage.Models
{
    public class Image
    {
        public int ImageId { get; set; } // Идентификатор изображения
        public int PostId { get; set; } // Идентификатор поста
        public string Path { get; set; } = string.Empty; // Путь к изображению
        public Post? Post { get; set; } // Ссылка на пост, к которому относится изображение
    }
}
