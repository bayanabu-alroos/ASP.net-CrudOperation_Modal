using Crud_Operation.Helper;
using Crud_Operation.Models;
using Crud_Operation.Models.Context;
using Crud_Operation.Models.DTO;
using Crud_Operation.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static Crud_Operation.Enums.SystemEnums;

namespace Crud_Operation.Controllers
{
    public class HomeController : Controller
    {
        private readonly CrudOperationDbContext _context;
        private readonly MappingHelper _mappingHelper;
        public HomeController(CrudOperationDbContext context,MappingHelper mappingHelper)
        {
            _context = context;
            _mappingHelper = mappingHelper;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => 
                (u.Email == model.EmailOrUsername || u.UserName == model.EmailOrUsername) &&
                u.Password == model.Password && !u.IsLoggedIn);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid email/username or password.");
                }
                if (user.IsLoggedIn = true)
                {
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                }
                else
                {
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    user.IsLoggedIn = true;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                TempData["SuccessMessage"] = "The login is successful!";
                return RedirectToAction("CRUD", "Home");
            }
            return View("Index", model);
        }
        public async Task<IActionResult> Logout()
        {
            if (int.TryParse(HttpContext.Session.GetString("UserId"), out int userId))
            {
                var user = await _context.Users.FindAsync(userId);

                if (user != null && user.IsLoggedIn)
                {
                    user.IsLoggedIn = false;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.Clear();
                }
            }
            TempData["SuccessMessage"] = "You have been successfully logged out.";
            return RedirectToAction("Index"); 
        }

        public async Task<IActionResult> CRUD()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            if (userId == 0)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var products = await _context.Products.Select(p => new ProductDTO
                {
                    Name = p.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Id = p.Id,
                    Category = p.Category,
                }).ToListAsync();
                return View(products);
            }
        }
        public IActionResult Create()
        {
            return PartialView("Create");
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO product, IFormFile imageFile)
        {
            try
            {
                string imageName = _mappingHelper.UploadFile(imageFile); 
                string imagePath = "~/Image/" + imageName; 
                var productData = new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    CreateDate = DateTime.Now,
                    IsActive = true,
                    Category = (Category)product.Category,
                    ImageUrl = imagePath 
                };

                await _context.Products.AddAsync(productData);
                await _context.SaveChangesAsync(); // Asynchronous save changes
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An unexpected error occurred while creating the product.");
            }
            TempData["SuccessMessage"] = "The Create New Product is successful!";
            return RedirectToAction("CRUD");
        }
        public IActionResult Update()
        {
            return PartialView("Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDTO product, IFormFile imageFile)
        {
            try
            {
                string imageName = _mappingHelper.UploadFile(imageFile);
                string imagePath = "~/Image/" + imageName;
                var productData = new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    UpdateDate = DateTime.Now,
                    IsActive = true,
                    Category = (Category)product.Category,
                    ImageUrl = imagePath
                };
                _context.Products.Update(productData);
                await _context.SaveChangesAsync(); // Asynchronous save changes
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An unexpected error occurred while creating the product.");
            }
            TempData["SuccessMessage"] = "The Update Product is successful!";
            return RedirectToAction("CRUD");
        }
        public IActionResult Delete()
        {
            return PartialView("Delete");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return BadRequest("Entity Dose not Exisit");
            }
            product.IsActive = false;
            _context.Update(product);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "The Delete Product is successful!";
            return RedirectToAction("CRUD");
        }





    }
}
