using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplePainterServer.Dto;
using SimplePainterServer.Model;

namespace SimplePainterServer.Controller;

[Route("[controller]"), ApiController]
public class GuessController(MainDateBase context, IMapper mapper)
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<GuessDto>> Post(GuessDto info)
    {
        var entityEntry = await context.AddAsync(mapper.Map<Guess>(info));
        await context.SaveChangesAsync();
        return mapper.Map<Guess, GuessDto>(entityEntry.Entity);
    }

    [HttpGet("ListForImage")]
    public async Task<ActionResult<List<GuessDto>>> ListForImage(int imageId)
    {
        var guesses = await context.Guesses.Where(g => g.ImageId == imageId).ToListAsync();
        return mapper.Map<List<Guess>, List<GuessDto>>(guesses);
    }

    [HttpGet("ListForUser")]
    public async Task<ActionResult<List<GuessDetail>>> ListForUser(int userId)
    {
        var guesses = await context.Guesses.Where(x => x.UserID == userId).Include(x => x.Image)
            .ThenInclude(x => x.Word).ToListAsync();
        var guessDetails = mapper.Map<List<Guess>, List<GuessDetail>>(guesses);
        foreach (var guessDetail in guessDetails)
        {
            guessDetail.SuccessWord = guesses.First(x => x.ID == guessDetail.ID).Image.Word.Name;
        }

        return guessDetails;
    }
}