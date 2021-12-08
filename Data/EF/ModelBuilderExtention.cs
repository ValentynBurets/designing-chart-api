using Domain.Entity;
using Domain.Entity.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.EF
{
    public static class ModelBuilderExtention
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            var categories = new CategoryType[]{
                new CategoryType
                {
                    Id = Guid.NewGuid(),
                    Name = "CategoryType"
                },
                new CategoryType
                {
                    Id = Guid.NewGuid(),
                    Name = "SecondCategoryType"
                } 
            };
            var exercises = new Exercise[]
            {
                new Exercise
                {
                    Id = Guid.NewGuid(),
                    Title = "exercise title",
                    Description = "description for exercise",
                    MaxMark = 100,
                    ExpirationDate = new DateTime(2000, 10, 6),
                    StatusType = StatusType.Active,
                    CategoryId = categories[0].Id,
                    EtalonChart = "test chart"
                },
                new Exercise
                {
                    Id = Guid.NewGuid(),
                    Title = "second exercise title",
                    Description = "second description for exercise",
                    MaxMark = 200,
                    ExpirationDate = new DateTime(2042, 10, 11),
                    StatusType = StatusType.Expired,
                    CategoryId = categories[1].Id,
                    EtalonChart = "second test chart"
                }
            };
            
            var attempts = new Attempt[]
            {
                new Attempt()
                {
                    Id = Guid.NewGuid(),
                    StartTime = new DateTime(2033, 5, 21, 8, 30, 52),
                    FinishTime = new DateTime(2033, 5, 21, 9, 30, 00),
                    Mark = 32,
                    ExerciseId = exercises[0].Id,
                    StudentId = new Guid("1a32c7da-b8df-49eb-76e5-08d9b65ced36"),
                    Chart = "created chart from student"
                },
                new Attempt()
                {
                    Id = Guid.NewGuid(),
                    StartTime = new DateTime(2032, 5, 21, 8, 30, 52),
                    FinishTime = new DateTime(2032, 5, 21, 9, 30, 00),
                    Mark = 42,
                    ExerciseId = exercises[0].Id,
                    StudentId = new Guid("1a32c7da-b8df-49eb-76e5-08d9b65ced36"),
                    Chart = "created 2 chart from student"
                },
                new Attempt()
                {
                    Id = Guid.NewGuid(),
                    StartTime = new DateTime(2033, 5, 11, 9, 25, 35),
                    FinishTime = new DateTime(2033, 5, 11, 10, 30, 52),
                    Mark = 45,
                    ExerciseId = exercises[1].Id,
                    StudentId = new Guid("1a32c7da-b8df-49eb-76e5-08d9b65ced36"),
                    Chart = "created chart from student"
                },
                new Attempt()
                {
                    Id = Guid.NewGuid(),
                    StartTime = new DateTime(2033, 6, 25, 6, 32, 53),
                    FinishTime = new DateTime(2033, 6, 25, 7, 12, 23),
                    Mark = 32,
                    ExerciseId = exercises[0].Id,
                    StudentId = new Guid("cd6c466a-6b9d-414b-6394-08d9ba292180"),
                    Chart = "created chart from student"
                }
            };
            
            modelBuilder.Entity<CategoryType>()
                .HasData(categories);

            modelBuilder.Entity<Exercise>()
                .HasData(exercises);

            modelBuilder.Entity<Attempt>()
                .HasData(attempts);
        }
    }
}
