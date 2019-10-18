using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.Models.ViewModels;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Varun" };
            /*
             * var viewResult = new ViewResult();
             * viewResult.ViewData.Model;
             * The movie object that we have passed in "return View(movie)" will be assign to the Model property in 
             * viewResult.ViewData.Model, "View" in "return View(movie)" will take care of it.
             */
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer1" },
                new Customer { Name = "Customer2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            //If we do not explicitly specify the name of the view to be returned (here "New"), MVC will look for the view called Edit.
            //Model behind this view(New) is "NewCustomerViewModel"
            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }

        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //movies
        /*public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;
            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";
            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }*/

        public ViewResult Index()
        {
           // var movies = _context.Movies.Include(m => m.Genre).ToList();
            //return View(movies);

            if (User.IsInRole(RoleName.canManageMovies))
                return View("List");

            return View("ReadOnlyList");
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }

        [Authorize(Roles = RoleName.canManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            //It will render a form for new movie. As there are 2 constructors in the MovieFormViewModel, the one with 0 args sets the id = 0. 
            //Hence no args are passed in the following when the new model object is being created which will render the heading New Movie in the form.
            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };
            return View("movieform", viewModel);
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}:range{1,12})}")]
        public ActionResult ByReleaseYear(int year, int month)
        {
            return Content(year + "/" + month);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            //If the movie is new and doesn't exist in the database it's id will be 0
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                //Adding the changes to the context object, the changes will be in the memory & not in the database
                _context.Movies.Add(movie);
            }
            else
            {
                //Here we're using Single method and not SingleOrDefault because we only want the movies that are present in the database
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();

            return RedirectToAction("index", "Movies");
        }
    }
}