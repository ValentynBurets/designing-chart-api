using AutoMapper;
using Business.Contract.Model;
using Business.Services;
using Data.Contract.UnitOfWork;
using Data.Repository;
using designing_chart_api.Configurations;
using designing_chart_api.Controllers;
using Domain.Entity;
using Domain.Entity.Constants;
using Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            new Student(new Guid())
            {
                Id = new Guid("dc578c85-56ac-47d4-a808-64b3ece9f0d2"),
                Name = "Ivan",
                SurName = "Ivanov"
            },
            new Student(new Guid())
            {
                Id = new Guid("dd3311b1-93a9-4146-a828-e1d7837f6528"),
                Name = "Petro",
                SurName = "Petrov"
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
                  ExpirationDate = new DateTime(2000, 10, 6),
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
                  ExpirationDate = new DateTime(2042, 10, 11),
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
                FinishTime = new DateTime(2033, 5, 21, 9, 30, 00),
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
                FinishTime = new DateTime(2033, 5, 11, 10, 30, 52),
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
        public void CreateAttempt()
        {
            // Arrange
            var new_attempt = mapper.Map<CreateAttemptViewModel>(attempts[1]);

            var attemptServiceRepositoryStub = new Mock<IAttemptRepository>();

            attemptServiceRepositoryStub.Setup(obj => obj.GetByStudentId(students[1].Id))
                .ReturnsAsync(studentAttempts);

            attemptServiceRepositoryStub.Setup(obj => obj.GetByExerciseId(exercises[1].Id))
                .ReturnsAsync(attempts);

            var exerciseRepositoryStub = new Mock<IExerciseRepository>();

            exerciseRepositoryStub.Setup(obj => obj.GetById(new_attempt.ExerciseId))
                .ReturnsAsync(exercises[1]);

            var studentRepositoryStub = new Mock<IStudentRepository>();

            studentRepositoryStub.Setup(obj => obj.GetById(new_attempt.StudentId))
                .ReturnsAsync(students[1]);

            //unit of work initialization
            var exerciseUnitOfWorkStub = new Mock<IExerciseUnitOfWork>();
            exerciseUnitOfWorkStub.Setup(obj => obj.AttemptRepository)
                .Returns(attemptServiceRepositoryStub.Object);

            exerciseUnitOfWorkStub.Setup(obj => obj.ExerciseRepository)
                .Returns(exerciseRepositoryStub.Object);

            exerciseUnitOfWorkStub.Setup(obj => obj.StudentRepository)
                .Returns(studentRepositoryStub.Object);

            var attemptService = new AttemptService(mapper,exerciseUnitOfWorkStub.Object);

            // Act
            var result = attemptService.Create(new_attempt);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void CreateAttemptWithoutStudentIdTest()
        {
            // Arrange
            var new_attempt = mapper.Map<CreateAttemptViewModel>(attempts[0]);
            new_attempt.StudentId = Guid.Empty;

            var attemptServiceRepositoryStub = new Mock<IAttemptRepository>();

            attemptServiceRepositoryStub.Setup(obj => obj.GetByStudentId(students[0].Id))
                .ReturnsAsync(studentAttempts);

            attemptServiceRepositoryStub.Setup(obj => obj.GetByExerciseId(exercises[1].Id))
                .ReturnsAsync(attempts);

            var exerciseRepositoryStub = new Mock<IExerciseRepository>();

            exerciseRepositoryStub.Setup(obj => obj.GetById(new_attempt.ExerciseId))
                .ReturnsAsync(exercises[1]);

            exerciseRepositoryStub.Setup(obj => obj.GetExpirationDateByExerciseId(new_attempt.ExerciseId))
                .ReturnsAsync(exercises[1].ExpirationDate);

            var studentRepositoryStub = new Mock<IStudentRepository>();

            studentRepositoryStub.Setup(obj => obj.Contains(new_attempt.StudentId))
                .ReturnsAsync(true);

            //unit of work initialization
            var exerciseUnitOfWorkStub = new Mock<IExerciseUnitOfWork>();
            exerciseUnitOfWorkStub.Setup(obj => obj.AttemptRepository)
                .Returns(attemptServiceRepositoryStub.Object);

            exerciseUnitOfWorkStub.Setup(obj => obj.ExerciseRepository)
                .Returns(exerciseRepositoryStub.Object);

            exerciseUnitOfWorkStub.Setup(obj => obj.StudentRepository)
                .Returns(studentRepositoryStub.Object);

            var attemptService = new AttemptService(unitOfWork: exerciseUnitOfWorkStub.Object, mapper:mapper);

            // Act
            var result = attemptService.Create(new_attempt);

            //Assert
            Assert.NotNull(result);
            var expected = "Student with this Id don`t exists";
            Assert.Equal(expected, result.Exception.InnerException.Message);
        }

        [Fact]
        public void CreateAttemptForExpiredExercise()
        {
            // Arrange
            var new_attempt = mapper.Map<CreateAttemptViewModel>(attempts[0]);

            var attemptServiceRepositoryStub = new Mock<IAttemptRepository>();

            attemptServiceRepositoryStub.Setup(obj => obj.GetByStudentId(students[0].Id))
                .ReturnsAsync(studentAttempts);

            attemptServiceRepositoryStub.Setup(obj => obj.GetByExerciseId(exercises[0].Id))
                .ReturnsAsync(attempts);

            var exerciseRepositoryStub = new Mock<IExerciseRepository>();

            exerciseRepositoryStub.Setup(obj => obj.GetById(new_attempt.ExerciseId))
                .ReturnsAsync(exercises[0]);

            var studentRepositoryStub = new Mock<IStudentRepository>();

            studentRepositoryStub.Setup(obj => obj.GetById(new_attempt.StudentId))
                .ReturnsAsync(students[0]);

            //unit of work initialization
            var exerciseUnitOfWorkStub = new Mock<IExerciseUnitOfWork>();
            exerciseUnitOfWorkStub.Setup(obj => obj.AttemptRepository)
                .Returns(attemptServiceRepositoryStub.Object);

            exerciseUnitOfWorkStub.Setup(obj => obj.ExerciseRepository)
                .Returns(exerciseRepositoryStub.Object);

            exerciseUnitOfWorkStub.Setup(obj => obj.StudentRepository)
                .Returns(studentRepositoryStub.Object);

            var attemptService = new AttemptService(mapper, exerciseUnitOfWorkStub.Object);

            // Act
            var result = attemptService.Create(new_attempt);

            //Assert
            Assert.NotNull(result);
            var expected = "One or more errors occurred. (The student started this exercise too late. solving started later than expiration time allowed.)";
            Assert.Equal(expected, result.Exception.Message);
        }

        [Fact]
        public void CreateExercise()
        {
            // Arrange

            var exercise = exercises[1];
            var new_exercise = mapper.Map<CreateExerciseViewModel>(exercise);
            new_exercise.Category = exercise.CategoryType.Name;

            var exerciseRepositoryStub = new Mock<IExerciseRepository>();

            exerciseRepositoryStub.Setup(obj => obj.Contains(exercise.Title))
                .ReturnsAsync(false);
            exerciseRepositoryStub.Setup(obj => obj.Contains(exercise))
                .ReturnsAsync(false);

            var categoryRepository = new Mock<ICategoryRepository>();
            categoryRepository.Setup(obj => obj.GetByCategoryName(exercise.CategoryType.Name))
                .ReturnsAsync(exercise.CategoryType);

            //unit of work initialization
            var exerciseUnitOfWorkStub = new Mock<IExerciseUnitOfWork>();
            exerciseUnitOfWorkStub.Setup(obj => obj.CategoryRepository)
                .Returns(categoryRepository.Object);

            exerciseUnitOfWorkStub.Setup(obj => obj.ExerciseRepository)
                .Returns(exerciseRepositoryStub.Object);

            var exerciseService = new ExerciseService(exerciseUnitOfWorkStub.Object, mapper);

            // Act
            var result = exerciseService.Create(new_exercise);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Exception == null);
        }

        [Fact]
        public void CreateExerciseWithExistedTitle()
        {
            // Arrange

            var exercise = exercises[1];
            var new_exercise = mapper.Map<CreateExerciseViewModel>(exercise);
            new_exercise.Category = exercise.CategoryType.Name;

            var exerciseRepositoryStub = new Mock<IExerciseRepository>();

            exerciseRepositoryStub.Setup(obj => obj.Contains(exercise.Title))
                .ReturnsAsync(true);
            exerciseRepositoryStub.Setup(obj => obj.Contains(exercise))
                .ReturnsAsync(false);

            var categoryRepository = new Mock<ICategoryRepository>();
            categoryRepository.Setup(obj => obj.GetByCategoryName(exercise.CategoryType.Name))
                .ReturnsAsync(exercise.CategoryType);

            //unit of work initialization
            var exerciseUnitOfWorkStub = new Mock<IExerciseUnitOfWork>();
            exerciseUnitOfWorkStub.Setup(obj => obj.CategoryRepository)
                .Returns(categoryRepository.Object);

            exerciseUnitOfWorkStub.Setup(obj => obj.ExerciseRepository)
                .Returns(exerciseRepositoryStub.Object);

            var exerciseService = new ExerciseService(exerciseUnitOfWorkStub.Object, mapper);

            // Act
            var result = exerciseService.Create(new_exercise);

            //Assert
            Assert.NotNull(result);
            var expected = "One or more errors occurred. (Exercise with this title exists)";
            Assert.Equal(expected, result.Exception.Message);
        }

        [Fact]
        public void CreateExerciseWithoutEtalonChart()
        {
            // Arrange
            var exercise = exercises[1];
            var new_exercise = mapper.Map<CreateExerciseViewModel>(exercise);

            //in new exercise set etalon Chart to null
            new_exercise.EtalonChart = null;

            new_exercise.Category = exercise.CategoryType.Name;

            var exerciseRepositoryStub = new Mock<IExerciseRepository>();

            exerciseRepositoryStub.Setup(obj => obj.Contains(exercise.Title))
                .ReturnsAsync(false);
            exerciseRepositoryStub.Setup(obj => obj.Contains(exercise))
                .ReturnsAsync(false);

            var categoryRepository = new Mock<ICategoryRepository>();
            categoryRepository.Setup(obj => obj.GetByCategoryName(exercise.CategoryType.Name))
                .ReturnsAsync(exercise.CategoryType);

            //unit of work initialization
            var exerciseUnitOfWorkStub = new Mock<IExerciseUnitOfWork>();
            exerciseUnitOfWorkStub.Setup(obj => obj.CategoryRepository)
                .Returns(categoryRepository.Object);

            exerciseUnitOfWorkStub.Setup(obj => obj.ExerciseRepository)
                .Returns(exerciseRepositoryStub.Object);

            var exerciseService = new ExerciseService(exerciseUnitOfWorkStub.Object, mapper);

            // Act
            var result = exerciseService.Create(new_exercise);

            //Assert
            Assert.NotNull(result);
            var expected = "One or more errors occurred. (Etalon chart don't exists)";
            Assert.Equal(expected, result.Exception.Message);
        }

        [Fact]
        public void CreateExerciseWithoutCategory()
        {
            // Arrange

            var exercise = exercises[1];
            var new_exercise = mapper.Map<CreateExerciseViewModel>(exercise);

            var exerciseRepositoryStub = new Mock<IExerciseRepository>();

            exerciseRepositoryStub.Setup(obj => obj.Contains(exercise.Title))
                .ReturnsAsync(false);
            exerciseRepositoryStub.Setup(obj => obj.Contains(exercise))
                .ReturnsAsync(false);

            var categoryRepository = new Mock<ICategoryRepository>();
            categoryRepository.Setup(obj => obj.GetByCategoryName(exercise.CategoryType.Name))
                .ReturnsAsync(exercise.CategoryType);

            //unit of work initialization
            var exerciseUnitOfWorkStub = new Mock<IExerciseUnitOfWork>();
            exerciseUnitOfWorkStub.Setup(obj => obj.CategoryRepository)
                .Returns(categoryRepository.Object);

            exerciseUnitOfWorkStub.Setup(obj => obj.ExerciseRepository)
                .Returns(exerciseRepositoryStub.Object);

            var exerciseService = new ExerciseService(exerciseUnitOfWorkStub.Object, mapper);

            // Act
            var result = exerciseService.Create(new_exercise);

            //Assert
            Assert.NotNull(result);
            var expected = "One or more errors occurred. (Category is null)";
            Assert.Equal(expected, result.Exception.Message);
        }

        [Fact]
        public void GetAllExercises()
        {
            // Arrange

            var exerciseRepositoryStub = new Mock<IExerciseRepository>();

            exerciseRepositoryStub.Setup(obj => obj.GetAll())
                .ReturnsAsync(exercises);

            //unit of work initialization
            var exerciseUnitOfWorkStub = new Mock<IExerciseUnitOfWork>();
            exerciseUnitOfWorkStub.Setup(obj => obj.ExerciseRepository)
                .Returns(exerciseRepositoryStub.Object);

            var exerciseService = new ExerciseService(exerciseUnitOfWorkStub.Object, mapper);

            // Act
            var result = exerciseService.GetAll().Result;

            //Assert

            var resultList = result.ToList();

            Assert.NotNull(result);
            Assert.Equal(exercises.Count(), result.Count());

            Assert.Equal(exercises[0].Description, resultList[0].Description);

            Assert.Equal(exercises[0].ExpirationDate, resultList[0].ExpirationDate);

            Assert.Equal(exercises[0].Title, resultList[0].Title);
        }

        [Fact]
        public void DeleteExerciseTest()
        {
            // Arrange

            var exerciseRepositoryStub = new Mock<IExerciseRepository>();

            exerciseRepositoryStub.Setup(obj => obj.GetById(exercises[0].Id))
                .ReturnsAsync(exercises[0]);

            //unit of work initialization
            var exerciseUnitOfWorkStub = new Mock<IExerciseUnitOfWork>();
            exerciseUnitOfWorkStub.Setup(obj => obj.ExerciseRepository)
                .Returns(exerciseRepositoryStub.Object);

            var exerciseService = new ExerciseService(exerciseUnitOfWorkStub.Object, mapper);

            // Act
            var result = exerciseService.Delete(exercises[0].Id);

            //Assert
            //Mock.Assert(() => exerciseRepositoryStub.Submit(), Occurs.Never());
            Assert.NotNull(result);

        }

        //exercise controller tests
        [Fact]
        public void ExericiseControllerGetAllTest()
        {
            // Arrange
                //initialization
                
                var exerciseRepositoryStub = new Mock<IExerciseRepository>();

                exerciseRepositoryStub.Setup(obj => obj.GetAll())
                    .ReturnsAsync(exercises);

                //unit of work initialization
                var exerciseUnitOfWorkStub = new Mock<IExerciseUnitOfWork>();
                exerciseUnitOfWorkStub.Setup(obj => obj.ExerciseRepository)
                    .Returns(exerciseRepositoryStub.Object);

                var exerciseService = new ExerciseService(exerciseUnitOfWorkStub.Object, mapper);

                //end of initialization

                var controller = new ExercisesController(exerciseService);

                 // Act
                var result = controller.GetAll();

            // Assert

            var viewResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result.Result);
            var test_model = viewResult.Value;

            var model = Assert.IsAssignableFrom<IEnumerable<GetExerciseViewModel>>(
                viewResult.Value);

           
            var resultList = model.ToList();

            Assert.NotNull(model);
            Assert.Equal(exercises.Count(), model.Count());

            Assert.Equal(exercises[0].Description, resultList[0].Description);

            Assert.Equal(exercises[0].ExpirationDate, resultList[0].ExpirationDate);

            Assert.Equal(exercises[0].Title, resultList[0].Title);
        }
    }
}
