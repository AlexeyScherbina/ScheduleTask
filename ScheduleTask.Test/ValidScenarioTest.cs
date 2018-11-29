﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleTask.BLL.DTO;
using ScheduleTask.BLL.Services;
using ScheduleTask.DAL;
using ScheduleTask.DAL.Repositories;
using Task1.Controllers;
using Task1.Models;

namespace ScheduleTask.Test
{
    [TestClass]
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

        [TestMethod]
        public void AddTask_NewTask_Added()
        {
            var result = tc.AddTask(new TaskViewModel()
            {
                Name = "TEST_TASK"
            });
            Assert.IsTrue(result is OkResult);

            IEnumerable<TaskViewModel> t = tc.GetTasks();
            var task = t.FirstOrDefault();
            
            Assert.AreEqual("TEST_TASK", task.Name);
        }

        [TestMethod]
        public void AddUser_NewUser_Added()
        {
            var result = uc.AddUser(new UserViewModel()
            {
                FullName = "TEST_USER"
            });
            Assert.IsTrue(result is OkResult);

            IEnumerable<UserViewModel> u = uc.GetUsers();
            var user = u.FirstOrDefault();

            Assert.AreEqual("TEST_USER", user.FullName);
        }

        [TestMethod]
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
            Assert.IsTrue(result is OkResult);

            t = tc.GetTasks();
            task = t.FirstOrDefault();

            Assert.AreEqual("TEST_USER", task.User.FullName);
        }

        [TestMethod]
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
            Assert.IsTrue(result is OkResult);

            t = tc.GetTasks();
            task = t.FirstOrDefault();

            Assert.AreEqual("Monday", task.Day);
        }

        [TestMethod]
        public void DeleteTask_AllTasks_TasksDeleted()
        {
            IEnumerable<TaskViewModel> t = tc.GetTasks();

            foreach (var task in t)
            {
                tc.DeleteTask(task.TaskId);
            }

            t = tc.GetTasks();

            Assert.AreEqual(0, t.Count());
        }

        [TestMethod]
        public void DeleteUser_AllUsers_UsersDeleted()
        {
            IEnumerable<UserViewModel> u = uc.GetUsers();

            foreach (var user in u)
            {
                uc.DeleteUser(user.UserId);
            }

            u = uc.GetUsers();

            Assert.AreEqual(0, u.Count());
        }
    }
}
