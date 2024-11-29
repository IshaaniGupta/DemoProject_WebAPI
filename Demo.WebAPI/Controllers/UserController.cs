﻿using Demo.Domain.Model;
using Demo.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Demo.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _login;

        public UserController(IUser login)
        {
            this._login = login;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Retrieve the authenticated user
            var user = HttpContext.Items["User"]?.ToString();
            if (user == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            var response = await _login.LoginRepo(username, password);
            return Ok(response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUser(UserLogin user_detail)
        {
            // Retrieve the authenticated user
            var user = HttpContext.Items["User"]?.ToString();
            if (user == null)
            {
                return Unauthorized("User is not authenticated.");
            }
            var response = await _login.AddUserRepo(user_detail);
            return Ok(response);
        }

        [HttpGet("update/{user_id}")]
        public async Task<IActionResult> UpdateUser(Guid user_id, UserLogin user_detail)
        {
            var response = await _login.UpdateUserRepo(user_id, user_detail);
            return Ok(response);
        }

        [HttpGet("delete")]
        public async Task<IActionResult> DeleteUser(Guid user_id)
        {
            var response = await _login.DeleteUserRepo(user_id);
            return Ok(response);
        }
    }
}