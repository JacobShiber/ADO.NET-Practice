using AdoPractice.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdoPractice.Controllers.API
{
    public class TeachersController : ApiController
    {
        string connectionString = "Data Source=DESKTOP-76KPC67;Initial Catalog=SchoolDB;Integrated Security=True;Pooling=False";
        // GET: api/Teachers
        public IHttpActionResult Get()
        {
            try
            {
                List<TeacherModel> teachersList = new List<TeacherModel>();

                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT * FROM Teacher";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            teachersList.Add(new TeacherModel(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetInt32(3), dataReader.GetDateTime(4)));
                        }
                    }
                    connection.Close();
                    return Ok(new { Massasge = "Success!", teachersList });
                }

            }
            catch(SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Teachers/5
        public IHttpActionResult GetById(int id)
        {
            try
            {
                TeacherModel expectedTeacher = new TeacherModel();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $@"SELECT * FROM Teacher WHERE Teacher.Id = {id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            expectedTeacher.Id = dataReader.GetInt32(0);
                            expectedTeacher.FirstName = dataReader.GetString(1);
                            expectedTeacher.LastName = dataReader.GetString(2);
                            expectedTeacher.Salary = dataReader.GetInt32(3);
                            expectedTeacher.BirthDate = dataReader.GetDateTime(4);
                        }
                        return Ok(new { Massasge = "Success!", expectedTeacher });
                    }
                    connection.Close();
                    return Ok(new { Massasge = "Faliure, no teacher been found"});
                }
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Teachers
        public IHttpActionResult AddTeacher([FromBody]TeacherModel newTeacher)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $@"INSERT INTO Teacher(FirstName, LastName, Salary, BirthDate) VALUES ('{newTeacher.FirstName}', '{newTeacher.LastName}', {newTeacher.Salary}, '{newTeacher.BirthDate}') ";
                    SqlCommand command = new SqlCommand(query, connection);
                    int rows = command.ExecuteNonQuery();
                    connection.Close();
                    if (rows > 0) return Ok(new { Massasge = "Success! new teacher been added" });
                    else return Ok(new { Massasge = "Faliure, no teacher been added" });
                }
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT: api/Teachers/5
        [HttpPut]
        public IHttpActionResult EditTeacher(int id, [FromBody] TeacherModel editedTeacher)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $@"UPDATE Teacher SET FirstName = '{editedTeacher.FirstName}', LastName = '{editedTeacher.LastName}', Salary = {editedTeacher.Salary}, BirthDate = '{editedTeacher.BirthDate}' WHERE Teacher.Id = {id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    int rows = command.ExecuteNonQuery();
                    connection.Close();
                    if (rows > 0) return Ok(new { Massasge = "Success! teacher been edited" });
                    else return Ok(new { Massasge = "Faliure, no teacher been edited" });
                }
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Teachers/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $@"DELETE FROM Teacher WHERE Teacher.Id = {id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    int rows = command.ExecuteNonQuery();
                    connection.Close();
                    if (rows > 0) return Ok(new { Massasge = "Success! teacher been deleted" });
                    else return Ok(new { Massasge = "Faliure, no teacher been deleted" });
                }
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
