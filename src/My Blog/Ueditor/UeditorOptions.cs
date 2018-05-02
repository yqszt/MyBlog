using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Blog.Ueditor
{
    public class UeditorOptions
    {
        public string UeditorRootPath { get; set; }
        public string UeditorUrlPrefix { get; set; }

        public UeditorOptions()
        {
            UeditorRootPath = "wwwroot/lib/ueditor";
            UeditorUrlPrefix = "/lib/ueditor";
        }
    }
}
