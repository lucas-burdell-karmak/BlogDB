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

        bool TryValidateAuthor(string name, string passwordHash,out Author author);

        bool TryRegisterAuthor(string name, string passwordHash, out Author author);

    }
}
