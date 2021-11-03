using System.Collections.Generic;
using System;
using A10_EF_Blogs_and_Posts_Assignment.Models;
using System.Linq;

namespace A10_EF_Blogs_and_Posts_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            bool run=true;
            Console.WriteLine("Welcome to the Blogs and Posts Database");
            while(run!=false){
                Console.WriteLine("Please select an option");
                string choice;
                try{
                    Console.WriteLine("1. Create a new blog, 2. Create a new post, 3. View all blogs, 4. View all posts, 5 to Exit: ");
                    choice = Console.ReadLine();
                }
                catch(Exception e){
                    Console.WriteLine("Please enter a valid option");
                    Console.WriteLine("1. Create a new blog, 2. Create a new post, 3. View all blogs, 4. View all posts, 5 to Exit: ");
                    choice = Console.ReadLine();
                }
                if(choice=="1"){
                    AddBlog();
                }
                else if(choice=="2"){
                    AddPost();
                }
                else if(choice=="3"){
                    DisplayBlogs();
                }
                else if(choice=="4"){
                    DisplayPosts();
                }
                else if(choice=="5"){
                    run = false;
                }
            }
            System.Console.WriteLine("Goodbye!");
        }
        public static void AddBlog(){
            string blogName;
            try{
                System.Console.WriteLine("Enter Your Blog Name: ");
                blogName = Console.ReadLine();
            }
            catch(Exception){
                System.Console.WriteLine("Invalid Blog Name");
                System.Console.WriteLine("Enter Your Blog Name: ");
                blogName = Console.ReadLine();
            }
            var blog = new Blog { Name = blogName };
            using (var db = new BlogContext()){
                db.Add(blog);
                db.SaveChanges();
            }
        }
        public static void DisplayBlogs(){
            using (var db = new BlogContext()){
                Console.WriteLine("Blogs: ");
                foreach(var blog in db.Blogs){
                    System.Console.WriteLine($"{blog.BlogId}: {blog.Name}");
                }
            }
        }
        public static void AddPost(){
            using (var db = new BlogContext()){
                List<Blog> blogs = db.Blogs.ToList();
                int blogId;
                try{
                    System.Console.WriteLine($"Enter Blog ID Between 1-{blogs.Count}: ");
                    blogId = Convert.ToInt32(Console.ReadLine());
                }
                catch(Exception){
                    System.Console.WriteLine("Invalid Blog ID");
                    System.Console.WriteLine($"Enter Blog ID Between 1-{blogs.Count}: ");
                    blogId = Convert.ToInt32(Console.ReadLine());
                }
                string postTitle;
                try{
                    System.Console.WriteLine("Enter Your Post Title: ");
                    postTitle = Console.ReadLine();
                }
                catch(Exception){
                    System.Console.WriteLine("Invalid Post Title");
                    System.Console.WriteLine("Enter Your Post Title: ");
                    postTitle = Console.ReadLine();
                }
                string postContent;
                try{
                    System.Console.WriteLine("Enter Your Post Content: ");
                    postContent = Console.ReadLine();
                }
                catch(Exception){
                    System.Console.WriteLine("Invalid Post Content");
                    System.Console.WriteLine("Enter Your Post Content: ");
                    postContent = Console.ReadLine();
                }
                var post = new Post { Title = postTitle, Content = postContent, BlogId = blogId };
                db.Add(post);
                db.SaveChanges();
            }
        }
        public static void DisplayPosts(){
            int blogId;
            using (var db = new BlogContext()){
                List<Blog> blogs = db.Blogs.ToList();
                try{
                    System.Console.WriteLine($"Enter Blog ID Between 1-{blogs.Count}: ");
                    blogId = Convert.ToInt32(Console.ReadLine());
                }
                catch(Exception){
                    System.Console.WriteLine("Invalid Blog ID");
                    System.Console.WriteLine($"Enter Blog ID Between 1-{blogs.Count}: ");
                    blogId = Convert.ToInt32(Console.ReadLine());
                }
                
                int postAmount=0;
                foreach(var post in db.Posts.Where(p => p.BlogId == blogId)){
                    System.Console.WriteLine($"{post.PostId}: {post.Title}: {post.Content}");
                    postAmount++;
                }
                System.Console.WriteLine($"There are {postAmount} posts in this blog.");
                }
            }
        }
    }