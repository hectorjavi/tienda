using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Profiles;

public class MappingProfiles: Profile
{
    public MappingProfiles() {
        CreateMap<Producto, ProductoDto>()
            .ReverseMap();

        CreateMap<Categoria, CategoriaDto>()
            .ReverseMap();

        CreateMap<Marca, MarcaDto>()
            .ReverseMap();

        CreateMap<Producto, ProductoListDto>()
            .ForMember( //Mapear el nombre Marca en producto
                dest => dest.Marca,
                origen => origen.MapFrom(origen => origen.Marca.Nombre)
            )
            .ForMember( //Mapear el nombre categoria en producto
                dest => dest.Categoria,
                origen => origen.MapFrom(origen => origen.Categoria.Nombre)
            )
            .ReverseMap()
            .ForMember(origen => origen.Categoria, dest => dest.Ignore()) // Para evitar la excepcion al traer la categoria, se ignora el mapeo
            .ForMember(origen => origen.Marca, dest => dest.Ignore());

        CreateMap<Producto, ProductoAddUpdateDto>()
            .ReverseMap()
            .ForMember(origen => origen.Categoria, dest => dest.Ignore()) // Para evitar la excepcion al traer la categoria, se ignora el mapeo
            .ForMember(origen => origen.Marca, dest => dest.Ignore());
    }
}