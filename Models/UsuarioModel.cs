namespace SistemaDeTarefas.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        //? quer dizer que a string pode ser nula
        public string? Nome { get; set; }

        public string? Email { get; set;}

    }
}
