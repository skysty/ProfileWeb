using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProfileWeb.Data;
using ProfileWeb.Models;
using ProfileWeb.ViewModel;

namespace ProfileWeb.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHost;

        public ProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHost)
        {
            _context = context;
            _userManager = userManager;
            _webHost = webHost;
        }
        #region user
        // GET: Profile
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            ViewBag.UserId = userId;
            if (userId != null) {
                return RedirectToAction(nameof(Details), new { id = userId });
            }
            var applicationDbContext = await _context.ApplicationUsers.Include(a => a.Degree).Include(a => a.Faculties).Include(a => a.Kafedra).Include(a => a.Ranks).Include(a => a.Sex).ToListAsync();
            return View(applicationDbContext);
        }

        // GET: Profile/Details/5
        [Route("Profile/Details/{id:int:min(1)}", Name = "DetailsRoute")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers
                .Include(a => a.Degree)
                .Include(a => a.Faculties)
                .Include(a => a.Kafedra)
                .Include(a => a.Ranks)
                .Include(a => a.Sex)
                .Include(a => a.Qulifications)
                .Include(d => d.Researches)
                .Include(c => c.Achievements)
                .Include(e => e.Workways)
                .Where(a => a.Id == id)
                .ToListAsync();
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: Profile/Create
        public IActionResult Create()
        {
           
            ViewData["Sex"] = new SelectList(_context.Sexes.ToList(), "Sex_ID", "Sex_KZ");
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Firstname_kz,Lastname_kz,Middlename_kz,Firstname_ru,Lastname_ru,Middlename_ru,Firstname_en,Lastname_en,Middlename_en,Firstname_tr,Lastname_tr,Middlename_tr,Sex_ID,UserName,PasswordHash")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Sex"] = new SelectList(_context.Sexes.ToList(), "Sex_ID", "Sex_KZ");
            return View(applicationUser);
        }

        // GET: Profile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers.FirstOrDefaultAsync(c => c.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            ViewData["degree"] = new SelectList(_context.Degrees.ToList(), "Degree_ID", "TR_AD");
            ViewData["Faculty"] = new SelectList(_context.Faculties.ToList(), "ID", "KZ_AD");
            ViewData["Department"] = new SelectList(_context.Kafedras.ToList(), "ID", "KZ_AD");
            ViewData["Rank"] = new SelectList(_context.Ranks.ToList(), "Rank_ID", "TR_AD");
            ViewData["Sex"] = new SelectList(_context.Sexes.ToList(), "Sex_ID", "Sex_KZ");
            return View(applicationUser);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser applicationUser)
        {
            
            var userInfo = await _context.ApplicationUsers.FirstOrDefaultAsync(c => c.Id == applicationUser.Id);
            if (userInfo == null)
            {
                return NotFound();
            }

            userInfo.Firstname_kz = applicationUser.Firstname_kz;
            userInfo.Lastname_kz = applicationUser.Lastname_kz;
            userInfo.Middlename_kz = applicationUser.Middlename_kz;
            userInfo.Degree_ID = applicationUser.Degree_ID;
            userInfo.PhoneNumber = applicationUser.PhoneNumber;
            userInfo.Email = applicationUser.Email;
            userInfo.Rank_ID = applicationUser.Rank_ID;
            userInfo.Address = applicationUser.Address;
            if (applicationUser.ProfilePhoto != null)
            {
                string uniqueFileName = GetUploadedFileName(applicationUser);
                userInfo.PhotoUrl = uniqueFileName;
            }
            
            var result = await _userManager.UpdateAsync(userInfo);
            if (result.Succeeded)
            {
                TempData["save"] = "User has been updated successfully";
                return RedirectToAction(nameof(Details), new { id = applicationUser.Id });
            }

            ViewData["degree"] = new SelectList(_context.Degrees.ToList(), "Degree_ID", "TR_AD");
            ViewData["Faculty"] = new SelectList(_context.Faculties.ToList(), "ID", "KZ_AD");
            ViewData["Department"] = new SelectList(_context.Kafedras.ToList(), "ID", "KZ_AD");
            ViewData["Rank"] = new SelectList(_context.Ranks.ToList(), "Rank_ID", "TR_AD");
            ViewData["Sex"] = new SelectList(_context.Sexes.ToList(), "Sex_ID", "Sex_KZ");
            return View(applicationUser);
        }

        // GET: Profile/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers
                .Include(a => a.Degree)
                .Include(a => a.Faculties)
                .Include(a => a.Kafedra)
                .Include(a => a.Ranks)
                .Include(a => a.Sex)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationUser = await _context.Users.FindAsync(id);
            _context.Users.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private string GetUploadedFileName(ApplicationUser applicant)
        {
            string uniqueFileName = null;

            if (applicant.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + applicant.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    applicant.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public async Task<IActionResult> Info(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers
                .Include(a => a.Degree)
                .Include(a => a.Faculties)
                .Include(a => a.Kafedra)
                .Include(a => a.Ranks)
                .Include(a => a.Sex)
                .Include(a => a.Qulifications)
                .Include(d => d.Researches)
                .Include(c => c.Achievements)
                .Include(e => e.Workways)
                .Where(a => a.Id == id)
                .ToListAsync();
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }
        private bool ApplicationUserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        #endregion user
        #region research
        public IActionResult CreateProfile() {
            
            return View();
        }
        [Route("Profile/ListFolder/{id:int:min(1)}", Name = "ListFolderRoute")]
        public async Task<IActionResult> ListFolder(int? id)
        {
            return View( await _context.ApplicationUsers
           .Include(a => a.Researches)
           .Where(c => c.Id == id).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProfile(ResearchViewModel model, int? id)
        {
           
            
           var userInfo =  await _context.ApplicationUsers
           .Include(a => a.Researches)
           .Where(c => c.Id == id).FirstOrDefaultAsync();
            string fileName = ProcessUploadedFile(model);
            Research research = new Research()
            {
                ApplicationUser = userInfo,
                FileUrl = fileName,
                KZ_Title = model.KZ_Title
            };
            if (ModelState.IsValid)
            {
                _context.Add(research);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListFolder), new { id = userInfo.Id });
                
            }
            return View(model);

        }
        public async Task<IActionResult> EditProfile(int? id) {
            if (id == null) {
                return NotFound();
            }
            var research = await _context.Researches.FindAsync(id);

            var researhViewModel = new ResearchViewModel()
            {
                Id = research.Res_Id,
                KZ_Title = research.KZ_Title,
                ExistingDoc = research.FileUrl
            };
            if (research == null) { 
                return NotFound();
            }
            return View(researhViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(int? id, ResearchViewModel model)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                var research = await _context.Researches.FindAsync(id);

                research.KZ_Title = model.KZ_Title;
                if (model.Document != null) {
                    if (model.ExistingDoc != null) {
                        string filePath = Path.Combine(_webHost.WebRootPath, FileLocation.FileUploadFolder, model.ExistingDoc);
                        System.IO.File.Delete(filePath);
                    }
                    research.FileUrl = ProcessUploadedFile(model);
                }
                _context.Update(research);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListFolder), new { id = userId });
            }

            return View();
        }
        public async Task<IActionResult> DeleteProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var research = await _context.Researches.FindAsync(id);

            var researhViewModel = new ResearchViewModel()
            {
                Id = research.Res_Id,
                KZ_Title = research.KZ_Title,
                ExistingDoc = research.FileUrl
                
            };
            if (research == null)
            {
                return NotFound();
            }

            return View(researhViewModel);
        }
        [HttpPost, ActionName("DeleteProfile")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProfileConfirmed(int? id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var research = await _context.Researches.FindAsync(id);
            if (research.FileUrl == null)
            {
                _context.Researches.Remove(research);
            }
            else
            {
                var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), FileLocation.DeleteFileFromFolder, research.FileUrl);
                _context.Researches.Remove(research);
                if (System.IO.File.Exists(CurrentImage))
                {
                    System.IO.File.Delete(CurrentImage);
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListFolder), new { id = userId });
        }
        private string ProcessUploadedFile(ResearchViewModel model)
        {
            string uniqueFileName = null;

            if (model.Document != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, FileLocation.FileUploadFolder);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Document.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Document.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        #endregion research
        #region qualification
        [Route("Profile/ListQu/{id:int:min(1)}", Name = "ListQu")]
        public async Task<IActionResult> ListQu(int? id)
        {
            return View(await _context.ApplicationUsers
           .Include(a => a.Qulifications)
           .Where(c => c.Id == id).ToListAsync());
        }
        public IActionResult CreateQu()
        {

            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateQu(QulificationViewModel  qmodel, int? id)
        {


            var userInfo = await _context.ApplicationUsers
            .Include(a => a.Qulifications)
            .Where(c => c.Id == id).FirstOrDefaultAsync();
            var qu = new Qulification()
            {
                ApplicationUser = userInfo,
                KZ_Qu = qmodel.KZ_Qu
            };
            if (ModelState.IsValid)
            {
                _context.Add(qu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListQu), new { id = userInfo.Id });

            }
            return View(qmodel);

        }

        public async Task<IActionResult> EditQu(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var qualification = await _context.Qulifications.FindAsync(id);

            var qu = new QulificationViewModel()
            {
                QuW_Id = qualification.Qu_Id,
                KZ_Qu  = qualification.KZ_Qu
            };
            if (qualification == null)
            {
                return NotFound();
            }
            return View(qu);
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQu(int? id, QulificationViewModel qmodel)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                var qu = await _context.Qulifications.FindAsync(id);

                qu.KZ_Qu = qmodel.KZ_Qu;
               
                _context.Update(qu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListQu), new { id = userId });
            }

            return View();
        }


        public async Task<IActionResult> DeleteQu(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qulification = await _context.Qulifications.FindAsync(id);

            var quW = new QulificationViewModel()
            {
                QuW_Id = qulification.Qu_Id,
                KZ_Qu = qulification.KZ_Qu

            };
            if (qulification == null)
            {
                return NotFound();
            }

            return View(quW);
        }
        [HttpPost, ActionName("DeleteQu")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteQuConfirmed(int? id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var qulification = await _context.Qulifications.FindAsync(id);
           
            _context.Qulifications.Remove(qulification);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListQu), new { id = userId });
        }
        #endregion qualification
        #region wokway
        [Route("Profile/ListWork/{id:int:min(1)}", Name = "ListWork")]
        public async Task<IActionResult> ListWork(int? id)
        {
            return View(await _context.ApplicationUsers
           .Include(a => a.Workways)
           .Where(c => c.Id == id).ToListAsync());
        }
        public IActionResult CreateWork()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWork(WorkwayViewModel wmodel, int? id)
        {


            var userInfo = await _context.ApplicationUsers
            .Include(a => a.Workways)
            .Where(c => c.Id == id).FirstOrDefaultAsync();
            var workw = new Workway()
            {
                ApplicationUser = userInfo,
                KZ_Work = wmodel.KZ_Work
            };
            if (ModelState.IsValid)
            {
                _context.Add(workw);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListWork), new { id = userInfo.Id });

            }
            return View(wmodel);

        }

        public async Task<IActionResult> EditWork(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var workway = await _context.Workways.FindAsync(id);

            var wokrw = new WorkwayViewModel()
            {
                WorkW_Id = workway.Work_Id,
                KZ_Work = workway.KZ_Work
            };
            if (workway == null)
            {
                return NotFound();
            }
            return View(wokrw);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWork(int? id, WorkwayViewModel wmodel)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                var wokrway = await _context.Workways.FindAsync(id);

                wokrway.KZ_Work = wmodel.KZ_Work;

                _context.Update(wokrway);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListWork), new { id = userId });
            }

            return View();
        }


        public async Task<IActionResult> DeleteWork(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workway = await _context.Workways.FindAsync(id);

            var wokrw = new WorkwayViewModel()
            {
                WorkW_Id = workway.Work_Id,
                KZ_Work  = workway.KZ_Work

            };
            if (workway == null)
            {
                return NotFound();
            }

            return View(wokrw);
        }
        [HttpPost, ActionName("DeleteWork")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWorkConfirmed(int? id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var workway = await _context.Workways.FindAsync(id);

            _context.Workways.Remove(workway);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListWork), new { id = userId });
        }
        #endregion workway
        #region achievment
        [Route("Profile/ListAchieve/{id:int:min(1)}", Name = "ListAchieve")]
        public async Task<IActionResult> ListAchieve(int? id)
        {
            return View(await _context.ApplicationUsers
           .Include(a => a.Achievements)
           .Where(c => c.Id == id).ToListAsync());
        }
        public IActionResult CreateAchieve()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAchieve(AchievementViewModel amodel, int? id)
        {


            var userInfo = await _context.ApplicationUsers
            .Include(a => a.Achievements)
            .Where(c => c.Id == id).FirstOrDefaultAsync();
            var achieve = new Achievement()
            {
                ApplicationUser = userInfo,
                KZ_Topic =amodel.KZ_Topic
            };
            if (ModelState.IsValid)
            {
                _context.Add(achieve);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListAchieve), new { id = userInfo.Id });

            }
            return View(amodel);

        }

        public async Task<IActionResult> EditAchieve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var achievement = await _context.Achievements.FindAsync(id);

            var amodel = new AchievementViewModel()
            {
                Ach_Id = achievement.Topic_Id,
                KZ_Topic = achievement.KZ_Topic
            };
            if (achievement == null)
            {
                return NotFound();
            }
            return View(amodel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAchieve(int? id, AchievementViewModel amodel)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                var achievement = await _context.Achievements.FindAsync(id);

                achievement.KZ_Topic = amodel.KZ_Topic;

                _context.Update(achievement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListAchieve), new { id = userId });
            }

            return View();
        }


        public async Task<IActionResult> DeleteAchieve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achievement = await _context.Achievements.FindAsync(id);

            var amodel = new AchievementViewModel()
            {
                Ach_Id = achievement.Topic_Id,
                KZ_Topic = achievement.KZ_Topic
 
            };
            if (achievement == null)
            {
                return NotFound();
            }

            return View(amodel);
        }
        [HttpPost, ActionName("DeleteAchieve")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAchieveConfirmed(int? id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var achievement = await _context.Achievements.FindAsync(id);

            _context.Achievements.Remove(achievement);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListAchieve), new { id = userId });
        }
        #endregion achievment


    }
}
