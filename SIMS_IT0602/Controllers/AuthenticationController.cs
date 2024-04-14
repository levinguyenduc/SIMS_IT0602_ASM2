using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SIMS_IT0602.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIMS_IT0602.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpPost]
        public IActionResult Login(User user)
        {
            // Đọc thông tin người dùng từ file users.json
            List<User> users = LoadUsersFromFile("users.json");
            var result = users.Find(u => u.UserName == user.UserName && u.Pass == user.Pass);

            if (result != null)
            {
                // Lưu thông tin người dùng vào session
                HttpContext.Session.SetString("UserName", result.UserName);
                HttpContext.Session.SetString("Role", result.Role);

                // Chuyển hướng đến trang tương ứng với vai trò của người dùng
                switch (result.Role)
                {
                    case "Admin":
                        return RedirectToAction("Index", "Admin");
                    case "Teacher":
                        return RedirectToAction("Index", "Teacher");
                    case "Student":
                        return RedirectToAction("Index", "Student");
                    default:
                        return RedirectToAction("Index", "Home"); // Chuyển hướng mặc định nếu không phân quyền
                }
            }
            else
            {
                // Thông báo lỗi nếu tài khoản không hợp lệ
                ViewBag.error = "Invalid user!";
                return View("Login");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            // Chuyển hướng đến trang đăng nhập
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public List<User>? LoadUsersFromFile(string fileName)
        {
            string readText = System.IO.File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<User>>(readText);
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            string role = Request.Form["Role"];

            // Assign the Role value to the user object
            user.Role = role;

            // Load existing users from the JSON file
            List<User> users = LoadUsersFromFile("users.json");

            // Check if the username already exists
            if (users.Any(u => u.UserName == user.UserName))
            {
                ViewBag.Error = "Username already exists!";
                return View("Register");
            }

            // Add the new user to the list of users
            users.Add(user);

            // Serialize the list of users back to JSON
            string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });

            // Write the updated JSON string back to the file
            System.IO.File.WriteAllText("users.json", jsonString);

            // Redirect to the Login page
            return RedirectToAction("Login");
        }

        [HttpGet] //click hyperlink
        public IActionResult Register()
        {
            return View();
        }
    }
}

