using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
namespace Infrastructure.Repositories;

//Atajo: repoent
public class RolRepository : GenericRepository<Rol>, IRolRepository
{
    public RolRepository(TiendaContext context) : base(context)
    {
    }
}
