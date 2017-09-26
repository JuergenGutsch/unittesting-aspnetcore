using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using WebApiDemo.Controllers;
using WebApiDemo.Models;
using Xunit;
using FluentAssertions;
using WebApiDemo.Services;
using System;

namespace WebApiDemo.Tests
{
    public class PersonsControllerUnitTests
    {
        [Fact]
        public async Task Persons_Get_All()
        {
            // Arrange
            var controller = new PersonsController(new PersonService());

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var persons = okResult.Value.Should().BeAssignableTo<IEnumerable<Person>>().Subject;

            persons.Count().Should().Be(50);
        }

        [Fact]
        public async Task Persons_Get_Specific()
        {
            // Arrange
            var controller = new PersonsController(new PersonService());

            // Act
            var result = await controller.Get(16);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var person = okResult.Value.Should().BeAssignableTo<Person>().Subject;
            person.Id.Should().Be(16);
        }

        [Fact]
        public async Task Persons_Add()
        {
            // Arrange
            var controller = new PersonsController(new PersonService());
            var newPerson = new Person
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 50,
                Title = "FooBar",
                Email = "john.doe@foo.bar"
            };

            // Act
            var result = await controller.Post(newPerson);

            // Assert
            var okResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            var person = okResult.Value.Should().BeAssignableTo<Person>().Subject;
            person.Id.Should().Be(51);
        }

        [Fact]
        public async Task Persons_Change()
        {
            // Arrange
            var service = new PersonService();
            var controller = new PersonsController(service);
            var newPerson = new Person
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 50,
                Title = "FooBar",
                Email = "john.doe@foo.bar"
            };

            // Act
            var result = await controller.Put(20, newPerson);

            // Assert
            var okResult = result.Should().BeOfType<NoContentResult>().Subject;

            var person = service.Get(20);
            person.Id.Should().Be(20);
            person.FirstName.Should().Be("John");
            person.LastName.Should().Be("Doe");
            person.Age.Should().Be(50);
            person.Title.Should().Be("FooBar");
            person.Email.Should().Be("john.doe@foo.bar");
        }

        [Fact]
        public async Task Persons_Delete()
        {
            // Arrange
            var service = new PersonService();
            var controller = new PersonsController(service);

            // Act
            var result = await controller.Delete(20);

            // Assert
            var okResult = result.Should().BeOfType<NoContentResult>().Subject;
            // should throw an eception, 
            // because the person with id==20 doesn't exist enymore
            AssertionExtensions.ShouldThrow<InvalidOperationException>(
                () => service.Get(20));
        }
    }
}

