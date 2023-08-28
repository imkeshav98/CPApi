using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPApi.Helpers.UserContext
{
    public interface IUserContext
    {
        int GetUserId();
        string GetUserRole();
    }
}