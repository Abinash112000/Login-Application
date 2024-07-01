using LoginApplication.Logic.DataLogic;
using LoginApplication.Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginApplication.Logic.BusinessLogic
{
    public class BllForLoginApp
    {
        DllForLoginApp DllObj;
        public BllForLoginApp(string appConnectionString)
        {
            DllObj = new DllForLoginApp(appConnectionString);
        }
        public List<LoginAppModel> GetAllData()
        {
            List<LoginAppModel> lstSetupOptions = new List<LoginAppModel>();
            DataTable dtTable = DllObj.GetAllData();
            if (dtTable.Rows.Count > 0)
            {
                lstSetupOptions = ConvertTable(dtTable);
            }
            return lstSetupOptions;
        }
        private List<LoginAppModel> ConvertTable(DataTable dtTable)
        {
            List<LoginAppModel> lstOptions = new List<LoginAppModel>();
            lstOptions = (from DataRow dr in dtTable.Rows
                               select new LoginAppModel()
                               {
                                   EmployeeId = Convert.ToInt32(dr["Employee_Id"]),
                                   Firstname = dr["Firstname"].ToString(),
                                   Lastname = dr["Lastname"].ToString(),
                                   Username = dr["Username"].ToString(),
                                   Password = dr["Password"].ToString(),
                                   IsActive = Convert.ToInt32(dr["IsActive"]),
                                   IsChecked = Convert.ToBoolean(dr["ISCHECKED"])
                               }).ToList();
            return lstOptions;
        }
        public bool CreateAcc(string Firstname, string Lastname, string Username, string Password)
        {
            return DllObj.CreateAcc(Firstname, Lastname, Username, Password);
        }
        public bool UpdateData(List<LoginAppModel> lstModel)
        {
            return DllObj.UpdateAllData(lstModel);
        }
    }
}
