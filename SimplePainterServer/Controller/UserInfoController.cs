using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplePainterServer.Dto;
using SimplePainterServer.Model;

namespace SimplePainterServer.Controller;

[Route("[controller]"), ApiController]
public class UserInfoController(MainDateBase context, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UserInfoDto>> Post(UserInfoDto userInfo)
    {
        var entityEntry = await context.AddAsync(mapper.Map<UserInfoDto, UserInfo>(userInfo));
        await context.SaveChangesAsync();
        return mapper.Map<UserInfo, UserInfoDto>(entityEntry.Entity);
    }

    [HttpGet]
    public async Task<ActionResult<UserInfoDto>> Get(int id)
    {
        var info = await context.UserInfos.FirstOrDefaultAsync(x => x.ID == id);
        if (info is null)
            return NotFound();
        return mapper.Map<UserInfo, UserInfoDto>(info);
    }

    [HttpGet("DrawGuessCompletion")]
    public async Task<ActionResult<float>> GetDrawGuessCompletion()
    {
        var wordCount = await context.WordInfos.CountAsync();
        var wordInfos = await context.WordInfos.Include(x => x.Images).ThenInclude(x => x.Guesses).ToArrayAsync();
        var wordCorrectCount = wordInfos.Count(wordInfo => (from wordInfoImage in wordInfo.Images
            let correctCount = wordInfoImage.Guesses.Count(x => x.Word == wordInfo.Name)
            let count = wordInfoImage.Guesses.Count
            where count                     >= Config.ImageMaxGuessCount &&
                  1f * correctCount / count > Config.ImageMaxGuessCorrectlyProportion
            select correctCount).Any());
        return 1f * wordCorrectCount / wordCount;
    }

    [HttpGet("ListForDetail")]
    public async Task<ActionResult<List<UserInfoDetail>>> ListForDetail()
    {
        var userInfos = await context.UserInfos.Include(x => x.Images).ThenInclude(x => x.Guesses)
            .Include(userInfo => userInfo.Images).ThenInclude(imageInfo => imageInfo.Word).ToListAsync();
        var listForDetail = mapper.Map<List<UserInfo>, List<UserInfoDetail>>(userInfos);
        foreach (var userInfo in listForDetail)
        {
            var first = userInfos.First(x => x.ID == userInfo.ID);
            userInfo.DrawCount         = first.Images.Count;
            userInfo.GuessCount        = first.Images.Sum(x => x.Guesses.Count);
            userInfo.GuessSuccessCount = first.Images.Sum(x => x.Guesses.Count(y => y.Word == x.Word.Name));
        }

        return listForDetail;
    }
}