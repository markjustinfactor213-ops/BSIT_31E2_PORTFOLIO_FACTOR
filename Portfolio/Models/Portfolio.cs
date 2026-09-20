namespace Portfolio.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string GitHubUrl { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public List<Comment> Comments { get; set; } = new();
    }

    public class Comment
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class LoginViewModel
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}