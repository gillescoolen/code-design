using System.Collections.Generic;
using CODE_GameLib.Models;

namespace CODE_Filesystem.Models
{
    public class ParsedConnection
    {
        public KeyValuePair<int, Side> In { get; set; }
        public KeyValuePair<int, Side> Out { get; set; }
    }
}