using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDataKPIISO.Models
{
    public class kpi_fix
    {
        public string proj_code { get; set; }
        public string proj_name { get; set; }
        public string io_code { get; set; }
        public string nj_code { get; set; }
        public DateTime? due_date { get; set; }
        public DateTime? postpone_due_date { get; set; }
        public DateTime? nj_closed_date { get; set; }
        public int? diff_date { get; set; }
        public int years { get; set; }
        public int months { get; set; }
        public Guid nj_id { get; set; }
        public Guid lob_id { get; set; }
        public Guid proj_id { get; set; }
        public string proj_type { get; set; }

    }
    public class prm_get_kpi_iso
    {
        public string in_lobid { get; set; }
        public string in_projid { get; set; }
        public string in_period { get; set; }
        public string in_projtype { get; set; }
    }
    public class kpi_ho
    {
        public string io_code { get; set; }
        public string proj_code { get; set; }
        public string proj_name { get; set; }
        public string proj_abbr_code { get; set; }
        public int? date_diff { get; set; }
        public string handover_type { get; set; }
        public int? month { get; set; }
        public int? year { get; set; }
        public DateTime? handover_date { get; set; }
        public DateTime? contract_transfer_date { get; set; }
        public bool cancel { get; set; }
        public Guid lob_id { get; set; }
        public Guid proj_id { get; set; }
        public string proj_type { get; set; }
    }

}
