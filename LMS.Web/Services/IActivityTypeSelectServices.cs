using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Services
{
    public interface IActivityTypeSelectServices
    {
        Task<IEnumerable<SelectListItem>> GetActivityTypeAsync();
    }
}
