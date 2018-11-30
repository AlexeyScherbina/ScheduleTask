﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http.Results;
using MvcContrib.TestHelper;
using Newtonsoft.Json;
using NUnit.Framework;
using Task1.Models;

namespace ScheduleTask.IntegrationTest
{
    [TestFixture]
    public class WebApiTest
    {
        private readonly HttpClient client;

        private string token;

        public class TokenResult
        {
            public string access_token { get; set; }
        }

        public WebApiTest()
        {
            client = new HttpClient();
        }

        [Test, Order(1)]
        public async Task Register()
        {
            var values = new Dictionary<string, string>
            {
                { "Email", "user1@user.com" },
                { "Password", "User1!" },
                { "ConfirmPassword", "User1!" }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://localhost:50029/api/Account/Register", content);
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.ShouldBe("{\"Message\":\"The request is invalid.\",\"ModelState\":{\"\":[\"Name user1@user.com is already taken.\"]}}");
        }

        [Test, Order(2)]
        public async Task Token()
        {
            var values = new Dictionary<string, string>
            {
                { "username", "user1@user.com" },
                { "password", "User1!" },
                { "grant_type", "password" }
            };
            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://localhost:50029/token", content);
            var responseString = await response.Content.ReadAsStringAsync();

            token = JsonConvert.DeserializeObject<TokenResult>(responseString).access_token;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            token.ShouldNotBeNull(token);
        }

        [Test, Order(3)]
        public async Task GetTasks()
        {
            var response = await client.GetStringAsync("http://localhost:50029/api/Task/GetTasks");
            var tasks =  JsonConvert.DeserializeObject<IEnumerable<TaskViewModel>>(response);
            (tasks != null).ShouldBe(true);
        }

        [Test, Order(4)]
        public async Task AddTask()
        {
            var values = new Dictionary<string, string>
            {
                { "Name", "TASK_TEST" }
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://localhost:50029/api/Task/AddTask",content);
            response.StatusCode.ToString().ShouldBe("OK");
        }
        [Test, Order(5)]
        public async Task AddUser()
        {
            var values = new Dictionary<string, string>
            {
                { "FullName", "USER_TEST" }
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://localhost:50029/api/User/AddUser", content);
            response.StatusCode.ToString().ShouldBe("OK");
        }
        [Test, Order(6)]
        public async Task AssignDay()
        {
            var get = await client.GetStringAsync("http://localhost:50029/api/Task/GetTasks");
            var tasks = JsonConvert.DeserializeObject<IEnumerable<TaskViewModel>>(get);
            var task = tasks.FirstOrDefault(x => x.Name == "TASK_TEST");

            var values = new Dictionary<string, string>
            {
                { "TaskId", task.TaskId.ToString() },
                { "Day", "Monday" }
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://localhost:50029/api/Task/AssignDay", content);
            response.StatusCode.ToString().ShouldBe("OK");
        }

        [Test, Order(7)]
        public async Task AssignUser()
        {
            var getTasks = await client.GetStringAsync("http://localhost:50029/api/Task/GetTasks");
            var tasks = JsonConvert.DeserializeObject<IEnumerable<TaskViewModel>>(getTasks);
            var task = tasks.FirstOrDefault(x => x.Name == "TASK_TEST");
            var getUsers = await client.GetStringAsync("http://localhost:50029/api/User/GetUsers");
            var users = JsonConvert.DeserializeObject<IEnumerable<UserViewModel>>(getUsers);
            var user = users.FirstOrDefault(x => x.FullName == "USER_TEST");

            var values = new Dictionary<string, string>
            {
                { "TaskId", task.TaskId.ToString() },
                { "UserId", user.UserId.ToString() }
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://localhost:50029/api/Task/AssignUser", content);
            response.StatusCode.ToString().ShouldBe("OK");
        }

        [Test, Order(8)]
        public async Task DeleteTask()
        {
            var getTasks = await client.GetStringAsync("http://localhost:50029/api/Task/GetTasks");
            var tasks = JsonConvert.DeserializeObject<IEnumerable<TaskViewModel>>(getTasks);
            var task = tasks.FirstOrDefault(x => x.Name == "TASK_TEST");

            var response = await client.DeleteAsync("http://localhost:50029/api/Task/DeleteTask/"+task.TaskId);
            response.StatusCode.ToString().ShouldBe("OK");
        }

        [Test, Order(9)]
        public async Task DeleteUser()
        {
            var getUsers = await client.GetStringAsync("http://localhost:50029/api/User/GetUsers");
            var users = JsonConvert.DeserializeObject<IEnumerable<UserViewModel>>(getUsers);
            var user = users.FirstOrDefault(x => x.FullName == "USER_TEST");

            var response = await client.DeleteAsync("http://localhost:50029/api/User/DeleteUser/" + user.UserId);
            response.StatusCode.ToString().ShouldBe("OK");
        }

        [OneTimeTearDown]
        public async Task ClearData()
        {
            var getTasks = await client.GetStringAsync("http://localhost:50029/api/Task/GetTasks");
            var tasks = JsonConvert.DeserializeObject<IEnumerable<TaskViewModel>>(getTasks);
            tasks = tasks.Where(x => x.Name == "TASK_TEST");
            foreach (var task in tasks)
            {
                await client.DeleteAsync("http://localhost:50029/api/Task/DeleteTask/" + task.TaskId);
            }

            var getUsers = await client.GetStringAsync("http://localhost:50029/api/User/GetUsers");
            var users = JsonConvert.DeserializeObject<IEnumerable<UserViewModel>>(getUsers);
            users = users.Where(x => x.FullName == "USER_TEST");
            foreach (var user in users)
            {
                await client.DeleteAsync("http://localhost:50029/api/User/DeleteUser/" + user.UserId);
            }
        }
    }
}