using System;

namespace knowledgenetwork.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateAt  { get; set; }= DateTime.Now;
        public  string Content { get; set; }
        public Comment Comment { get; set; }
        public int  CommentId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public int ReadCount { get; set; }
        public bool IsPublish { get; set; }

        public DateTime UpdateAt { get; set; }= DateTime.Now;
    }
}