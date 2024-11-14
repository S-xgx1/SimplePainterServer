using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplePainterServer.Dto;
using SimplePainterServer.Model;

namespace SimplePainterServer.Controller;

[Route("[controller]"), ApiController,]
public class WordController(MainDateBase context, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddWord(WordInfoDto dto)
    {
        var wordInfo = mapper.Map<WordInfo>(dto);
        context.WordInfos.Add(wordInfo);
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("List")]
    public async Task<ActionResult<WordInfoDto[]>> GetList()
    {
        var infos = await context.WordInfos.Include(x => x.Images).Where(x => x.Images.Count <= Config.WordMaxDrawCount)
                                 .ToArrayAsync();
        return mapper.Map<WordInfo[], WordInfoDto[]>(infos).OrderBy(x => new Random().Next()).ToArray();
    }

    [HttpGet("DetailList")]
    public async Task<ActionResult<List<WordInfoDetail>>> GetDetailList()
    {
        var infos           = await context.WordInfos.Include(x => x.Images).ToListAsync();
        var wordInfoDetails = mapper.Map<List<WordInfo>, List<WordInfoDetail>>(infos);
        foreach (var wordInfoDetail in wordInfoDetails)
            wordInfoDetail.DrawCount = infos.First(x => x.ID == wordInfoDetail.ID).Images.Count;
        return wordInfoDetails;
    }


    [HttpDelete("ClearData")]
    public async Task<IActionResult> ClearData(int wordId)
    {
        var data = await context.WordInfos.Include(x => x.Images).FirstOrDefaultAsync(x => x.ID == wordId);
        if (data is null)
            return NotFound();
        context.ImageInfos.RemoveRange(data.Images);
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int wordId)
    {
        var data = await context.WordInfos.Include(x => x.Images).FirstOrDefaultAsync(x => x.ID == wordId);
        if (data is null)
            return NotFound();
        context.WordInfos.Remove(data);
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("AddAllWord")]
    public async Task<IActionResult> AddAllWord()
    {
        var wordInfos = new List<WordInfo>
                        {
                            new(0, "man", "Noun"),
                            new(0, "woman", "Noun"),
                            new(0, "child", "Noun"),
                            new(0, "friend", "Noun"),
                            new(0, "teacher", "Noun"),
                            new(0, "home", "Noun"),
                            new(0, "school", "Noun"),
                            new(0, "park", "Noun"),
                            new(0, "city", "Noun"),
                            new(0, "country", "Noun"),
                            new(0, "dog", "Noun"),
                            new(0, "cat", "Noun"),
                            new(0, "bird", "Noun"),
                            new(0, "fish", "Noun"),
                            new(0, "horse", "Noun"),
                            new(0, "book", "Noun"),
                            new(0, "pen", "Noun"),
                            new(0, "table", "Noun"),
                            new(0, "chair", "Noun"),
                            new(0, "car", "Noun"),
                            new(0, "apple", "Noun"),
                            new(0, "bread", "Noun"),
                            new(0, "water", "Noun"),
                            new(0, "milk", "Noun"),
                            new(0, "rice", "Noun"),
                            new(0, "day", "Noun"),
                            new(0, "night", "Noun"),
                            new(0, "week", "Noun"),
                            new(0, "month", "Noun"),
                            new(0, "year", "Noun"),
                            new(0, "sun", "Noun"),
                            new(0, "moon", "Noun"),
                            new(0, "star", "Noun"),
                            new(0, "sky", "Noun"),
                            new(0, "tree", "Noun"),
                            new(0, "father", "Noun"),
                            new(0, "mother", "Noun"),
                            new(0, "brother", "Noun"),
                            new(0, "sister", "Noun"),
                            new(0, "baby", "Noun"),
                            new(0, "head", "Noun"),
                            new(0, "hand", "Noun"),
                            new(0, "foot", "Noun"),
                            new(0, "eye", "Noun"),
                            new(0, "ear", "Noun"),
                            new(0, "job", "Noun"),
                            new(0, "work", "Noun"),
                            new(0, "office", "Noun"),
                            new(0, "boss", "Noun"),
                            new(0, "meeting", "Noun"),
                            new(0, "happy", "Adjective"),
                            new(0, "sad", "Adjective"),
                            new(0, "angry", "Adjective"),
                            new(0, "excited", "Adjective"),
                            new(0, "tired", "Adjective"),
                            new(0, "sunny", "Adjective"),
                            new(0, "rainy", "Adjective"),
                            new(0, "windy", "Adjective"),
                            new(0, "cloudy", "Adjective"),
                            new(0, "snowy", "Adjective"),
                            new(0, "big", "Adjective"),
                            new(0, "small", "Adjective"),
                            new(0, "heavy", "Adjective"),
                            new(0, "light", "Adjective"),
                            new(0, "new", "Adjective"),
                            new(0, "red", "Adjective"),
                            new(0, "blue", "Adjective"),
                            new(0, "green", "Adjective"),
                            new(0, "yellow", "Adjective"),
                            new(0, "black", "Adjective"),
                            new(0, "hot", "Adjective"),
                            new(0, "cold", "Adjective"),
                            new(0, "warm", "Adjective"),
                            new(0, "cool", "Adjective"),
                            new(0, "freezing", "Adjective"),
                            new(0, "round", "Adjective"),
                            new(0, "square", "Adjective"),
                            new(0, "flat", "Adjective"),
                            new(0, "long", "Adjective"),
                            new(0, "short", "Adjective"),
                            new(0, "kind", "Adjective"),
                            new(0, "mean", "Adjective"),
                            new(0, "brave", "Adjective"),
                            new(0, "shy", "Adjective"),
                            new(0, "friendly", "Adjective"),
                            new(0, "fast", "Adjective"),
                            new(0, "slow", "Adjective"),
                            new(0, "quick", "Adjective"),
                            new(0, "early", "Adjective"),
                            new(0, "late", "Adjective"),
                            new(0, "sweet", "Adjective"),
                            new(0, "sour", "Adjective"),
                            new(0, "bitter", "Adjective"),
                            new(0, "salty", "Adjective"),
                            new(0, "spicy", "Adjective"),
                            new(0, "clean", "Adjective"),
                            new(0, "dirty", "Adjective"),
                            new(0, "full", "Adjective"),
                            new(0, "empty", "Adjective"),
                            new(0, "broken", "Adjective"),
                            new(0, "eat", "Verb"),
                            new(0, "drink", "Verb"),
                            new(0, "sleep", "Verb"),
                            new(0, "walk", "Verb"),
                            new(0, "run", "Verb"),
                            new(0, "read", "Verb"),
                            new(0, "write", "Verb"),
                            new(0, "learn", "Verb"),
                            new(0, "study", "Verb"),
                            new(0, "teach", "Verb"),
                            new(0, "see", "Verb"),
                            new(0, "hear", "Verb"),
                            new(0, "feel", "Verb"),
                            new(0, "touch", "Verb"),
                            new(0, "smell", "Verb"),
                            new(0, "jump", "Verb"),
                            new(0, "swim", "Verb"),
                            new(0, "play", "Verb"),
                            new(0, "ride", "Verb"),
                            new(0, "climb", "Verb"),
                            new(0, "talk", "Verb"),
                            new(0, "listen", "Verb"),
                            new(0, "ask", "Verb"),
                            new(0, "answer", "Verb"),
                            new(0, "shout", "Verb"),
                            new(0, "clean", "Verb"),
                            new(0, "cook", "Verb"),
                            new(0, "wash", "Verb"),
                            new(0, "sweep", "Verb"),
                            new(0, "mop", "Verb"),
                            new(0, "travel", "Verb"),
                            new(0, "drive", "Verb"),
                            new(0, "fly", "Verb"),
                            new(0, "sail", "Verb"),
                            new(0, "ride", "Verb"),
                            new(0, "work", "Verb"),
                            new(0, "meet", "Verb"),
                            new(0, "plan", "Verb"),
                            new(0, "build", "Verb"),
                            new(0, "create", "Verb"),
                            new(0, "open", "Verb"),
                            new(0, "close", "Verb"),
                            new(0, "start", "Verb"),
                            new(0, "stop", "Verb"),
                            new(0, "move", "Verb"),
                            new(0, "think", "Verb"),
                            new(0, "know", "Verb"),
                            new(0, "remember", "Verb"),
                            new(0, "forget", "Verb"),
                            new(0, "understand", "Verb"),
                            new(0, "I", "Pronoun"),
                            new(0, "you", "Pronoun"),
                            new(0, "he", "Pronoun"),
                            new(0, "she", "Pronoun"),
                            new(0, "it", "Pronoun"),
                            new(0, "we", "Pronoun"),
                            new(0, "they", "Pronoun"),
                            new(0, "me", "Pronoun"),
                            new(0, "him", "Pronoun"),
                            new(0, "her", "Pronoun"),
                            new(0, "one", "Numeral"),
                            new(0, "two", "Numeral"),
                            new(0, "three", "Numeral"),
                            new(0, "four", "Numeral"),
                            new(0, "five", "Numeral"),
                            new(0, "six", "Numeral"),
                            new(0, "seven", "Numeral"),
                            new(0, "eight", "Numeral"),
                            new(0, "nine", "Numeral"),
                            new(0, "ten", "Numeral"),
                            new(0, "oh", "Interjection"),
                            new(0, "wow", "Interjection"),
                            new(0, "hey", "Interjection"),
                            new(0, "ouch", "Interjection"),
                            new(0, "yay", "Interjection"),
                            new(0, "oops", "Interjection"),
                            new(0, "ah", "Interjection"),
                            new(0, "huh", "Interjection"),
                            new(0, "alas", "Interjection"),
                            new(0, "bravo", "Interjection"),
                        };
        foreach (var wordInfo in wordInfos)
        {
            await context.WordInfos.AddAsync(wordInfo);
            await context.SaveChangesAsync();
        }

        Console.WriteLine("ok");
        return Ok("111");
    }
}