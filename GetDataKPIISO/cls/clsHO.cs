using GetDataKPIISO.Models;
using System;
using System.Linq;
using GetDataKPIISO.Data.Dao;

namespace GetDataKPIISO.cls
{
    class clsHO
    {
        public void excHO()
        {
            DateTime dt_now = DateTime.Now.AddMonths(-1);
            string current_year = dt_now.Year.ToString();
            string current_month = string.Empty;//dt_now.Month.ToString();
            if (dt_now.Month < 10)
            {
                current_month = "0" + dt_now.Month.ToString();
            }
            else
            {
                current_month = dt_now.Month.ToString();
            }

            prm_get_kpi_iso prm_ = new prm_get_kpi_iso();
            //202107
            //prm_.in_period = "202109";
            prm_.in_period = current_year + current_month;

            ///GET DATA
            ReportDao rptDao = new ReportDao();
            var result = rptDao.GetDataKPIISO_HO(prm_);

            ///INSERT DATA
            if (result.Count() > 0)
                rptDao.InsertDataKPIISO_HO(result);
 
        }

        public void UpdateHO()
        {
            ReportDao rptDao = new ReportDao();
            rptDao.UpdateDataKPIISO_HO();
        }
    }
}
