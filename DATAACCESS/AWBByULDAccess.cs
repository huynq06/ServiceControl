using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using TASK.Model.DBModel;


namespace DATAACCESS
{
    public class AWBByULDAccess : DATABASE.OracleProvider
    {
        private AWBByULD GetProperties(OracleDataReader reader)
        {
            AWBByULD awb = new AWBByULD();
            awb.Lagi_Identity = Convert.ToInt32(GetValueField(reader, "LAGI_INDENT_NO", 0));
            awb.AWB = Convert.ToString(GetValueField(reader, "AWB", string.Empty));
            awb.SHC = Convert.ToString(GetValueField(reader, "SHC", string.Empty));
            awb.CheckValue = Convert.ToInt32(GetValueField(reader, "CheckCol", 0));
            return awb;
        }
        public List<AWBByULD> GetAWBByULD(Flight flight)
        {
            //lay danh code hang lanh
            List<SHCConfig> shcConfigs = SHCConfig.GetAll();
            List<SHC> listCode = new List<SHC>();
            string query = "";
            foreach (var config in shcConfigs)
            {
              
                if (!listCode.Any(c=>c.Code==config.Code))
                {
                    SHC shc = new SHC();
                    shc.Code = config.Code;
                    listCode.Add(shc);
                }
            }
            foreach (var code in listCode)
            {
                query = query + " WHEN listagg(t.SHC, ',') within group(order by t.SHC) LIKE '%"+code.Code+"%' THEN 1 ";
            }
            string sql = "select LAGI_INDENT_NO,FLIGHT_NO,ATA_DATE,AWB, " +
                         "listagg(t.SHC, ',') within group(order by t.SHC) as SHC, " +
                           "CASE " +
            query +
            "ELSE 0 " +
            "END AS CheckCol " +
                          "from " +
                "(SELECT " +
  "lagi.lagi_ident_no as LAGI_INDENT_NO, " +
  "flui.flui_al_2_3_letter_code || flui.flui_flight_no AS FLIGHT_NO, " +
  "to_date('02-01-0001' , 'DD-MM-YYYY') +flui.flui_landed_date AS ATA_DATE, " +
  "awbu.awbu_mawb_prefix || awbu.awbu_mawb_serial_no as AWB, " +
  "awbu.awbu_specialhandlingcodes SHC, " +
      "CASE " +
            "WHEN awbu.awbu_specialhandlingcodes LIKE '%COL%' THEN " +
            "1 " +
            "ELSE 0 " +
            "END AS CheckCol " +
  "FROM flui flui " +
  "LEFT JOIN PALO palo " +
  "on palo.palo_lvg_in = flui.flui_al_2_3_letter_code " +
  "and palo.palo_flight_no_in = flui.flui_flight_no " +
  "and to_date('02-01-0001' , 'DD-MM-YYYY') +flui.flui_schedule_date = to_date('02-01-0001', 'DD-MM-YYYY') + palo.palo_flight_arrival_date " +
  "LEFT JOIN AWBU_AWBPERULD_LIST awbu " +
     "on awbu.awbu_uld_isn = palo.palo_uld_isn " +
            "and awbu.awbu_uld_serial = palo.palo_serial_no_ " +
            "and awbu.awbu_uld_no = palo.palo_type " +
            "and awbu.awbu_uld_owner = palo.palo_owner " +
            "and awbu.awbu_object_type = 'IMPORT AWB' " +
"LEFT JOIN LAGI lagi " +
     "on awbu.awbu_mawb_prefix = lagi.lagi_mawb_prefix " +
     "and awbu.awbu_mawb_serial_no = lagi.lagi_mawb_no " +
"WHERE lagi.lagi_deleted = 0 " +
   "AND flui.flui_al_2_3_letter_code || flui.flui_flight_no = '" + flight.FlightNumber + "' " +
             " AND flui.flui_schedule_date =" + flight.FLUI_SCHEDULE_DATE +
             " AND flui.flui_schedule_time = " + flight.FLUI_SCHEDULE_TIME +
  "AND lagi.lagi_hawb = ' ' " +
  "AND lagi.LAGI_GOODS_CONTENT NOT LIKE '%MAIL%' " +
"GROUP BY " +
      "lagi.lagi_ident_no, " +
      "flui.flui_al_2_3_letter_code || flui.flui_flight_no, " +
      "to_date('02-01-0001', 'DD-MM-YYYY') + flui.flui_landed_date, " +
      "awbu.awbu_mawb_prefix || awbu.awbu_mawb_serial_no, " +
         "awbu.awbu_specialhandlingcodes " +
"ORDER BY ATA_DATE, FLIGHT_NO, AWB ASC)t group by LAGI_INDENT_NO,FLIGHT_NO,ATA_DATE,AWB";
            List<AWBByULD> awbs = new List<AWBByULD>();
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                while (reader.Read())
                {
                    AWBByULD awb = GetProperties(reader);

                    awbs.Add(awb);
                }

            }
            return awbs;
        }
        public List<AWBByULD> GetAWBByULDTest(Flight flight)
        {
            string sql = "select LAGI_INDENT_NO,FLIGHT_NO,ATA_DATE,AWB, " +
                         "listagg(t.SHC, ',') within group(order by t.SHC) as SHC, " +
                           "CASE " +
            "WHEN listagg(t.SHC, ',') within group(order by t.SHC) LIKE '%COL%' THEN " +
            "1 " +
            "ELSE 0 " +
            "END AS CheckCol " +
                          "from " +
                "(SELECT " +
  "lagi.lagi_ident_no as LAGI_INDENT_NO, " +
  "flui.flui_al_2_3_letter_code || flui.flui_flight_no AS FLIGHT_NO, " +
  "to_date('02-01-0001' , 'DD-MM-YYYY') +flui.flui_landed_date AS ATA_DATE, " +
  "awbu.awbu_mawb_prefix || awbu.awbu_mawb_serial_no as AWB, " +
  "awbu.awbu_specialhandlingcodes SHC, " +
      "CASE " +
            "WHEN awbu.awbu_specialhandlingcodes LIKE '%COL%' THEN " +
            "1 " +
            "ELSE 0 " +
            "END AS CheckCol " +
  "FROM flui flui " +
  "LEFT JOIN PALO palo " +
  "on palo.palo_lvg_in = flui.flui_al_2_3_letter_code " +
  "and palo.palo_flight_no_in = flui.flui_flight_no " +
  "and to_date('02-01-0001' , 'DD-MM-YYYY') +flui.flui_schedule_date = to_date('02-01-0001', 'DD-MM-YYYY') + palo.palo_flight_arrival_date " +
  "LEFT JOIN AWBU_AWBPERULD_LIST awbu " +
     "on awbu.awbu_uld_isn = palo.palo_uld_isn " +
            "and awbu.awbu_uld_serial = palo.palo_serial_no_ " +
            "and awbu.awbu_uld_no = palo.palo_type " +
            "and awbu.awbu_uld_owner = palo.palo_owner " +
            "and awbu.awbu_object_type = 'IMPORT AWB' " +
"LEFT JOIN LAGI lagi " +
     "on awbu.awbu_mawb_prefix = lagi.lagi_mawb_prefix " +
     "and awbu.awbu_mawb_serial_no = lagi.lagi_mawb_no " +
"WHERE lagi.lagi_deleted = 0 " +
   "AND flui.flui_al_2_3_letter_code || flui.flui_flight_no = '" + flight.FlightNumber + "' " +
             " AND flui.flui_schedule_date =" + flight.FLUI_SCHEDULE_DATE +
             " AND flui.flui_schedule_time = " + flight.FLUI_SCHEDULE_TIME +
  "AND lagi.lagi_hawb = ' ' " +
  "AND lagi.LAGI_GOODS_CONTENT NOT LIKE '%MAIL%' " +
"GROUP BY " +
      "lagi.lagi_ident_no, " +
      "flui.flui_al_2_3_letter_code || flui.flui_flight_no, " +
      "to_date('02-01-0001', 'DD-MM-YYYY') + flui.flui_landed_date, " +
      "awbu.awbu_mawb_prefix || awbu.awbu_mawb_serial_no, " +
         "awbu.awbu_specialhandlingcodes " +
"ORDER BY ATA_DATE, FLIGHT_NO, AWB ASC)t group by LAGI_INDENT_NO,FLIGHT_NO,ATA_DATE,AWB";
            List<AWBByULD> awbs = new List<AWBByULD>();
            using (OracleDataReader reader = GetScriptOracleDataReader(sql))
            {
                while (reader.Read())
                {
                    AWBByULD awb = GetProperties(reader);

                    awbs.Add(awb);
                }

            }
            return awbs;
        }

    }
    }

