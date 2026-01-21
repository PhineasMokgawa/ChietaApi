using Abp.Application.Services.Dto;
using Abp.Modules;
using Abp.Zero;
using CHIETAMIS.Controllers;
using CHIETAMIS.JobTitleOFOs.DTOs;
using CHIETAMIS.Lookups;
using CHIETAMIS.MandatoryGrants;
using CHIETAMIS.MandatoryGrants.Dtos;
//using Abp.Web.Security.AntiForgery;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Antiforgery;

namespace CHIETAMIS.Web.Host.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api")]
    public class BulkInsertController : CHIETAMISControllerBase
    {
        //string connectionString = "Data Source=TERACO-VMDG2\\IMS;Initial Catalog=CHIETA_INTEGRATED;User ID=chieta;Password=@dm1n#123;";
        string connectionString = "Data Source=ho-ICTSpare3\\SQLEXPRESS;Initial Catalog=IMS_04062025;Integrated Security=true";
        //string connectionString = GetConnectionString().Replace(@"\\\\", @"\\");

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("bioinsert")]
        public void BioInsert(CBiodataDto[] bio)
        {
            int applicationid;
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("Id", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("ApplicationId", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("SA_Id_Number", typeof(string)));
            tbl.Columns.Add(new DataColumn("Passport_Number", typeof(string)));
            tbl.Columns.Add(new DataColumn("Firstname", typeof(string)));
            tbl.Columns.Add(new DataColumn("Middlename", typeof(string)));
            tbl.Columns.Add(new DataColumn("Surname", typeof(string)));
            tbl.Columns.Add(new DataColumn("Birth_Year", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("Gender", typeof(string)));
            tbl.Columns.Add(new DataColumn("Race", typeof(string)));
            tbl.Columns.Add(new DataColumn("Disability", typeof(string)));
            tbl.Columns.Add(new DataColumn("Nationality", typeof(string)));
            tbl.Columns.Add(new DataColumn("Province", typeof(string)));
            tbl.Columns.Add(new DataColumn("Municipality", typeof(string)));
            tbl.Columns.Add(new DataColumn("Highest_Qualification_Type", typeof(string)));
            tbl.Columns.Add(new DataColumn("Employment_Status", typeof(string)));
            tbl.Columns.Add(new DataColumn("Occupation_Level_For_Equity_Reporting", typeof(string)));
            tbl.Columns.Add(new DataColumn("Organisational_Structure_Filter", typeof(string)));
            tbl.Columns.Add(new DataColumn("Post_Reference", typeof(string)));
            tbl.Columns.Add(new DataColumn("Job_Title", typeof(string)));
            tbl.Columns.Add(new DataColumn("OFO_Occupation_Code", typeof(string)));
            tbl.Columns.Add(new DataColumn("OFO_Specialisation", typeof(string)));
            tbl.Columns.Add(new DataColumn("OFO_Occupation", typeof(string)));
            tbl.Columns.Add(new DataColumn("Status", typeof(string)));
            tbl.Columns.Add(new DataColumn("Comment", typeof(string)));
            tbl.Columns.Add(new DataColumn("UserId", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("DateCreated", typeof(DateTime)));
            tbl.Columns.Add(new DataColumn("UsrUpd", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("DteUpd", typeof(DateTime)));

            for (int i = 0; i < bio.Length; i++)
            {
                DataRow dr = tbl.NewRow();
                dr["Id"] = 0;
                dr["ApplicationId"] = bio[i].ApplicationId;
                dr["SA_Id_Number"] = bio[i].SA_Id_Number;
                dr["Passport_Number"] = bio[i].Passport_Number;
                dr["Firstname"] = bio[i].Firstname;
                dr["Middlename"] = bio[i].Middlename;
                dr["Surname"] = bio[i].Surname;
                dr["Birth_Year"] = bio[i].Birth_Year;
                dr["Gender"] = bio[i].Gender;
                dr["Race"] = bio[i].Race;
                dr["Disability"] = bio[i].Disability;
                dr["Nationality"] = bio[i].Nationality;
                dr["Province"] = bio[i].Province;
                dr["Municipality"] = bio[i].Municipality;
                dr["Highest_Qualification_Type"] = bio[i].Highest_Qualification_Type;
                dr["Employment_Status"] = bio[i].Employment_Status;
                dr["Occupation_Level_For_Equity_Reporting"] = bio[i].Occupation_Level_For_Equity_Reporting;
                dr["Organisational_Structure_Filter"] = bio[i].Organisational_Structure_Filter;
                dr["Post_Reference"] = bio[i].Post_Reference;
                dr["Job_Title"] = bio[i].Job_Title;
                dr["OFO_Occupation_Code"] = bio[i].OFO_Occupation_Code;
                dr["OFO_Specialisation"] = bio[i].OFO_Specialisation;
                dr["OFO_Occupation"] = bio[i].OFO_Occupation;
                dr["Status"] = DBNull.Value;
                dr["Comment"] = DBNull.Value;
                dr["UserId"] = 0;
                dr["DateCreated"] = DateTime.Now;
                dr["UsrUpd"] = DBNull.Value;
                dr["DteUpd"] = DBNull.Value;

                tbl.Rows.Add(dr);
            }

            if (tbl.Rows.Count > 0)
            {
                applicationid = bio[0].ApplicationId;

                using (SqlConnection sourceConnection = new SqlConnection(connectionString))
                {
                    SqlBulkCopy objbulk = new SqlBulkCopy(sourceConnection);
                    objbulk.DestinationTableName = "tbl_Mandatory_Biodata";

                    objbulk.ColumnMappings.Add("Id", "Id");
                    objbulk.ColumnMappings.Add("ApplicationId", "ApplicationId");
                    objbulk.ColumnMappings.Add("SA_Id_Number", "SA_Id_Number");
                    objbulk.ColumnMappings.Add("Passport_Number", "Passport_Number");
                    objbulk.ColumnMappings.Add("Firstname", "Firstname");
                    objbulk.ColumnMappings.Add("Middlename", "Middlename");
                    objbulk.ColumnMappings.Add("Surname", "Surname");
                    objbulk.ColumnMappings.Add("Birth_Year", "Birth_Year");
                    objbulk.ColumnMappings.Add("Gender", "Gender");
                    objbulk.ColumnMappings.Add("Race", "Race");
                    objbulk.ColumnMappings.Add("Disability", "Disability");
                    objbulk.ColumnMappings.Add("Nationality", "Nationality");
                    objbulk.ColumnMappings.Add("Province", "Province");
                    objbulk.ColumnMappings.Add("Municipality", "Municipality");
                    objbulk.ColumnMappings.Add("Highest_Qualification_Type", "Highest_Qualification_Type");
                    objbulk.ColumnMappings.Add("Employment_Status", "Employment_Status");
                    objbulk.ColumnMappings.Add("Occupation_Level_For_Equity_Reporting", "Occupation_Level_For_Equity_Reporting");
                    objbulk.ColumnMappings.Add("Organisational_Structure_Filter", "Organisational_Structure_Filter");
                    objbulk.ColumnMappings.Add("Job_Title", "Job_Title");
                    objbulk.ColumnMappings.Add("OFO_Occupation_Code", "OFO_Occupation_Code");
                    objbulk.ColumnMappings.Add("OFO_Specialisation", "OFO_Specialisation");
                    objbulk.ColumnMappings.Add("OFO_Occupation", "OFO_Occupation");
                    objbulk.ColumnMappings.Add("Status", "Status");
                    objbulk.ColumnMappings.Add("Comment", "Comment");
                    objbulk.ColumnMappings.Add("UserId", "UserId");
                    objbulk.ColumnMappings.Add("DateCreated", "DateCreated");
                    objbulk.ColumnMappings.Add("UsrUpd", "UsrUpd");
                    objbulk.ColumnMappings.Add("DteUpd", "DteUpd");

                    try
                    {
                        sourceConnection.Open();
                        objbulk.WriteToServer(tbl);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    finally
                    {
                        sourceConnection.Close();
                    }; ;
                }

                using (SqlConnection sourceConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        string cmd = "with cte as (select row_number() over (partition by SA_Id_Number, Passport_Number order by Id desc) rownum, Id from tbl_Mandatory_Biodata where applicationid = " + applicationid + ") delete from cte where cte.rownum > 1";
                        SqlCommand dBio = new SqlCommand(cmd, sourceConnection);
                        sourceConnection.Open();
                        dBio.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    finally
                    {
                        sourceConnection.Close();
                    };
                }
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("traininsert")]
        public void TrainInsert(TrainingDto[] train)
        {
            int applicationid;
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("Id", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("ApplicationId", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("SA_Id_Number", typeof(string)));
            tbl.Columns.Add(new DataColumn("Passport_Number", typeof(string)));
            tbl.Columns.Add(new DataColumn("Qualification_Learning_Program_Type", typeof(string)));
            tbl.Columns.Add(new DataColumn("Details_Of_Learning_Program", typeof(string)));
            tbl.Columns.Add(new DataColumn("Study_Field_Or_Specialisation_Specification", typeof(string)));
            tbl.Columns.Add(new DataColumn("Total_Training_Cost", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("Achievement_status", typeof(string)));
            tbl.Columns.Add(new DataColumn("Year_enrolled_or_completed", typeof(string)));
            tbl.Columns.Add(new DataColumn("BiodataId", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("Status", typeof(string)));
            tbl.Columns.Add(new DataColumn("Comment", typeof(string)));
            tbl.Columns.Add(new DataColumn("DateCreated", typeof(DateTime)));
            tbl.Columns.Add(new DataColumn("UserId", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("DteUpd", typeof(DateTime)));
            tbl.Columns.Add(new DataColumn("UsrUpd", typeof(Int32)));

            for (int i = 0; i < train.Length; i++)
            {
                DataRow dr = tbl.NewRow();
                dr["Id"] = 0;
                dr["ApplicationId"] = train[i].ApplicationId;
                dr["SA_Id_Number"] = train[i].SA_Id_Number;
                dr["Passport_Number"] = train[i].Passport_Number;
                dr["Qualification_Learning_Program_Type"] = train[i].Qualification_Learning_Program_Type;
                dr["Details_Of_Learning_Program"] = train[i].Details_Of_Learning_Program;
                dr["Study_Field_Or_Specialisation_Specification"] = train[i].Study_Field_Or_Specialisation_Specification;
                dr["Total_Training_Cost"] = train[i].Total_Training_Cost;
                dr["Achievement_status"] = train[i].Achievement_status;
                dr["Year_enrolled_or_completed"] = train[i].Year_enrolled_or_completed;
                dr["BiodataId"] = DBNull.Value; ;
                dr["Status"] = DBNull.Value;
                dr["Comment"] = DBNull.Value;
                dr["DateCreated"] = DateTime.Now;
                dr["UserId"] = DBNull.Value;
                dr["DteUpd"] = DBNull.Value;
                dr["UsrUpd"] = DBNull.Value;

                tbl.Rows.Add(dr);
            }

            if (tbl.Rows.Count > 0)
            {
                applicationid = train[0].ApplicationId;
                using (SqlConnection sourceConnection = new SqlConnection(connectionString))
                {
                    SqlBulkCopy objbulk = new SqlBulkCopy(sourceConnection);
                    objbulk.DestinationTableName = "tbl_Mandatory_Trainings";

                    objbulk.ColumnMappings.Add("Id", "Id");
                    objbulk.ColumnMappings.Add("ApplicationId", "ApplicationId");
                    objbulk.ColumnMappings.Add("SA_Id_Number", "SA_Id_Number");
                    objbulk.ColumnMappings.Add("Passport_Number", "Passport_Number");
                    objbulk.ColumnMappings.Add("Qualification_Learning_Program_Type", "Qualification_Learning_Program_Type");
                    objbulk.ColumnMappings.Add("Details_Of_Learning_Program", "Details_Of_Learning_Program");
                    objbulk.ColumnMappings.Add("Study_Field_Or_Specialisation_Specification", "Study_Field_Or_Specialisation_Specification");
                    objbulk.ColumnMappings.Add("Total_Training_Cost", "Total_Training_Cost");
                    objbulk.ColumnMappings.Add("Achievement_status", "Achievement_status");
                    objbulk.ColumnMappings.Add("Year_enrolled_or_completed", "Year_enrolled_or_completed");
                    objbulk.ColumnMappings.Add("BiodataId", "BiodataId");
                    objbulk.ColumnMappings.Add("Status", "Status");
                    objbulk.ColumnMappings.Add("Comment", "Comment");
                    objbulk.ColumnMappings.Add("DateCreated", "DateCreated");
                    objbulk.ColumnMappings.Add("UserId", "UserId");
                    objbulk.ColumnMappings.Add("DteUpd", "DteUpd");
                    objbulk.ColumnMappings.Add("UsrUpd", "UsrUpd");

                    try
                    {
                        sourceConnection.Open();
                        objbulk.WriteToServer(tbl);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    finally
                    {
                        sourceConnection.Close();
                    };
                }

                using (SqlConnection sourceConnection = new SqlConnection(connectionString))
                {
                    try {
                        string cmd = "with cte as (select row_number() over (partition by SA_Id_Number, Passport_Number, Qualification_Learning_Program_Type, Details_Of_Learning_Program, Study_Field_Or_Specialisation_Specification, Achievement_status, Year_enrolled_or_completed order by Id desc) rownum, Id from tbl_Mandatory_Trainings where applicationid = " + applicationid + ") delete from cte where cte.rownum > 1";
                        SqlCommand dTr = new SqlCommand(cmd, sourceConnection);
                        dTr.CommandTimeout = 120;
                        sourceConnection.Open();
                        dTr.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    finally
                    {
                        sourceConnection.Close();
                    };
                }
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("deleteallbio")]
        public OkResult DeleteAllBio([FromQuery] int ApplicationId)
        {
            using (SqlConnection sourceConnection =
                       new SqlConnection(connectionString))
            {
                string cmd;
                try
                {
                    cmd = "DELETE FROM [dbo].[tbl_Mandatory_Biodata] where ApplicationId = " + ApplicationId;
                    SqlCommand dBio = new SqlCommand(cmd, sourceConnection);
                    dBio.CommandTimeout = 120;
                    sourceConnection.Open();
                    dBio.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                finally
                {
                    sourceConnection.Close();
                }; ;
            }
            return Ok();
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("deletealltrain")]
        public async Task<ActionResult> DeleteAllTrain([FromQuery] string ApplicationId)
        {
            using (SqlConnection sourceConnection =
                       new SqlConnection(connectionString))
            {
                string cmd;
                try
                {
                    cmd = "DELETE FROM [dbo].[tbl_Mandatory_Trainings] where ApplicationId = " + ApplicationId;
                    SqlCommand dTrain = new SqlCommand(cmd, sourceConnection);
                    dTrain.CommandTimeout = 120;
                    sourceConnection.Open();
                    dTrain.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
                finally
                {
                    sourceConnection.Close();
                }; ;
            }

            return Ok();
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("validatebio")]
        public string ValidateBio([FromQuery] int ApplicationId)
        {
            using (SqlConnection sourceConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand dTrain = new SqlCommand())
                {
                    string cmd;
                    dTrain.Connection = sourceConnection;
                    dTrain.CommandText = "sp_Mand_BioValidate";
                    dTrain.CommandType = CommandType.StoredProcedure;
                    dTrain.CommandTimeout = 240;
                    dTrain.Parameters.Add("@applicationid", SqlDbType.Int).Value = ApplicationId;
                    try
                    {
                        sourceConnection.Open();
                        dTrain.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    finally
                    {
                        sourceConnection.Close();
                    };

                }
            }

            return "Ok";
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("validatetrain")]
        public string ValidateTrain([FromQuery] int ApplicationId)
        {
            using (SqlConnection sourceConnection =
                       new SqlConnection(connectionString))
            {
                // Perform Inserts.
                using (SqlCommand dTrain = new SqlCommand())
                {
                    string cmd;
                    dTrain.Connection = sourceConnection;
                    dTrain.CommandText = "sp_Mand_TrainValidate";
                    dTrain.CommandType = CommandType.StoredProcedure;
                    dTrain.CommandTimeout = 360;
                    dTrain.Parameters.Add("@applicationid", SqlDbType.Int).Value = ApplicationId;
                    try
                    {
                        sourceConnection.Open();
                        dTrain.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    finally
                    {
                        sourceConnection.Close();
                    };
                }
            }
            return "Ok";
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("workplaceprofile")]
        public PagedResultDto<MandWorkforceProfileView> GetWorkplaceProfile([FromQuery] int ApplicationId)
        {
            SqlDataReader sqlDr = null;

            List<MandWorkforceProfile> mwps = new List<MandWorkforceProfile>();
            using (SqlConnection sourceConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand dt = new SqlCommand())
                {
                    dt.Connection = sourceConnection;
                    dt.CommandText = "sp_GetWorkplaceProfile";
                    dt.CommandType = CommandType.StoredProcedure;
                    dt.Parameters.Add("@applicationid", SqlDbType.Int).Value = ApplicationId;
                    dt.CommandTimeout = 120;
                    try
                    {
                        sourceConnection.Open();
                        sqlDr = dt.ExecuteReader();
                        while (sqlDr.Read())
                        {
                            MandWorkforceProfile mwp = new MandWorkforceProfile();
                            mwp.OFOCode = sqlDr.GetString(sqlDr.GetOrdinal("OFOCode")).ToString();
                            mwp.OFODescription = sqlDr.GetString(sqlDr.GetOrdinal("OFODescription")).ToString();
                            mwp.Municipality = sqlDr.GetString(sqlDr.GetOrdinal("Municipality")).ToString();
                            mwp.AM = sqlDr[sqlDr.GetOrdinal("AM")] as int? ?? default(int);
                            mwp.AF = sqlDr[sqlDr.GetOrdinal("AF")] as int? ?? default(int);
                            mwp.CM = sqlDr[sqlDr.GetOrdinal("CM")] as int? ?? default(int);
                            mwp.CF = sqlDr[sqlDr.GetOrdinal("CF")] as int? ?? default(int);
                            mwp.IM = sqlDr[sqlDr.GetOrdinal("IM")] as int? ?? default(int);
                            mwp.IF = sqlDr[sqlDr.GetOrdinal("IF")] as int? ?? default(int);
                            mwp.WM = sqlDr[sqlDr.GetOrdinal("WM")] as int? ?? default(int);
                            mwp.WF = sqlDr[sqlDr.GetOrdinal("WF")] as int? ?? default(int);
                            mwp.TM = sqlDr[sqlDr.GetOrdinal("TM")] as int? ?? default(int);
                            mwp.TF = sqlDr[sqlDr.GetOrdinal("TF")] as int? ?? default(int);
                            mwp.TD = sqlDr[sqlDr.GetOrdinal("TD")] as int? ?? default(int);
                            mwp.TNSA = sqlDr[sqlDr.GetOrdinal("TNSA")] as int? ?? default(int);
                            mwp.A35 = sqlDr[sqlDr.GetOrdinal("A35")] as int? ?? default(int);
                            mwp.A55 = sqlDr[sqlDr.GetOrdinal("A55")] as int? ?? default(int);
                            mwp.A55P = sqlDr[sqlDr.GetOrdinal("A55P")] as int? ?? default(int);
                            mwps.Add(mwp);
                        }
                    }
                    finally
                    {
                        if (sourceConnection != null)
                        {
                            sourceConnection.Close();
                        }
                        if (sqlDr != null)
                        {
                            sqlDr.Close();
                        }
                    }
                }
            }

            var workprof = from o in mwps
                           select new MandWorkforceProfileView()
                           {
                               WorkplaceProfile = new MandWorkforceProfile
                               {
                                   OFOCode = o.OFOCode,
                                   OFODescription = o.OFODescription,
                                   Municipality = o.Municipality,
                                   AM = o.AM,
                                   AF = o.AF,
                                   CM = o.CM,
                                   CF = o.CF,
                                   IM = o.IM,
                                   IF = o.IF,
                                   WM = o.WM,
                                   WF = o.WF,
                                   TM = o.TM,
                                   TF = o.TF,
                                   TD = o.TD,
                                   TNSA = o.TNSA,
                                   A35 = o.A35,
                                   A55 = o.A55,
                                   A55P = o.A55P,
                               }
                           };

            var totalCount = mwps.Count();

            return new PagedResultDto<MandWorkforceProfileView>(
                totalCount,
                workprof.ToList()
            );
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("geodistribution")]
        public PagedResultDto<MandGeoDistributionView> GetGeoDistribution([FromQuery] int ApplicationId)
        {
            SqlDataReader sqlDr = null;

            List<MandGeoDistribution> mgds = new List<MandGeoDistribution>();
            using (SqlConnection sourceConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand dt = new SqlCommand())
                {
                    dt.Connection = sourceConnection;
                    dt.CommandText = "sp_GeoDistribution";
                    dt.CommandType = CommandType.StoredProcedure;
                    dt.Parameters.Add("@applicationid", SqlDbType.Int).Value = ApplicationId;
                    dt.CommandTimeout = 120;
                    try
                    {
                        sourceConnection.Open();
                        sqlDr = dt.ExecuteReader();
                        while (sqlDr.Read())
                        {
                            MandGeoDistribution mgd = new MandGeoDistribution();
                            mgd.Province = sqlDr.GetString(sqlDr.GetOrdinal("Province")).ToString();
                            mgd.AM = sqlDr[sqlDr.GetOrdinal("AM")] as int? ?? default(int);
                            mgd.AF = sqlDr[sqlDr.GetOrdinal("AF")] as int? ?? default(int);
                            mgd.CM = sqlDr[sqlDr.GetOrdinal("CM")] as int? ?? default(int);
                            mgd.CF = sqlDr[sqlDr.GetOrdinal("CF")] as int? ?? default(int);
                            mgd.IM = sqlDr[sqlDr.GetOrdinal("IM")] as int? ?? default(int);
                            mgd.IF = sqlDr[sqlDr.GetOrdinal("IF")] as int? ?? default(int);
                            mgd.WM = sqlDr[sqlDr.GetOrdinal("WM")] as int? ?? default(int);
                            mgd.WF = sqlDr[sqlDr.GetOrdinal("WF")] as int? ?? default(int);
                            mgd.TM = sqlDr[sqlDr.GetOrdinal("TM")] as int? ?? default(int);
                            mgd.TF = sqlDr[sqlDr.GetOrdinal("TF")] as int? ?? default(int);
                            mgd.TD = sqlDr[sqlDr.GetOrdinal("TD")] as int? ?? default(int);
                            mgd.TNSA = sqlDr[sqlDr.GetOrdinal("TNSA")] as int? ?? default(int);
                            mgd.A35 = sqlDr[sqlDr.GetOrdinal("A35")] as int? ?? default(int);
                            mgd.A55 = sqlDr[sqlDr.GetOrdinal("A55")] as int? ?? default(int);
                            mgd.A55P = sqlDr[sqlDr.GetOrdinal("A55P")] as int? ?? default(int);
                            mgds.Add(mgd);
                        }
                    }
                    finally
                    {
                        if (sourceConnection != null)
                        {
                            sourceConnection.Close();
                        }
                        if (sqlDr != null)
                        {
                            sqlDr.Close();
                        }
                    }
                }
            }

            var geod = from o in mgds
                       select new MandGeoDistributionView()
                       {
                           GeoDistribution = new MandGeoDistribution
                           {
                               Province = o.Province,
                               AM = o.AM,
                               AF = o.AF,
                               CM = o.CM,
                               CF = o.CF,
                               IM = o.IM,
                               IF = o.IF,
                               WM = o.WM,
                               WF = o.WF,
                               TM = o.TM,
                               TF = o.TF,
                               TD = o.TD,
                               TNSA = o.TNSA,
                               A35 = o.A35,
                               A55 = o.A55,
                               A55P = o.A55P,
                           }
                       };

            var totalCount = geod.Count();

            return new PagedResultDto<MandGeoDistributionView>(
                totalCount,
                geod.ToList()
            );
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("educationallevel")]
        public PagedResultDto<MandEducationLevelView> GetEducationLevel([FromQuery] int ApplicationId)
        {
            SqlDataReader sqlDr = null;

            List<MandEducationLevel> edls = new List<MandEducationLevel>();
            using (SqlConnection sourceConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand dt = new SqlCommand())
                {
                    dt.Connection = sourceConnection;
                    dt.CommandText = "sp_EducationLevel";
                    dt.CommandType = CommandType.StoredProcedure;
                    dt.Parameters.Add("@applicationid", SqlDbType.Int).Value = ApplicationId;
                    dt.CommandTimeout = 120;
                    try
                    {
                        sourceConnection.Open();
                        sqlDr = dt.ExecuteReader();
                        while (sqlDr.Read())
                        {
                            MandEducationLevel edl = new MandEducationLevel();
                            edl.Classification = sqlDr.GetString(sqlDr.GetOrdinal("Classification")).ToString();
                            edl.NQF_Level = sqlDr.GetString(sqlDr.GetOrdinal("NQF_Level")).ToString();
                            edl.Band = sqlDr.GetString(sqlDr.GetOrdinal("Band")).ToString();
                            edl.AM = sqlDr[sqlDr.GetOrdinal("AM")] as int? ?? default(int);
                            edl.AF = sqlDr[sqlDr.GetOrdinal("AF")] as int? ?? default(int);
                            edl.CM = sqlDr[sqlDr.GetOrdinal("CM")] as int? ?? default(int);
                            edl.CF = sqlDr[sqlDr.GetOrdinal("CF")] as int? ?? default(int);
                            edl.IM = sqlDr[sqlDr.GetOrdinal("IM")] as int? ?? default(int);
                            edl.IF = sqlDr[sqlDr.GetOrdinal("IF")] as int? ?? default(int);
                            edl.WM = sqlDr[sqlDr.GetOrdinal("WM")] as int? ?? default(int);
                            edl.WF = sqlDr[sqlDr.GetOrdinal("WF")] as int? ?? default(int);
                            edl.TM = sqlDr[sqlDr.GetOrdinal("TM")] as int? ?? default(int);
                            edl.TF = sqlDr[sqlDr.GetOrdinal("TF")] as int? ?? default(int);
                            edl.TD = sqlDr[sqlDr.GetOrdinal("TD")] as int? ?? default(int);
                            edl.TNSA = sqlDr[sqlDr.GetOrdinal("TNSA")] as int? ?? default(int);
                            edl.A35 = sqlDr[sqlDr.GetOrdinal("A35")] as int? ?? default(int);
                            edl.A55 = sqlDr[sqlDr.GetOrdinal("A55")] as int? ?? default(int);
                            edl.A55P = sqlDr[sqlDr.GetOrdinal("A55P")] as int? ?? default(int);
                            edls.Add(edl);
                        }
                    }
                    finally
                    {
                        if (sourceConnection != null)
                        {
                            sourceConnection.Close();
                        }
                        if (sqlDr != null)
                        {
                            sqlDr.Close();
                        }
                    }
                }
            }

            var edulvl = from o in edls
                         select new MandEducationLevelView()
                         {
                             EducationLevel = new MandEducationLevel
                             {
                                 Classification = o.Classification,
                                 NQF_Level = o.NQF_Level,
                                 Band = o.Band,
                                 AM = o.AM,
                                 AF = o.AF,
                                 CM = o.CM,
                                 CF = o.CF,
                                 IM = o.IM,
                                 IF = o.IF,
                                 WM = o.WM,
                                 WF = o.WF,
                                 TM = o.TM,
                                 TF = o.TF,
                                 TD = o.TD,
                                 TNSA = o.TNSA,
                                 A35 = o.A35,
                                 A55 = o.A55,
                                 A55P = o.A55P,
                             }
                         };

            var totalCount = edulvl.Count();

            return new PagedResultDto<MandEducationLevelView>(
                totalCount,
                edulvl.ToList()
            );
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("plannedtrained")]
        public PagedResultDto<MandEmployeesTrainedView> GetPlannedTrained([FromQuery] int ApplicationId)
        {
            SqlDataReader sqlDr = null;

            List<MandEmployeesTrained> ptrs = new List<MandEmployeesTrained>();
            using (SqlConnection sourceConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand dt = new SqlCommand())
                {
                    dt.Connection = sourceConnection;
                    dt.CommandText = "sp_PlannedTrained";
                    dt.CommandType = CommandType.StoredProcedure;
                    dt.Parameters.Add("@applicationid", SqlDbType.Int).Value = ApplicationId;
                    dt.CommandTimeout = 120;
                    try
                    {
                        sourceConnection.Open();
                        sqlDr = dt.ExecuteReader();
                        while (sqlDr.Read())
                        {
                            MandEmployeesTrained ptr = new MandEmployeesTrained();
                            ptr.Occupation = sqlDr.GetString(sqlDr.GetOrdinal("Occupation")).ToString();
                            ptr.AM = sqlDr[sqlDr.GetOrdinal("AM")] as int? ?? default(int);
                            ptr.AF = sqlDr[sqlDr.GetOrdinal("AF")] as int? ?? default(int);
                            ptr.CM = sqlDr[sqlDr.GetOrdinal("CM")] as int? ?? default(int);
                            ptr.CF = sqlDr[sqlDr.GetOrdinal("CF")] as int? ?? default(int);
                            ptr.IM = sqlDr[sqlDr.GetOrdinal("IM")] as int? ?? default(int);
                            ptr.IF = sqlDr[sqlDr.GetOrdinal("IF")] as int? ?? default(int);
                            ptr.WM = sqlDr[sqlDr.GetOrdinal("WM")] as int? ?? default(int);
                            ptr.WF = sqlDr[sqlDr.GetOrdinal("WF")] as int? ?? default(int);
                            ptr.TM = sqlDr[sqlDr.GetOrdinal("TM")] as int? ?? default(int);
                            ptr.TF = sqlDr[sqlDr.GetOrdinal("TF")] as int? ?? default(int);
                            ptr.TD = sqlDr[sqlDr.GetOrdinal("TD")] as int? ?? default(int);
                            ptr.TNSA = sqlDr[sqlDr.GetOrdinal("TNSA")] as int? ?? default(int);
                            ptr.A35 = sqlDr[sqlDr.GetOrdinal("A35")] as int? ?? default(int);
                            ptr.A55 = sqlDr[sqlDr.GetOrdinal("A55")] as int? ?? default(int);
                            ptr.A55P = sqlDr[sqlDr.GetOrdinal("A55P")] as int? ?? default(int);
                            ptrs.Add(ptr);
                        }
                    }
                    finally
                    {
                        if (sourceConnection != null)
                        {
                            sourceConnection.Close();
                        }

                        if (sqlDr != null)
                        {
                            sqlDr.Close();
                        }
                    }
                }
            }

            var pt = from o in ptrs
                     select new MandEmployeesTrainedView()
                     {
                         EmployeesTrained = new MandEmployeesTrained
                         {
                             Occupation = o.Occupation,
                             AM = o.AM,
                             AF = o.AF,
                             CM = o.CM,
                             CF = o.CF,
                             IM = o.IM,
                             IF = o.IF,
                             WM = o.WM,
                             WF = o.WF,
                             TM = o.TM,
                             TF = o.TF,
                             TD = o.TD,
                             TNSA = o.TNSA,
                             A35 = o.A35,
                             A55 = o.A55,
                             A55P = o.A55P,
                         }
                     };

            var totalCount = pt.Count();

            return new PagedResultDto<MandEmployeesTrainedView>(
                totalCount,
                pt.ToList()
            );
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("trainingintervention")]
        public PagedResultDto<MandTrainingInterventionsView> GetEmployeeTrainingInter([FromQuery] int ApplicationId)
        {
            SqlDataReader sqlDr = null;

            List<MandTrainingInterventions> tints = new List<MandTrainingInterventions>();
            using (SqlConnection sourceConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand dt = new SqlCommand())
                {
                    dt.Connection = sourceConnection;
                    dt.CommandText = "sp_TrainingIntervention";
                    dt.CommandType = CommandType.StoredProcedure;
                    dt.Parameters.Add("@applicationid", SqlDbType.Int).Value = ApplicationId;
                    dt.CommandTimeout = 120;
                    try
                    {
                        sourceConnection.Open();
                        sqlDr = dt.ExecuteReader();
                        while (sqlDr.Read())
                        {
                            MandTrainingInterventions tint = new MandTrainingInterventions();
                            tint.ProgramType = sqlDr.GetString(sqlDr.GetOrdinal("ProgramType")).ToString();
                            tint.Classification = sqlDr.GetString(sqlDr.GetOrdinal("Classification")).ToString();
                            tint.AM = sqlDr[sqlDr.GetOrdinal("AM")] as int? ?? default(int);
                            tint.AF = sqlDr[sqlDr.GetOrdinal("AF")] as int? ?? default(int);
                            tint.CM = sqlDr[sqlDr.GetOrdinal("CM")] as int? ?? default(int);
                            tint.CF = sqlDr[sqlDr.GetOrdinal("CF")] as int? ?? default(int);
                            tint.IM = sqlDr[sqlDr.GetOrdinal("IM")] as int? ?? default(int);
                            tint.IF = sqlDr[sqlDr.GetOrdinal("IF")] as int? ?? default(int);
                            tint.WM = sqlDr[sqlDr.GetOrdinal("WM")] as int? ?? default(int);
                            tint.WF = sqlDr[sqlDr.GetOrdinal("WF")] as int? ?? default(int);
                            tint.TM = sqlDr[sqlDr.GetOrdinal("TM")] as int? ?? default(int);
                            tint.TF = sqlDr[sqlDr.GetOrdinal("TF")] as int? ?? default(int);
                            tint.TD = sqlDr[sqlDr.GetOrdinal("TD")] as int? ?? default(int);
                            tint.TNSA = sqlDr[sqlDr.GetOrdinal("TNSA")] as int? ?? default(int);
                            tint.A35 = sqlDr[sqlDr.GetOrdinal("A35")] as int? ?? default(int);
                            tint.A55 = sqlDr[sqlDr.GetOrdinal("A55")] as int? ?? default(int);
                            tint.A55P = sqlDr[sqlDr.GetOrdinal("A55P")] as int? ?? default(int);
                            tints.Add(tint);
                        }
                    }
                    finally
                    {
                        if (sourceConnection != null)
                        {
                            sourceConnection.Close();
                        }
                        if (sqlDr != null)
                        {
                            sqlDr.Close();
                        }
                    }
                }
            }

            var trint = from o in tints
                        select new MandTrainingInterventionsView()
                        {
                            TrainingInterventions = new MandTrainingInterventions
                            {
                                ProgramType = o.ProgramType,
                                Classification = o.Classification,
                                Program = o.Program,
                                AM = o.AM,
                                AF = o.AF,
                                CM = o.CM,
                                CF = o.CF,
                                IM = o.IM,
                                IF = o.IF,
                                WM = o.WM,
                                WF = o.WF,
                                TM = o.TM,
                                TF = o.TF,
                                TD = o.TD,
                                TNSA = o.TNSA,
                                A35 = o.A35,
                                A55 = o.A55,
                                A55P = o.A55P,
                            }
                        };

            var totalCount = trint.Count();

            return new PagedResultDto<MandTrainingInterventionsView>(
                totalCount,
                trint.ToList()
            );
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("plannedtraining")]
        public PagedResultDto<MandPlannedTrainingView> GetPlannedTraining([FromQuery] int ApplicationId)
        {
            SqlDataReader sqlDr = null;

            List<MandPlannedTraining> pltrs = new List<MandPlannedTraining>();
            using (SqlConnection sourceConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand dt = new SqlCommand())
                {
                    dt.Connection = sourceConnection;
                    dt.CommandText = "sp_PlannedTraining";
                    dt.CommandType = CommandType.StoredProcedure;
                    dt.Parameters.Add("@applicationid", SqlDbType.Int).Value = ApplicationId;
                    dt.CommandTimeout = 120;
                    try
                    {
                        sourceConnection.Open();
                        sqlDr = dt.ExecuteReader();
                        while (sqlDr.Read())
                        {
                            MandPlannedTraining pltr = new MandPlannedTraining();
                            pltr.Occupation = sqlDr.GetString(sqlDr.GetOrdinal("Occupation")).ToString();
                            pltr.AM = sqlDr[sqlDr.GetOrdinal("AM")] as int? ?? default(int);
                            pltr.AF = sqlDr[sqlDr.GetOrdinal("AF")] as int? ?? default(int);
                            pltr.CM = sqlDr[sqlDr.GetOrdinal("CM")] as int? ?? default(int);
                            pltr.CF = sqlDr[sqlDr.GetOrdinal("CF")] as int? ?? default(int);
                            pltr.IM = sqlDr[sqlDr.GetOrdinal("IM")] as int? ?? default(int);
                            pltr.IF = sqlDr[sqlDr.GetOrdinal("IF")] as int? ?? default(int);
                            pltr.WM = sqlDr[sqlDr.GetOrdinal("WM")] as int? ?? default(int);
                            pltr.WF = sqlDr[sqlDr.GetOrdinal("WF")] as int? ?? default(int);
                            pltr.TM = sqlDr[sqlDr.GetOrdinal("TM")] as int? ?? default(int);
                            pltr.TF = sqlDr[sqlDr.GetOrdinal("TF")] as int? ?? default(int);
                            pltr.TD = sqlDr[sqlDr.GetOrdinal("TD")] as int? ?? default(int);
                            pltr.TNSA = sqlDr[sqlDr.GetOrdinal("TNSA")] as int? ?? default(int);
                            pltr.A35 = sqlDr[sqlDr.GetOrdinal("A35")] as int? ?? default(int);
                            pltr.A55 = sqlDr[sqlDr.GetOrdinal("A55")] as int? ?? default(int);
                            pltr.A55P = sqlDr[sqlDr.GetOrdinal("A55P")] as int? ?? default(int);
                            pltrs.Add(pltr);
                        }
                    }
                    finally
                    {
                        if (sourceConnection != null)
                        {
                            sourceConnection.Close();
                        }
                        if (sqlDr != null)
                        {
                            sqlDr.Close();
                        }
                    }
                }
            }

            var trn = from o in pltrs
                      select new MandPlannedTrainingView()
                      {
                          PlannedTraining = new MandPlannedTraining
                          {
                              Occupation = o.Occupation,
                              AM = o.AM,
                              AF = o.AF,
                              CM = o.CM,
                              CF = o.CF,
                              IM = o.IM,
                              IF = o.IF,
                              WM = o.WM,
                              WF = o.WF,
                              TM = o.TM,
                              TF = o.TF,
                              TD = o.TD,
                              TNSA = o.TNSA,
                              A35 = o.A35,
                              A55 = o.A55,
                              A55P = o.A55P,
                          }
                      };

            var totalCount = trn.Count();

            return new PagedResultDto<MandPlannedTrainingView>(
                totalCount,
                trn.ToList()
            );
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("pivotalprogrammesemp")]
        public PagedResultDto<MandPivotalView> GetPivotalProg([FromQuery] int ApplicationId)
        {
            SqlDataReader sqlDr = null;

            List<MandPivotal> pvemps = new List<MandPivotal>();
            using (SqlConnection sourceConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand dt = new SqlCommand())
                {
                    dt.Connection = sourceConnection;
                    dt.CommandText = "sp_PivotalProgramme";
                    dt.CommandType = CommandType.StoredProcedure;
                    dt.Parameters.Add("@applicationid", SqlDbType.Int).Value = ApplicationId;
                    dt.CommandTimeout = 120;
                    try
                    {
                        sourceConnection.Open();
                        sqlDr = dt.ExecuteReader();
                        while (sqlDr.Read())
                        {
                            MandPivotal pvemp = new MandPivotal();
                            pvemp.Occupation = sqlDr.GetString(sqlDr.GetOrdinal("Occupation")).ToString();
                            pvemp.Learning = sqlDr.GetString(sqlDr.GetOrdinal("Learning")).ToString();
                            pvemp.Employed = sqlDr[sqlDr.GetOrdinal("Employed")] as int? ?? default(int);
                            pvemp.Cost = sqlDr.GetDecimal(sqlDr.GetOrdinal("Cost"));
                            pvemps.Add(pvemp);
                        }
                    }
                    finally
                    {
                        if (sourceConnection != null)
                        {
                            sourceConnection.Close();
                        }
                        if (sqlDr != null)
                        {
                            sqlDr.Close();
                        }
                    }
                }
            }

            var pivemp = from o in pvemps
                         select new MandPivotalView()
                         {
                             Pivotal = new MandPivotal
                             {
                                 Occupation = o.Occupation,
                                 Learning = o.Learning,
                                 Employed = o.Employed,
                                 Cost = o.Cost
                             }
                         };

            var totalCount = pivemp.Count();

            return new PagedResultDto<MandPivotalView>(
                totalCount,
                pivemp.ToList()
            );
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("pivotalprogrammesunemp")]
        public PagedResultDto<MandPivotalView> GetPivotalProgUnemp([FromQuery] int ApplicationId)
        {
            SqlDataReader sqlDr = null;

            List<MandPivotal> pvunemps = new List<MandPivotal>();
            using (SqlConnection sourceConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand dt = new SqlCommand())
                {
                    dt.Connection = sourceConnection;
                    dt.CommandText = "sp_PivotalProgramme_Unemp";
                    dt.CommandType = CommandType.StoredProcedure;
                    dt.Parameters.Add("@applicationid", SqlDbType.Int).Value = ApplicationId;
                    dt.CommandTimeout = 120;
                    try
                    {
                        sourceConnection.Open();
                        sqlDr = dt.ExecuteReader();
                        while (sqlDr.Read())
                        {
                            MandPivotal pvunemp = new MandPivotal();
                            pvunemp.Occupation = sqlDr.GetString(sqlDr.GetOrdinal("Occupation")).ToString();
                            pvunemp.Learning = sqlDr.GetString(sqlDr.GetOrdinal("Learning")).ToString();
                            pvunemp.Employed = sqlDr[sqlDr.GetOrdinal("Employed")] as int? ?? default(int);
                            pvunemp.Cost = sqlDr.GetDecimal(sqlDr.GetOrdinal("Cost"));
                            pvunemps.Add(pvunemp);
                        }
                    }
                    finally
                    {
                        if (sourceConnection != null)
                        {
                            sourceConnection.Close();
                        }
                        if (sqlDr != null)
                        {
                            sqlDr.Close();
                        }
                    }
                }
            }

            var pivu = from o in pvunemps
                       select new MandPivotalView()
                       {
                           Pivotal = new MandPivotal
                           {
                               Occupation = o.Occupation,
                               Learning = o.Learning,
                               Employed = o.Employed,
                               Cost = o.Cost
                           }
                       };

            var totalCount = pivu.Count();

            return new PagedResultDto<MandPivotalView>(
                totalCount,
                pivu.ToList()
            );
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("contractors")]
        public PagedResultDto<MandContractorsView> GetContractors([FromQuery] int ApplicationId)
        {
            SqlDataReader sqlDr = null;

            List<MandContractors> cnts = new List<MandContractors>();
            using (SqlConnection sourceConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand dt = new SqlCommand())
                {
                    dt.Connection = sourceConnection;
                    dt.CommandText = "sp_Contractors";
                    dt.CommandType = CommandType.StoredProcedure;
                    dt.Parameters.Add("@applicationid", SqlDbType.Int).Value = ApplicationId;
                    dt.CommandTimeout = 120;
                    try
                    {
                        sourceConnection.Open();
                        sqlDr = dt.ExecuteReader();
                        while (sqlDr.Read())
                        {
                            MandContractors cnt = new MandContractors();
                            cnt.Programme = sqlDr.GetString(sqlDr.GetOrdinal("Programme")).ToString();
                            cnt.Man = sqlDr[sqlDr.GetOrdinal("Man")] as int? ?? default(int);
                            cnt.Pro = sqlDr[sqlDr.GetOrdinal("Pro")] as int? ?? default(int);
                            cnt.Tech = sqlDr[sqlDr.GetOrdinal("Tech")] as int? ?? default(int);
                            cnt.Crit = sqlDr[sqlDr.GetOrdinal("Crit")] as int? ?? default(int);
                            cnt.Serv = sqlDr[sqlDr.GetOrdinal("Serv")] as int? ?? default(int);
                            cnt.Trad = sqlDr[sqlDr.GetOrdinal("Trad")] as int? ?? default(int);
                            cnt.Op = sqlDr[sqlDr.GetOrdinal("Op")] as int? ?? default(int);
                            cnt.Ele = sqlDr[sqlDr.GetOrdinal("Ele")] as int? ?? default(int);
                            cnt.Man2 = sqlDr[sqlDr.GetOrdinal("Man2")] as int? ?? default(int);
                            cnt.Pro2 = sqlDr[sqlDr.GetOrdinal("Pro2")] as int? ?? default(int);
                            cnt.Tech2 = sqlDr[sqlDr.GetOrdinal("Tech2")] as int? ?? default(int);
                            cnt.Crit2 = sqlDr[sqlDr.GetOrdinal("Crit2")] as int? ?? default(int);
                            cnt.Serv2 = sqlDr[sqlDr.GetOrdinal("Serv2")] as int? ?? default(int);
                            cnt.Trad2 = sqlDr[sqlDr.GetOrdinal("Trad2")] as int? ?? default(int);
                            cnt.Op2 = sqlDr[sqlDr.GetOrdinal("Op2")] as int? ?? default(int);
                            cnt.Ele2 = sqlDr[sqlDr.GetOrdinal("Ele2")] as int? ?? default(int);
                            cnts.Add(cnt);
                        }
                    }
                    finally
                    {
                        if (sourceConnection != null)
                        {
                            sourceConnection.Close();
                        }
                        if (sqlDr != null)
                        {
                            sqlDr.Close();
                        }
                    }
                }
            }

            var cnrt = from o in cnts
                       select new MandContractorsView()
                       {
                           Contractors = new MandContractors
                           {
                               Programme = o.Programme,
                               Man = o.Man,
                               Pro = o.Pro,
                               Tech = o.Tech,
                               Crit = o.Serv,
                               Serv = o.Serv,
                               Trad = o.Trad,
                               Op = o.Op,
                               Ele = o.Ele,
                               Man2 = o.Man2,
                               Pro2 = o.Pro2,
                               Tech2 = o.Tech2,
                               Crit2 = o.Crit2,
                               Serv2 = o.Serv2,
                               Trad2 = o.Trad2,
                               Op2 = o.Op2,
                               Ele2 = o.Ele2
                           }
                       };

            var totalCount = cnrt.Count();

            return new PagedResultDto<MandContractorsView>(
                totalCount,
                cnrt.ToList()
            );
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("jobtitle")]
        public PagedResultDto<JobTitleOFOViewList> GetJobTitleOFO([FromQuery] int first, int rows, string jobtitle, int majorid)
        {
            SqlDataReader sqlDr = null;
            SqlDataReader sqlDs = null;
            List<JobTitleOFOView> ofos = new List<JobTitleOFOView>();
            List<OFOSpecilaization> specs = new List<OFOSpecilaization>();

            using (SqlConnection sourceConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand dt = new SqlCommand())
                {
                    dt.Connection = sourceConnection;
                    dt.CommandText = "sp_OFOJobTitle";
                    dt.CommandType = CommandType.StoredProcedure;
                    dt.Parameters.Add("@first", SqlDbType.Int).Value = first;
                    dt.Parameters.Add("@rows", SqlDbType.Int).Value = rows;
                    dt.Parameters.Add("@jobtitle", SqlDbType.VarChar).Value = jobtitle;
                    dt.CommandTimeout = 120;
                    try
                    {
                        sourceConnection.Open();
                        sqlDr = dt.ExecuteReader();
                        while (sqlDr.Read())
                        {
                            JobTitleOFOView ofo = new JobTitleOFOView();
                            ofo.Id = sqlDr[sqlDr.GetOrdinal("Id")] as int? ?? default(int);
                            ofo.OFO_Code = sqlDr.GetString(sqlDr.GetOrdinal("OFO_Code")).ToString();
                            ofo.Occupation = sqlDr.GetString(sqlDr.GetOrdinal("Occupation")).ToString();
                            ofos.Add(ofo);
                        }
                    }
                    finally
                    {
                        if (sourceConnection != null)
                        {
                            sourceConnection.Close();
                        }
                        if (sqlDr != null)
                        {
                            sqlDr.Close();
                        }
                    }
                }
            }

            if (majorid > 0)
            {
                ofos = ofos.Where(a => a.OFO_Code.StartsWith(majorid.ToString())).ToList();
            }

            if (ofos.Count > 0)
            {
                using (SqlConnection sourceConnection = new SqlConnection(connectionString))
                {
                    foreach (var ofo in ofos)
                    {
                        string cmdstr = "select Id, Specilization Specialization from lkp_OFO_Specialization where OFO_Code = '" + ofo.OFO_Code + "'";
                        using (SqlCommand ds = new SqlCommand(cmdstr, sourceConnection))
                        {
                            try
                            {
                                specs = new List<OFOSpecilaization>();
                                sourceConnection.Open();
                                sqlDs = ds.ExecuteReader();
                                while (sqlDs.Read())
                                {
                                    OFOSpecilaization spec = new OFOSpecilaization();
                                    spec.Id = sqlDs[sqlDs.GetOrdinal("Id")] as int? ?? default(int);
                                    spec.Specialization = sqlDs.GetString(sqlDs.GetOrdinal("Specialization")).ToString();
                                    specs.Add(spec);
                                }

                                ofo.Specialization = specs;
                            }
                            finally
                            {
                                if (sourceConnection != null)
                                {
                                    sourceConnection.Close();
                                }
                                if (sqlDs != null)
                                {
                                    sqlDs.Close();
                                }
                            }
                        }
                    }
                }
            }

            var ofov = from o in ofos
                       select new JobTitleOFOViewList()
                       {
                           JobTitleOFOView = new JobTitleOFOView
                           {
                               Id = o.Id,
                               OFO_Code = o.OFO_Code,
                               Occupation = o.Occupation,
                               Specialization = o.Specialization
                           }
                       };

            var totalCount = ofos.Count();
            ofov = ofov
            .Distinct()
            .OrderByDescending(a => a.JobTitleOFOView.OFO_Code) //.OrderByDescending(a=> a.JobTitleOFOView.Rank)
            .Skip(first)
            .Take(rows);

            return new PagedResultDto<JobTitleOFOViewList>(
                totalCount,
                ofov.ToList()
            );
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("jobtitlelazy")]
        public PagedResultDto<JobTitleOFOViewList> GetJobTitleOFOLazy([FromQuery] int first, int rows, string jobtitle, int majorid, string OFOCodeFilter, string OFOCodeFilterMode, string OccupationFilter, string OccupationFilterMode)
        {
            SqlDataReader sqlDr = null;
            SqlDataReader sqlDs = null;

            List<JobTitleOFOView> ofos = new List<JobTitleOFOView>();
            List<OFOSpecilaization> specs = new List<OFOSpecilaization>();

            using (SqlConnection sourceConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand dt = new SqlCommand())
                {
                    dt.Connection = sourceConnection;
                    dt.CommandText = "sp_OFOJobTitleLazy";
                    dt.CommandType = CommandType.StoredProcedure;
                    dt.Parameters.Add("@first", SqlDbType.Int).Value = first;
                    dt.Parameters.Add("@rows", SqlDbType.Int).Value = rows;
                    dt.Parameters.Add("@jobtitle", SqlDbType.VarChar).Value = jobtitle;
                    dt.Parameters.Add("@ofocode", SqlDbType.VarChar).Value = OFOCodeFilter;
                    dt.Parameters.Add("@ofocodemode", SqlDbType.VarChar).Value = OFOCodeFilterMode;
                    dt.Parameters.Add("@occupation", SqlDbType.VarChar).Value = OccupationFilter;
                    dt.Parameters.Add("@occupationmode", SqlDbType.VarChar).Value = OccupationFilterMode;
                    dt.CommandTimeout = 120;
                    try
                    {
                        sourceConnection.Open();
                        sqlDr = dt.ExecuteReader();
                        while (sqlDr.Read())
                        {
                            JobTitleOFOView ofo = new JobTitleOFOView();
                            ofo.Id = sqlDr[sqlDr.GetOrdinal("Id")] as int? ?? default(int);
                            ofo.OFO_Code = sqlDr.GetString(sqlDr.GetOrdinal("OFO_Code")).ToString();
                            ofo.Occupation = sqlDr.GetString(sqlDr.GetOrdinal("Occupation")).ToString();

                            ofos.Add(ofo);
                        }
                    }
                    finally
                    {
                        if (sourceConnection != null)
                        {
                            sourceConnection.Close();
                        }
                        if (sqlDr != null)
                        {
                            sqlDr.Close();
                        }
                    }
                }
            }

            if (majorid > 0)
            {
                ofos = ofos.Where(a => a.OFO_Code.StartsWith(majorid.ToString())).ToList();
            }

            if (ofos.Count > 0)
            {
                using (SqlConnection sourceConnection = new SqlConnection(connectionString))
                {
                    foreach (var ofo in ofos)
                    {
                        string cmdstr = "select Id, Specilization Specialization from lkp_OFO_Specialization where OFO_Code = '" + ofo.OFO_Code + "'";
                        using (SqlCommand ds = new SqlCommand(cmdstr, sourceConnection))
                        {
                            try
                            {
                                specs = new List<OFOSpecilaization>();
                                sourceConnection.Open();
                                sqlDs = ds.ExecuteReader();
                                while (sqlDs.Read())
                                {
                                    OFOSpecilaization spec = new OFOSpecilaization();
                                    spec.Id = sqlDr[sqlDr.GetOrdinal("Id")] as int? ?? default(int);
                                    spec.Specialization = sqlDr.GetString(sqlDr.GetOrdinal("Specialization")).ToString();
                                    specs.Add(spec);
                                }

                                ofo.Specialization = specs;
                            }
                            finally
                            {
                                if (sourceConnection != null)
                                {
                                    sourceConnection.Close();
                                }
                                if (sqlDs != null)
                                {
                                    sqlDs.Close();
                                }
                            }
                        }
                    }
                }
            }

            var ofov = from o in ofos
                       select new JobTitleOFOViewList()
                       {
                           JobTitleOFOView = new JobTitleOFOView
                           {
                               Id = o.Id,
                               OFO_Code = o.OFO_Code,
                               Occupation = o.Occupation,
                               Specialization = o.Specialization
                           }
                       };

            var totalCount = ofos.Count();
            ofov = ofov
            .Distinct()
            .OrderBy(a => a.JobTitleOFOView.OFO_Code) //.OrderByDescending(a => a.Rank)
            .Skip(first)
            .Take(rows);

            return new PagedResultDto<JobTitleOFOViewList>(
                totalCount,
                ofov.ToList()
            );

        }

        //public Task<HttpResponseMessage> APILogin(string userNameOrEmailAddress, string password)
        //public Task<string> APILogin(string userNameOrEmailAddress, string password)
        //{
            //    HttpClient _httpClient = new HttpClient();
            //    var a = new HttpResponseMessage();
            //    //var requestMessage = new HttpRequestMessage
            //    //{
            //    //    Method = System.Net.Http.HttpMethod.Post,
            //    //    Content = new StringContent("", Encoding.UTF8, "application/json"),
            //    //    RequestUri = new Uri
            //    //};
            //    //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjgyMiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJzbWxvdHNod2EiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJzbWxvdHNod2ExQGNoaWV0YS5vcmcuemEiLCJBc3BOZXQuSWRlbnRpdHkuU2VjdXJpdHlTdGFtcCI6IjM1VU03VUFRTERENElZRzNUNlNOTVZKSklLR1JDUU9KIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIlJTQSIsIlVzZXJzIiwiR3JhbnQgTWFuYWdlciJdLCJodHRwOi8vd3d3LmFzcG5ldGJvaWxlcnBsYXRlLmNvbS9pZGVudGl0eS9jbGFpbXMvdGVuYW50SWQiOiIyIiwic3ViIjoiODIyIiwianRpIjoiOWEwZWVlNjMtZmY0NS00MjQ5LThlZTctOGJmNDk2YmJjNTgyIiwiaWF0IjoxNzUzMTg5NDczLCJuYmYiOjE3NTMxODk0NzMsImV4cCI6MTc1MzI3NTg3MywiaXNzIjoiQ0hJRVRBTUlTIiwiYXVkIjoiQ0hJRVRBTUlTIn0.CzXdAHpl5ILaWd_1JlAZFl4XsygHXh_uBjjpGHybARs");
            //    //requestMessage.Headers.Add("userNameOrEmailAddress", userNameOrEmailAddress);
            //    //requestMessage.Headers.Add("password", password);
            //    //requestMessage.Headers.Add("rememberClient", "true");

            //    //var response = _httpClient.SendAsync(requestMessage);

            //return "";
        //}
    }
}
