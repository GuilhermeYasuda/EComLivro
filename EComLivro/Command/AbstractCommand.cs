using Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Aplicacao;
using Dominio;
using Core.Controle;

namespace EComLivro.Command
{
    public abstract class AbstractCommand : ICommand
    {
        protected IFachada fachada = Fachada.UniqueInstance;

        public abstract Resultado execute(EntidadeDominio entidade);
    }
}