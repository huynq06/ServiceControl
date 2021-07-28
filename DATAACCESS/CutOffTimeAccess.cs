using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Model.DBModel;
using StorageManagement;
using TASK.DATA;

namespace DATAACCESS
{
    public class CutOffTimeAccess : DATABASE.OracleProvider
    {
        private DateTime GetProperties(OracleDataReader reader)
        {
            return Convert.ToDateTime(GetValueField(reader, "CutOFF_TIME", 0));
        }

        public void GetCutOffTimeByAWB(DictionaryStorage<string,DateTime> dt)
        {
            foreach (var booking in dt.GetKeys())
            {
                string flightAirline = "";
                string flightNumber = "";
                string flightDate = "";
                form.Booking(booking, ref flightAirline,ref flightNumber,ref flightDate);
                string sql = "SELECT DISTINCT (labs.labs_mawb_prefix || labs.labs_mawb_serial_no) as AWB,labs.labs_quantity_booked as PIECES,book.book_flight_airline as BOOK_FLIGHT_AIRLINE,book.book_flight_number as BOOK_FLIGHT_NUMBER,to_char(book.book_flight_date, 'DDMON') as BOOK_FLIGHT_DATE, " +
                "(select to_date(to_date('02-01-0001', 'DD-MM-YYYY') + flup.flup_lat_date || ' ' || to_char(to_date(flup.flup_lat_time, 'HH24MISS'), 'HH24:MI:SS'), 'dd/mm/RR HH24:MI:SS') as CUTOFF_DATETIME from flup " +
                      "where flup.flup_flight_no_lvg = book.book_flight_airline and flup.flup_flight_no = book.book_flight_number and to_date('02-01-0001', 'DD-MM-YYYY') + flup.flup_scheduled_date = book.book_flight_date ) as CutOFF_TIME " +
              "from labs join book_bookings book on book.book_labs_ident = labs.labs_ident_no " +
            "and book.book_flight_airline = " + "'" + flightAirline + "'" +
            "and to_char(book.book_flight_date, 'DDMON') =" + "'" + flightDate + "'" +
            "and BOOK_FLIGHT_NUMBER = " + "'" + flightNumber + "'" +
            " order by CutOFF_TIME desc fetch first 1 row only ";
                DateTime getCutOffTime = new DateTime();

                using (OracleDataReader reader = GetScriptOracleDataReader(sql))
                {
                    if (reader.Read())
                        getCutOffTime = GetProperties(reader);

                    if (getCutOffTime.ToString() != "1/1/0001 12:00:00 AM")
                    {
                        int year = getCutOffTime.Year;
                        int yearNow = DateTime.Now.Year;
                        string getCutOffTimeString = getCutOffTime.ToString().Replace(year.ToString(), yearNow.ToString());
                        getCutOffTime = Convert.ToDateTime(getCutOffTimeString);
                        dt.SetValueKey(booking, getCutOffTime);
                    }
                        
                    else
                    {
                        dt.DequeueElementData(booking);
                    }

                }
               
            }
            
        }
    }
}
