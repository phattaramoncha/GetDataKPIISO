using GetDataKPIISO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using GetDataKPIISO.Data.Dao;

namespace GetDataKPIISO.cls
{
    class clsFix
    {  
        public void excFix()
        {
            DateTime dt_now = DateTime.Now.AddMonths(-1);
            string current_year = dt_now.Year.ToString();
            string current_month = string.Empty; //dt_now.Month.ToString();
            if (dt_now.Month < 10)
            {
                current_month = "0" + dt_now.Month.ToString();
            }
            else
            {
                current_month = dt_now.Month.ToString();
            }

            prm_get_kpi_iso prm_ = new prm_get_kpi_iso();
            //prm_.in_period = "202109";
            prm_.in_period = current_year + current_month;

            ///GET DATA
            ReportDao rptDao = new ReportDao();
            var result = rptDao.GetDataKPIISO_FIX(prm_);

            ///INSERT DATA
            if (result.Count() > 0)
                rptDao.InsertDataKPIISO_FIX(result);

            //insert to temp
            //foreach (var item in data_kpi_fix)
            //{
            //    bool flg = insert_data_kpi_fix(item);
            //}
        }

        private bool insert_data_kpi_fix(kpi_fix item)
        {
            bool flg = false;


            //ReportDao rptDao = new ReportDao();
            //flg = rptDao.InsertDataKPIISO_FIX(item);


            return flg;
        }

        private List<kpi_fix> getDataFIX(prm_get_kpi_iso prm_)
        {
            List<kpi_fix> result = new List<kpi_fix>();


            ReportDao rptDao = new ReportDao();
            result = rptDao.GetDataKPIISO_FIX(prm_);


            return result;
        }
    }
}