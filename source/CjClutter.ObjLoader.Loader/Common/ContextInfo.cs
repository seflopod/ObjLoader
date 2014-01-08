using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjLoader.Loader.Common
{
    //Added this to account for a situation in which the obj being loaded is in a different
    //directory than the program that loads it.  When the obj is loaded, the DirectoryName
    //is set and can then be referenced by the material loader.
    public static class ContextInfo
    {
        public static string DirectoryName { get; set; }
    }
}
