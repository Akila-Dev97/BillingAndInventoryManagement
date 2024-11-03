using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyStore.BLL
{
    internal class transactionBLL
    {
        public int id { get; set; }
        public string type { get; set; }
        public int dea_cust_id { get; set; }
        public decimal grandTotal { get; set; }
        public DateTime transaction_date { get; set; }
        public string tax { get; set; }
        public decimal discount { get; set; }
        public int added_by { get; set; }
    }
}
