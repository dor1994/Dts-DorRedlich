using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enums
{
    public enum EnumResponse
    {
        UserFound = 0,
        UserNotFound = 1,
        WorngPassword = 2,
        UserNameExist = 3,
        DateRequestExist = 4,
        CustomerAdded = 5,
        CustomerDeleted = 6,
        CustomerUpdate = 7,
        CustomerRequestNotFound = 8,
        UserRegistered = 9,
    }
}
