using Hệ_thống_quản_lý_sân_bóng_mini.DAL;
using Hệ_thống_quản_lý_sân_bóng_mini.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hệ_thống_quản_lý_sân_bóng_mini.BLL
{
    public class BillBLL
    {
        private static BillBLL _Instance;
        public static BillBLL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BillBLL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private BillBLL() { }

        public DataTable ShowBill(string date = null)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[]
            {
                    new DataColumn {ColumnName = "name", DataType = typeof(string)},
                    new DataColumn {ColumnName = "phone", DataType = typeof(string)},
                    new DataColumn {ColumnName = "FieldType", DataType = typeof(string)},
                    new DataColumn {ColumnName = "FieldName", DataType = typeof(string) },
                    new DataColumn {ColumnName = "startTime", DataType = typeof(string)},
                    new DataColumn {ColumnName = "endTime", DataType = typeof(string)},
                    new DataColumn {ColumnName = "priceBooking", DataType = typeof(float)},
                    new DataColumn {ColumnName = "status", DataType = typeof(string)},
                    new DataColumn {ColumnName = "totalPrice", DataType = typeof(float)},
                    new DataColumn {ColumnName = "payMentDay", DataType = typeof(string)}
            });
            List<BillDetail> listBillDetal = new List<BillDetail>();
            if (date == null)
            {
                listBillDetal =  BillDetailDAL.Instance.LoadBill(date);
            }
            else
            {
                listBillDetal = BillDetailDAL.Instance.LoadBill(date);
            }
            foreach (BillDetail billdetail in listBillDetal)
            {
                dataTable.Rows.Add(billdetail.Name, billdetail.Phone, billdetail.FieldType, billdetail.FieldName,
                    billdetail.StartTime.ToString("HH:mm"), billdetail.EndTime.ToString("HH:mm"),
                    billdetail.PriceBooking, billdetail.Status, billdetail.TotalPrice, billdetail.PaymentDay.ToString("MM/dd/yyyy"));
            }
            return dataTable;
        }
        public void AddBill(int idField, float price)
        {
            string BookingStatus = "truc tiep";
            string FieldStatus = "empty";
            // lấy ra id của đơn đã đặt sân bằng id sân và trạng thái 'truc tiep' của đơn đó
            int idCustomerBooking = CustomerBookingDAL.Instance.GetBookingByFieldAndStatus(idField, BookingStatus).Id;
            // thêm Bill
            BillDAL.Instance.insertBill(idCustomerBooking, price);
            // update trạng thái của đơn sau khi Bill đã được thêm ( trạng thái 'đã thanh toán' )
            CustomerBookingDAL.Instance.updateCustomerBookingDone(idField);
            // update lại trạng thái sân ( 'empty')
            FieldDAL.Instance.updateFieldState(idField, FieldStatus);
        }   

    }
}
