using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using SimplePainterServer.Dto;
using SimplePainterServer.Model;

namespace SimplePainterServer.Controller;

[Route("[controller]"), ApiController,]
public class ImageController(MainDateBase context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ImageInfoDto>> Post(ImageInfoDto info)
    {
        var entityEntry = await context.AddAsync(mapper.Map<ImageInfo>(info));
        await context.SaveChangesAsync();
        var count = await context.ImageInfos.CountAsync(x => x.WordId == info.WordId);
        // 这个数量为整数则记录时间
        const int recordNumber = 5;
        if (count % recordNumber != 0) return mapper.Map<ImageInfo, ImageInfoDto>(entityEntry.Entity);

        await context.WordCreateTimes.AddAsync(new(info.WordId, DateTime.Now, CreateTimeType.Image));
        await context.SaveChangesAsync();

        return mapper.Map<ImageInfo, ImageInfoDto>(entityEntry.Entity);
    }

    [HttpGet("List")]
    public async Task<ActionResult<List<ImageInfoDto>>> GetList()
    {
        var infos = await context.ImageInfos.Include(x => x.Word).Include(x => x.Guesses).ToListAsync();
        var infosToRemove = (from imageInfo in infos
                             let count = imageInfo.Guesses.Count(x => x.Word == imageInfo.Word.Name)
                             where imageInfo.Guesses.Count              >= Config.ImageMaxGuessCount &&
                                   1f * count / imageInfo.Guesses.Count > Config.ImageMaxGuessCorrectlyProportion
                             select imageInfo).ToList();
        infos.RemoveAll(info => infosToRemove.Contains(info));
        infos = infos.OrderBy(x => new Random().Next()).ToList();
        return mapper.Map<List<ImageInfo>, List<ImageInfoDto>>(infos);
    }

    [HttpGet("ListForWord")]
    public async Task<ActionResult<List<ImageInfoDetail>>> GetListForWord(int wordId)
    {
        await context.ImageInfos.Include(x => x.Word).Include(x => x.Guesses).ToArrayAsync();
        var infos = await context.ImageInfos.Include(x => x.Word).Include(x => x.Guesses).Where(x => x.WordId == wordId)
                                 .ToListAsync();
        var imageInfoDetails = mapper.Map<List<ImageInfo>, List<ImageInfoDetail>>(infos);
        foreach (var imageInfoDetail in imageInfoDetails)
        {
            var imageInfo = infos.First(x => x.ID == imageInfoDetail.ID);
            imageInfoDetail.GuessCount   = imageInfo.Guesses.Count;
            imageInfoDetail.CorrectCount = imageInfo.Guesses.Count(x => x.Word == imageInfo.Word.Name);
        }

        return imageInfoDetails;
    }

    [HttpGet("ListForUser")]
    public async Task<ActionResult<List<ImageInfoDetail>>> GetListForUser(int userId)
    {
        var infos = await context.ImageInfos.Include(x => x.Word).Include(imageInfo => imageInfo.Guesses)
                                 .Where(x => x.UserID == userId).ToListAsync();
        var list = mapper.Map<List<ImageInfo>, List<ImageInfoDetail>>(infos);
        foreach (var imageInfoDetail in list)
        {
            imageInfoDetail.GuessWord  = infos.First(x => x.ID == imageInfoDetail.ID).Word.Name;
            imageInfoDetail.GuessCount = infos.First(x => x.ID == imageInfoDetail.ID).Guesses.Count;
            imageInfoDetail.CorrectCount =
                infos.First(x => x.ID == imageInfoDetail.ID).Guesses.Count(x => x.UserID == userId);
        }

        return list;
    }

    [HttpPost("File")]
    public async Task<IActionResult> PostFile(int id, IFormFile file)
    {
        var             filePath = Path.Combine(webHostEnvironment.ContentRootPath, "Image", id.ToString());
        await using var stream   = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        return Ok();
    }

    [HttpGet("TipWords")]
    public async Task<ActionResult<List<string>>> GetTipWords(int id)
    {
        var imageInfo = await context.ImageInfos.Include(x => x.Word).SingleOrDefaultAsync(x => x.ID == id);
        if (imageInfo == null) return NotFound();
        // 获取当前imageInfo在数据库前面的两个单词和后面的两个单词
        var words    = await context.WordInfos.OrderBy(x => x.ID).ToListAsync();
        var index    = words.FindIndex(x => x.ID == imageInfo.WordId);
        var tipWords = new List<string>();
        if (index > 0) tipWords.Add(words[index               - 1].Name);
        if (index > 1) tipWords.Add(words[index               - 2].Name);
        if (index < words.Count - 1) tipWords.Add(words[index + 1].Name);
        if (index < words.Count - 2) tipWords.Add(words[index + 2].Name);
        tipWords.Add(imageInfo.Word.Name);
        return tipWords.OrderBy(x => new Random().Next()).ToList();
    }

    [HttpGet("File")]
    public async Task<IActionResult> GetFile(int id)
    {
        var             filePath     = Path.Combine(webHostEnvironment.ContentRootPath, "Image", id.ToString());
        await using var stream       = System.IO.File.OpenRead(filePath);
        var             provider     = new FileExtensionContentTypeProvider();
        var             mapping      = provider.Mappings[".png"];
        var             memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        return File(memoryStream.ToArray(), mapping);
    }
}