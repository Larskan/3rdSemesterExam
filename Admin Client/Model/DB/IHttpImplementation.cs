using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.DB
{
    public interface IHttpImplementation
    {
        //Task<IHttpImplementation> GetAsync();
        Task Execute();
        
    }
}
