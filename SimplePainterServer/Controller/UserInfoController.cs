using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplePainterServer.Dto;
using SimplePainterServer.Model;

namespace SimplePainterServer.Controller;

[Route("[controller]"), ApiController,]
public class UserInfoController(MainDateBase context, IMapper mapper) : ControllerBase
{
    [HttpPut]
    public async Task<ActionResult<UserInfoDto>> Put(UserInfoDto userInfo)
    {
        var entity = mapper.Map<UserInfoDto, UserInfo>(userInfo);
        var exInfo = await context.UserInfos.FirstOrDefaultAsync(x => x.Name == entity.Name);
        if (exInfo is not null)
            exInfo.Update(entity);
        else
            exInfo = (await context.AddAsync(entity)).Entity;

        await context.SaveChangesAsync();
        return mapper.Map<UserInfo, UserInfoDto>(exInfo);
    }

    [HttpGet("OrUserName")]
    public async Task<ActionResult<UserInfoDto>> GetOrUserName(string userName)
    {
        var info = await context.UserInfos.FirstOrDefaultAsync(x => x.Name == userName);
        if (info is null)
            return NotFound();
        return mapper.Map<UserInfo, UserInfoDto>(info);
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
                                                            let correctCount =
                                                                wordInfoImage.Guesses.Count(
                                                                    x => x.Word == wordInfo.Name)
                                                            let count = wordInfoImage.Guesses.Count
                                                            where count >= Config.ImageMaxGuessCount &&
                                                                  1f * correctCount / count >
                                                                  Config.ImageMaxGuessCorrectlyProportion
                                                            select correctCount).Any());
        return 1f * wordCorrectCount / wordCount;
    }

    [HttpGet("ListForDetail")]
    public async Task<ActionResult<List<UserInfoDetail>>> ListForDetail()
    {
        var userInfos = await context.UserInfos.Include(x => x.Images).ThenInclude(x => x.Guesses)
                                     .Include(userInfo => userInfo.Images).ThenInclude(imageInfo => imageInfo.Word)
                                     .Include(userInfo => userInfo.Guesses).ThenInclude(guess => guess.Image)
                                     .ToListAsync();
        var listForDetail = mapper.Map<List<UserInfo>, List<UserInfoDetail>>(userInfos);
        foreach (var userInfo in listForDetail)
        {
            var first = userInfos.First(x => x.ID == userInfo.ID);
            userInfo.DrawCount         = first.Images.Count;
            userInfo.GuessCount        = first.Guesses.Count;
            userInfo.GuessSuccessCount = first.Guesses.Sum(x => x.Image.Word.Name == x.Word ? 1 : 0);
            userInfo.Progress          = await InGetPersonalProgress(first.ID);
        }

        return listForDetail;
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        var info = await context.UserInfos.FirstOrDefaultAsync(x => x.ID == id);
        if (info is null)
            return NotFound();
        context.UserInfos.Remove(info);
        await context.SaveChangesAsync();
        return Ok();
    }


    [HttpGet("PersonalProgress")]
    public async Task<ActionResult<double>> GetPersonalProgress(int id) => await InGetPersonalProgress(id);

    async Task<double> InGetPersonalProgress(int id)
    {
        var wordCount  = await context.ImageInfos.CountAsync(x => x.UserID == id);
        var guessCount = await context.Guesses.CountAsync(x => x.UserID    == id);
        return Math.Min(wordCount  * Config.CreateWordAddPersonalProgress, 0.5) +
               Math.Min(guessCount * Config.GuessWordAddPersonalProgress,  0.5);
    }
}