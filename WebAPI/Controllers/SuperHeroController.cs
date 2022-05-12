﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuperHeroController : ControllerBase
{
    private readonly DataContext _context;

    public SuperHeroController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<SuperHero>>> Get()
    {
        return Ok(await _context.SuperHeroes.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<SuperHero>>> Get(int id)
    {
        var hero = await _context.SuperHeroes.FindAsync(id);
        if (hero is null)
            return BadRequest("Hero not found");

        return Ok(hero);
    }

    [HttpPost]
    public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
    {
        _context.SuperHeroes.Add(hero);
        await _context.SaveChangesAsync();

        return Ok(await _context.SuperHeroes.ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
    {
        var dbHero = await _context.SuperHeroes.FindAsync(request.Id);
        if (dbHero is null)
            return BadRequest("Hero not found");

        dbHero.Name = request.Name;
        dbHero.Firstname = request.Firstname;
        dbHero.LastName = request.LastName;
        dbHero.Place = request.Place;
        await _context.SaveChangesAsync();

        return Ok(await _context.SuperHeroes.ToListAsync());
    }

    [HttpDelete]
    public async Task<ActionResult<List<SuperHero>>> Delete(int id)
    {
        var dbHero = await _context.SuperHeroes.FindAsync(id);
        if (dbHero is null)
            return BadRequest("Hero not found");

        _context.SuperHeroes.Remove(dbHero);
        await _context.SaveChangesAsync();
        return Ok(await _context.SuperHeroes.ToListAsync());
    }
}