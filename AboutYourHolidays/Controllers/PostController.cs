using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AboutYourHolidays.Models;
using AboutYourHolidays.Repositories;
using AboutYourHolidays.ViewModels.PostViewModels;
using Microsoft.AspNet.Identity;

namespace AboutYourHolidays.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        private PostRepository _postRepository = null;
        private CommentRepository _commentRepository = null;
        private UserRepository _userRepository = null;
        public PostController()
        {
            _postRepository = new PostRepository(_context);
            _commentRepository = new CommentRepository(_context);
            _userRepository = new UserRepository(_context);
        }
        public ActionResult Index(string text = null)
        {
            var posts = _postRepository.GetAll();//_context.Post.Include(p => p.User);

            if (text != null)
                posts = posts.Where(x => x.Tilte.Contains(text) || x.Description.Contains(text));

            return View(prepareResponsePosts(posts));
        }

        public ActionResult MyPosts(string text = null)
        {
            var postsL = _postRepository.GetAll().ToList();//_context.Post.Include(p => p.User);
            var posts = postsL.Where(x => User.Identity.GetUserId() == x.UserId).AsQueryable();
            if (text != null)
                posts = posts.Where(x => x.Tilte.Contains(text) || x.Description.Contains(text));

            return View(prepareResponsePosts(posts));
        }

        // GET: Post/Details/5
        public ActionResult Details(int id)
        {
            //Post model = new Post();
            var postmodel = (PostDetailsModel)_postRepository.Get(id);
            string fulPathName = ConfigurationManager.AppSettings["UpladPath"];

            List<string> fullPathToPostFolder= Directory.GetFiles(Server.MapPath(fulPathName) + postmodel.Id).ToList();
            for(int z=0;z<fullPathToPostFolder.Count();z++)
            {
                fullPathToPostFolder[z]= fulPathName+ postmodel.Id.ToString()+'/'+ Path.GetFileName(fullPathToPostFolder[z]);
            }
            User user = new User();
            postmodel.Urls = fullPathToPostFolder;
            if(User.Identity.GetUserId() != null)
            {
                user = _userRepository.Get(User.Identity.GetUserId());
                ViewBag.UserFullName = user.Name + " " + user.Surname;
            }
            else
                ViewBag.UserFullName = null;
            return View(postmodel);
        }

        // GET: Post/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(_context.Users, "Id", "Email");
            PostAddViewModel postmodel = new PostAddViewModel()
            {

            };
            return View(postmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostAddViewModel addmodel, HttpPostedFileBase[] ImageFile)
        {
            if (addmodel.City != null && addmodel.Country != null && addmodel.Description != null && addmodel.Title != null && ImageFile!=null)
            {
                Post post = PostAddViewModel.ToDB(addmodel);
                post.CreatedOn = DateTime.Now;
                post.UserId = User.Identity.GetUserId();
                _postRepository.Add(post);
                string fulPathName = ConfigurationManager.AppSettings["UpladPath"];
                string uploadFullPath = Server.MapPath(fulPathName);
                string endString = post.Id.ToString();
                Directory.CreateDirectory(uploadFullPath + endString);
                int i = 0;
                foreach (var pp in ImageFile)
                {
                    string extension = ".jpg";
                    string fileName = i + extension;
                    addmodel.ImageUrl = uploadFullPath + endString + "/" + fileName;
                    //sciezka zapisu zdjecia
                    fileName = Path.Combine(Server.MapPath("/Uploads/" + endString), fileName);
                    pp.SaveAs(fileName);

                    i++;
                }
                return RedirectToAction("Index", "Home", null);
            }
            else{
                ModelState.AddModelError(
                   string.Empty,
                   "Wprowadzone dane są nieprawidłowe.");
                return View(addmodel);
            }
        }

        [Authorize]
        public ActionResult AddComment(int id)
        {
            //ViewBag.UserId = new SelectList(_context.Users, "Id", "Email");
            //PostAddViewModel postmodel = new PostAddViewModel()
            //{

            //};
            return RedirectToAction("Details", "Post", new { id = id });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(int id, string text)
        {
            Comment dbComment = new Comment();
            dbComment.CreatedOn = DateTime.Now;
            dbComment.LastUpdatedOn = DateTime.Now;
            dbComment.UserId = User.Identity.GetUserId();
            dbComment.PostId = id;
            dbComment.Text = text;
            
            _commentRepository.Add(dbComment);
            _context.SaveChanges();
            return RedirectToAction("Details", "Post", new { id = id });
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = _postRepository.Get(id.Value);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(_context.Users, "Id", "Email", post.UserId);
            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tilte,Description,Country,City,ImageUrl,UserId,CreatedOn,LastUpdatedOn")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(post).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(_context.Users, "Id", "Email", post.UserId);
            return View(post);
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = _postRepository.Get(id.Value);//_context.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Post post = _postRepository.Get(id);
            _postRepository.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private List<PostDetailsModel> prepareResponsePosts(IQueryable<Post> posts)
        {
            List<PostDetailsModel> list = new List<PostDetailsModel>();

            string fulPathName = ConfigurationManager.AppSettings["UpladPath"];
            foreach (var el in posts.ToList())
            {
                string uploadFullPath = Server.MapPath(fulPathName);
                var fullPathToPostFolder = Directory.GetFiles(Server.MapPath(fulPathName) + el.Id.ToString());
                var firstPhotoName = Path.GetFileName(fullPathToPostFolder.FirstOrDefault());
                el.ImageUrl = fulPathName + el.Id.ToString() + '/' + firstPhotoName;
                list.Add((PostDetailsModel)el);
            }

            foreach (var item in list)
            {
                var dlugosc = item.Description.Length;
                if (dlugosc >= 20)
                    item.ShortDescription = item.Description.Substring(0, 20);
                else
                    item.ShortDescription = item.Description.Substring(0, dlugosc);
            }

            return list;
        }
    }
}
