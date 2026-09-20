using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using System.Text.Json;

namespace PortfolioApp.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly string _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "projects.json");

        private List<Project> GetProjectsFromFile()
        {
            if (!System.IO.File.Exists(_jsonFilePath))
                return new List<Project>();

            var jsonData = System.IO.File.ReadAllText(_jsonFilePath);
            return JsonSerializer.Deserialize<List<Project>>(jsonData) ?? new List<Project>();
        }

        private void SaveProjectsToFile(List<Project> projects)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonData = JsonSerializer.Serialize(projects, options);
            System.IO.File.WriteAllText(_jsonFilePath, jsonData);
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsLoggedIn")))
            {
                return RedirectToAction("Login", "Auth");
            }

            var projects = GetProjectsFromFile();
            return View(projects);
        }

        public IActionResult Detail(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsLoggedIn")))
            {
                return RedirectToAction("Login", "Auth");
            }

            var projects = GetProjectsFromFile();
            var project = projects.FirstOrDefault(p => p.Id == id);
            if (project == null) return NotFound();

            return View(project);
        }

        [HttpPost]
        public IActionResult AddComment(int projectId, string author, string text)
        {
            var projects = GetProjectsFromFile();
            var project = projects.FirstOrDefault(p => p.Id == projectId);

            if (project != null && !string.IsNullOrWhiteSpace(text))
            {
                project.Comments.Add(new Comment
                {
                    Id = project.Comments.Count + 1,
                    ProjectId = projectId,
                    Author = string.IsNullOrWhiteSpace(author) ? "Anonymous" : author,
                    Text = text,
                    CreatedAt = DateTime.Now
                });

                SaveProjectsToFile(projects);
            }

            return RedirectToAction("Detail", new { id = projectId });
        }
    }
}