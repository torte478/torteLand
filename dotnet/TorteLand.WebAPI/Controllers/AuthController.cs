﻿using Microsoft.AspNetCore.Mvc;
using TorteLand.WebAPI2.Auth;
using TorteLand.WebAPI2.Dto;

namespace TorteLand.WebAPI2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuth _auth;

    public AuthController(IAuth auth)
    {
        _auth = auth;
    }

    #if DEBUG

    [HttpPost("register")]
    public ActionResult Register(User user)
    {
        _auth.Register(user.Password);

        return Ok();
    }

    #endif

    [HttpPost("login")]
    public ActionResult<AuthToken> Login(User user)
    {
        var token = _auth.Login(user.Password);
        return Ok(new AuthToken(token));
    }
}