using DSG.Enumerations;
using DSG.Lib.DataAccess;
using LoginApplication.Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginApplication.Logic.DataLogic
{
    public class DllForLoginApp
    {
        string connectionString = string.Empty;
        IDataAccess objDataAccess;
        public DllForLoginApp(string appConnectionString)
        {
            connectionString = appConnectionString;
            objDataAccess = DataAccessFactory.GetDataAccess(DataAccessType.ENTLIB, connectionString);

        }
        public DataTable GetAllData()
        {
            DataTable dtTable = new DataTable();
            ObjDataAccessInfo objReturn = new ObjDataAccessInfo();
            string sqlQuery = @"SELECT * , CASE WHEN ISNULL(IsActive,0)=1 THEN 'TRUE' ELSE 'FALSE' END AS ISCHECKED  FROM EMPLOYEES_DETAILS";
            objReturn = objDataAccess.GetDataTableForSqlCommand(sqlQuery);
            dtTable = objReturn.Output;
            return dtTable;
        }
        public bool CreateAcc(string Firstname, string Lastname, string Username, string Password)
        {

            try
            {
                bool isdata = false;
                ObjDataAccessInfo objReturn = new ObjDataAccessInfo();
                string sqlQuery = $@"INSERT INTO EMPLOYEES_DETAILS(Firstname,Lastname,Username,Password)VALUES('{Firstname}','{Lastname}','{Username}','{Password}')";
                objReturn = objDataAccess.ExecuteNonQuery(sqlQuery);
                if (objReturn.IsSuccessful)
                {
                    isdata = true;
                }
                return isdata;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateAllData(List<LoginAppModel> lstModel)
        {
            bool issaved = true;
            ObjDataAccessInfo objReturn = new ObjDataAccessInfo();
            if (lstModel.Count > 0)
            {
                for(int i = 0; i<lstModel.Count; i++)
                {
                    LoginAppModel lstUpdate = lstModel[i];
                    string Firstname = lstUpdate.Firstname;
                    string Lastname = lstUpdate.Lastname;
                    string Username = lstUpdate.Username;
                    string Password = lstUpdate.Password;
                    int IsActive = lstUpdate.IsActive;
                    int EmpId = lstUpdate.EmployeeId;
                    string sqlQuery = $@"UPDATE EMPLOYEES_DETAILS SET Firstname = '{Firstname}' , Lastname='{Lastname}' , Username = '{Username}' , Password = '{Password}' ,
                                   IsActive = {IsActive}  WHERE Employee_Id = {EmpId}";
                    objReturn = objDataAccess.ExecuteNonQuery(sqlQuery);
                    if (!objReturn.IsSuccessful)
                    {
                        issaved = false;
                    }
                }
            }
            return issaved;
        }

    }
}
