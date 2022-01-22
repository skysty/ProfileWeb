using System;
using System.Collections.Generic;
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

        // GET: Profile
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ApplicationUsers.Include(a => a.Degree).Include(a => a.Faculties).Include(a => a.Kafedra).Include(a => a.Ranks).Include(a => a.Sex);
            return View(await applicationDbContext.ToListAsync());
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: Profile/Create
        public IActionResult Create()
        {
            ViewData["Degree_ID"] = new SelectList(_context.Degrees, "Degree_ID", "Degree_ID");
            ViewData["Faculty_ID"] = new SelectList(_context.Faculties, "ID", "ID");
            ViewData["Department_ID"] = new SelectList(_context.Kafedras, "ID", "ID");
            ViewData["Rank_ID"] = new SelectList(_context.Ranks, "Rank_ID", "Rank_ID");
            ViewData["Sex_ID"] = new SelectList(_context.Sexes, "Sex_ID", "Sex_ID");
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Firstname_kz,Lastname_kz,Middlename_kz,Firstname_ru,Lastname_ru,Middlename_ru,Firstname_en,Lastname_en,Middlename_en,Firstname_tr,Lastname_tr,Middlename_tr,UsernameChangeLimit,PhotoUrl,Sex_ID,Department_ID,Faculty_ID,Rank_ID,Address,BirthDate,Degree_ID,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Degree_ID"] = new SelectList(_context.Degrees, "Degree_ID", "Degree_ID", applicationUser.Degree_ID);
            ViewData["Faculty_ID"] = new SelectList(_context.Faculties, "ID", "ID", applicationUser.Faculty_ID);
            ViewData["Department_ID"] = new SelectList(_context.Kafedras, "ID", "ID", applicationUser.Department_ID);
            ViewData["Rank_ID"] = new SelectList(_context.Ranks, "Rank_ID", "Rank_ID", applicationUser.Rank_ID);
            ViewData["Sex_ID"] = new SelectList(_context.Sexes, "Sex_ID", "Sex_ID", applicationUser.Sex_ID);
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
            string uniqueFileName = GetUploadedFileName(applicationUser);
            userInfo.PhotoUrl = uniqueFileName;
            var result = await _userManager.UpdateAsync(userInfo);
            if (result.Succeeded)
            {
                TempData["save"] = "User has been updated successfully";
                return RedirectToAction(nameof(Index));
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

            var applicationUser = await _context.Users
                .Include(a => a.Degree)
                .Include(a => a.Faculties)
                .Include(a => a.Kafedra)
                .Include(a => a.Ranks)
                .Include(a => a.Sex)
                .FirstOrDefaultAsync(m => m.Id == id);
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

        private bool ApplicationUserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers
                .Include(a => a.Qulifications)
                .Include(d => d.Researches)
                .Include(c => c.Achievements)
                .Include(e => e.WorkWays)
                .Where(c => c.Id == id).FirstOrDefaultAsync();
            if (applicationUser == null)
            {
                return NotFound();
            }
            applicationUser.Qulifications.Add(new Qulification() { Qu_Id = 1 });
            applicationUser.Qulifications.Add(new Qulification() { Qu_Id = 2 });
            applicationUser.Researches.Add(new Research() { Res_Id = 1 });
            applicationUser.Researches.Add(new Research() { Res_Id = 2 });
            applicationUser.Achievements.Add(new Achievement() { Topic_Id = 1 });
            applicationUser.Achievements.Add(new Achievement() { Topic_Id = 2 });
            applicationUser.WorkWays.Add(new Workway() { Work_Id = 1 });
            applicationUser.WorkWays.Add(new Workway() { Work_Id = 2 });
            return View(applicationUser);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(ApplicationUser applicant)
        {
            var userInfo = await _context.ApplicationUsers
                .Include(a => a.Qulifications)
                .Include(d => d.Researches)
                .Include(c => c.Achievements)
                .Include(e => e.WorkWays)
                .Where(c => c.Id == applicant.Id).FirstOrDefaultAsync();
            if (userInfo == null)
            {
                return NotFound();
            }
            userInfo.Qulifications = applicant.Qulifications;
            userInfo.WorkWays = applicant.WorkWays;
            userInfo.Researches = applicant.Researches;
            userInfo.Achievements = applicant.Achievements;


            foreach (Qulification qulification in userInfo.Qulifications.ToList())
            {
                if (qulification.KZ_Qu == null || qulification.KZ_Qu.Length == 0)
                    userInfo.Qulifications.Remove(qulification);
            }
            //Research
            foreach (Research research in userInfo.Researches.ToList())
            {
                if ((research.KZ_Title == null || research.KZ_Title.Length == 0)&&
                    (research.Document == null || research.Document.Length == 0))
                { 
                    userInfo.Researches.Remove(research);
                }
                else
                {
                    List<string> uniqueFileNames = GetUploadedFileNames(applicant);

                    foreach (string uniqueFileName in uniqueFileNames)
                    {
                            for (int i = 0; i < userInfo.Researches.Count; i++)
                            {
                            if (research.FileUrl != null) {
                                continue;
                            }else
                            {
                                userInfo.Researches.Insert(i, new Research { FileUrl = uniqueFileName });
                                break;
                            }
                        }
                    }
                }
            }
            foreach (Achievement achievement in userInfo.Achievements.ToList())
            {
                if (achievement.KZ_Topic == null || achievement.KZ_Topic.Length == 0)
                    userInfo.Achievements.Remove(achievement);
            }
            foreach (Workway workway in userInfo.WorkWays.ToList())
            {
                if (workway.KZ_Work == null || workway.KZ_Work.Length == 0)
                    userInfo.WorkWays.Remove(workway);
            }

            var result = await _userManager.UpdateAsync(userInfo);
            if (result.Succeeded)
            {
                TempData["save"] = "User has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(applicant);

        }
        private List<string> GetUploadedFileNames(ApplicationUser applicant)
        {
            
             var uniqueFileNames=new List<string>();

            if (applicant.Researches != null)
            {
                foreach (var file in applicant.Researches)
                {
                    string uniq = null;
                    string uploadsFolder = Path.Combine(_webHost.WebRootPath, "documents");
                    uniq = Guid.NewGuid().ToString() + "_" + file.Document.FileName;
                    uniqueFileNames.Add(uniq);
                    string filePath = Path.Combine(uploadsFolder, uniq);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.Document.CopyTo(fileStream);
                    }
                    break;
                }
            }
            return uniqueFileNames;
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
    }
}
