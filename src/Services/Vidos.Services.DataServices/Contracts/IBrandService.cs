using System.Collections.Generic;

namespace Vidos.Services.DataServices.Contracts
{
    public interface IBrandService
    {
        IEnumerable<string> AllNames();
    }
}
