using AutoMapper;
using Business.Contract.Model;
using Business.Services;
using Data.Contract.UnitOfWork;
using Data.Repository;
using designing_chart_api.Configurations;
using Domain.Entity;
using Domain.Entity.Constants;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessTests
{
    public class ServiceTests
    {
        private static IMapper mapper = new Mapper(new MapperConfiguration(cnf => cnf.AddProfile(new MapperInitializer())));

        static List<CategoryType> categories = new List<CategoryType>
        {
            new CategoryType
            {
                Id = new Guid("c49db605-95ac-449a-9aba-348569e5a852"),
                Name = "CategoryType"
            },
            new CategoryType
            {
                Id = new Guid("c08ad180-b5b1-4b6e-8daf-f7231982424e"),
                Name = "SecondCategoryType"
            }
        };

        static List<Student> students = new List<Student>
        {
            new Student
            {
                Id = new Guid("dc578c85-56ac-47d4-a808-64b3ece9f0d2"),
                Name = "Ivan",
                SurName = "Ivanov",
                Password = "password",
                Email = "Ivan.Ivanov@gmail.com"
            },
            new Student
            {
                Id = new Guid("dd3311b1-93a9-4146-a828-e1d7837f6528"),
                Name = "Petro",
                SurName = "Petrov",
                Password = "password2",
                Email = "Petro.Petrov@gmail.com"
            }
        };

        static List<Exercise> exercises = new List<Exercise>
        {
            new Exercise
            {
                  Id = new Guid("bb38641a-21c5-481f-8053-d91ab46fbb27"),
                  Title = "exercise title",
                  Description = "description for exercise",
                  MaxMark = 100,
                  ExpirationDate = new DateTime(2033, 10, 6),
                  StatusType = StatusType.Active,
                  CategoryId = categories[0].Id,
                  CategoryType = categories[0],
                  EtalonChart = "test chart"
            },
            new Exercise
            {
                  Id = new Guid("b1962eca-acdf-4f5f-83e7-e814b53d8694"),
                  Title = "second exercise title",
                  Description = "second description for exercise",
                  MaxMark = 200,
                  ExpirationDate = new DateTime(2042, 5, 1),
                  StatusType = StatusType.Expired,
                  CategoryId = categories[1].Id,
                  CategoryType = categories[1],
                  EtalonChart = "second test chart"
            }
        };

        private static List<Attempt> attempts = new List<Attempt>
        {
            new Attempt()
            {
                Id = new Guid("bf89be71-3540-48df-b991-fc8066eb97a0"),
                StartTime = new DateTime(2033, 5, 21, 8, 30, 52),
                FinishTime = new DateTime(2033, 6, 30, 9, 30, 00),
                Mark = 32,
                ExerciseId =  exercises[0].Id,
                Exercise =  exercises[0],
                StudentId = students[0].Id,
                Student = students[0],
                Chart = "created chart from student"
            },
            new Attempt()
            {
                Id = new Guid("9fa01d5e-b130-4dee-aaec-d39554e9a686"),
                StartTime = new DateTime(2033, 5, 11, 9, 25, 35),
                FinishTime = new DateTime(2033, 5, 20, 10, 30, 52),
                Mark = 45,
                ExerciseId =  exercises[1].Id,
                Exercise =  exercises[1],
                StudentId = students[1].Id,
                Student = students[1],
                Chart = "created chart from student"
            }
        };

        private static List<Attempt> studentAttempts = new List<Attempt>
        {
            new Attempt()
            {
                Id = new Guid("ef1c8f5d-3062-48eb-bfa8-77697af25f42"),
                StartTime = new DateTime(2033, 6, 25, 6, 32, 53),
                FinishTime = new DateTime(2033, 6, 25, 7, 12, 23),
                Mark = 32,
                ExerciseId =  exercises[0].Id,
                Exercise =  exercises[0],
                StudentId = students[0].Id,
                Student = students[0],
                Chart = "created chart from student"
            }
        };


        [Fact]
        public async void CreateAttempt()
        {
            var test = attempts;
            // Arrange
            var new_attempt = mapper.Map<CreateAttemptViewModel>(attempts[1]);

            //var mock = new Mock<IExerciseUnitOfWork>();
            var attemptServiceRepositoryStub = new Mock<AttemptRepository>();

            attemptServiceRepositoryStub.Setup(obj => obj.GetByStudentId(students[0].Id))
                .ReturnsAsync(studentAttempts);

            attemptServiceRepositoryStub.Setup(obj => obj.GetByExerciseId(exercises[0].Id))
                .ReturnsAsync(attempts);

            //unit of work initialization
            var exerciseUnitOfWorkStub = new Mock<IExerciseUnitOfWork>();
            exerciseUnitOfWorkStub.Setup(obj => obj.AttemptRepository)
                .Returns(attemptServiceRepositoryStub.Object);

            var attemptService = new AttemptService(mapper, exerciseUnitOfWorkStub.Object);

            // Act
            var result = attemptService.Create(new_attempt);

            //Assert
            Assert.NotNull(result);
        }
    }
}
