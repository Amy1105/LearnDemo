using System;
using System.Collections.Generic;
using System.Text;

namespace SG3
{
    public interface IMapper<TSource, TDest>
    {
        TDest Map(TSource src);
    }
}
