using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using TASK.Settings;

namespace TASK.Services
{
    public class FormService
    {
        public static form GetFormDetailFromAPI(string issueKey)
        {
            form form = new form();
            HttpRequest rq = new HttpRequest();
            rq.Credentials = new Credentials()
            {
                UserName = AppSetting.USER_NAME,
                Password = AppSetting.PASSWORD
            };
            string url = "http://support.als.com.vn:8882/rest/api/2/issue/" + issueKey + "/properties/proforma.forms.i1";
            rq.Url = url;
            bool check = false;
            string rp = rq.Execute(null, "GET", "", false, issueKey + " GetFormDetailFromAPI",ref check);
            //data_Issue data_Issue = JsonConvert.DeserializeObject<data_Issue>(rp);
            var jOject = Newtonsoft.Json.Linq.JObject.Parse(rp);
            form.author = Convert.ToString(jOject["value"]["author"]);
            form.AWB = Convert.ToString(jOject["value"]["questions"][1]["text"]);
            var quantity = jOject["value"]["questions"][2]["text"];
            var weight = jOject["value"]["questions"][3]["text"];
            var booking = jOject["value"]["questions"][4]["text"];
            var custom_status = jOject["value"]["questions"][5]["choices"][0]["selected"];
            var car_number = jOject["value"]["questions"][7]["text"];
            var phone_number = jOject["value"]["questions"][8]["text"];
            var email = jOject["value"]["questions"][9]["text"];
            form.quantity = quantity != null ? Convert.ToString(quantity) : "";
            form.weight = weight != null ?  Convert.ToString(weight) : "";
            form.booking = booking != null ? Convert.ToString(booking) : "";
            form.custom_status = custom_status != null ? true : false;
            form.car_number = car_number != null ? Convert.ToString(car_number) : "";
            form.phone_number = phone_number != null ? Convert.ToString(phone_number) : "";
            form.email = email != null  ? Convert.ToString(email) : "";
            form.issue_key = issueKey;
            form.createdDate = DateTime.Now;
            return form;

        }
    }
}
