using System.Collections.Generic;

namespace RestWithASPNETUdemy.Hypermedia.Abstract
{
    public interface ISupportHypermedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
