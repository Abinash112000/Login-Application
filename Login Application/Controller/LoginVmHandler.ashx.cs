using LoginApplication.Logic.BusinessLogic;
using LoginApplication.Logic.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Westwind.Web;

namespace Login_Application.Controller 
{
    public class LoginVmHandler : CallbackHandler
    {
        private string _connectionstring = ConfigurationManager.ConnectionStrings["appConnectionString"].ConnectionString;
        [CallbackMethod(RouteUrl ="GetAllData")]
        public string GetAllData()
        {
            BllForLoginApp BllObj = new BllForLoginApp(_connectionstring);
            List<LoginAppModel> lstModel = new List<LoginAppModel>();
            lstModel = BllObj.GetAllData();
            string jsonResponce = string.Empty;
            jsonResponce = JsonConvert.SerializeObject(lstModel);
            return jsonResponce;
        }
        [CallbackMethod(RouteUrl ="CreateAcc")]
        public bool CreateAcc(string Firstname, string Lastname, string Username, string Password)
        {
            BllForLoginApp BllObj = new BllForLoginApp(_connectionstring);
            bool IsSuccessfull = false;
            IsSuccessfull = BllObj.CreateAcc(Firstname, Lastname, Username, Password);
            return IsSuccessfull;
        }
    }
}