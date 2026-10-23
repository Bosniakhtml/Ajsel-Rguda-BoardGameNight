using BoardGameNight.Domain.Models;
using BoardGameNight.DomainServices;
using BoardGameNight.DomainServices.Services;
using BoardGameNight.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging.Signing;
using System.Drawing.Printing;

namespace BoardGameNight.UI.Controllers
{
    [Authorize]
    public class BoardgameNightController : Controller
    {

        private readonly IBoardgameService boardgameService;
        private readonly IBoardgameNightService boardgameNightService;
        private readonly IBoardgameNightRepository boardgameNightRepository;
        private readonly IUserService userService;
        private readonly IReviewService reviewService;
        private readonly ILogger<BoardgameNightController> logger;

        public BoardgameNightController(IBoardgameService boardgameService, IBoardgameNightService boardgameNightService, IBoardgameNightRepository boardgameNightRepository, IUserService userService, IReviewService reviewService, ILogger<BoardgameNightController> logger)
        {
            this.boardgameService = boardgameService;
            this.boardgameNightService = boardgameNightService;
            this.boardgameNightRepository = boardgameNightRepository;
            this.userService = userService;
            this.reviewService = reviewService;
            this.logger = logger;
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Overview()
        {
            return View();
        }

        public IActionResult Participate()
        {
            var identity = HttpContext.User.Identity;

            User? user = userService.getByIdentityUserName(identity!);

            if (user == null) return View("ErrorPage", "Gebruiker bestaat niet!");

            ICollection<BoardgameNight> boardgameNights = boardgameNightService.getAll().Where(b => b.Organizer.Id != user.Id && !b.Participants.Contains(user)).ToList();

            BoardgameNightListViewModel model = new()
            {
                BoardgameNights = boardgameNights
                .Select(b =>
                    new BoardgameNightViewModel
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Organizer = new UserViewModel
                        {
                            Id = b.Organizer.Id,
                            Name = b.Organizer.Name,
                            Email = b.Organizer.Email,
                            Address = b.Organizer.getAddress(),
                            BirthDate = b.Organizer.BirthDate,
                            Gender = b.Organizer.Gender

                        },
                        Address = b.getAddress(),
                        IsAdult = b.IsAdult,
                        MaxParticipants = b.MaxParticipants,
                        CurrentNumberOfParticipants = b.Participants.Count(),
                        DateTime = b.DateTime,
                        FoodAndDrinks = b.FoodAndDrinks,
                        Boardgames = b.Boardgames
                    }),
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Organise()
        {

            var identity = HttpContext.User.Identity;

            User? user = userService.getByIdentityUserName(identity!);

            if (user == null) return View("ErrorPage", "Gebruiker bestaat niet!");

            var model = new OrganiseViewModel
            {
                Organizer = new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Address = user.getAddress(),
                    BirthDate = user.BirthDate,
                    Gender = user.Gender
                },
                AvailableBoardgames = new SelectList(boardgameService.getAll(), "Id", "Name")
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Organise(OrganiseViewModel model)
        {
            var identity = HttpContext.User.Identity;

            User? organiser = userService.getByIdentityUserName(identity!);

            if (organiser == null) return View("ErrorPage", "Gebruiker bestaat niet!");

            if (model.DateTime.Date <= DateTime.Today) ModelState.AddModelError("", "Datum en tijd kan niet vandaag zijn of in het verleden liggen!");

            if (model.SelectedBoardgameIds.IsNullOrEmpty()) ModelState.AddModelError("", "Moet minstens 1 bordspel kiezen!");

            if (ModelState.IsValid)
            {

                List<Boardgame> boardgames = boardgameService
                .getAll()
                .Where(b => model.SelectedBoardgameIds.Contains(b.Id))
                .ToList();

                BoardgameNight boardgameNight = new()
                {
                    Name = model.Name,
                    Organizer = organiser,
                    HouseNumber = model.HouseNumber,
                    Street = model.Street,
                    City = model.City,
                    IsAdult = model.IsAdult,
                    MaxParticipants = model.MaxParticipants,
                    DateTime = model.DateTime,
                    FoodAndDrinks = model.FoodAndDrinks,
                    Boardgames = boardgames,
                    HasAlcoholFreeDrinks = model.HasAlcoholFreeDrinks,
                    HasLactoseFreeFood = model.HasLactoseFreeFood,
                    HasNutFreeFood = model.HasNutFreeFood,
                    HasVegetarianfood = model.HasVegetarianfood
                };

                try
                {
                    boardgameNightService.add(boardgameNight);
                }
                catch(Exception e)
                {
                    return View("ErrorPage", e.Message);
                }

                return RedirectToAction("OverviewOrganised");
            }

            model.AvailableBoardgames = new SelectList(boardgameService.getAll(), "Id", "Name");
            model.Organizer = new UserViewModel
            {
                Id = organiser.Id,
                Name = organiser.Name,
                Email = organiser.Email,
                Address = organiser.getAddress(),
                BirthDate = organiser.BirthDate,
                Gender = organiser.Gender
            };
            return View(model);
        }

        public IActionResult OverviewEnrolled()
        {

            var identity = HttpContext.User.Identity;

            User? user = userService.getByIdentityUserName(identity!);

            if (user == null) return View("ErrorPage", "Gebruiker bestaat niet!");

            ICollection<BoardgameNight> boardgameNights = boardgameNightService.getAllBoardgameNightsWithUser(user.Id);

            BoardgameNightListViewModel model = new()
            {
                BoardgameNights = boardgameNights
                .Select(b =>
                    new BoardgameNightViewModel
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Organizer = new UserViewModel
                        {
                            Id = b.Organizer.Id,
                            Name = b.Organizer.Name,
                            Email = b.Organizer.Email,
                            Address = b.Organizer.getAddress(),
                            BirthDate = b.Organizer.BirthDate,
                            Gender = b.Organizer.Gender

                        },
                        Address = b.getAddress(),
                        IsAdult = b.IsAdult,
                        MaxParticipants = b.MaxParticipants,
                        CurrentNumberOfParticipants = b.Participants.Count(),
                        DateTime = b.DateTime,
                        FoodAndDrinks = b.FoodAndDrinks,
                        Boardgames = b.Boardgames
                    }),
            };

            return View(model);
        }

        public IActionResult OverviewOrganised()
        {

            var identity = HttpContext.User.Identity;

            User? user = userService.getByIdentityUserName(identity!);

            if (user == null) return View("ErrorPage", "Gebruiker bestaat niet!");

            ICollection<BoardgameNight> boardgameNights = boardgameNightService.getAllBoardgameNightsForUser(user.Id);

            BoardgameNightListViewModel model = new()
            {
                BoardgameNights = boardgameNights
                .Select(b =>
                    new BoardgameNightViewModel
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Organizer = new UserViewModel
                        {
                            Id = b.Organizer.Id,
                            Name = b.Organizer.Name,
                            Email = b.Organizer.Email,
                            Address = b.Organizer.getAddress(),
                            BirthDate = b.Organizer.BirthDate,
                            Gender = b.Organizer.Gender

                        },
                        Address = b.getAddress(),
                        IsAdult = b.IsAdult,
                        MaxParticipants = b.MaxParticipants,
                        CurrentNumberOfParticipants = b.Participants.Count(),
                        DateTime = b.DateTime,
                        FoodAndDrinks = b.FoodAndDrinks,
                        Boardgames = b.Boardgames
                    }),
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var identity = HttpContext.User.Identity;
            User? u = userService.getByIdentityUserName(identity!);
            if (u == null) return View("ErrorPage", "Gebruiker bestaat niet!");

            BoardgameNight? b = boardgameNightService.getById(id);
            if (b == null) return View("ErrorPage", "Bordspellen avond bestaat niet!");

            bool alreadyEnrolled = boardgameNightService.userIsEnrolledinBoardgameNight(u.Id, b.Id);

            if (!b.HasNutFreeFood && u.HasNutAllergy && alreadyEnrolled)
            {
                ViewBag.Message = "Let op! Bordspellen avond voeldoet niet aan dieet voorkeuren!";
            }

            if (!b.HasVegetarianfood && u.IsVegetarian && alreadyEnrolled)
            {
                ViewBag.Message = "Let op! Bordspellen avond voeldoet niet aan dieet voorkeuren!";
            }

            if (!b.HasAlcoholFreeDrinks && !u.DrinksAlcohol && alreadyEnrolled)
            {
                ViewBag.Message = "Let op! Bordspellen avond voeldoet niet aan dieet voorkeuren!";
            }

            if (!b.HasLactoseFreeFood && u.IsLactoseIntolerant && alreadyEnrolled)
            {
                ViewBag.Message = "Let op! Bordspellen avond voeldoet niet aan dieet voorkeuren!";
            }

            BoardgameNightViewModel model = new()
            {
                Id = b.Id,
                Name = b.Name,
                Organizer = new UserViewModel
                {
                    Id = b.Organizer.Id,
                    Name = b.Organizer.Name,
                    Email = b.Organizer.Email,
                    Address = b.Organizer.getAddress(),
                    BirthDate = b.Organizer.BirthDate,
                    Gender = b.Organizer.Gender,
                    AverageScore = userService.GetReviewScore(b.Organizer)

                },
                Address = b.getAddress(),
                IsAdult = b.IsAdult,
                MaxParticipants = b.MaxParticipants,
                CurrentNumberOfParticipants = b.Participants.Count(),
                DateTime = b.DateTime,
                FoodAndDrinks = b.FoodAndDrinks,
                Boardgames = b.Boardgames,
                alreadyEnrolled = alreadyEnrolled,
                alreadyEnrolledOnDate = boardgameNightService.userIsEnrolledInGameNightOnDate(u.Id, b.DateTime),
                Participant = new UserViewModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Address = u.getAddress(),
                    BirthDate = u.BirthDate,
                    Gender = u.Gender,
                    DrinksAlcohol = u.DrinksAlcohol,
                    IsVegetarian = u.IsVegetarian,
                    IsLactoseIntolerant = u.IsLactoseIntolerant,
                    HasNutAllergy = u.HasNutAllergy
                },
                HasAlcoholFreeDrinks = b.HasAlcoholFreeDrinks,
                HasLactoseFreeFood = b.HasLactoseFreeFood,
                HasNutFreeFood = b.HasNutFreeFood,
                HasVegetarianfood = b.HasVegetarianfood,
                Reviews = b.Reviews
                .Select(r =>
                    new GameNightReviewViewModel
                    {
                        Id = r.Id,
                        ReviewDescription = r.ReviewDescription,
                        Score = r.Score,
                        BoardgameNightId = b.Id,
                        Reviewer = new UserViewModel()
                        {
                            Id = r.Reviewer.Id,
                            Name = r.Reviewer.Name,
                        }
                    }),
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult postReview(GameNightReviewViewModel reviewVM)
        {
            var identity = HttpContext.User.Identity;
            User? u = userService.getByIdentityUserName(identity!);
            if (u == null) return View("ErrorPage", "Gebruiker bestaat niet!");

            BoardgameNight? b = boardgameNightService.getById(reviewVM.BoardgameNightId);
            if (b == null) return View("ErrorPage", "Bordspellen avond bestaat niet!");

            GameNightReview review = new()
            {
                Score = reviewVM.Score,
                ReviewDescription = reviewVM.ReviewDescription,
                Reviewer = u
            };

            try
            {
                b.Reviews.Add(review);
                boardgameNightRepository.update(b);
            }
            catch(Exception ex)
            {
                return View("ErrorPage", ex.Message);
            }

            return RedirectToAction("Details", new {id = b.Id});
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var identity = HttpContext.User.Identity;
            User? user = userService.getByIdentityUserName(identity!);
            if (user == null) return View("ErrorPage", "Gebruiker bestaat niet!");

            BoardgameNight? boardgameNight = boardgameNightService.getById(id);

            if (boardgameNight == null) return View("ErrorPage", "Bordspellen avond bestaat niet!");

            List<int> boardgameIds = boardgameNight.Boardgames.Select(b => b.Id).ToList();

            EditBoardgameNightViewModel editBoardgameNight = new()
            {
                Id = boardgameNight.Id,
                Name = boardgameNight.Name,
                HouseNumber = boardgameNight.HouseNumber,
                Street = boardgameNight.Street,
                City = boardgameNight.City,
                IsAdult = boardgameNight.IsAdult,
                MaxParticipants = boardgameNight.MaxParticipants,
                DateTime = boardgameNight.DateTime,
                FoodAndDrinks = boardgameNight.FoodAndDrinks,
                SelectedBoardgameIds = boardgameIds,
                HasAlcoholFreeDrinks = boardgameNight.HasAlcoholFreeDrinks,
                HasLactoseFreeFood = boardgameNight.HasLactoseFreeFood,
                HasNutFreeFood = boardgameNight.HasNutFreeFood,
                HasVegetarianfood = boardgameNight.HasVegetarianfood
            };

            editBoardgameNight.AvailableBoardgames = new SelectList(boardgameService.getAll(), "Id", "Name");
            return View(editBoardgameNight);
        }

        [HttpPost]
        public IActionResult Edit(EditBoardgameNightViewModel model)
        {
            if (model.DateTime.Date <= DateTime.Today) ModelState.AddModelError("", "Datum en tijd kan niet vandaag zijn of in het verleden liggen!");
            if (model.SelectedBoardgameIds.IsNullOrEmpty()) ModelState.AddModelError("", "Moet minstens 1 bordspel kiezen!");
            if (ModelState.IsValid)
            {

                List<Boardgame> boardgames = boardgameService
                .getAll()
                .Where(b => model.SelectedBoardgameIds.Contains(b.Id))
                .ToList();

                BoardgameNight? boardgameNight = boardgameNightService.getById(model.Id);

                if (boardgameNight == null)
                {
                    ModelState.AddModelError("", "Internal server error: boardgameNight not found.");
                    model.AvailableBoardgames = new SelectList(boardgameService.getAll(), "Id", "Name");
                    return View(model);
                }

                boardgameNight.Name = model.Name;
                boardgameNight.HouseNumber = model.HouseNumber;
                boardgameNight.Street = model.Street;
                boardgameNight.City = model.City;
                boardgameNight.IsAdult = model.IsAdult;
                boardgameNight.MaxParticipants = model.MaxParticipants;
                boardgameNight.DateTime = model.DateTime;
                boardgameNight.FoodAndDrinks = model.FoodAndDrinks;
                boardgameNight.Boardgames = boardgames;
                boardgameNight.HasAlcoholFreeDrinks = model.HasAlcoholFreeDrinks;
                boardgameNight.HasLactoseFreeFood = model.HasLactoseFreeFood;
                boardgameNight.HasNutFreeFood = model.HasNutFreeFood;
                boardgameNight.HasVegetarianfood = model.HasVegetarianfood;

                try
                {
                    boardgameNightService.update(boardgameNight);
                }
                catch (Exception err)
                {
                    logger.LogInformation(err.Message);
                    return View("ErrorPage", err.Message);
                }
                
                return RedirectToAction("Details", new { id = boardgameNight.Id });
            }

            model.AvailableBoardgames = new SelectList(boardgameService.getAll(), "Id", "Name");
            return View(model);
        }

        public IActionResult Enroll(int id)
        {

            var identity = HttpContext.User.Identity;
            User? user = userService.getByIdentityUserName(identity!);
            if (user == null) return View("ErrorPage", "Gebruiker bestaat niet!");

            try
            {
                boardgameNightService.addParticipant(id, user.Id);
                
            }
            catch (Exception err)
            {
                logger.LogInformation(err.Message);
                return View("ErrorPage", err.Message);
            }

            return RedirectToAction("Details", new { id });
        }

        public IActionResult Leave(int id)
        {
            var identity = HttpContext.User.Identity;
            User? user = userService.getByIdentityUserName(identity!);
            if (user == null) return View("ErrorPage", "Gebruiker bestaat niet!");

            try
            {
                boardgameNightService.removeParticipant(id, user.Id);
            }
            catch (Exception err)
            {
                logger.LogInformation(err.Message);
                return View("ErrorPage", err.Message);
            }

            return RedirectToAction("Details", new { id });
        }

        public IActionResult Delete(int id)
        {
            try
            {
                boardgameNightService.delete(id);
            }
            catch (Exception err)
            {
                logger.LogInformation(err.Message);
                return View("ErrorPage", err.Message);
            }

            return RedirectToAction("OverviewOrganised");
        }
    }
}
