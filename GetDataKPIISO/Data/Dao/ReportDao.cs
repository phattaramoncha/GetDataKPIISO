using GetDataKPIISO.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using GetDataKPIISO.App_Helpers;
using GetDataKPIISO.Data.CLS;

namespace GetDataKPIISO.Data.Dao
{
    public class ReportDao  
    {
        private string _connectSting; //= ConfigurationManager.AppSetting["crm_db"];

        public ReportDao()
        {
            GetConnString();
        }

        public void GetConnString()
        {
            var appSettings = ConfigurationManager.AppSettings;
            _connectSting = appSettings["cm_db"];
        }
        
        
        public List<kpi_fix> GetDataKPIISO_FIX(prm_get_kpi_iso prm_)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectSting))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("spl_get_kpi_iso_fix", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("in_period", NpgsqlTypes.NpgsqlDbType.Text, string.IsNullOrEmpty(prm_.in_period) ? (object)DBNull.Value : prm_.in_period); //XXXX,XXXX,XXXX

                        using (var reader = cmd.ExecuteReader())
                        {
                            return SQLDataMapper.MapToCollection<kpi_fix>(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string text = "function => spl_get_kpi_iso_fix: " + ex.Message.ToString();
                SEND_EMAIL mail = new SEND_EMAIL();
                mail.SendtoEmail(text);
                throw new Exception(ex.Message);
            }
        }

        public void UpdateDataKPIISO_HO()
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectSting))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("spl_updated_data_temp_kpi_iso_ho", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("in_period", NpgsqlTypes.NpgsqlDbType.Text, string.IsNullOrEmpty(prm_.in_period) ? (object)DBNull.Value : prm_.in_period); //XXXX,XXXX,XXXX
                        int result = cmd.ExecuteNonQuery();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                string text = "function => spl_updated_data_temp_kpi_iso_ho: " + ex.Message.ToString();
                SEND_EMAIL mail = new SEND_EMAIL();
                mail.SendtoEmail(text);
                throw new Exception(ex.Message);
            }
        }

        public void InsertDataKPIISO_HO(List<kpi_ho> data)
        {
            //bool flg = false;

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectSting))
                {
                    String query = "INSERT INTO spl_temp_kpi_iso_ho(io_code, proj_code, proj_name, proj_abbr_code, date_diff, handover_type, month, year, handover_date, contract_transfer_date, cancel, lob_id, proj_id, proj_type) " +//, updated_at
                        "VALUES (@io_code, @proj_code, @proj_name, @proj_abbr_code, @date_diff, @handover_type, @month, @year, @handover_date, @contract_transfer_date, @cancel, @lob_id, @proj_id, @proj_type)";//, NOW()

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        conn.Open();

                        foreach (var item in data)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@io_code", item.io_code);
                            cmd.Parameters.AddWithValue("@proj_code", item.proj_code);
                            cmd.Parameters.AddWithValue("@proj_name", item.proj_name);
                            cmd.Parameters.AddWithValue("@proj_abbr_code", item.proj_abbr_code);
                            cmd.Parameters.AddWithValue("@date_diff", item.date_diff.HasValue ? item.date_diff.Value : (object)DBNull.Value); 
                            cmd.Parameters.AddWithValue("@handover_type", string.IsNullOrEmpty(item.handover_type) ? (object)DBNull.Value : item.handover_type);
                            cmd.Parameters.AddWithValue("@month", item.month.HasValue ? item.month.Value : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@year", item.year.HasValue ? item.year.Value : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@handover_date", item.handover_date.HasValue ? item.handover_date.Value : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@contract_transfer_date", item.contract_transfer_date.HasValue ? item.contract_transfer_date.Value : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@cancel", item.cancel);
                            cmd.Parameters.AddWithValue("@lob_id", item.lob_id);
                            cmd.Parameters.AddWithValue("@proj_id", item.proj_id);
                            cmd.Parameters.AddWithValue("@proj_type", item.proj_type);

                            int result = cmd.ExecuteNonQuery();

                            // Check Error
                            if (result < 0)
                                Console.WriteLine("Error inserting data into Database!");
                            //else flg = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string text = "INSERT DATA TO TABLE => INSERT INTO spl_temp_kpi_iso_ho: " + ex.Message.ToString();
                SEND_EMAIL mail = new SEND_EMAIL();
                mail.SendtoEmail(text);
                throw new Exception(ex.Message);
            }

            //return flg;
        }

        public void InsertDataKPIISO_FIX(List<kpi_fix> data)
        {
            bool flg = false;
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connectSting))
                {
                    String query = "INSERT INTO spl_temp_kpi_iso_fix(project_code, project_name, io_code, nj_code, due_date, postpone_due_date, nj_closed_date, diff_date , years, months, nj_id, lob_id, proj_id, proj_type) " +
                        "VALUES (@project_code, @project_name, @io_code, @nj_code, @due_date, @postpone_due_date, @nj_closed_date, @diff_date , @years, @months, @nj_id, @lob_id, @proj_id, @proj_type)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        conn.Open();

                        foreach (var item in data)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@project_code", item.proj_code);
                            cmd.Parameters.AddWithValue("@project_name", item.proj_name);
                            cmd.Parameters.AddWithValue("@io_code", item.io_code);
                            cmd.Parameters.AddWithValue("@nj_code", item.nj_code);
                            cmd.Parameters.AddWithValue("@due_date", item.due_date.HasValue ? item.due_date.Value : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@postpone_due_date", item.postpone_due_date.HasValue ? item.postpone_due_date.Value : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@nj_closed_date", item.nj_closed_date.HasValue ? item.nj_closed_date.Value : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@diff_date", item.diff_date.HasValue ? item.diff_date.Value : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@years", item.years);
                            cmd.Parameters.AddWithValue("@months", item.months);
                            cmd.Parameters.AddWithValue("@nj_id", item.nj_id);
                            cmd.Parameters.AddWithValue("@lob_id", item.lob_id);
                            cmd.Parameters.AddWithValue("@proj_id", item.proj_id);
                            cmd.Parameters.AddWithValue("@proj_type", item.proj_type);


                            int result = cmd.ExecuteNonQuery();

                            // Check Error
                            if (result < 0)
                                Console.WriteLine("Error inserting data into Database!");
                            else flg = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string text = "INSERT DATA TO TABLE => INSERT INTO spl_temp_kpi_iso_fix: " + ex.Message.ToString();
                SEND_EMAIL mail = new SEND_EMAIL();
                mail.SendtoEmail(text);
                throw new Exception(ex.Message);
            }
        }

        public List<kpi_ho> GetDataKPIISO_HO(prm_get_kpi_iso prm_)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectSting))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("spl_get_kpi_iso_ho", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("in_period", NpgsqlTypes.NpgsqlDbType.Text, string.IsNullOrEmpty(prm_.in_period) ? (object)DBNull.Value : prm_.in_period); //XXXX,XXXX,XXXX

                        using (var reader = cmd.ExecuteReader())
                        {
                            return SQLDataMapper.MapToCollection<kpi_ho>(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string text = "function => spl_get_kpi_iso_ho: " + ex.Message.ToString();
                SEND_EMAIL mail = new SEND_EMAIL();
                mail.SendtoEmail(text);
                throw new Exception(ex.Message);
            }
        } 
    }
}