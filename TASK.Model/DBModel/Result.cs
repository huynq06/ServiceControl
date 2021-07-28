using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TASK.Model.DBModel
{
    [XmlRoot(ElementName = "DocumentElement")]
    public class Result
    {
        public List<FLightExort> FLights { set; get; }
    }
}
