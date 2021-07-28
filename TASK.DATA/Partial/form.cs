using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TASK.Settings;
using StorageManagement;

namespace TASK.DATA
{
    public partial class form
    {
        public static void Insert(List<form> listForm)
        {
            using (DBContextDataContext dbConext = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                if (listForm.Count > 0)
                {
                    dbConext.forms.InsertAllOnSubmit(listForm);
                }
                dbConext.SubmitChanges();
            }
        }
        public static void Update(List<form> listForm)
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                foreach (var form in listForm)
                {
                    var formDb = ct.forms.FirstOrDefault(c => c.ID == form.ID);
                    formDb.CutoffTime = form.CutoffTime;
                }
                ct.SubmitChanges();
            }

        }
        public static List<form> GetAll()
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.forms.ToList();
            }
        }
        public static string GetAWB(string key)
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                return ct.forms.FirstOrDefault(p => p.issue_key == key).AWB;
            }
        }
        public static List<form> GetListAWBToProcessCutOffTime()
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                //return ct.forms.Where(p => p.CutoffTime != null && p.createdDate > DateTime.Today).ToList();
                return ct.forms.Where(p => p.CutoffTime == null).ToList();
            }
        }
        public static void UpdateCutOffTime(string awb, DateTime cutOffTime)
        {
            using (DBContextDataContext ct = new DBContextDataContext(AppSetting.ConnectionStringSyncData))
            {
                    var formDB = ct.forms.FirstOrDefault(c => c.AWB == awb);
                     formDB.CutoffTime = cutOffTime;
                     ct.SubmitChanges();
            }
        }
        public static void Booking(string booking, ref string flightAriline, ref string flightNumber, ref string bookingDate)
        {
            string[] result = booking.Split('/');
            bookingDate = result[1];
            string flight = result[0];
            string flightNumberPattern = @"[\d]+";
            string flightAirlinePattern = @"[\D]+";
            flightAriline = Regex.Match(flight.Trim(), flightAirlinePattern).ToString();
            flightNumber = Regex.Match(flight.Trim(), flightNumberPattern).ToString();
        }
        public static DictionaryStorage<string, DateTime> LoadFromList(List<form> listForm)
        {
            DictionaryStorage<string, DateTime> dictData = new DictionaryStorage<string, DateTime>();
            foreach (var form in listForm)
            {
                DateTime dt = new DateTime();
                dictData.EnqueueElementData(form.booking,dt);
            }
            return dictData;

        }
    }
}
