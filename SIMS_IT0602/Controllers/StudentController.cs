﻿using Microsoft.AspNetCore.Mvc;
using SIMS_IT0602.Models;
using System.Text.Json;

namespace SIMS_IT0602.Controllers
{
    public class StudentController : Controller
    {
        static List<Student> students = new List<Student>();
        public IActionResult Delete(int Id)
        {
            var students = LoadStudentFromFile("student.json");

            //find teacher in an array
            var searchStudent = students.FirstOrDefault(t => t.Id == Id);
            students.Remove(searchStudent);

            //save to file
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(students, options);
            //Save file
            using (StreamWriter writer = new StreamWriter("student.json"))
            {
                writer.Write(jsonString);
            }
            return RedirectToAction("ManageStudent");

        }
        public List<Student>? LoadStudentFromFile(string fileName)
        {
            string readText = System.IO.File.ReadAllText("student.json");
            return JsonSerializer.Deserialize<List<Student>>(readText);
        }
        public IActionResult ManageStudent()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            // Read a file
            students = LoadStudentFromFile("student.json");
            return View(students);
            // Trả về view Managestudent.cshtml
            // return View("Managestudent");//
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            var existingStudent = students.FirstOrDefault(t => t.Id == student.Id);
            if (existingStudent == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy giáo viên
            }
            existingStudent.Name = student.Name;
            existingStudent.DoB = student.DoB;
            existingStudent.Email = student.Email;
            existingStudent.Address = student.Address;
            existingStudent.Major = student.Major;

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(students, options);
            // Lưu thông tin mới vào file
            System.IO.File.WriteAllText("teacher.json", jsonString);
            // Chuyển hướng về trang quản lý giáo viên
            return RedirectToAction("ManageStudent");
        }
        [HttpPost]
        public IActionResult Save(Student student)
        {
            var existingStudent = students.FirstOrDefault(t => t.Id == student.Id);
            if (existingStudent == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy giáo viên
            }

            // Cập nhật thông tin giáo viên
            existingStudent.Name = student.Name;
            existingStudent.DoB = student.DoB;
            existingStudent.Email= student.Email;
            existingStudent.Address = student.Address;
            existingStudent.Major = student.Major;

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(students, options);

            // Lưu thông tin mới vào file
            System.IO.File.WriteAllText("student.json", jsonString);

            // Chuyển hướng về trang quản lý giáo viên
            return RedirectToAction("ManageStudent");
        }

        [HttpPost] //submit new Teacher
        public IActionResult NewStudent(Student student)
        {
            students.Add(student);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(students, options);
            //Save file
            using (StreamWriter writer = new StreamWriter("student.json"))
            {
                writer.Write(jsonString);
            }

            return RedirectToAction("ManageStudent", new { students = jsonString });
        }
        [HttpGet] //click hyperlink
        public IActionResult Save()
        {
            return View();
        }
        [HttpGet] //click hyperlink
        public IActionResult EditStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sinh viên
            }

            return View(student);

        }
        [HttpPost]
        public ActionResult Cancel(string returnUrl)
        {
            // Redirect to the ManageCourse action method
            return RedirectToAction("ManageStudent");
        }
        [HttpGet] //click hyperlink
        public IActionResult NewStudent()
        {
            return View();
        }

        public IActionResult ViewClass()
        {
            // Xử lý logic để hiển thị thông tin lớp học
            return View();
        }
        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            // Read a file
            students = LoadStudentFromFile("student.json");
            return View(students);
        }



    }
}
