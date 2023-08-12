using AutoMapper;
using LogisticaAPI.ApplicationServices.DataTransferObjects;
using LogisticaAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticaAPI.ApplicationServices.Profiles
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Domain.Models.DataTransferObjects.ProdutoResponse, ProdutoResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(org => org.Id))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(org => org.Nome))
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(org => org.Categoria))
                .ForMember(dest => dest.Localizacao, opt => opt.MapFrom(org => org.Localizacao));

            CreateMap<Domain.Models.DataTransferObjects.ProdutoRuaResponse, ProdutoRuaResponse>();

            CreateMap<ProdutoFilterRequest, Domain.Models.DataTransferObjects.ProdutoFilterRequest>();

        }

    }
}
