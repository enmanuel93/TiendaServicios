using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreService.Api.Gateway.LibroRemote;

namespace StoreService.Api.Gateway.InterfaceRemote
{
    public interface IAutorRemote
    {
        Task<(bool resultado, AutorModeloRemote autor, string ErrorMessage)> GetAutor(Guid AutorId);
    }
}