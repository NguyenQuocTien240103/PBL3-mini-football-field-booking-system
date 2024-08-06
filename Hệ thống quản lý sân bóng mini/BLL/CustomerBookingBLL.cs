using Hệ_thống_quản_lý_sân_bóng_mini.DAL;
using Hệ_thống_quản_lý_sân_bóng_mini.demo;
using Hệ_thống_quản_lý_sân_bóng_mini.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Hệ_thống_quản_lý_sân_bóng_mini.BLL
{
    public class CustomerBookingBLL
    {
        private static CustomerBookingBLL _Instance;
        public static CustomerBookingBLL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CustomerBookingBLL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private CustomerBookingBLL() { }
        public DataTable ShowCustomerBooking(int idField = 0, string State = null,string TypeName = null, string FieldName = null, string search = null)
        {
            if (TypeName == null && FieldName == null && search == null)
            {
                TypeName = "";
                FieldName = "";
                search = "";
            }
            if (TypeName == "All" && FieldName == "All")
            {
                TypeName = "";
                FieldName = "";
            }
            if (FieldName == "All")
            {
                FieldName = "";
            }
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[]
            {
                    new DataColumn {ColumnName = "id", DataType = typeof(int)},
                    new DataColumn {ColumnName = "idField", DataType = typeof(int)},
                    new DataColumn {ColumnName = "TypeName", DataType = typeof(string)},
                    new DataColumn {ColumnName = "FieldName", DataType = typeof(string)},
                    new DataColumn {ColumnName = "CustomerName", DataType = typeof(string)},
                    new DataColumn {ColumnName = "CustomerPhone", DataType = typeof(string)},
                    new DataColumn {ColumnName = "startTime", DataType = typeof(string)},
                    new DataColumn {ColumnName = "endTime", DataType = typeof(string)},
                    new DataColumn {ColumnName = "priceBooking", DataType = typeof(float)},
                    new DataColumn {ColumnName = "status", DataType = typeof(string)},
                    new DataColumn {ColumnName = "bookingDay", DataType = typeof(string)}
            });
            List<CustomerBookingDetail> customerBookingDetails = CustomerBookingDetailDAL.Instance.LoadCustomerBookingById(State,idField);
            foreach (CustomerBookingDetail customerbooking in customerBookingDetails)
            {
                if (customerbooking.ToString().Contains(TypeName) && customerbooking.ToString().Contains(FieldName) && customerbooking.ToString().ToLower().Contains(search))
                {
                    dataTable.Rows.Add(customerbooking.Id, customerbooking.IdField,
                    customerbooking.TypeName, customerbooking.FieldName, customerbooking.CustomerName,
                    customerbooking.CustomerPhone, customerbooking.startTime.ToString("HH:mm"), customerbooking.endTime.ToString("HH:mm"),
                    customerbooking.priceBooking, customerbooking.status, customerbooking.Ngaydat.ToString("MM/dd/yyyy"));
                }
            }
            return dataTable;
        }
        public void AddCustomerBooking(int IdField,Customer Cus,float PriceBooking,DateTime StartTime,DateTime EndTime,DateTime Date,string State)
        {
            // đặt trục tiếp
            if(PriceBooking == 0)
            {
                CustomerDAL.Instance.InsertCustomer(Cus.Name, Cus.Phone);
                int IdCustomer = CustomerDAL.Instance.getIdCustomerLast(); // lấy ra id của Customer mới thêm vào
                CustomerBookingDAL.Instance.InSertCustomerBooking(IdCustomer, IdField, StartTime, EndTime, PriceBooking, State, Date);
                FieldDAL.Instance.updateFieldState(IdField, "busy"); //update Field sau khi đặt sân
            }
            // đặt trước
            if(PriceBooking > 0)
            {
                if (checktimeFieldNeedAdd(IdField, StartTime.ToString("HH:mm"), EndTime.ToString("HH:mm"), Date))
                {
                    CustomerDAL.Instance.InsertCustomer(Cus.Name, Cus.Phone);
                    int IdCustomer = CustomerDAL.Instance.getIdCustomerLast(); // lấy ra id của Customer mới thêm vào
                    CustomerBookingDAL.Instance.InSertCustomerBooking(IdCustomer, IdField, StartTime, EndTime, PriceBooking, State, Date);
                }
                else
                {
                    MessageBox.Show("Sân đã có người đặt");
                }
            }
        }
        public void UpdateCustomerBooking(int IdCustomerBooking,int IdField,DateTime StartTime,DateTime EndTime,DateTime Date, Customer Cus, float PriceBooking,string State)
        {
            if (checktimeFieldNeedUpdate(IdField,StartTime.ToString("HH:mm"), EndTime.ToString("HH:mm"), IdCustomerBooking,Date))
            {
                CustomerBooking customerBooking = CustomerBookingDAL.Instance.getCustomerBookingById(IdCustomerBooking);
                CustomerDAL.Instance.updateCustomer(customerBooking.IdCustomer, Cus.Name, Cus.Phone);
                CustomerBookingDAL.Instance.updateCustomerBooking(IdCustomerBooking, customerBooking.IdCustomer, IdField, StartTime, EndTime, PriceBooking, State, Date);
                if (State == "truc tiep")
                {
                    FieldDAL.Instance.updateFieldState(customerBooking.IdFieldName, "empty");
                    FieldDAL.Instance.updateFieldState(IdField,"busy");
                }
            }
            else
            {
                MessageBox.Show("Sân đã có người đặt");
            }
        }
        public void DelCustomerBooking(int IdCustomerBooking, float PriceBooking)
        {
            CustomerBookingDAL.Instance.updateStatusCustomerBookingById(IdCustomerBooking); // update status "da huy"
            BillDAL.Instance.insertBill(IdCustomerBooking, PriceBooking); // add bill
        }
        public void ConfirmCustomerBooking(int IdCustomerBooking,int IdField,string State)
        {
            if (checkFieldStatus(IdField))
            {
                FieldDAL.Instance.updateFieldState(IdField, State);
                CustomerBookingDAL.Instance.updateCustomerBookingById(IdCustomerBooking);
            }
            else
            {
                MessageBox.Show("Sân đang có người đá");
            }
        }
        public bool checkFieldStatus(int idField)
        {
            Field fields = FieldDAL.Instance.getFieldByIdField(idField);
            if (fields.Status == "empty")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool checktimeFieldNeedAdd(int idField, string startTime, string endTime, DateTime ngaydat)
        {
            bool check = true;
            List<CustomerBooking> customerbookings = CustomerBookingDAL.Instance.GetCustomerBookingByIdField(idField);
            foreach (CustomerBooking customerbooking in customerbookings)
            {
                if ((customerbooking.Status == "dat truoc" || customerbooking.Status == "truc tiep") && customerbooking.Ngaydat.ToString("MM/dd/yyyy") == ngaydat.ToString("MM/dd/yyyy"))
                {
                    if (compare(endTime, customerbooking.StartTime.ToString("HH:mm")) == true && compare(endTime, customerbooking.EndTime.ToString("HH:mm")) == false)
                    {
                        check = false;
                        break;
                    }
                    if (compare(startTime, customerbooking.StartTime.ToString("hh:mm")) == true && compare(startTime, customerbooking.EndTime.ToString("hh:mm")) == false)
                    {
                        check = false;
                        break;
                    }
                    if (compare(startTime, customerbooking.StartTime.ToString("hh:mm")) == false && compare(endTime, customerbooking.EndTime.ToString("hh:mm")) == true)
                    {
                        check = false;
                        break;
                    }
                }
            }
            return check;
        }
        public bool checktimeFieldNeedUpdate(int idField, string startTime, string endTime, int idCustomerBooking, DateTime ngaydat)
        {
            bool check = true;
            List<CustomerBooking> customerbookings = CustomerBookingDAL.Instance.GetCustomerBookingByIdField(idField);
            foreach (CustomerBooking customerbooking in customerbookings)
            {
                if ((customerbooking.Status == "dat truoc" || customerbooking.Status == "truc tiep") && customerbooking.IdFieldName == idField && customerbooking.Id != idCustomerBooking && customerbooking.Ngaydat == ngaydat)
                {
                    if (compare(endTime, customerbooking.StartTime.ToString("HH:mm")) == true && compare(endTime, customerbooking.EndTime.ToString("HH:mm")) == false)
                    {
                        check = false;
                        break;
                    }
                    if (compare(startTime, customerbooking.StartTime.ToString("hh:mm")) == true && compare(startTime, customerbooking.EndTime.ToString("hh:mm")) == false)
                    {
                        check = false;
                        break;
                    }
                    if (compare(startTime, customerbooking.StartTime.ToString("hh:mm")) == false && compare(endTime, customerbooking.EndTime.ToString("hh:mm")) == true)
                    {
                        check = false;
                        break;
                    }
                }
            }
            return check;
        }
        public bool compare(string time1, string time2)
        {
            int minutes1 = TimeToMinutes(time1);
            int minutes2 = TimeToMinutes(time2);
            if (minutes1 > minutes2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int TimeToMinutes(string time)
        {
            string[] parts = time.Split(':');
            int hours = int.Parse(parts[0]);
            int minutes = int.Parse(parts[1]);
            return hours * 60 + minutes;
        }
        public CustomerBooking GetCustomerBooking(int IdField, string status)
        {
                return CustomerBookingDAL.Instance.GetBookingByFieldAndStatus(IdField, status);
        }
    }
}
