using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using NUnit.Framework;
using ScheduleTask.BLL.DTO;
using ScheduleTask.BLL.Services;
using ScheduleTask.DAL;
using ScheduleTask.DAL.Repositories;
using Task1.Controllers;
using Task1.Models;

namespace ScheduleTask.Test
{
    [TestFixture]
    public class ValidScenarioTest
    {
        private TaskController tc;
        private UserController uc;

        public ValidScenarioTest()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            tc = new TaskController(new TaskService(new TaskRepository(db), new UserRepository(db)));
            uc = new UserController(new UserService(new UserRepository(db)));
        }

        [Test]
        public void AddTask_NewTask_Added()
        {
            var result = tc.AddTask(new TaskViewModel()
            {
                Name = "TEST_TASK"
            });
            (result is OkResult).ShouldBe(true);

            IEnumerable<TaskViewModel> t = tc.GetTasks();
            var task = t.FirstOrDefault();

            task.Name.ShouldBe("TEST_TASK");
        }

        [Test]
        public void AddUser_NewUser_Added()
        {
            var result = uc.AddUser(new UserViewModel()
            {
                FullName = "TEST_USER"
            });
            (result is OkResult).ShouldBe(true);

            IEnumerable<UserViewModel> u = uc.GetUsers();
            var user = u.FirstOrDefault();

            user.FullName.ShouldBe("TEST_USER");
        }

        [Test]
        public void AssignUser_ExistentUserAndTask_Assigned()
        {
            IEnumerable<UserViewModel> u = uc.GetUsers();
            var user = u.FirstOrDefault();
            IEnumerable<TaskViewModel> t = tc.GetTasks();
            var task = t.FirstOrDefault();

            var result = tc.AssignUser(new AssignViewModel()
            {
                TaskId = task.TaskId,
                UserId = user.UserId
            });
            (result is OkResult).ShouldBe(true);

            t = tc.GetTasks();
            task = t.FirstOrDefault();

            task.User.FullName.ShouldBe("TEST_USER");
        }

        [Test]
        public void AssignDay_ExistentTaskAndDay_Assigned()
        {
            IEnumerable<TaskViewModel> t = tc.GetTasks();
            var task = t.FirstOrDefault();
            string day = "Monday";

            var result = tc.AssignDay(new TaskViewModel()
            {
                TaskId = task.TaskId,
                Name = task.Name,
                Description = task.Description,
                Day = day,
                User = task.User
            });
            (result is OkResult).ShouldBe(true);

            t = tc.GetTasks();
            task = t.FirstOrDefault();

            task.Day.ShouldBe("Monday");
        }

        [Test]
        public void DeleteTask_AllTasks_TasksDeleted()
        {
            IEnumerable<TaskViewModel> t = tc.GetTasks();

            foreach (var task in t)
            {
                tc.DeleteTask(task.TaskId);
            }

            t = tc.GetTasks();

            t.Count().ShouldBe(0);
        }

        [Test]
        public void DeleteUser_AllUsers_UsersDeleted()
        {
            IEnumerable<UserViewModel> u = uc.GetUsers();

            foreach (var user in u)
            {
                uc.DeleteUser(user.UserId);
            }

            u = uc.GetUsers();

            u.Count().ShouldBe(0);
        }
    }
}
