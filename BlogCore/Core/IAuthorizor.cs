using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BlogDB.Core
{
    public interface IAuthorizor {

        Author ValidateAuthor(string name, string passwordHash);

        void RegisterAuthor(string name, string passwordHash);

    }
}
