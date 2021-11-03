namespace A10_EF_Blogs_and_Posts_Assignment.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }

        public virtual Blog Blog {get;set;}
    }
}