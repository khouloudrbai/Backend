

using System;
using System.Numerics;

namespace PFE.SMSNotification.Library.DTO.Services
{
    public class ServiceToGetListDTO
    {

    }
    public class ServiceToReturnListDTO

    {
        public int id_service { get; set; }
        public string shortcode { get; set; }
        public string libelle { get; set; }
        public int number_gamers { get; set; }


    }
}