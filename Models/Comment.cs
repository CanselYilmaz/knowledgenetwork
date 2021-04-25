using System;

namespace knowledgenetwork.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public Blog Blog { get; set; }
        public int BlogId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}