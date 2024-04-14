using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIMS_IT0602.Controllers;
using SIMS_IT0602.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Xunit;

namespace SIMS_TEST
{

    public class StudentControllerTests
    {
        [Fact]
        public void TestDeleteStudent()
        {
            // Arrange
            var controller = new StudentController();
            var idToDelete = 1;

            // Act
            var result = controller.Delete(idToDelete) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ManageStudent", result.ActionName);
        }

        [Fact]
        public void TestLoadStudentFromFile()
        {
            // Arrange
            var controller = new StudentController();
            var fileName = "student.json";
            var studentsJson = "[{\"Id\":1,\"Name\":\"hoan\", \"DoB\": \"2004-090-25T00:00:00\",\"\"}]";
            System.IO.File.WriteAllText(fileName, studentsJson);
            // Act
            var result = controller.LoadStudentFromFile(fileName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Hoan Dinh", result[0].Name);
            Assert.Equal("Tam Nguyen", result[1].Name);
        }

    }
    public class AuthenticationControllerTests
    {
    

        [Fact]
        public void TestLogin_InvalidUser_ReturnsErrorView()
        {
            // Arrange
            var controller = new AuthenticationController();
            var user = new User { UserName = "invalid", Pass = "invalid" };

            // Act
            var result = controller.Login(user) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Login", result.ViewName);
            Assert.Equal("Invalid user!", result.ViewData["error"]);
        }

    }
    public class ClassControllerTests
    {
        [Fact]
        public void TestDeleteClass()
        {
            // Arrange
            var controller = new ClassController();
            var idToDelete = 1;

            // Act
            var result = controller.Delete(idToDelete) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ManageClass", result.ActionName);
        }

        [Fact]
        public void TestLoadClassFromFile()
        {
            // Arrange
            var controller = new ClassController();
            var fileName = "class.json";
            var classesJson = "[{\"Id\":1,\"Name\":\"Class A\",\"Major\":\"Computer Science\",\"Lecturer\":\"John Doe\"},{\"Id\":2,\"Name\":\"Class B\",\"Major\":\"Electrical Engineering\",\"Lecturer\":\"Jane Doe\"}]";
            System.IO.File.WriteAllText(fileName, classesJson);

            // Act
            var result = controller.LoadClassFromFile(fileName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Class A", result[0].ClassName);
            Assert.Equal("Class B", result[1].ClassName);
        }
    }
}

