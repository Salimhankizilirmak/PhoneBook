using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Storage;
using PhoneBook.Models;
using PhoneBook.Services;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace PhoneBook.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public ContactsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            var contacts = context.Contacts.OrderByDescending(c => c.Id).ToList();
            return View(contacts);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ContactDto contactDto)
        {
            if (contactDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The image file is required");
            }
            if (!ModelState.IsValid)
            {
                return View(contactDto);
            }

            string newFileName = DateTime.Now.ToString("Mq");
            newFileName += Path.GetExtension(contactDto.ImageFile!.FileName);
            string imageFullPath = environment.WebRootPath + "/images/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                contactDto.ImageFile.CopyTo(stream);
            }
            Contact contact = new Contact()
            {
                Name = contactDto.Name,
                Email = contactDto.Email,
                PhoneNumber = contactDto.PhoneNumber,
                ImageFileName = newFileName,
                CreatedAt = DateTime.Now,

            };
            context.Contacts.Add(contact);
            context.SaveChanges();
            return RedirectToAction("Index", "Contacts");
        }
        public IActionResult Edit(int id)
        {
            var contact = context.Contacts.Find(id);
            if (contact == null)
            {
                return RedirectToAction("Index", "Contacts");
            }
            var contactDto = new ContactDto()
            {
                Name = contact.Name,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
            };

            ViewData["ContactId"] = contact.Id;
            ViewData["ImageFileName"] = contact.ImageFileName;
            ViewData["CreatedAt"] = contact.CreatedAt.ToString("d");
            return View(contactDto);
        }
        [HttpPost]
        public IActionResult Edit(int id, ContactDto contactDto)
        {
            var contact = context.Contacts.Find(id);
            if (contact == null)
            {
                return RedirectToAction("Index", "Contacts");
            }
            if (!ModelState.IsValid)
            {
                ViewData["ContactId"] = contact.Id;
                ViewData["ImageFileName"] = contact.ImageFileName;
                ViewData["CreatedAt"] = contact.CreatedAt.ToString("d");
                return View(contactDto);
            }
            string newFileName = contact.ImageFileName;
            if (contact.ImageFileName != null)
            {
                newFileName = DateTime.Now.ToString("Mq");
                newFileName += Path.GetExtension(contactDto.ImageFile!.FileName);
                string imageFullPath = environment.WebRootPath + "/images/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    contactDto.ImageFile.CopyTo(stream);
                }
                string oldImageFullPath = environment.WebRootPath + "/images/" + contact.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }
            contact.Name = contactDto.Name;
            contact.Email = contactDto.Email;
            contact.PhoneNumber = contactDto.PhoneNumber;
            contact.ImageFileName = newFileName;
            context.SaveChanges();
            return RedirectToAction("Index", "Contacts");
        }
        public IActionResult Delete(int id)
        {
            var contact = context.Contacts.Find(id);
            if (contact == null)
            {
                return RedirectToAction("Index", "Contacts");
            }
            string imageFullPath = environment.WebRootPath + "/images/" + contact.ImageFileName;
            System.IO.File.Delete(imageFullPath);
            context.Contacts.Remove(contact);
            context.SaveChanges(true);
            return RedirectToAction("Index", "Contacts");

        }
    }
}
