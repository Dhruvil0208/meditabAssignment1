using crud_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Transactions;

namespace crud_api.Controllers
{
    [Route("Patient/")]
    [ApiController]

    public class PatientController : Controller
    {
        private readonly IConfiguration _configuration;

        public PatientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }




        /// <summary>
        /// Function to get element by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>table containig data for the given id</returns>

        [HttpGet("{id}")]
        public JsonResult patientgetbyid(int id)
        {
            string query = @"
               select patientgetbyid(@id)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudApi");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }
          
            return new JsonResult((table));
        }
       
/// <summary>
/// insert data
/// </summary>
/// <param name="p"></param>
/// <returns>primary key</returns>

        [HttpPost]
       
        public JsonResult Post(Patient p)
        {
            string query = @"select patientcreate(@FirstName,@LastName,@MiddleName,@Sex,@Dob::date)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudApi");
            NpgsqlDataReader myReader;


            System.IO.StringWriter writer = new System.IO.StringWriter();



            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                    {


                        using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                        {
                            myCon.Open();

                            myCommand.Parameters.AddWithValue("@PatientId", p.PatientId);
                            myCommand.Parameters.AddWithValue("@FirstName", p.FirstName);
                            myCommand.Parameters.AddWithValue("@LastName", p.LastName);
                            myCommand.Parameters.AddWithValue("@MiddleName", p.MiddleName);
                            myCommand.Parameters.AddWithValue("@Sex", p.Sex);
                            myCommand.Parameters.AddWithValue("@Dob", p.dob);

                            /*if (p.FirstName == "")
                            {
                                throw new InvalidOperationException("firstname is required");
                            }
*/
                            myReader = myCommand.ExecuteReader();
                            table.Load(myReader);

                            myReader.Close();
                            myCon.Close();
                            

                        }


                    }
                    scope.Complete();

                }
            }
            catch (TransactionAbortedException ex)
            {
                writer.WriteLine("TransactionAbortedException Message: {0}", ex.Message);
            }


            return new JsonResult((table));
        }



/// <summary>
/// Update Patient
/// </summary>
/// <param name="p"></param>
/// <returns></returns>
        [HttpPut]
        
        public JsonResult Put(Patient p)
        {
            string query = @"select patientupdate(@PatientId,@FirstName,@LastName,@MiddleName,@Sex,@Dob::date)";
            

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudApi");
            NpgsqlDataReader myReader;

            System.IO.StringWriter writer = new System.IO.StringWriter();



            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                    {


                        using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                        {
                            myCon.Open();

                            myCommand.Parameters.AddWithValue("@PatientId", p.PatientId);
                            myCommand.Parameters.AddWithValue("@FirstName", p.FirstName);
                            myCommand.Parameters.AddWithValue("@MiddleName", p.MiddleName);
                            myCommand.Parameters.AddWithValue("@LastName", p.LastName);
                            
                            myCommand.Parameters.AddWithValue("@Sex", p.Sex);
                            myCommand.Parameters.AddWithValue("@Dob", p.dob);

                            /*if (p.FirstName == "")
                            {
                                throw new InvalidOperationException("firstname is required");
                            }
*/
                            myReader = myCommand.ExecuteReader();
                            table.Load(myReader);

                            myReader.Close();
                            myCon.Close();


                        }


                    }
                    scope.Complete();

                }
            }
            catch (TransactionAbortedException ex)
            {
                writer.WriteLine("TransactionAbortedException Message: {0}", ex.Message);
            }


            return new JsonResult((table));
        }


        /// <summary>
        /// function to delete data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
   
        public JsonResult Delete(int id)
        {
            string query = @"
                select patientdelete(@id)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudApi");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("record deleted");
        }



        /// <summary>
        /// function to get all data with sorting and pagination
        /// </summary>
        /// <param name="pat"></param>
        /// <returns></returns>
       /* [HttpPost("search")]
        public JsonResult patientget(getPatient pat)
        {
            string query = @"
                select * FROM search_patient(@PageNumber,@PageSize,@FIRST_NAME,@LAST_NAME,@SEX,@DOB,@orderby);
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudApi");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@PageNumber", pat.pageNumber);
                    myCommand.Parameters.AddWithValue("@PageSize", pat.pageSize);
                    myCommand.Parameters.AddWithValue("@FIRST_NAME", pat.FirstName);
                    myCommand.Parameters.AddWithValue("@LAST_NAME", pat.LastName);
                    myCommand.Parameters.AddWithValue("@SEX", pat.Sex);
                    myCommand.Parameters.AddWithValue("@DOB", pat.dob);
                    myCommand.Parameters.AddWithValue("@orderby", pat.orderBy);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult((table));
        }*/
        [HttpPost("search")]
        public JsonResult Post(GetPatient gp)
        {
            string query = @"
                select * from search_patient(@pageNumber::int,@pageSize::int,@firstName,@lastName,@gender,@dob::date,@orderBy
                )	
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("CrudApi");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {



                    myCommand.Parameters.AddWithValue("@pageNumber", gp.pageNumber);
                    myCommand.Parameters.AddWithValue("@pageSize", gp.pageSize);
                    //firstname check
                    if (gp.firstName != null && gp.firstName!="")
                    {
                        myCommand.Parameters.AddWithValue("@firstName", gp.firstName);
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@firstName", DBNull.Value);
                    }
                    //lastname check
                    if (gp.lastName != null && gp.lastName!="")
                    {
                        myCommand.Parameters.AddWithValue("@lastName", gp.lastName);
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@lastName", DBNull.Value);
                    }
                    //gender check
                    if (gp.gender != null && gp.gender!="")
                    {
                        myCommand.Parameters.AddWithValue("@gender", gp.gender);
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@gender", DBNull.Value);
                    }
                    //dobcheck
                    if (gp.dateOfBirth != null )
                    {
                        myCommand.Parameters.AddWithValue("@dob", gp.dateOfBirth);
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@dob", DBNull.Value);
                    }
                    //order by check
                    if (gp.orderBy != null && gp.orderBy != "")
                    {
                        myCommand.Parameters.AddWithValue("@orderBy", gp.orderBy);
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@orderBy", DBNull.Value);
                    }
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult((table));
        }
    }
}



//not working attempts


