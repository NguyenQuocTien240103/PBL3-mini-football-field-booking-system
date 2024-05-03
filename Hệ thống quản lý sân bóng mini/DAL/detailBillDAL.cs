using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hệ_thống_quản_lý_sân_bóng_mini.DAL
{
    public class detailBillDAL
    {
        private static detailBillDAL _Instance;
        public static detailBillDAL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new detailBillDAL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private detailBillDAL() { }

        
    }
}
