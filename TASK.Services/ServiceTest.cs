using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.DATA;
using DATAACCESS;
using TASK.Model.DBModel;
namespace TASK.Services
{
    public class ServiceTest
    {
        public static void ProgramTest()
        {
            System.Text.StringBuilder content = new StringBuilder();
            //lấy ra danh sách xe gọi vào để hiện thị lên bảng led
            //TH1:- số lượng vị trí còn trống > 10
            //List<tblTicketStatus> listCheckIn = _ticketService.GetListTicketMonthyCheckIn().ToList();
            //List<tblTicketStatus> listTruckMonthlyCheckIn = listCheckIn.Where(c => c.ActionValue != "GATEOUT").ToList();
            //List<tblTicketStatus> listToRemove = new List<tblTicketStatus>();
            //foreach (var item in listTruckMonthlyCheckIn)
            //{
            //    if (listCheckIn.Where(c => c.TicketUID == item.TicketUID && c.ActionValue == "GATEOUT").Count() > 0)
            //    {
            //        item.ActionCode = "CHECK_OUT";
            //    }
            //}
            //listTruckMonthlyCheckIn = listTruckMonthlyCheckIn.Where(c => c.ActionCode == "CHECK_IN").ToList();
            //List<tblDangKyVaoRa> ListTruckOnFloor2 = tblDangKyVaoRa.GetTruckSecondFloor();
            //List<tblDangKyVaoRa> ListTruckOnFloor1 = tblDangKyVaoRa.GetTruckFirstFloor();
            //double countFloor2 = 0;
            //foreach(var truck in ListTruckOnFloor2)
            //{
            //    if(truck.TrongTai.Trim() == "Xe tải 3.5-7 tấn" || truck.TrongTai.Trim() == "Xe > 7 tấn")
            //    {
            //        countFloor2 += 1.5;
            //    }
            //    else
            //    {
            //        countFloor2 += 1;
            //    }
            //}
            List<tblTicketStatus> listTruckMonthyToday = tblTicketStatus.GetAllToDay();
            List<tblTicketStatus> listTruckMonthlyCheckInT2 = listTruckMonthyToday.Where(c => c.ActionValue == "GATEIN_T2").ToList();
            int countTruckMonthlyCheckInT2 = listTruckMonthlyCheckInT2.Count();
            if(countTruckMonthlyCheckInT2 > 0)
            {
                for (int i = listTruckMonthlyCheckInT2.Count - 1; i >= 0; i--)
                {
                    string bsx = listTruckMonthlyCheckInT2[i].TicketUID.ToString();
                    // some code
                    // safePendingList.RemoveAt(i);
                    if (listTruckMonthyToday.Where(c => c.TicketUID == listTruckMonthlyCheckInT2[i].TicketUID && c.ActionValue == "GATEOUT").Count() > 0)
                    {
                        listTruckMonthlyCheckInT2.RemoveAt(i);
                    }
                }
                //foreach (var truck in listTruckMonthlyCheckInT2)
                //{
                //    if(listTruckMonthyToday.Where(c=>c.TicketUID==truck.TicketUID && c.ActionValue== "GATEOUT").Count() > 0)
                //    {
                //        countTruckMonthlyCheckInT2 -= 1;
                //    }
                //}
            }
            List<tblTicketStatus> listTruckMonthlyCheckInT1 = listTruckMonthyToday.Where(c => c.ActionValue == "GATEIN_T1").ToList();
            int countTruckMonthlyCheckInT1 = listTruckMonthlyCheckInT1.Count();
            if (countTruckMonthlyCheckInT1 > 0)
            {
                for (int i = listTruckMonthlyCheckInT1.Count - 1; i >= 0; i--)
                {
                    // some code
                    // safePendingList.RemoveAt(i);
                    if (listTruckMonthyToday.Where(c => c.TicketUID == listTruckMonthlyCheckInT1[i].TicketUID && c.ActionValue == "GATEOUT").Count() > 0)
                    {
                        listTruckMonthlyCheckInT1.RemoveAt(i);
                    }
                }
                //foreach (var truck in listTruckMonthlyCheckInT1)
                //{
                //    if (listTruckMonthyToday.Where(c => c.TicketUID == truck.TicketUID && c.ActionValue == "GATEOUT").Count() > 0)
                //    {
                //        countTruckMonthlyCheckInT1 -= 1;
                //    }
                //}
            }

            double totalCountTruckinFLoor2 = listTruckMonthlyCheckInT2.Count;
            //ktra danh sach nay co xe nao da dc check out chua
            //tiep tuc dem xe ve thang vao tang 1 + 2
            var setting2 = LocationConfig.GetCheckPoint(2);
            var setting1 = LocationConfig.GetCheckPoint(1);
            int space2 = setting2.TotalSpace.Value - (int)Math.Ceiling(totalCountTruckinFLoor2);
            int spaceEmptyFloor2 = space2 < 0 ? 0 : space2;
            int space1 = setting1.TotalSpace.Value - listTruckMonthlyCheckInT1.Count;
            int spaceEmptyFloor1 = space1 < 0 ? 0 : space1;
            #region Normal
            //if (spaceEmptyFloor2 > setting2.ThresholdPoint)
            if (true)
            {
               List<CallTruckModel> listCallTruckFloor2 = new CallTruckAccess().GetDataFloor2().Take(40).OrderBy(c=>c.GioGoi).ToList();
                for (int i = listCallTruckFloor2.Count - 1; i >= 0; i--)
                {
                    var truck = listTruckMonthlyCheckInT2.FirstOrDefault(c => c.BienSoXe == listCallTruckFloor2[i].BienSoXe);
                    if (truck != null && truck.ActionDateTime > DateTime.Now.AddHours(-1) && truck.ActionCode == "CHECK_IN")
                    {
                        var truckCheckAgain = listTruckMonthlyCheckInT2.FirstOrDefault(c => c.TicketUID == truck.TicketUID && c.ActionCode == "CHECK_OUT");
                        if(truckCheckAgain==null)
                        {
                            listCallTruckFloor2.RemoveAt(i);
                        }
                    }
                }
                if(listCallTruckFloor2.Count > 0)
                {
                    DateTime dateCheck = listCallTruckFloor2[0].GioGoi.Value;
                    if(listCallTruckFloor2.Count > 1)
                    {
                        for (int i = 1; i < listCallTruckFloor2.Count; i++)
                        {
                            if (dateCheck.AddMilliseconds(60 * 1000) > listCallTruckFloor2[i].GioGoi && listCallTruckFloor2[1].SortValue != 1)
                            {
                                listCallTruckFloor2[i].GioGoi = dateCheck.AddMilliseconds(60 * 1000);
                            }
                            dateCheck = listCallTruckFloor2[i].GioGoi.Value;
                        }
                    }

                    
                }
                
                listCallTruckFloor2 = listCallTruckFloor2.Where(c=>c.GioGoi.Value < DateTime.Now).OrderByDescending(c=>c.SortValue).Take(spaceEmptyFloor2).ToList();
                List<CallTruckModel> listCallTruckFloor1 = new CallTruckAccess().GetDataFloor1().Take(20).ToList();
                for (int i = listCallTruckFloor1.Count - 1; i >= 0; i--)
                {
                    var truck = listTruckMonthlyCheckInT1.FirstOrDefault(c => c.BienSoXe == listCallTruckFloor1[i].BienSoXe);
                    if (truck != null && truck.ActionDateTime > DateTime.Now.AddHours(-1) && truck.ActionCode == "CHECK_IN")
                    {
                        var truckCheckAgain = listTruckMonthlyCheckInT1.FirstOrDefault(c => c.TicketUID == truck.TicketUID && c.ActionCode == "CHECK_OUT");
                        listCallTruckFloor1.RemoveAt(i);
                    }
                    // some code
                    // safePendingList.RemoveAt(i);
                }
                List<CallTruckModel> listCallTruckALSx = new CallTruckAccess().GetDataALSx().Take(20).ToList();
                for (int i = listCallTruckALSx.Count - 1; i >= 0; i--)
                {
                    var truck = listTruckMonthlyCheckInT1.FirstOrDefault(c => c.BienSoXe == listCallTruckALSx[i].BienSoXe);
                    if (truck != null && truck.ActionDateTime > DateTime.Now.AddHours(-1))
                    {
                        listCallTruckALSx.RemoveAt(i);
                    }
                    // some code
                    // safePendingList.RemoveAt(i);
                }
                listCallTruckFloor2.AddRange(listCallTruckFloor1);
                listCallTruckFloor2.AddRange(listCallTruckALSx);
                List<CallTruckModel> listTruck = listCallTruckFloor2.OrderByDescending(c => c.SortValue).ThenBy(c => c.ThoiGianDangKy).ToList();
                List<CallTruck> listCallTruck = new List<CallTruck>();
                int index = 0;
                foreach (var item in listTruck)
                {
                    index += 1;
                    string color = "";
                    if (item.ViTri == 1)
                    {
                        color = "<R>";
                    }
                    else if (item.ViTri == 3)
                    {
                        color = "<Y>";
                    }
                    else
                    {
                        color = "<G>";
                    }
                    string space = "";
                    if (item.BienSoXe.Length == 9)
                    {
                        space = "    ";
                    }
                    else if (item.BienSoXe.Length == 8)
                    {
                        space = "     ";
                    }
                    else if (item.BienSoXe.Length == 10)
                    {
                        space = "   ";
                    }
                    else if (item.BienSoXe.Length == 11)
                    {
                        space = "  ";
                    }
                    else
                    {
                        space = " ";
                    }
                    if(item.GioGoi.HasValue)
                    {
                        if (item.ViTri == 3)
                            content.AppendLine(color + item.BienSoXe + space + color + item.GioGoi.Value.ToString("HH:mm") + "   " + color + "ALSX " + color + "KD");
                        else
                            content.AppendLine(color + item.BienSoXe + space + color + item.GioGoi.Value.ToString("HH:mm") + "   " + color + "TANG " + color + item.ViTri);
                    }
                    else
                    {
                        if (item.ViTri == 3)
                            content.AppendLine(color + item.BienSoXe + space + color + item.ThoiGianDangKy.Value.ToString("HH:mm") + "   " + color + "ALSX " + color + "KD");
                        else
                            content.AppendLine(color + item.BienSoXe + space + color + item.ThoiGianDangKy.Value.ToString("HH:mm") + "   " + color + "TANG " + color + item.ViTri);
                    }
                    CallTruck ct = new CallTruck();
                    ct.SortValue = item.SortValue.HasValue ? item.SortValue : 0;
                    ct.BSX = item.BienSoXe;
                    ct.Created = item.ThoiGianDangKy;
                    ct.Floor = item.ViTri;
                    ct.Remark = "";
                    ct.SpaceEmptyFloor2 = spaceEmptyFloor2;
                    ct.SpaceEmptyFloor1 = spaceEmptyFloor1;
                    ct.VCT = "";
                    ct.Dock = item.Note;
                    ct.TimeCalled = item.GioGoi;
                    listCallTruck.Add(ct);
                }
                //  tblDangKyGoiXe.UpdateCallTruck(listTruck);
                //update 
                // lấy ra danh sách xe đc vào ALSC
                CallTruck.Insert(listCallTruck);
            }
            #endregion
            #region Xungđột
            else
            {
                //TH2- số lượng vị trí còn trống < 10
                List<CallTruckModel> listCallTruckFloor2 = new CallTruckAccess().GetDataFloor2().Where(c => c.ViTri == 2).ToList();
                foreach (var item in listCallTruckFloor2)
                {
                    if (!string.IsNullOrEmpty(item.LoHang) && item.LoHang.StartsWith("3003") && (item.SortValue == 0 || item.SortValue == 2))
                    {
                        try
                        {

                            if (new CheckMoveIDAAccess().CheckMoveAllToIDA(item.LoHang))
                            {
                                tblDangKyGoiXe.UpdateSortValue(1, item.ID);
                            }
                        }
                        catch (Exception ex)
                        {

                            Log.WriteLog(ex.ToString(), "CallTruck.txt");
                        }
                    }

                }
                //   List<tblDangKyGoiXe> listCallTruckFloor2Select = tblDangKyGoiXe.GetListTruckFloor2Select(spaceEmptyFloor2);
                //List<CallTruckModel> listCallTruckFloor2Select = new CallTruckAccess().GetDataFloor2().OrderByDescending(c => c.SortValue).ThenBy(c => c.ThoiGianDangKy).Take(30).ToList();
                List<CallTruckModel> listCallTruckFloor2Select = new CallTruckAccess().GetDataFloor2().OrderByDescending(c => c.SortValue).ThenBy(c => c.ThoiGianDangKy).Take(40).ToList();
                for (int i = listCallTruckFloor2Select.Count - 1; i >= 0; i--)
                {
                    if (listTruckMonthlyCheckInT2.FirstOrDefault(c => c.BienSoXe == listCallTruckFloor2Select[i].BienSoXe) != null)
                    {
                        listCallTruckFloor2Select.RemoveAt(i);
                    }
                    // some code
                    // safePendingList.RemoveAt(i);
                }
                List<CallTruckModel> listCallTruckFloor1 = new CallTruckAccess().GetDataFloor1().OrderByDescending(c => c.SortValue).ThenBy(c => c.ThoiGianDangKy).Take(20).ToList();
                for (int i = listCallTruckFloor1.Count - 1; i >= 0; i--)
                {
                    if (listTruckMonthlyCheckInT1.FirstOrDefault(c => c.BienSoXe == listCallTruckFloor1[i].BienSoXe) != null)
                    {
                        listCallTruckFloor1.RemoveAt(i);
                    }
                    // some code
                    // safePendingList.RemoveAt(i);
                }
                List<CallTruckModel> listCallTruckALSx = new CallTruckAccess().GetDataALSx().Take(20).ToList();
                for (int i = listCallTruckALSx.Count - 1; i >= 0; i--)
                {
                    if (listTruckMonthlyCheckInT1.FirstOrDefault(c => c.BienSoXe == listCallTruckALSx[i].BienSoXe) != null)
                    {
                        listCallTruckALSx.RemoveAt(i);
                    }
                    // some code
                    // safePendingList.RemoveAt(i);
                }
                //List<CallTruckModel> listCallTruckFloor1 = new CallTruckAccess().GetDataFloor1().OrderByDescending(c => c.SortValue).ThenBy(c => c.ThoiGianDangKy).Take(50).ToList();
                listCallTruckFloor2Select.AddRange(listCallTruckFloor1);
                listCallTruckFloor2Select.AddRange(listCallTruckALSx);
                List<CallTruck> listCallTruck = new List<CallTruck>();
                int index = 0;
                List<CallTruckModel> listTruck = listCallTruckFloor2Select.OrderByDescending(c => c.SortValue).ThenBy(c => c.ThoiGianDangKy).ToList();
                foreach (var item in listTruck)
                {
                    index += 1;
                    string color = "";
                    if (item.ViTri == 1)
                    {
                        color = "<R>";
                    }
                    else if (item.ViTri == 3)
                    {
                        color = "<Y>";
                    }
                    else
                    {
                        color = "<G>";
                    }
                    string space = "";
                    if (item.BienSoXe.Length == 9)
                    {
                        space = "    ";
                    }
                    else if (item.BienSoXe.Length == 8)
                    {
                        space = "     ";
                    }
                    else if (item.BienSoXe.Length == 10)
                    {
                        space = "   ";
                    }
                    else if (item.BienSoXe.Length == 11)
                    {
                        space = "  ";
                    }
                    else
                    {
                        space = " ";
                    }
                    if (item.ViTri == 3)
                        content.AppendLine(color + item.BienSoXe + space + color + item.ThoiGianDangKy.Value.ToString("HH:mm") + "   " + color + "ALSX " + color + "KD");
                    else
                        content.AppendLine(color + item.BienSoXe + space + color + item.ThoiGianDangKy.Value.ToString("HH:mm") + "   " + color + "TANG " + color + item.ViTri);
                    CallTruck ct = new CallTruck();
                    ct.SortValue = item.SortValue.HasValue ? item.SortValue : 0;
                    ct.BSX = item.BienSoXe;
                    ct.Created = item.ThoiGianDangKy;
                    ct.Floor = item.ViTri;
                    ct.Remark = item.Remark;
                    ct.SpaceEmptyFloor2 = spaceEmptyFloor2;
                    ct.SpaceEmptyFloor1 = spaceEmptyFloor1;
                    ct.VCT = item.LoHang;
                    listCallTruck.Add(ct);
                }
                //     tblDangKyGoiXe.UpdateCallTruck(listTruck);
                //update trang thai dang goi
                CallTruck.Insert(listCallTruck);
                //kiem tra co lô hàng nào đã đc trả thi set ưu tiên = 1
            }
            #endregion


            string result = content.ToString();
            Log.WriteCfsLog(result, "Dulieu.txt");
            Log.WriteCfsLog(spaceEmptyFloor1.ToString().PadLeft(2,'0') + " " + spaceEmptyFloor2.ToString().PadLeft(2, '0'), "ChoTrong.txt");
           // Log.WriteCfsLog(spaceEmptyFloor1.ToString(), "Floor1.txt");
        }
    }
}
