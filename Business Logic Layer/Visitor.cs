using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class Visitor
    {
        OracleDBAccess da = new OracleDBAccess();


        public List<string> LoginD(string id, string pin)
        {
            return da.Login(id, pin);
        }

        public List<string> GetBatchIDAndCourseID(string accID, string tableName)
        {
            return da.GetBatchIDAndCourseID(accID, tableName);
        }    

        public List<string> GetEmailAndSecretAns(string id, string tableName)
        {
            return da.GetEmailAndSecretAns(id, tableName);
        }
        

        public int checkUniqueUserName(string userName)
        {
            return da.checkUniqueUserName(userName);
        }

        public string CheckSecreAnswer(string id, string ans)
        {
            return da.CheckSecreAns(id, ans);
        }

        public int GetLastLogRecID()
        {
            return da.GetLastLogRecID();
        }

        public string InsertLoginRecord(int lastRecID, string acccID, string date, string time, string courseID, string batchID, string status)
        {
            return da.InsertLoginRecord(lastRecID, acccID, date, time, courseID, batchID, status);
        }

        public string CheckID(string id)
        {
            return da.CheckID(id);
        }

        public DataTable GetOpenCourseName()
        {
            return da.GetOpenCourseName();
        }

        public string GetAccStatus(string id)
        {
            return da.GetAccStatus(id);
        }

        

        public string ForgetPassChange(int id, int pass)
        {
            return da.ChangePass(id,pass);
        }

        public double GetCourseFee(string batchID)
        {
            return da.GetCourseFee(batchID);
        }

        public string AdvisorRegistration(int id, string name, string userName, string gender, string DOB, string maritialStatus, string email, string bloodGroup, string photo, string phone, string address, string joinDate, string validity, int pin, string status, string secretAns, string getBatchID)
        {
            return da.InsertAdvisorRegistration(id, name, userName, gender, DOB, maritialStatus, email, bloodGroup, photo, phone, address, joinDate, validity, pin, status, secretAns, getBatchID);
        }
        
        public string InsertExamineeRegistration(int id, string name, string userName, string gender, string DOB, string maritialStatus, string email, string bloodGroup, string photo, string phone, string address, string joinDate, string validity, int pin, string status, string secretAns, string getBatchID, int lastRegID)
        {
            return da.InsertExamineeRegistration(id, name, userName, gender, DOB, maritialStatus, email, bloodGroup, photo, phone, address, joinDate, validity, pin, status, secretAns, getBatchID, lastRegID);
        }

        public int GetLastRegID()
        {
            return da.GetLastRegID();
        }


        public int GetNewAccountID()
        {
            return da.GetNewACCID();
        }

        public int GetLastOpenCourseBatchID(string courseName)
        {
            return da.GetLastOpenCourseBatchID(courseName);
        }

        

        public string CheckAdvisorValidity(string advisorAccID)
        {
            return da.CheckAdvisorValidity(advisorAccID);
        }

    }
}
