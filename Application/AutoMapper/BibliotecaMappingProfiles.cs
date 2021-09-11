using Application.ViewModels;
using AutoMapper;
using Domain.Model.Models;

namespace Application.AutoMapper
{
    public class BibliotecaMappingProfiles : Profile
    {
        public BibliotecaMappingProfiles()
        {
            CreateMap<AutorViewModel, AutorModel>().ReverseMap();
            CreateMap<LivroViewModel, LivroModel>().ReverseMap();
            CreateMap<HomeEstatisticaViewModel, HomeEstatisticaModel>().ReverseMap();
        }
    }
}
