using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class Admin
    {
        OracleDBAccess da = new OracleDBAccess();

        public DataTable GetAdvisorAccounts()
        {
            return da.GetAdvisorAccounts();
        }

        public DataTable GetInvalidAdvisorAccounts()
        {
            return da.GetInvalidAdvisorAccounts();
        }

        public DataTable GetLoginRec()
        {
            return da.GetLoginRec();
        }

        public DataTable GetLoginRec(string accID)
        {
            return da.GetLoginRec(accID);
        }

        public DataTable GetBill()
        {
            return da.GetBill();
        }

        public string ChangePIN(int id, int pin)
        {
            return da.ChangePIN(id,pin);
        }


        public DataTable GetReg()
        {
            return da.GetReg();
        }

        public string ConfirmAdvisorID(string id, string Batchid, string adminID)
        {
            return da.ConfirmAdvisorID(id, Batchid,adminID);
        }

        public DataTable GetAdminAccounts()
        {
            return da.GetAdminAccounts();
        }

        public DataTable GetAllNotice()
        {
            return da.GetAllNotice();
        }


        public DataTable GetExamineeAccounts()
        {
            return da.GetExamineeAccounts();
        }

        public DataTable GetThisAccount(int id, string tableName)
        {
            return da.GetSpecificAccounts(id, tableName);
        }

        public DataTable GetCourse()
        {
            return da.GetCourse();
        }
        public DataTable GetBatch()
        {
            return da.GetBatch();
        } 

         public DataTable GetAdvisorBatch(string accID)
        {
            return da.GetAdvisorBatch(accID);
        } 

        public DataTable GetAdvisorID()
        {
            return da.GetAdvisorID();
        }


        public string GetAccStatus(string id)
        {
            return da.GetAccStatus(id);
        }

        public string ValidACC(string id, string tableName)
        {
            return da.ValidACC(id, tableName);
        }

        public string InvalidACC(string id, string tableName)
        {
            return da.InvalidACC(id, tableName);
        }

        public int GetOldPIN(string accID)
        {
            return da.GetOldPIN(accID);
        }

        public string UploadNotice(string noticeID, string Notice, string date, string status, string accID, string time)
        {
            return da.UploadNotice( noticeID, Notice, date, status, accID, time);
        }

        public string DeleteNotice(int noticeID)
        {
            return da.DeleteNotice(noticeID);
        }

        public int GetLastNoticeID()
        {
            return da.GetLastNoticeID();
        }

        public int GetLastTID()
        {
             return da.GetLastTID();
        }

        public string DeleteTransaction(string tID)
        {
            return da.DeleteTransaction(int.Parse(tID));
        }

        public string InsertAdminAccount(int id, int newAdminId, string name, string userName, string gender, string DOB, string maritialStatus, string email, string bloodGroup, string photo, string phone, string address, string joinDate, string validity, int pin, string status, string secretAns)
        {
            return da.InsertAdminRegistration(id, newAdminId, name, userName, gender, DOB, maritialStatus, email, bloodGroup, photo, phone, address, joinDate, validity, pin, status, secretAns);
        }


        public string PayAdvisorBill(int tid, int accid, float payFee, string status, string date, int adminID, int batchID)
        {
            return da.PayAdvisorBill(tid, accid, payFee, status, date, adminID, batchID);
        }

        public string RecAdvisorBill(int tid, int accid, double recFee, string status, string date, int adminID, int batchID)
        {
            return da.RecAdvisorBill(tid, accid, recFee, status, date, adminID, batchID);
        }
        

        public List<string> GetAdminProfile(int id)
        {
            List<string> list = new List<string>();
            return da.GetAdminProfile(id);
        }

        public List<string> GetEmailAndSecretAns(string id, string tableName)
        {
            return da.GetEmailAndSecretAns(id, tableName);
        }

        public string UpdateAdminAccount(int id, string name, string gender, string DOB, string maritialStatus, string email, string bloodGroup, string photo, string phone, string address,string secretAns)
        {
            return da.UpdateAdminAccount( id, name, gender, DOB, maritialStatus, email, bloodGroup, photo, phone, address, secretAns);
        }

        public double GetBatchFee(string batchID)
        {
            return da.GetBatchFee(batchID);
        }
        

        public string GetSecretQuesAns(string id)
        {
            return da.GetSecretQuesAns(id);
        }
        
        public string UpdateSecretQuesAns(string id,string ans)
        {
            return da.UpdateSecretQuesAns(id, ans);
        }

        public string ConfirmExamineeByAdminID(string id, string adminID)
        {
            return da.ConfirmExamineeByAdminID(id,adminID);
        }


        public int GetLastCourseID()
        {
            return da.GetLastCourseID();
        }

        public int GetLastBatchID()
        {
            return da.GetLastBatchID();
        }

        public string UpdateCourse(string courseID, string courseName, string courseProg)
        {
            return da.UpdateCourse(courseID, courseName, courseProg);
        }


        public string UpdateBatch(string batchID, string batchName, string courseID, string advisorID, string fee, string prog, string status)
        {
            return da.UpdateBatch(batchID, batchName, courseID, advisorID, fee, prog, status);
        }

        public string InsertCourse(string courseID, string courseName, string courseProg)
        {
            return da.InsertCourse(courseID, courseName, courseProg);
        }

        public string DeleteCourse(int courseID)
        {
            return da.DeleteCourse(courseID);
        }

        public string InsertBatch(string batchID, string batchName, string courseID, string advisorID, string fee, string prog, string status)
        {
            return da.InsertBatch(batchID, batchName, courseID, advisorID, fee, prog, status);
        }

        public string DeleteBatch(int BatchID)
        {
            return da.DeleteBatch(BatchID);
        }
    }
}
