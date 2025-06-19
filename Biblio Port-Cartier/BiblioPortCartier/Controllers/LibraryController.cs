using BiblioPortCartier.Data;
using BiblioPortCartier.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BiblioPortCartier.ViewModels;
using BiblioPortCartier.Helpers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace BiblioPortCartier.Controllers
{
    public class LibraryController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext _db;
        private string[] actions = new string[]{
                "createLoan",
                "createReservation",
                "returnLoan",
                "cancelReservation",
                "changeResToLoan"
            };

        private Object[] provinces = new Object[] {
                new{Id=0, Name="--Sélectionnez une province--"},
                new{Id=1, Name="AB"},
                new{Id=2, Name="BC"},
                new{Id=3, Name="MB"},
                new{Id=4, Name="NB"},
                new{Id=5, Name="NL"},
                new{Id=6, Name="NT"},
                new{Id=7, Name="NS"},
                new{Id=8, Name="NU"},
                new{Id=9, Name="ON"},
                new{Id=10, Name="PE"},
                new{Id=11, Name="QC"},
                new{Id=12, Name="SK"},
                new{Id=13, Name="YT"}
            };

        private Object[] sexes = new Object[] {
                new{Id=0, Name="--Sélectionnez un sexe--"},
                new{Id=1, Name="Homme"},
                new{Id=2, Name="Femme"}
            };

        private Object[] typeEmp = new Object[] {
                new{Id=0, Name="--Sélectionnez un type d'employé--"},
                new{Id=1, Name="Base"},
                new{Id=2, Name="Administrateur"}
            };

        public LibraryController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AppDbContext db)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this._db = db;
        }
        public IActionResult Home(User user)
        {
            User currentUser = new User();
            if (User.IsInRole("Member"))
            {
                currentUser = JsonConvert.DeserializeObject<Member>(HttpContext.Session.GetString("currentUser"));
            }
            else
            {
                currentUser = JsonConvert.DeserializeObject<Employee>(HttpContext.Session.GetString("currentUser"));
            }

            return View(currentUser);
        }

        public IActionResult Documents(BiblioViewModel model)
        {
            ViewBag.TitleSortParm = String.IsNullOrEmpty(model.SortOrder) ? "title_desc" : "";
            ViewBag.MakerSortParm = model.SortOrder == "Maker" ? "maker_desc" : "Maker";
            ViewBag.YearSortParm = model.SortOrder == "Year" ? "year_desc" : "Year";
            ViewBag.CategorySortParm = model.SortOrder == "Category" ? "category_desc" : "Category";
            ViewBag.TypeSortParm = model.SortOrder == "Type" ? "type_desc" : "Type";
            ViewBag.GenreSortParm = model.SortOrder == "Genre" ? "genre_desc" : "Genre";

            model.Documents = _db.Documents.Include(l => l.Loan).Include(r => r.Reservation).OrderBy(t => t.Title).ToList();
            model.Member = JsonConvert.DeserializeObject<Member>(HttpContext.Session.GetString("currentUser"));

            foreach (Document doc in model.Documents)
            {
                if (doc.Loan != null)
                {
                    doc.Loan.Member = _db.Members.Where(a => a.Id == doc.Loan.MemberId).First();
                }
                if (doc.Reservation != null) 
                {
                    doc.Reservation.Member = _db.Members.Where(a => a.Id == doc.Reservation.MemberId).First();
                }
            }

            if (!String.IsNullOrEmpty(model.SearchString))
                model.Documents = SearchDocuments(model);
            
            model.Documents = SortDocuments(model);

            return View(model);
        }

        public IActionResult MemberReservations(BiblioViewModel model)
        {
            Member member = JsonConvert.DeserializeObject<Member>(HttpContext.Session.GetString("currentUser"));
           
            model.ReservationsMember = _db.Reservations.Include(d => d.Document).Where(i => i.MemberId == member.Id).OrderBy(t => t.ReservationDate);
            member.Reservations = model.ReservationsMember;
            model.Member = member;

            return View(model);
        }

        public IActionResult MemberLoans(BiblioViewModel model)
        {
            Member member = JsonConvert.DeserializeObject<Member>(HttpContext.Session.GetString("currentUser"));

            model.LoansMember = _db.Loans.Include(d => d.Document).Where(i => i.MemberId == member.Id).OrderBy(t => t.BorrowedDate);
            member.Loans = model.LoansMember;
            model.Member = member;

            return View(model);
        }

        public IActionResult Details(BiblioViewModel model, string? id)
        {
            if (String.IsNullOrEmpty(id)) 
            { 
                return NotFound();
            }

            model.Document = _db.Documents.Include(l => l.Loan).Include(r => r.Reservation).Where(d => d.DocumentCode == id).First();
            model.Membres = _db.Members;

            if (model.Document.Loan != null)
            {
                model.Document.Loan.Member = model.Membres.Where(a => a.Id == model.Document.Loan.MemberId).First();
            }
            if (model.Document.Reservation != null) 
            {
                model.Document.Reservation.Member = model.Membres.Where(a => a.Id == model.Document.Reservation.MemberId).First();

            }
      
            UsersHelper.Put<Document>(TempData, "doc", model.Document);

            return View(model);
        }

        public IActionResult DocumentHandler(BiblioViewModel model, string? id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            model.Document = _db.Documents.Include(l => l.Loan).Include(r => r.Reservation).Where(d => d.DocumentCode == id).First();
            model.Membres = _db.Members;

            if (model.Document.Loan != null)
            {
                model.Document.Loan.Member = model.Membres.Where(a => a.Id == model.Document.Loan.MemberId).First();
            }
            if (model.Document.Reservation != null)
            {
                model.Document.Reservation.Member = model.Membres.Where(a => a.Id == model.Document.Reservation.MemberId).First();

            }
            
            UsersHelper.Put<Document>(TempData, "doc", model.Document);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DocumentHandler(BiblioViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool success = true;
            Document doc = UsersHelper.Get<Document>(TempData, "doc");

            switch (actions[id])
            {
                case "createLoan":
                    success = CreateLoan(model, doc);
                    break;
                case "createReservation":
                    CreateReservation(model, doc);
                    break;
                case "returnLoan":
                    ReturnLoan(doc);
                    break;
                case "cancelReservation":
                    CancelReservation(doc);
                    break;
                case "changeResToLoan":
                    CancelReservation(doc);
                    success = CreateLoan(model, doc);
                    break;
                default:
                    break;
            }

            model.Membres = _db.Members;
            if (success) {
                return RedirectToAction("Documents");
            }
            else if(!success && actions[id] == "createLoan")
            {
                ModelState.AddModelError(string.Empty, "La date de retour prévue doit être supérieure à la date d'emprunt.");
                UsersHelper.Put<Document>(TempData, "doc", doc);
                model.Document = doc;
                return View(model);
            }
            else { return View(model); }
        }

        public IActionResult Borrow(BiblioViewModel model, string? id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            model.Document = _db.Documents.Find(id);
            UsersHelper.Put<Document>(TempData, "doc", model.Document);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Borrow(BiblioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Document doc = UsersHelper.Get<Document>(TempData, "doc");
            int result = DateTime.Compare(model.BorrowedDate, model.ScheduledReturnDate);

            if (result < 0)
            {
                Member member = JsonConvert.DeserializeObject<Member>(HttpContext.Session.GetString("currentUser"));

                Loan loan = new Loan
                {
                    BorrowedDate = model.BorrowedDate,
                    ScheduledReturnDate = model.ScheduledReturnDate,
                    ExactReturnDate = null,
                    MemberId = member.Id,
                    DocumentCode = doc.DocumentCode
                };

                doc.IsBorrowed = true;

                _db.Loans.Add(loan);
                _db.Documents.Update(doc);
                _db.SaveChanges();
                return RedirectToAction("MemberLoans");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "La date de retour prévue doit être supérieure à la date d'emprunt.");
                UsersHelper.Put<Document>(TempData, "doc", doc);
                model.Document = doc;
                return View(model);
            }    
        }

        public IActionResult Reserve(BiblioViewModel model, string? id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            model.Document = _db.Documents.Find(id);
            UsersHelper.Put<Document>(TempData, "doc", model.Document);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reserve(BiblioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Member member = JsonConvert.DeserializeObject<Member>(HttpContext.Session.GetString("currentUser"));
            Document doc = UsersHelper.Get<Document>(TempData,"doc");

            Reservation reservation = new Reservation
            {
                ReservationDate = model.ReservationDate,
                MemberId = member.Id,
                DocumentCode = doc.DocumentCode
            };

            doc.IsReserved = true;

            _db.Reservations.Add(reservation);
            _db.Documents.Update(doc);
            _db.SaveChanges();
            return RedirectToAction("MemberReservations");
        }

        public IActionResult MembersList(BiblioViewModel model)
        {
            if (String.IsNullOrEmpty(model.SearchString))
            {
                model.SearchString = "";
            }
            model.Membres = _db.Members.Include(a => a.Address).Where(n => (n.FirstName + " " + n.LastName).ToLower().Contains(model.SearchString.ToLower())).ToList();

            return View(model);
        }

        public IActionResult LoansReservationsMember(BiblioViewModel model, string id)
        {
            model.ReservationsMember = _db.Reservations.Include(d => d.Document) .Where(r => r.MemberId == id).ToList();
            model.LoansMember = _db.Loans.Include(d => d.Document).Where(l => l.MemberId == id).ToList();
            model.Member = _db.Members.Find(id);

            return View(model);
        }

        public IActionResult LoansList(BiblioViewModel model)
        { 
            model.LoansMember = _db.Loans.Include(d => d.Document).Include(r => r.Member).OrderBy(b => b.BorrowedDate) .ToList();

            return View(model);
        }

        public IActionResult ReservationsList(BiblioViewModel model)
        {
            model.ReservationsMember = _db.Reservations.Include(d => d.Document).Include(r => r.Member).OrderBy(b => b.ReservationDate).ToList();
        
            return View(model);
        }

        public IActionResult LatesList(BiblioViewModel model)
        {
            model.LoansMember = _db.Loans.Include(d => d.Document).Include(r => r.Member).OrderBy(b => b.BorrowedDate).ToList();
            model.LateDocs = [];

            foreach (var loan in model.LoansMember)
            {
                int comp = DateTime.Compare(loan.ScheduledReturnDate, DateTime.Now);

                if (comp < 0)
                {
                    model.LateDocs.Add(loan);
                }
            }

            return View(model);
        }

        public IActionResult CreateMember()
        {
            CreateViewBags();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMember(CreateMemberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                CreateViewBags();
                return View(model);
            }

            object objProvince = provinces.ElementAt(Int32.Parse(model.Province));
            object objSexe = sexes.ElementAt(Int32.Parse(model.Gender));

            var province = objProvince.GetType().GetProperty("Name").GetValue(objProvince).ToString();
            var sexe = objSexe.GetType().GetProperty("Name").GetValue(objSexe).ToString();

            var address = Address.CreateAddress(model, province);
            var userCode = UsersHelper.CreateUserCode(model.UserCode, "Member");

            var member = new Member
            {
                Id = userCode,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = sexe,
                PhoneNumber = model.PhoneNumber,
                Address = address,
                BirthdayDate = model.BirthdayDate,
                RegisterDate = DateTime.Now,
                UserName = model.Email,
                NormalizedUserName = model.Email.ToUpper(),
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper()
            };

            var result = await userManager.CreateAsync(member, model.Password);

            if (result.Succeeded)
            {
                var roleExist = await roleManager.RoleExistsAsync("Member");

                if(!roleExist)
                {
                    var role = new IdentityRole("Member");
                    await roleManager.CreateAsync(role);
                }

                await userManager.AddToRoleAsync(member, "Member");

                return RedirectToAction("MembersList","Library");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            CreateViewBags();
            return View(model);
        }
       
        public IActionResult CreateEmployee()
        {
            CreateViewBags();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                CreateViewBags();
                return View(model);
            }

            object objTypeEmp = typeEmp.ElementAt(Int32.Parse(model.Type));
            object objSexe = sexes.ElementAt(Int32.Parse(model.Gender));

            var type = objTypeEmp.GetType().GetProperty("Name").GetValue(objTypeEmp).ToString();
            var sexe = objSexe.GetType().GetProperty("Name").GetValue(objSexe).ToString();

            
            string userCode;

            switch (type)
            {
                case "Administrateur":
                    userCode = UsersHelper.CreateUserCode(model.UserCode, "Admin");
                    break;
                case "Base":
                default:
                    userCode = UsersHelper.CreateUserCode(model.UserCode, "Employee");
                    break;
            }

            var employee = new Employee
            {
                Id = userCode,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = sexe,
                PhoneNumber = model.PhoneNumber,
                HiredDate = model.HiredDate,
                UserName = model.Email,
                NormalizedUserName = model.Email.ToUpper(),
                Email = model.Email,
                NormalizedEmail = model.Email.ToUpper()
            };

            var result = await userManager.CreateAsync(employee, model.Password);

            if (result.Succeeded)
            {
                if (type == "Base") 
                {
                    var roleExist = await roleManager.RoleExistsAsync("Employee");

                    if (!roleExist)
                    {
                        var role = new IdentityRole("Employee");
                        await roleManager.CreateAsync(role);
                    }

                    await userManager.AddToRoleAsync(employee, "Employee");
                }
                if (type == "Administrateur")
                {
                    var roleExist = await roleManager.RoleExistsAsync("Admin");

                    if (!roleExist)
                    {
                        var role = new IdentityRole("Admin");
                        await roleManager.CreateAsync(role);
                    }

                    await userManager.AddToRoleAsync(employee, "Admin");
                }

                return RedirectToAction("Home", "Library");

            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            CreateViewBags();
            return View(model);
        }

        public bool CreateLoan(BiblioViewModel model, Document doc)
        {
            int result = DateTime.Compare(model.BorrowedDate, model.ScheduledReturnDate);

            if (result < 0)
            {
                Member member = new Member();
                if (!string.IsNullOrEmpty(model.MemberName))
                {
                    member = _db.Members.Find(model.MemberName);
                }
                else if(doc.Reservation.Member != null)
                {
                    member = _db.Members.Find(doc.Reservation.Member.Id);
                }

                Loan loan = new Loan
                {
                    BorrowedDate = model.BorrowedDate,
                    ScheduledReturnDate = model.ScheduledReturnDate,
                    ExactReturnDate = null,
                    MemberId = member.Id,
                    DocumentCode = doc.DocumentCode
                };


                _db.Loans.Add(loan);
                _db.SaveChanges();
                _db.Documents.Where(d => d.DocumentCode == doc.DocumentCode)
                    .ExecuteUpdate(b => b.SetProperty(c => c.IsBorrowed, true));
                return true;
            }
            else
                return false;
        }
        public void CreateReservation(BiblioViewModel model, Document doc)
        {
            Member member = _db.Members.Find(model.MemberName);

            Reservation reservation = new Reservation
            {
                ReservationDate = model.ReservationDate,
                MemberId = member.Id,
                DocumentCode = doc.DocumentCode
            };

            _db.Reservations.Add(reservation);
            _db.SaveChanges();
            _db.Documents.Where(d => d.DocumentCode == doc.DocumentCode)
                    .ExecuteUpdate(r => r.SetProperty(s => s.IsReserved, true));
        }
        public void ReturnLoan(Document doc)
        {
            var loan = doc.Loan;
            if (loan == null)
            {
                NotFound();
            }
            _db.Loans.Remove(loan);
            _db.SaveChanges();
            _db.Documents.Where(d => d.DocumentCode == doc.DocumentCode)
                .ExecuteUpdate(b => b.SetProperty(c => c.IsBorrowed, false));
        }
        public void CancelReservation(Document doc)
        {
            var reservation = doc.Reservation;
            if (reservation == null)
            {
                NotFound();
            }

            _db.Reservations.Remove(reservation);
            _db.SaveChanges();
            _db.Documents.Where(d => d.DocumentCode == doc.DocumentCode)
                .ExecuteUpdate(r => r.SetProperty(s => s.IsReserved, false));
        }
        private IList<Document> SortDocuments(BiblioViewModel model)
        {
            switch(model.SortOrder)
            {
                case "title_desc":
                    return model.Documents = model.Documents.OrderByDescending(t => t.Title).ToList(); ;

                case "Maker":
                    return model.Documents = model.Documents.OrderBy(m => m.MakerName).ToList();
                case "maker_desc":
                    return model.Documents = model.Documents.OrderByDescending(m => m.MakerName).ToList();

                case "Year":
                    return model.Documents = model.Documents.OrderBy(y => y.PublishYear).ToList();
                case "year_desc":
                    return model.Documents = model.Documents.OrderByDescending(y => y.PublishYear).ToList();

                case "Category":
                    return model.Documents = model.Documents.OrderBy(c => c.Category).ToList();
                case "category_desc":
                    return model.Documents = model.Documents.OrderByDescending(c => c.Category).ToList();

                case "Type":
                    return model.Documents = model.Documents.OrderBy(r => r.Rating).ToList();
                case "type_desc":
                    return model.Documents = model.Documents.OrderByDescending(r => r.Rating).ToList();

                case "Genre":
                    return model.Documents = model.Documents.OrderBy(g => g.Genre).ToList();
                case "genre_desc":
                    return model.Documents = model.Documents.OrderByDescending(g => g.Genre).ToList();

                default:
                    return model.Documents = model.Documents.OrderBy(t => t.Title).ToList();
            }
        }
        private IList<Document> SearchDocuments(BiblioViewModel model)
        {
            switch (model.SelectedValue)
            {
                case "Maker":
                    return model.Documents = model.Documents.Where(o => o.MakerName.ToLower().Contains(model.SearchString.ToLower())).ToList();
                case "Year":
                    return model.Documents = model.Documents.Where(o => o.PublishYear.ToString().Contains(model.SearchString)).ToList();
                case "Category":
                    return model.Documents = model.Documents.Where(o => o.Category.ToLower().Contains(model.SearchString.ToLower())).ToList();
                case "Type":
                    return model.Documents = model.Documents.Where(o => o.Rating.ToLower().Contains(model.SearchString.ToLower())).ToList();
                case "Genre":
                    return model.Documents = model.Documents.Where(o => o.Genre.ToLower().Contains(model.SearchString.ToLower())).ToList();
                case "Title":
                default:
                    return model.Documents = model.Documents.Where(o => o.Title.ToLower().Contains(model.SearchString.ToLower())).ToList();
          
            }
        }
        private void CreateViewBags()
        {
            ViewBag.Provinces = new SelectList(provinces, "Id", "Name");
            ViewBag.Sexes = new SelectList(sexes, "Id", "Name");
            ViewBag.TypeEmp = new SelectList(typeEmp, "Id", "Name");

        }
    }
}
