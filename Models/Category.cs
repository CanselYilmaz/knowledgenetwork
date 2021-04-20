using System;

namespace knowledgenetwork.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string  Title { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime UpdateAt { get; set; }= DateTime.Now;
        
    }
}