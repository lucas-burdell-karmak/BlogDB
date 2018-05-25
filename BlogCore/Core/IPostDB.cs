using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogDB.Core
{
    public interface IPostDB<T>
    {

        List<T> ReadAll();

        void WriteAll(List<T> posts);


    }
}