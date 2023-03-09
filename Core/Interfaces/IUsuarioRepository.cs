using Core.Entities;
namespace Core.Interfaces;

//Atajo: irepo

public interface IUsuarioRepository : IGenericRepository<Usuario> {
    Task<Usuario> GetByUsernameAsync(string username);
    Task<Usuario> GetByRefreshTokenAsync(string refreshToken);

}
