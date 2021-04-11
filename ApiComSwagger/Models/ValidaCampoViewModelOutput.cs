using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiComSwagger.Models
{
    /// <summary>
    /// ViewModel do validador de campo
    /// </summary>
    public class ValidaCampoViewModelOutput
    {
        /// <summary>
        /// Lista de erros
        /// </summary>
        public IEnumerable<string> Erros { get; private set; }
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="erros"></param>
        public ValidaCampoViewModelOutput(IEnumerable<string> erros)
        {
            Erros = erros;
        }

    }
}
