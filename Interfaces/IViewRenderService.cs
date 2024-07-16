using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeddyMVC.Interfaces
{
    public interface IViewRenderService
    {

        Task<string> RenderToStringAsync(string viewName, object model);
    }
}