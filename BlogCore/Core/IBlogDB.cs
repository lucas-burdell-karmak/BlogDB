using System;
using System.Collections.Generic;

namespace BlogDB.Core
{
    public interface IBlogDB
    {

        List<Post> ReadAll();

        void WriteAll(List<Post> posts);


    }
}