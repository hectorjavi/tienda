namespace Core.Interfaces;

//Atajo: iuow
public interface IUnitOfWork
{
    IProductoRepository Productos { get; }
    IMarcaRepository Marcas { get; }
    ICategoriaRepository Categorias { get; }
    IRolRepository Roles { get; }
    IUsuarioRepository Usuarios { get; }
    Task<int> SaveAsync();
}
