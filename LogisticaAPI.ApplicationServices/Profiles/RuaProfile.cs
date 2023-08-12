using AutoMapper;
using LogisticaAPI.ApplicationServices.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.ApplicationServices.Profiles
{
    public class RuaProfile : Profile
    {
        public RuaProfile()
        {
            CreateMap<Domain.Models.DataTransferObjects.RuaDataResponse, RuaDataResponse>();
            CreateMap<Domain.Models.DataTransferObjects.RuaResponse, RuaResponse>();
        }
    }
}
