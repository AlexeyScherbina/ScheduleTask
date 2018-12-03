using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using NUnit.Framework;
using ScheduleTask.BLL.Services;
using ScheduleTask.DAL;
using ScheduleTask.DAL.Repositories;
using Task1.Controllers;
using Task1.Models;

namespace ScheduleTask.Test
{
    [TestFixture]
    public class InvalidScenarioTest
    {

        private TaskController tc;
        private UserController uc;

        public InvalidScenarioTest()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            tc = new TaskController(new TaskService(new TaskRepository(db), new UserRepository(db)));
            uc = new UserController(new UserService(new UserRepository(db)));
        }

        [Test]
        public void AddTask_Null_BadRequest()
        {
            var result = tc.AddTask(null);
            (result is BadRequestErrorMessageResult).ShouldBe(true);
        }
        [Test]
        public void AddUser_Null_BadRequest()
        {
            var result = uc.AddUser(null);
            (result is BadRequestErrorMessageResult).ShouldBe(true);
        }
        [Test]
        public void AssignUser_UnexistingIds_ExceptionResult()
        {
            var result = tc.AssignUser(new AssignViewModel()
            {
                TaskId = 0,
                UserId = 0
            });
            (result is ExceptionResult).ShouldBe(true);
        }
        [Test]
        public void AssignUser_Null_BadRequest()
        {
            var result = tc.AssignUser(null);
            (result is BadRequestErrorMessageResult).ShouldBe(true);
        }
        [Test]
        public void AssignDay_Null_BadRequest()
        {
            var result = tc.AssignDay(null);
            (result is BadRequestErrorMessageResult).ShouldBe(true);
        }
        [Test]
        public void DeleteTask_UnexistingId_ExceptionResult()
        {
            var result = tc.DeleteTask(0);
            (result is ExceptionResult).ShouldBe(true);
        }
        [Test]
        public void DeleteUser_UnexistingId_ExceptionResult()
        {
            var result = uc.DeleteUser(0);
            (result is ExceptionResult).ShouldBe(true);
        }

    }
}
