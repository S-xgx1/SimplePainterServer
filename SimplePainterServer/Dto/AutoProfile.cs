using AutoMapper;
using SimplePainterServer.Dto;
using SimplePainterServer.Model;

namespace AutoLiveMainServer.Dto;

public class AutoProfile : Profile
{
    public AutoProfile()
    {
        CreateMap<Guess, GuessDto>()
            .ConstructUsing(x => new(x.ID, x.ImageId, x.Word, x.UserID))
            .ReverseMap()
            .ConstructUsing(x => new(x.ImageId, x.Word, x.UserID));
        CreateMap<Guess, GuessDetail>()
            .ConstructUsing(x => new(x.ID, x.ImageId, x.Word, x.UserID));
        CreateMap<ImageInfo, ImageInfoDto>()
            .ConstructUsing(x => new(x.ID, x.WordId, x.UserID))
            .ReverseMap()
            .ConstructUsing(x => new(x.ID, x.WordId, x.UserID));
        CreateMap<UserInfo, UserInfoDto>()
            .ConstructUsing(x => new(x.ID, x.Name, x.Language, x.Sex, x.Age, x.Career, x.EducationLevel))
            .ReverseMap()
            .ConstructUsing(x => new(x.ID, x.Name, x.Language, x.Sex, x.Age, x.Career, x.EducationLevel));
        CreateMap<UserInfo, UserInfoDetail>()
            .ConstructUsing(x => new(x.ID, x.Name, x.Language, x.Sex, x.Age, x.Career, x.EducationLevel));
        CreateMap<WordInfo, WordInfoDto>()
            .ConstructUsing(x => new(x.ID, x.Name, x.PartSpeech))
            .ReverseMap()
            .ConstructUsing(x => new(x.ID, x.Name, x.PartSpeech));
        CreateMap<WordInfo, WordInfoDetail>()
            .ConstructUsing(x => new(x.ID, x.Name, x.PartSpeech, 0));
        CreateMap<ImageInfo, ImageInfoDetail>()
            .ConstructUsing(x => new(x.ID, x.WordId, x.UserID, 0, 0));
    }
}