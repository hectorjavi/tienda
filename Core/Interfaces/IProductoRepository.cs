using Core.Entities;

namespace Core.Interfaces;

//Atajajo: iprorepo
public interface IProductoRepository : IGenericRepository<Producto>
{
    Task<IEnumerable<Producto>> GetProductosMasCaros(int cantidad);
}
