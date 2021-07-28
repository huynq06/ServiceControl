using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Model.DBModel;

namespace DATAACCESS
{
    public class CheckPXKAccess : DATABASE.CapSoProvider
    {
        public CapSo GetProperties(System.Data.IDataReader reader)
        {
            CapSo CapSo = new CapSo();
            CapSo.MAWB = Convert.ToString(GetValueField(reader, "MAWB", string.Empty));
            CapSo.QUEUE = Convert.ToString(GetValueField(reader, "TICKETNO", string.Empty));
            CapSo.HAWB = Convert.ToString(GetValueField(reader, "HAWB", string.Empty));
            return CapSo;
        }
        public List<CapSo> GetAll(DateTime? date)
        {
            List<CapSo> capsoList = new List<CapSo>();
            using (System.Data.IDataReader reader = CommandDataReader("GetAWBProcessByDate", date))
            {

                while (reader.Read())
                    capsoList.Add(GetProperties(reader));

            }
            return capsoList;
        }
    }
}
