using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASK.Model.DBModel
{
    public class XmlObjectWrapperAttribute : Attribute
    {
        public string ElementName { get; set; }
    }
}
