using AutoMapper;
using Quotes.Data.DTO.RequestDTO;
using Quotes.Data.DTO.ResponseDTO;
using Quotes.Data.EntityModals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Service.Infrastructure.AutoMapper
{
    public class AppAutoMapperProfile : Profile
    {
        public AppAutoMapperProfile()
        {
            CreateMap<QuoteReqDto, Quote>().ForMember(dest => dest.Tags, opt => opt.MapFrom(x => x.Tags.Select(y => new Tag { TagName = y }).ToList()));
            //.ForMember(dest => dest.Tags, opt => opt.MapFrom(x => string.Join("||", x.Tags)));
            CreateMap<Quote, QuoteRespDto>().ForMember(dest => dest.Tags, opt => opt.MapFrom(x => x.Tags.Select(x => x.TagName).ToList()));
                //.ForMember(dest => dest.Tags,
                //opt => opt.MapFrom(x => x.Tags != null ? x.Tags.Split("||", StringSplitOptions.TrimEntries).ToList() : new List<string>()));
        
        }
    }
}
