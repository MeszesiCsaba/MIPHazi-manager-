using System;
using System.Collections.Generic;
using System.Text;

namespace MIPHazi
{
    public interface Authenticatable
    {
        string GetName();
        bool CheckPassword(string password);
    }
}
