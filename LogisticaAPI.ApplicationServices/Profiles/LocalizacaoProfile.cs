using AutoMapper;
using LogisticaAPI.ApplicationServices.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.ApplicationServices.Profiles
{
    public class LocalizacaoProfile : Profile
    {
        public LocalizacaoProfile()
        {
            CreateMap<Domain.Models.DataTransferObjects.LocalizacaoResponse, LocalizacaoResponse>();
        }
    }
}
