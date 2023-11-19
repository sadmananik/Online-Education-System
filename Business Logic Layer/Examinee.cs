using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class Examinee
    {
        OracleDBAccess da = new OracleDBAccess();

        public List<string> GetExamineeProfile(int id)
        {
            List<string> list = new List<string>();
            return da.GetExamineeProfile(id);
        }

        public List<string> GetExamineeTopicName(string courseName)
        {
            List<string> list = new List<string>();
            return da.GetExamineeTopicName(courseName);
        }

        public DataTable GetExamineeNotice(string id)
        {
            return da.GetExamineeNotice(id);
        }
        public DataTable GetExamRec(string id)
        {
            return da.GetExamRec(id);
        }

        public DataTable GetExamineeRating(string id)
        {
            return da.GetExamineeRating(id);
        }

        

         public DataTable GetBatchForRegistration()
        {
            return da.GetBatchForRegistration();
        }


        public DataTable GetExamineeBillingDetails(string id)
        {
            DataTable dt = da.GetExamineeBillingDetails(id);
            return dt;
        }

        public string ChangePIN(int id, int pin)
        {
            return da.ChangePIN(id, pin);
        }

        public string UpdateExamineeAccount(int id, string name, string gender, string DOB, string maritialStatus, string email, string bloodGroup, string photo, string phone, string address, string secretAns)
        {
            return da.UpdateExamineeAccount(id, name, gender, DOB, maritialStatus, email, bloodGroup, photo, phone, address, secretAns);
        }

       

        public int GetOldPIN(string accID)
        {
            return da.GetOldPIN(accID);
        }

        public string UpdateBatchRating(string batchID, double rating, int counter, string id, int rate)
        {
            return da.UpdateBatchRating(batchID, rating, counter, id, rate);
        }


        public double GetBatchRating(string batchID)
        {
            return da.GetBatchRating(batchID);
        }

        public int GetBatchRatingCounter(string batchID)
        {
            return da.GetBatchRatingCounter(batchID);
        }

        public int GetExamineeBatchRatingStatus(string id, string batchID)
        {
            return da.GetExamineeBatchRatingStatus(id, batchID);
        }

        public string GetExamineeBatchID(string id)
        {
            return da.GetExamineeBatchID(id);
        }

        
        public string GetSecretQuesAns(string id)
        {
            return da.GetSecretQuesAns(id);
        }

        public string GetExamineeExamCourseBatch(string id)
        {
            return da.GetExamineeExamCourseBatch(id);
        }
        

        public string UpdateSecretQuesAns(string id, string ans)
        {
            return da.UpdateSecretQuesAns(id, ans);
        }

        public int GetLastRegID()
        {
            return da.GetLastRegID();
        }


        public int GetExamineeBatchStatus(string id)
        {
            return da.GetExamineeBatchStatus(id);
        }
        


        public string GetRegisteredBatch(string batchID)
        {
            return da.GetRegisteredBatch(batchID);
        }

        public List<string> GetExamineeCourseName(int id)
        {
            List<string> list = new List<string>();
            return da.GetExamineeCourseName(id);
        }

        public string GetExamineeNotes(string topicName)
        {

            return da.GetExamineeNotes(topicName);
        }

        public string GetExamineeReference(string topicName)
        {

            return da.GetExamineeReference(topicName);
        }
        


        public string InsertCourseRegistration(int regID, string examineeID, string batchID, string validity)
        {
            return da.InsertCourseRegistration(regID,examineeID,batchID,validity);
        }


        public DataTable GetQuestion(int id)
        {
            return da.GetQuestion(id);
        }

        public int GetLastRecID()
        {
            return da.GetLastRecID();
        }

        public string InsertExamineeResult(int recId, int aId, string grd, int bId, string fdate, double mrk)
        {
            return da.InsertExamineeResult(recId, aId, grd, bId, fdate, mrk);
        }
    }
}
