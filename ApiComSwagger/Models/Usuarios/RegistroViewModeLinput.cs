using System.ComponentModel.DataAnnotations;

namespace ApiComSwagger.Models.Usuarios
{
    /// <summary>
    /// ModelView do Registro
    /// </summary>
    public class RegistroViewModeLinput
    {
        /// <summary>
        /// Login do usuário
        /// </summary>
        [Required(ErrorMessage= "Login é obrigatório")]
        public string Login { get; set; }
        
        /// <summary>
        /// Email do usuário
        /// </summary>
        [Required(ErrorMessage = "Email é obrigatório")]       

        public string Email { get; set; }

        /// <summary>
        /// Senha do Usuário
        /// </summary>
        [Required(ErrorMessage ="Senha é obrigatória")]
        public string Senha { get; set; }

    }
}
