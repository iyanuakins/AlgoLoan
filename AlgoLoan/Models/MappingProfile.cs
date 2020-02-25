using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlgoLoan.Models.ViewModels;
using AutoMapper;
using DAL.EF;

namespace AlgoLoan.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Provider, ProviderViewModel>()
                .ForMember(p => p.Id, 
                    opt => opt.MapFrom(m => m.providerId))
                .ReverseMap();
            CreateMap<Search, SearchViewModel>().ReverseMap();
            CreateMap<Visit, VisitViewModel>().ReverseMap();
            CreateMap<Subscription, SubscriptionViewModel>().ReverseMap();
        }
    }
}