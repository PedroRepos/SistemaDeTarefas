using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;

namespace SistemaDeTarefas.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;
        public UsuarioRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext)
        {
            _dbContext = sistemaTarefasDBContext;
        }
        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
          await _dbContext.Usuario.AddAsync(usuario);
            _dbContext.SaveChangesAsync();

            return usuario;
        }
        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorID(id);
            
            if(usuarioPorId == null)
            {
                throw new Exception($"Usuário para p ID: {id} Não foi encontrado");
                
            }
            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;

            _dbContext.Usuario.Update(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return usuarioPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuarioModel usuarioPorId = await BuscarPorID(id);

            if(usuarioPorId == null)
            {
                throw new Exception($"Usuário para o Id: {id} não foi encontrado no banco de dados");
            }

            _dbContext.Usuario.Remove(usuarioPorId);
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<UsuarioModel> BuscarPorID(int id)
        {
            return await _dbContext.Usuario.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<UsuarioModel>> BuscarTodoUsuarios()
        {
            return await _dbContext.Usuario.ToListAsync();
        }
    }
}
