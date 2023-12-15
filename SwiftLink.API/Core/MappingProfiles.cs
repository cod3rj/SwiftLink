using AutoMapper;
using SwiftLink.API.Features.Url;

namespace SwiftLink.API.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // CreateMap<Source, Destination>();
            CreateMap<Url, UrlDto>()
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.CreationDate));
        }
    }
}