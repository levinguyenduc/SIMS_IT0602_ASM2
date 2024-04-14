using Moq;
using SIMS_IT0602.Controllers;
using SIMS_IT0602.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc;
namespace TestProject1


{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var studentRepositoryMock = new Mock<IStudentRepository>();
            var studentService = new StudentService(studentRepositoryMock.Object);
            var newStudent = new Student
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                // Add other necessary student details
            };

            // Act
            var result = studentService.RegisterStudent(newStudent);

            // Assert
            Assert.IsTrue(result.Success);

        }
    }
}