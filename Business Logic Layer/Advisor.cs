using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class Advisor
    {
        OracleDBAccess da = new OracleDBAccess();

        public DataTable GetQuestionBank(string accID)
        {
            DataTable dt = da.GetQuestionBank(accID);
            return dt;
        }

        public DataTable GetAdvisorNotice(string accID)
        {
            DataTable dt = da.GetAdvisorNotice(accID);
            return dt;
        }
        
        
        public DataTable AdvisorGetExamineeResult(string id)
        {
            DataTable dt = da.AdvisorGetExamineeResult(id);
            return dt;
        }

        public string DeleteNotice(int noticeID)
        {
            return da.DeleteNotice(noticeID);
        }

        public int GetLastNoticeID()
        {
            return da.GetLastNoticeID();
        }

        public DataTable GetAdvisorCourseDetails(string accID)
        {
            DataTable dt = da.GetAdvisorCourseDetails(accID);
            return dt;
        }


        public DataTable GetAdvisorTopicName(string id)
        {
            DataTable dt = da.GetAdvisorTopicName(id);
            return dt;
        }

        public DataTable GetQuestionBankByTopic(string id, string topicName)
        {
            DataTable dt = da.GetQuestionBankByTopic(id, topicName);
            return dt;
        }

        public DataTable GetAdvisorBillingDetails(string id)
        {
            DataTable dt = da.GetAdvisorBillingDetails(id);
            return dt;
        }
        

        public DataTable GetAdvisorNotes(string id)
        {
            DataTable dt = da.GetAdvisorNotes(id);
            return dt;
        }

        public DataTable GetAdvisorQueBank(int id)
        {
            DataTable dt = da.GetAdvisorQueBank(id);
            return dt;
        }

        public DataTable GetSelectedQuestion(int id)
        {
            DataTable dt = da.GetSelectedQuestion(id);
            return dt;
        }
        

        public DataTable GetTopicWiseQue(string topicName, int id)
        {
            DataTable dt = da.GetTopicWiseQue(topicName, id);
            return dt;
        }

        public DataTable GetAdvisorTopicID(string id)
        {
            DataTable dt = da.GetAdvisorTopicID(id);
            return dt;
        }
        


        public List<string> GetAdvisorSetQuestionInfo(int id, string topicName)
        {
            List<string> list = new List<string>();
            return da.GetAdvisorSetQuestionInfo(id, topicName);
        }

        public string GetSecretQuesAns(string id)
        {
            return da.GetSecretQuesAns(id);
        }

        public string DeleteNotes(int topicID)
        {
            return da.DeleteNotes(topicID);
        
        }

        public string UploadNotice(string noticeID, string Notice, string date, string status, string accID, string time)
        {
            return da.UploadNotice(noticeID, Notice, date, status, accID, time);
        }

        public int GetOldPIN(string accID)
        {
            return da.GetOldPIN(accID);
        }

        public string AdvisorComment(int id, string comment)
        {
            return da.AdvisorComment(id, comment);
        }

        public string GetSpecificTopicID(string topicName)
        {
            return da.GetSpecificTopicID(topicName);
        }


        
        public DataTable GetOpenCourseName()
        {
            return da.GetOpenCourseName();
        }

        public int GetLastOpenCourseBatchID(string courseName)
        {
            return da.GetLastOpenCourseBatchID(courseName);
        }


        public string DeleteSelectedQues(int dqid)
        {
            return da.DeleteSelectedQues(dqid);
        
        }

        public string InsertSetQuestion(int qID, int batchID, int topicID, int courseID, string id)
        {
            return da.InsertSetQuestion( qID,  batchID,  topicID, courseID,id);       
        }

        public string ChangePIN(int id, int pin)
        {
            return da.ChangePIN(id, pin);
        }
        

        public string InsertNotes(int topicID, string topicName, string type, int courseID, string notes, string reference)
        {
            return da.InsertNotes(topicID, topicName, type, courseID, notes, reference);
        }

        public int GetLastTopicID()
        {
            return da.GetLastTopicID();
        }

        public int GetAdvisorCourseID(string id)
        {
            return da.GetAdvisorCourseID(id);
        }

        public int GetLastQueID()
        {            
            return da.GetLastQID();
        }

        public string UpdateAdvisorAccount(int id, string name, string gender, string DOB, string maritialStatus, string email, string bloodGroup, string photo, string phone, string address, string secretAns)
        {
            return da.UpdateAdvisorAccount(id, name, gender, DOB, maritialStatus, email, bloodGroup, photo, phone, address, secretAns);
        }

        public string InsertQestionBank(int qID, string que, int topicID, string qType, string optionA, string optionB, string optionC, string optionD, string correctoption)
        {
            return da.InsertQueBank(qID,que,topicID,qType,optionA,optionB,optionC,optionD,correctoption);
        }

        public string DeleteQueBank(int qID)
        {
            return da.DeleteQueBank(qID);
        }

        public string UpdateQueBank(string que, int topicID, string qType, string optionA, string optionB, string optionC, string optionD, string correctoption, int updateQID)
        {
            return da.UpdateQueBank(que, topicID, qType, optionA, optionB, optionC, optionD, correctoption, updateQID);
        }

        public string UpdateNotes(int topicID, string topicName, string type, int courseID, string notes, string reference)
        {
            return da.UpdateNotes(topicID, topicName, type, courseID, notes, reference);
        }

        

        public List<string> GetAdvisorProfile(int id)
        {
            List<string> list = new List<string>();
            return da.GetAdvisorProfile(id);
        }

        public string UpdateSecretQuesAns(string id, string ans)
        {
            return da.UpdateSecretQuesAns(id, ans);
        }

    }
}
