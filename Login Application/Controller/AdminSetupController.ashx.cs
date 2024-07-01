using LoginApplication.Logic.BusinessLogic;
using LoginApplication.Logic.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Westwind.Web;

namespace Login_Application.Controller 
{

    public class AdminSetupController : CallbackHandler
    {
        private string _connectionstring = ConfigurationManager.ConnectionStrings["appConnectionString"].ConnectionString;
        
        [CallbackMethod(RouteUrl= "GetAllData")]
        public string GetAllData()
        {
            BllForLoginApp BllObjForAdminSetup = new BllForLoginApp(_connectionstring);
            List<LoginAppModel> lstmodel = new List<LoginAppModel>();
            lstmodel = BllObjForAdminSetup.GetAllData();
            string jsonResponce = JsonConvert.SerializeObject(lstmodel);
            return jsonResponce;
        }
        [CallbackMethod(RouteUrl ="UpdateData")]
        public bool UpdateData()
        {
            bool isSuccessfull = true;
            string JsonResult = Request.Form[0];
            List<LoginAppModel> lstModel = JsonConvert.DeserializeObject<List<LoginAppModel>>(JsonResult);
            BllForLoginApp BllObjForUpdate = new BllForLoginApp(_connectionstring);
            isSuccessfull = BllObjForUpdate.UpdateData(lstModel);
            return isSuccessfull;
        }
    }
}