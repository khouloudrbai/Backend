
using PFE.SMSNotification.Library.DTO.Services;
using PFE.SMSNotification.Library.Utility;
using PFE.SMSNotification.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;



namespace Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        //lire les requete sql
        NpgsqlConnection npgsqlConnection;
        //faire connection
        TraceManager traceManager;

        public ServiceController()
        {
            traceManager = new TraceManager(HttpContext);
            npgsqlConnection = new NpgsqlConnection(Config.CONNECTION_STRING);
        }
        [HttpPost("getservice")]
        public IActionResult get_player(ServiceToGetListDTO serviceget)
        {
            try
            {
                npgsqlConnection.Open();
                string requeteSQL = @"select * from get_service()";

                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(requeteSQL, npgsqlConnection);

                //read requete
                NpgsqlDataReader UserReader = npgsqlCommand.ExecuteReader();
                List<ServiceToReturnListDTO> results = new List<ServiceToReturnListDTO>();

                //n7otouha fi list 

                //si user doesnt have a row then
                if (!UserReader.HasRows)
                {

                    npgsqlCommand.Dispose();
                    npgsqlConnection.Close();
                    return Ok(new DataResponse<ServiceToReturnListDTO>(false, "User EMPTY", "201", results));
                }

                //else 
                while (UserReader.Read())
                {
                    try
                    {
                        ServiceToReturnListDTO ServiceToReturnDTO = new ServiceToReturnListDTO();

                        ServiceToReturnDTO.id_service = Convert.ToInt32(UserReader["id_service"]);
                        ServiceToReturnDTO.libelle = Convert.ToString(UserReader["libelle"]);
                        ServiceToReturnDTO.shortcode = Convert.ToString(UserReader["shortcode"]);
                       
                        results.Add(ServiceToReturnDTO);


                    }
                    catch (Exception ex)
                    {
                        npgsqlConnection.Close();
                        traceManager.WriteLog(ex, System.Reflection.MethodInfo.GetCurrentMethod().Name);
                        return BadRequest(new DataResponse<object>(true, "server error", "500", null));
                    }
                }
                npgsqlCommand.Dispose();
                npgsqlConnection.Close();

                return Ok(new DataResponse<ServiceToReturnListDTO>(false, "", "201", results));

            }
            catch (Exception ex)
            {
                npgsqlConnection.Close();
                traceManager.WriteLog(ex, System.Reflection.MethodInfo.GetCurrentMethod().Name);
                return BadRequest(new DataResponse<ServiceToReturnListDTO>(true, "server error", "500", null));
            }
        }

    }
}

    
