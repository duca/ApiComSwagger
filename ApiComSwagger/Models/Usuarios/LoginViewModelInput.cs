using System.ComponentModel.DataAnnotations;

namespace ApiComSwagger.Models.Usuarios
{
    /// <summary>
    /// ModelView do Login
    /// </summary>
    public class LoginViewModelInput
    {
        /// <summary>
        /// Parâmetro login
        /// </summary>
        [Required(ErrorMessage = "Login é obrigatório")]
        public string Login { get; set; }

        /// <summary>
        /// Parametro de senha
        /// </summary>
        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Senha { get; set; }
    }
}
