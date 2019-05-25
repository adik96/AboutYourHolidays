using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AboutYourHolidays.Models;
using AboutYourHolidays.Repositories;
using AboutYourHolidays.ViewModels.PostViewModels;

namespace AboutYourHolidays.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        private PostRepository _postRepository = null;
 

        public PostController()
        {
            _postRepository = new PostRepository(_context);
        }
        public ActionResult Index(string text = null)
        {
            var posts = _postRepository.GetAll();//_context.Post.Include(p => p.User);
            if (text != null)
                posts = posts.Where(x => x.Tilte.Contains(text) || x.Description.Contains(text));

            List<PostDetailsModel> list = new List<PostDetailsModel>();
            foreach (var el in posts.ToList())
                list.Add((PostDetailsModel)el);

            return View(list);
        }

        // GET: Post/Details/5
        public ActionResult Details(int? id)
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

        // GET: Post/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tilte,Description,Country,City,ImageUrl,UserId,CreatedOn,LastUpdatedOn")] Post post)
        {
            if (ModelState.IsValid)
            {
                //_context.Post.Add(post);
                //_context.SaveChanges();
                var aa = _postRepository.Add(post);
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(_context.Users, "Id", "Email", post.UserId);
            return View(post);
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
    }
}
