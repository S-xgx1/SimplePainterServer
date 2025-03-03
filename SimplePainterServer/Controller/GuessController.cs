using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplePainterServer.Dto;
using SimplePainterServer.Model;

namespace SimplePainterServer.Controller;

[Route("[controller]"), ApiController,]
public class GuessController(MainDateBase context, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<GuessDto>> Post(GuessDto info)
    {
        var entityEntry = await context.AddAsync(mapper.Map<Guess>(info));
        await context.SaveChangesAsync();
        var wordId = await context.ImageInfos.Where(x => x.ID == info.ImageId).Select(x => x.WordId)
                                  .FirstOrDefaultAsync();
        var       count = await context.Guesses.Include(x => x.Image).CountAsync(x => x.Image.WordId == wordId);
        // 这个数量为整数则记录时间
        const int recordNumber = 5;
        if (count % recordNumber == 0)
        {
            await context.WordCreateTimes.AddAsync(new(wordId, DateTime.Now, CreateTimeType.Guess));
            await context.SaveChangesAsync();
        }

        var guessDto = mapper.Map<Guess, GuessDto>(entityEntry.Entity);
        var wordInfo = await context.ImageInfos.Include(x => x.Word).FirstOrDefaultAsync(x => x.ID == guessDto.ImageId);
        guessDto.IsCorrect = wordInfo is not null && info.Word == wordInfo.Word.Name;
        return guessDto;
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
            guessDetail.SuccessWord = guesses.First(x => x.ID == guessDetail.ID).Image.Word.Name;

        return guessDetails;
    }
}