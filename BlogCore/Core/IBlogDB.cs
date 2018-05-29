using System;
using System.Collections.Generic;

namespace BlogDB.Core
{
    public interface IBlogDB<T>
    {

        List<T> ReadAll();

        void WriteAll(List<T> posts);


    }
}