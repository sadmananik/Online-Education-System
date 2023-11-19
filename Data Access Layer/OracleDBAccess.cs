using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Collections;

namespace Data_Access_Layer
{
    public class OracleDBAccess
    {
        static string strConnectionString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
        string loginStatus="";
        bool haveID;
        List<string> list = new List<string>();
        string result = "", accID;

        public List<string> Login(string getID, string getPIN)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {               
                try
                {                    
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("Select * from LOGININFO", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader.GetValue(reader.GetOrdinal("ACCID")).ToString();
                            string userName = reader.GetValue(reader.GetOrdinal("USERNAME")).ToString();
                            string pin = reader.GetValue(reader.GetOrdinal("PIN")).ToString();
                            string status = reader.GetValue(reader.GetOrdinal("STATUS")).ToString();

                            if (id.Equals(getID) || userName.Equals(getID))
                            {
                                haveID = true;
                                if (pin.Equals(getPIN))
                                {
                                    result=  "Found";
                                    accID = id;
                                    loginStatus = status; 
                                }
                                else
                                {
                                    result = "Incorrect PIN";
                                }
                                
                            }
                        }
                        if (!haveID)
                        {
                            result = "Incorrect User Name or ID";
                        }
                        list.Add(result);
                        list.Add(loginStatus);
                        list.Add(accID);

                        reader.Close();
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    return list;
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public DataTable GetQuestionBank(string accID)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
                using (OracleConnection con = new OracleConnection(ConString))
                {
                    OracleCommand cmd = new OracleCommand("select q.QID, q.QUE, q.TOPICID, t.TOPICNAME, q.Q_TYPE, q.OPTIONA, q.OPTIONB, q.OPTIONC, q.OPTIOND, q.CORRECTOPTION from QUEBANK q, TOPIC t, BATCH b, ADVISORACCOUNTS a where t.COURSEID=b.COURSEID and t.TOPICID=q.TOPICID and a.CURRENTBATCHID=b.BATCHID and a.ACCID="+accID+" ORDER BY q.QID ASC", con);
                    OracleDataAdapter oda = new OracleDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    oda.Fill(ds);
                    return ds.Tables[0];
                }                        
        }


        public DataTable AdvisorGetExamineeResult(string accID)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("select r.RECID,r.ACCID,l.name,r.GRADE,r.BATCHID,r.P_COMMENT,r.FINISHDATE,r.MARK from RESULTRECORD r,BATCH c,ADVISORACCOUNTS v, EXAMINEEACCOUNTS l where l.accid=r.accid and r.BATCHID=c.BATCHID and v.CURRENTBATCHID=c.BATCHID and v.accid="+accID+" ORDER BY r.recid ASC", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }

        public DataTable GetQuestionBankByTopic(string accID, string topicName)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("select q.QID, q.QUE, q.TOPICID, t.TOPICNAME, q.Q_TYPE, q.OPTIONA, q.OPTIONB, q.OPTIONC, q.OPTIOND, q.CORRECTOPTION from QUEBANK q, TOPIC t, BATCH b, ADVISORACCOUNTS a where t.COURSEID=b.COURSEID and t.TOPICID=q.TOPICID and a.CURRENTBATCHID=b.BATCHID and t.TOPICNAME='"+topicName+"' and a.ACCID=" +accID+ " ORDER BY q.QID ASC", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }


        public DataTable GetAdvisorBillingDetails(string id)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("select a.NAME, a.ACCID, t.U_RECEIVE, t.TRANSDATE , t.BATCHID , ad.NAME as Recieved_BY from ADVISORACCOUNTS a, TRANSACTION t,ADMINACCOUNTS ad where t.ACCID=a.ACCID and a.ACCID="+id+" ORDER BY t.TRANSACTIONID ASC", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }




        public DataTable GetExamineeBillingDetails(string id)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("select e.NAME, e.ACCID, t.U_PAY, t.TRANSDATE , t.BATCHID , ad.NAME as Recieved_BY from EXAMINEEACCOUNTS e, TRANSACTION t,ADMINACCOUNTS ad where t.ACCID=e.ACCID and e.ACCID="+id+" ORDER BY t.TRANSACTIONID ASC", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }

        public DataTable GetAdvisorAccounts()
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM ADVISORACCOUNTS ORDER BY ACCID ASC", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }

        public DataTable GetAdvisorNotice(string accID)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("select distinct n.NOTICEID,n.STATUS, n.NOTICE, n.NOTICEDATE, n.TIME, n.accid from NOTICE n, EXAMINEEACCOUNTS e, ADVISORACCOUNTS a where e.CURRENTBATCHID =a.CURRENTBATCHID and a.ACCID=n.ACCID and e.ACCID=" + accID + " or n.status in ('Admin', 'Advisor')", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }

        public DataTable GetExamineeNotice(string accID)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("select distinct notice, NOTICEDATE, time, status from notice n,EXAMINEEACCOUNTS e,ADVISORACCOUNTS a,ADMINACCOUNTS,COURSEREGISTRATION r where r.EXAMINEEACCID=e.ACCID and r.VALIDITY='Valid' and r.batchid=a.CURRENTBATCHID and e.ACCID="+accID, con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }

        public DataTable GetAllNotice()
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM NOTICE ORDER BY NoticeID ASC", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }

        public DataTable GetLoginRec()
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("Select * from LOGINRECORD order by LOGINSEQID desc", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }

        public DataTable GetExamineeRating(string id)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("select b.BATCHID, b.BATCHNAME, c.COURSENAME, c.C_PROGRAM, a.NAME as AdvisorName, b.C_RATING, b.P_SESSION from EXAMINEEACCOUNTS e, BATCH b, ADVISORACCOUNTS a, COURSE c where b.COURSEID=c.COURSEID and e.CURRENTBATCHID=b.BATCHID and a.CURRENTBATCHID=b.BATCHID and e.accid="+id, con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }

        public DataTable GetExamRec(string accID)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("select r.RECID, e.ACCID, e.NAME, GRADE,BATCHID,P_COMMENT,FINISHDATE,MARK from RESULTRECORD r, EXAMINEEACCOUNTS e where r.ACCID=" + accID + " and r.ACCID=e.ACCID ORDER BY r.RECID ASC", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }


        public DataTable GetInvalidAdvisorAccounts()
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("SELECT * from GetInvalidAdvisorAccounts", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }


        public DataTable GetAdminAccounts()
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM ADMINACCOUNTS ORDER BY ACCID ASC", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }


        public DataTable GetAdvisorTopicName(string accID)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("Select TOPICNAME from GetAdvisorTopicName where ACCID =" + accID, con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }

        public DataTable GetAdvisorTopicID(string accID)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("select distinct t.TOPICID from COURSE c, BATCH b, TOPIC t, ADVISORACCOUNTS a where a.CURRENTBATCHID=b.BATCHID and b.COURSEID=c.COURSEID and t.COURSEID=c.COURSEID and a.ACCID=" + accID, con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        } 

        public DataTable GetExamineeAccounts()
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM EXAMINEEACCOUNTS ORDER BY ACCID ASC", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }  

        

        public DataTable GetSpecificAccounts(int id, string tableName)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM "+tableName+" WHERE ACCID="+id+"", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }

        public DataTable GetAdvisorQueBank(int id)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("select q.QID, q.QUE, t.TOPICNAME from QUEBANK q, TOPIC t, ADVISORACCOUNTS a,  BATCH b where q.TOPICID=t.TOPICID and t.COURSEID=b.COURSEID and a.CURRENTBATCHID=b.BATCHID and a.ACCID="+id +"order by q.qid asc", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }


        public DataTable GetTopicWiseQue(string topicName, int id)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("select q.QID, q.QUE, t.TOPICNAME from QUEBANK q, TOPIC t, ADVISORACCOUNTS a,  BATCH b where q.TOPICID=t.TOPICID and t.COURSEID=b.COURSEID and a.CURRENTBATCHID=b.BATCHID and t.TOPICNAME='"+topicName+"' and a.ACCID="+id, con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }


        public DataTable GetSelectedQuestion(int id)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("Select QID, QUE from GetSelectedQuestion where ACCID =" + id +"order by qid", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }




        public DataTable GetAdvisorID()
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM ADVISORACCOUNTS WHERE VALIDITY = 'Valid'", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }


        public DataTable GetCourse()
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM COURSE ORDER BY COURSEID ASC", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }


        public DataTable GetBatch()
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM BATCH ORDER BY BATCHID ASC", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }

        public DataTable GetAdvisorBatch(string accID)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("SELECT b.BATCHID FROM BATCH b, ADVISORACCOUNTS a where b.BATCHID=a.CURRENTBATCHID and a.ACCID="+accID+" ORDER BY BATCHID ASC", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }


        public DataTable GetBill()
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("SELECT * FROM TRANSACTION ORDER BY TRANSACTIONID ASC", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }


        public DataTable GetAdvisorCourseDetails(string id)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("select COURSEID, COURSENAME, C_PROGRAM from GetAdvisorCourseDetails where ACCID =" + id, con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }


        public DataTable GetReg()
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("SELECT * from GetReg", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }

        public DataTable GetAdvisorNotes(string id)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("select t.TOPICID, t.TOPICNAME, t.TYPE, t.NOTES, t.REFERENCE from ADVISORACCOUNTS a, BATCH b, COURSE c, TOPIC t where a.CURRENTBATCHID=b.BATCHID and b.COURSEID =c.COURSEID and  c.COURSEID=t.COURSEID and a.accid=" + id, con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }


        public DataTable GetBatchForRegistration()
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("SELECT BATCHID, BATCHNAME, C.COURSENAME, A.NAME, FEETK, P_SESSION, C_RATING FROM BATCH B, COURSE C, ADVISORACCOUNTS A WHERE B.COURSEID=C.COURSEID AND A.ACCID=B.ADVISORID AND B.STATUS='Open'", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }

        public DataTable GetOpenCourseName()
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("SELECT * from GetOpenCourseName", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }



        public string InsertAdvisorRegistration(int id, string name, string userName, string gender, string DOB, string maritialStatus, string email, string bloodGroup, string photo, string phone, string address, string joinDate, string validity, int pin, string status, string secretAns, string getBatchID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "INSERT INTO ADVISORACCOUNTS ( AccID, Name, UserName, Gender, DOB, Maritial_Status, Email, BloodGroup, Photo, Phone, Address, JoinDate, Validity, CURRENTBATCHID ) VALUES ( " + id + ", '" + name + "', '" + userName + "', '" + gender + "', '" + DOB + "', '" + maritialStatus + "', '" + email + "', '" + bloodGroup + "', '" + photo + "', '" + phone + "', '" + address + "', '" + joinDate + "', '" + validity + "',"+getBatchID+")";
                    objCmd.ExecuteNonQuery();
                    objCmd.CommandText = "INSERT INTO LOGININFO ( ACCID, USERNAME, PIN, STATUS, SECRETQUEANS ) VALUES ( " +id+ ", '" +userName + "', '" +pin+ "', '" +status+ "', '" +secretAns+ "')";
                    objCmd.ExecuteNonQuery();
                    return "Registration Complete";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public List<string> GetAdminProfile(int id)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string adminID = "", name="", uName = "", gender = "", DOB = "", maritialStatus = "", email = "", bloodgroup = "", photo = "", phone = "", address = "";
                List<string> list = new List<string>();

                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("Select * from ADMINACCOUNTS Where ACCID ="+id+"" , objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                             adminID = reader.GetValue(reader.GetOrdinal("ACCID")).ToString();
                             name = reader.GetValue(reader.GetOrdinal("NAME")).ToString();
                             uName = reader.GetValue(reader.GetOrdinal("USERNAME")).ToString();
                             gender = reader.GetValue(reader.GetOrdinal("GENDER")).ToString();
                             DOB = reader.GetValue(reader.GetOrdinal("DOB")).ToString();
                             maritialStatus = reader.GetValue(reader.GetOrdinal("MARITIAL_STATUS")).ToString();
                             email = reader.GetValue(reader.GetOrdinal("EMAIL")).ToString();
                             bloodgroup = reader.GetValue(reader.GetOrdinal("BLOODGROUP")).ToString();
                             photo = reader.GetValue(reader.GetOrdinal("PHOTO")).ToString();
                             phone = reader.GetValue(reader.GetOrdinal("PHONE")).ToString();
                             address = reader.GetValue(reader.GetOrdinal("ADDRESS")).ToString();
                         }

                        list.Add(adminID);
                        list.Add(name);
                        list.Add(uName);
                        list.Add(gender);
                        list.Add(DOB);
                        list.Add(maritialStatus);
                        list.Add(email);
                        list.Add(bloodgroup);
                        list.Add(photo);
                        list.Add(phone);
                        list.Add(address);
 
                        reader.Close();                       

                    }
                    return list;
                }
                catch (Exception ex)
                {
                    list.Add(ex.ToString());
                    return list;
                }
                finally
                {
                    objConn.Close();
                }

            }

        }


        public List<string> GetAdvisorSetQuestionInfo(int id, string topicName)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string batchID = "";
                string topicID = "";
                string courseID = "";

                List<string> list = new List<string>();

                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("select b.BATCHID, q.TOPICID, b.COURSEID from QUEBANK q, TOPIC t, ADVISORACCOUNTS a,  BATCH b where q.TOPICID=t.TOPICID and t.COURSEID=b.COURSEID and t.TOPICNAME='"+topicName+"' and a.CURRENTBATCHID=b.BATCHID and a.ACCID=" + id + "", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            batchID = reader.GetValue(reader.GetOrdinal("BATCHID")).ToString();
                            topicID = reader.GetValue(reader.GetOrdinal("TOPICID")).ToString();
                            courseID = reader.GetValue(reader.GetOrdinal("COURSEID")).ToString();                           
                        }

                        list.Add(batchID);
                        list.Add(topicID);
                        list.Add(courseID);                      

                        reader.Close();

                    }
                    return list;
                }
                catch (Exception ex)
                {
                    list.Add(ex.ToString());
                    return list;
                }
                finally
                {
                    objConn.Close();
                }

            }

        }


        public List<string> GetAdvisorProfile(int id)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string adminID = "", name = "", uName = "", gender = "", DOB = "", maritialStatus = "", email = "", bloodgroup = "", photo = "", phone = "", address = "";
                List<string> list = new List<string>();

                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("Select * from ADVISORACCOUNTS Where ACCID =" + id + "", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            adminID = reader.GetValue(reader.GetOrdinal("ACCID")).ToString();
                            name = reader.GetValue(reader.GetOrdinal("NAME")).ToString();
                            uName = reader.GetValue(reader.GetOrdinal("USERNAME")).ToString();
                            gender = reader.GetValue(reader.GetOrdinal("GENDER")).ToString();
                            DOB = reader.GetValue(reader.GetOrdinal("DOB")).ToString();
                            maritialStatus = reader.GetValue(reader.GetOrdinal("MARITIAL_STATUS")).ToString();
                            email = reader.GetValue(reader.GetOrdinal("EMAIL")).ToString();
                            bloodgroup = reader.GetValue(reader.GetOrdinal("BLOODGROUP")).ToString();
                            photo = reader.GetValue(reader.GetOrdinal("PHOTO")).ToString();
                            phone = reader.GetValue(reader.GetOrdinal("PHONE")).ToString();
                            address = reader.GetValue(reader.GetOrdinal("ADDRESS")).ToString();
                        }

                        list.Add(adminID);
                        list.Add(name);
                        list.Add(uName);
                        list.Add(gender);
                        list.Add(DOB);
                        list.Add(maritialStatus);
                        list.Add(email);
                        list.Add(bloodgroup);
                        list.Add(photo);
                        list.Add(phone);
                        list.Add(address);

                        reader.Close();

                    }
                    return list;
                }
                catch (Exception ex)
                {
                    list.Add(ex.ToString());
                    return list;
                }
                finally
                {
                    objConn.Close();
                }

            }

        }


        public List<string> GetExamineeProfile(int id)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string adminID = "", name = "", uName = "", gender = "", DOB = "", maritialStatus = "", email = "", bloodgroup = "", photo = "", phone = "", address = "";
                List<string> list = new List<string>();

                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("Select * from EXAMINEEACCOUNTS Where ACCID =" + id + "", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            adminID = reader.GetValue(reader.GetOrdinal("ACCID")).ToString();
                            name = reader.GetValue(reader.GetOrdinal("NAME")).ToString();
                            uName = reader.GetValue(reader.GetOrdinal("USERNAME")).ToString();
                            gender = reader.GetValue(reader.GetOrdinal("GENDER")).ToString();
                            DOB = reader.GetValue(reader.GetOrdinal("DOB")).ToString();
                            maritialStatus = reader.GetValue(reader.GetOrdinal("MARITIAL_STATUS")).ToString();
                            email = reader.GetValue(reader.GetOrdinal("EMAIL")).ToString();
                            bloodgroup = reader.GetValue(reader.GetOrdinal("BLOODGROUP")).ToString();
                            photo = reader.GetValue(reader.GetOrdinal("PHOTO")).ToString();
                            phone = reader.GetValue(reader.GetOrdinal("PHONE")).ToString();
                            address = reader.GetValue(reader.GetOrdinal("ADDRESS")).ToString();
                        }

                        list.Add(adminID);
                        list.Add(name);
                        list.Add(uName);
                        list.Add(gender);
                        list.Add(DOB);
                        list.Add(maritialStatus);
                        list.Add(email);
                        list.Add(bloodgroup);
                        list.Add(photo);
                        list.Add(phone);
                        list.Add(address);

                        reader.Close();

                    }
                    return list;
                }
                catch (Exception ex)
                {
                    list.Add(ex.ToString());
                    return list;
                }
                finally
                {
                    objConn.Close();
                }

            }

        }


        public string InsertAdminRegistration(int id, int newAdminId, string name, string userName, string gender, string DOB, string maritialStatus, string email, string bloodGroup, string photo, string phone, string address, string joinDate, string validity, int pin, string status, string secretAns)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "INSERT INTO ADMINACCOUNTS ( AccID, Name, UserName, Gender, DOB, Maritial_Status, Email, BloodGroup, Photo, Phone, Address, JoinDate, Validity, CONFIRM_BY_ACCID) VALUES ( " + newAdminId + ", '" + name + "', '" + userName + "', '" + gender + "', '" + DOB + "', '" + maritialStatus + "', '" + email + "', '" + bloodGroup + "', '" + photo + "', '" + phone + "', '" + address + "', '" + joinDate + "', '" + validity + "','" + id + "')";
                    objCmd.ExecuteNonQuery();
                    objCmd.CommandText = "INSERT INTO LOGININFO ( ACCID, USERNAME, PIN, STATUS, SECRETQUEANS ) VALUES ( " + newAdminId + ", '" + userName + "', '" + pin + "', '" + status + "', '" + secretAns + "')";
                    objCmd.ExecuteNonQuery();
                    return "Registration Complete";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public string UpdateAdminAccount(int id, string name, string gender, string DOB, string maritialStatus, string email, string bloodGroup, string photo, string phone, string address, string secretAns)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "UPDATE ADMINACCOUNTS SET Name='" +name+ "' , Gender='" +gender+ "' , DOB='" +DOB+ "' , Maritial_Status='" +maritialStatus+ "' , Email='" +email+ "' , BloodGroup='" +bloodGroup+ "' , Photo='" +photo+ "' , Phone='" +phone+ "' , Address='" +address+ "' WHERE ACCID=" +id+ "";
                    objCmd.ExecuteNonQuery();
                    objCmd.CommandText = "UPDATE LOGININFO SET SECRETQUEANS='" + secretAns + "' Where ACCID="+id;
                    objCmd.ExecuteNonQuery();
                    return "Update Successfully";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }




        public string ChangePIN(int id, int pin)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "Update LOGININFO set pin="+pin+" where ACCID="+id;
                    objCmd.ExecuteNonQuery();
                    return "PIN Update Successfully";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public string UpdateAdvisorAccount(int id, string name, string gender, string DOB, string maritialStatus, string email, string bloodGroup, string photo, string phone, string address, string secretAns)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "UPDATE ADVISORACCOUNTS SET Name='" + name + "' , Gender='" + gender + "' , DOB='" + DOB + "' , Maritial_Status='" + maritialStatus + "' , Email='" + email + "' , BloodGroup='" + bloodGroup + "' , Photo='" + photo + "' , Phone='" + phone + "' , Address='" + address + "' WHERE ACCID=" + id + "";
                    objCmd.ExecuteNonQuery();
                    objCmd.CommandText = "UPDATE LOGININFO SET SECRETQUEANS='" + secretAns + "' Where ACCID="+id;
                    objCmd.ExecuteNonQuery();
                    return "Update Successfully";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public string AdvisorComment(int id, string comment)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "update RESULTRECORD set P_COMMENT='" + comment + "' where  RECID=" + id;
                    objCmd.ExecuteNonQuery();
                    return "Comment Successfully";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public string ConfirmAdvisorID(string id, string Batchid, string adminID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "UPDATE ADVISORACCOUNTS SET VALIDITY='Valid', CURRENTBATCHID =" + Batchid + ", CONFIRM_BY_ACCID="+adminID+"  WHERE ACCID=" + id + "";
                    objCmd.ExecuteNonQuery();
                    return "Confirmed";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public string ConfirmExamineeByAdminID(string id, string adminID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "UPDATE EXAMINEEACCOUNTS SET VALIDITY='Valid', CONFIRM_BY_ACCID=" + adminID + "  WHERE ACCID=" + id + "";
                    objCmd.ExecuteNonQuery();
                    objCmd.CommandText = "UPDATE COURSEREGISTRATION SET VALIDITY='Valid' WHERE EXAMINEEACCID=" + id + "";
                    objCmd.ExecuteNonQuery();
                    return "Confirmed";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public string UpdateExamineeAccount(int id, string name, string gender, string DOB, string maritialStatus, string email, string bloodGroup, string photo, string phone, string address, string secretAns)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "UPDATE EXAMINEEACCOUNTS SET Name='" + name + "' , Gender='" + gender + "' , DOB='" + DOB + "' , Maritial_Status='" + maritialStatus + "' , Email='" + email + "' , BloodGroup='" + bloodGroup + "' , Photo='" + photo + "' , Phone='" + phone + "' , Address='" + address + "' WHERE ACCID=" + id + "";
                    objCmd.ExecuteNonQuery();
                    objCmd.CommandText = "UPDATE LOGININFO SET SECRETQUEANS='" + secretAns + "' Where ACCID="+id;
                    objCmd.ExecuteNonQuery();
                    return "Update Successfully";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public string InsertExamineeRegistration(int id, string name, string userName, string gender, string DOB, string maritialStatus, string email, string bloodGroup, string photo, string phone, string address, string joinDate, string validity, int pin, string status, string secretAns, string getBatchID, int lastRegID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "INSERT INTO EXAMINEEACCOUNTS ( AccID, Name, UserName, Gender, DOB, Maritial_Status, Email, BloodGroup, Photo, Phone, Address, JoinDate, Validity, CURRENTBATCHID ) VALUES ( " + id + ", '" + name + "', '" + userName + "', '" + gender + "', '" + DOB + "', '" + maritialStatus + "', '" + email + "', '" + bloodGroup + "', '" + photo + "', '" + phone + "', '" + address + "', '" + joinDate + "', '" + validity + "',"+getBatchID+")";
                    objCmd.ExecuteNonQuery();
                    objCmd.CommandText = "INSERT INTO LOGININFO ( ACCID, USERNAME, PIN, STATUS, SECRETQUEANS ) VALUES ( " + id + ", '" + userName + "', '" + pin + "', '" + status + "', '" + secretAns + "')";
                    objCmd.ExecuteNonQuery();
                    objCmd.CommandText = "INSERT INTO COURSEREGISTRATION VALUES ( " + lastRegID + ", " + id + ", " + getBatchID + ", 'Invalid', 0)";
                    objCmd.ExecuteNonQuery();
                    return "Registration Complete";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public string PayAdvisorBill(int tid, int accid, float payFee, string status, string date, int adminID, int batchID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    IDbTransaction trans = objConn.BeginTransaction(); // Turn off AUTOCOMMIT
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "LOCK TABLE TRANSACTION IN EXCLUSIVE MODE NOWAIT";
                    objCmd.ExecuteNonQuery();
                    objCmd.CommandText = "INSERT INTO TRANSACTION ( TransactionID, AccID, U_Pay, U_Status, TransDate, AdminAccID, BATCHID) VALUES (" +tid+ "," +accid+ "," +payFee+ ", '" +status+"', '" + date + "', " + adminID + ", " + batchID + ")";
                    objCmd.ExecuteNonQuery();
                    trans.Commit();
                    return "Bill Paid";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public string RecAdvisorBill(int tid, int accid, double recFee, string status, string date, int adminID, int batchID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    IDbTransaction trans = objConn.BeginTransaction(); // Turn off AUTOCOMMIT
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "LOCK TABLE TRANSACTION IN EXCLUSIVE MODE NOWAIT";
                    objCmd.ExecuteNonQuery();
                    objCmd.CommandText = "INSERT INTO TRANSACTION (TRANSACTIONID,	ACCID,	U_RECEIVE,	U_STATUS,	TRANSDATE,	ADMINACCID,	BATCHID) VALUES ( " + tid + ", " + accid + ", " + recFee + ", '" + status + "', '" + date + "', " + adminID + ", " + batchID + ")";
                    objCmd.ExecuteNonQuery();
                    trans.Commit();
                    return "Bill Recieved";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public string InsertSetQuestion(int qID, int batchID, int topicID, int courseID, string id)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    IDbTransaction trans = objConn.BeginTransaction(); // Turn off AUTOCOMMIT
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "INSERT INTO SETQUESTION  VALUES ( " + qID + ", " + batchID + ", " + topicID + ", " + courseID + ","+id+")";
                    objCmd.ExecuteNonQuery();
                    trans.Commit();
                    return " Added";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public string InsertNotes(int topicID, string topicName, string type, int courseID, string notes, string reference)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "INSERT INTO TOPIC VALUES (" + topicID + ", ' " + topicName + " ',  '" + type + "',  " + courseID + " , ' " + notes + " ', ' " + reference + " ')";                    
                    objCmd.ExecuteNonQuery();
                    return "INSERTED";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }
                
            }
        }

        public string InsertQueBank(int qID, string que, int topicID, string qType, string optionA, string optionB, string optionC, string optionD, string correctoption)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "INSERT INTO quebank VALUES (" + qID + ", ' " + que + " ',  " + topicID + ", ' " + qType + " ', ' " + optionA + " ', ' " + optionB + " ', ' " + optionC + " ', ' " + optionD + " ', ' " + correctoption + " ')";                    
                    objCmd.ExecuteNonQuery();
                    return "INSERTED";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }
                
            }
        }

        public string InsertCourseRegistration(int regID, string examineeID, string batchID, string validity)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "INSERT INTO COURSEREGISTRATION VALUES (" + regID + ",  " + examineeID + " ,  " + batchID + ", '" + validity + "', 0)";
                    objCmd.ExecuteNonQuery();
                    objCmd.CommandText = "UPDATE EXAMINEEACCOUNTS  SET CURRENTBATCHID=" + batchID + " Where ACCID=" + examineeID;
                    objCmd.ExecuteNonQuery();
                    return "INSERTED";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public string UpdateQueBank(string que, int topicID, string qType, string optionA, string optionB, string optionC, string optionD, string correctoption, int updateQID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "UPDATE QUEBANK SET QUE= '" + que + "', TOPICID=" + topicID + ", Q_TYPE='" + qType + "', OPTIONA='" + optionA + "', OPTIONB='" + optionB + "',OPTIONC='" + optionC + "',OPTIOND='" + optionD + "' WHERE QID= " + updateQID + "";   
                    objCmd.ExecuteNonQuery();
                    return "UPDATED";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }
                
            }
        }

        public string UpdateNotes(int topicID, string topicName, string type, int courseID, string notes, string reference)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "UPDATE TOPIC SET TOPICNAME= '" + topicName + "', TYPE='" + type + "', COURSEID=" + courseID + ", NOTES='" + notes + "', REFERENCE='" + reference + "' WHERE TOPICID= " + topicID + "";
                    objCmd.ExecuteNonQuery();
                    return "UPDATED";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public string DeleteQueBank(int qID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "DELETE FROM QUEBANK WHERE QID =" + qID;
                    objCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }
                return "DELETED";
            }
        }

        public string DeleteTransaction(int tID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "DELETE FROM TRANSACTION WHERE TRANSACTIONID =" + tID;
                    objCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }
                return "DELETED";
            }
        }


        

        public string DeleteNotes(int topicID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "DELETE FROM TOPIC WHERE TOPICID =" + topicID;
                    objCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }
                return "DELETED";
            }
        }

        public string DeleteSelectedQues(int dqid)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "DELETE FROM SETQUESTION WHERE QID =" + dqid;
                    objCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }
                return "DELETED";
            }
        }

        public string InsertLoginRecord(int lastRecID, string acccID, string date, string time, string courseID, string batchID, string status)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "Insert into LOGINRECORD values(" + lastRecID + "," + acccID + ", '" + date + "', '" + time + "', " + courseID + ", " + batchID + ", '" + status + "')";
                    objCmd.ExecuteNonQuery();
                    return "Record Inserted";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public int GetLastLogRecID()
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string lastLogID = "";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("select LOGINSEQID_seq1.nextval from dual;", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lastLogID = reader.GetValue(reader.GetOrdinal("NEXTVAL")).ToString();
                        }

                        reader.Close();
                    }
                    return Convert.ToInt32(lastLogID);

                }
                catch (Exception ex)
                {
                    return 1;
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public int GetLastNoticeID()
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string lastQID = "";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("SELECT NoticeID from NOTICE WHERE ROWNUM <= 1 order by NoticeID desc", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lastQID = reader.GetValue(reader.GetOrdinal("NoticeID")).ToString();
                        }

                        reader.Close();
                    }
                    return Convert.ToInt32(lastQID) + 1;

                }
                catch (Exception ex)
                {
                    return 1;
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public int GetBatchRatingCounter(string batchID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string lastQID = "";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("SELECT C_RATING_COUNTER from BATCH WHERE BATCHID="+batchID, objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lastQID = reader.GetValue(reader.GetOrdinal("C_RATING_COUNTER")).ToString();
                        }

                        reader.Close();
                    }
                    return Convert.ToInt32(lastQID);

                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public double GetBatchRating(string batchID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string lastQID = "";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("SELECT C_RATING from BATCH WHERE BATCHID=" + batchID, objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lastQID = reader.GetValue(reader.GetOrdinal("C_RATING")).ToString();
                        }

                        reader.Close();
                    }
                    return Convert.ToDouble(lastQID);

                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    objConn.Close();
                }

            }
        }



        public string GetExamineeBatchID(string accID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string lastQID = "";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("select CURRENTBATCHID from EXAMINEEACCOUNTS where accid =" + accID, objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lastQID = reader.GetValue(reader.GetOrdinal("CURRENTBATCHID")).ToString();
                        }

                        reader.Close();
                    }
                    return lastQID;

                }
                catch (Exception ex)
                {
                    return ex.ToString() ;
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public List<string> GetBatchIDAndCourseID(string accID, string tableName)
        {
            List<string> list = new List<string>();

            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string batch = "", course = "";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("select c.COURSEID, b.BATCHID from COURSE c, BATCH  b, " + tableName + " a where c.COURSEID=b.COURSEID and b.BATCHID=a.CURRENTBATCHID and a.ACCID=" + accID, objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            course = reader.GetValue(reader.GetOrdinal("COURSEID")).ToString();
                            batch = reader.GetValue(reader.GetOrdinal("BATCHID")).ToString();
                        }

                        list.Add(batch);
                        list.Add(course);

                        reader.Close();
                    }
                    return list;

                }
                catch (Exception ex)
                {
                    return list; ;
                }
                finally
                {
                    objConn.Close();
                }
            }
        }



        public double GetBatchFee(string batchID)
        {
            double value = 0;
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "GetBatchFee";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("bID ", OracleDbType.Int32).Value = 1; 
                objCmd.Parameters.Add("fee", OracleDbType.Double, 100).Direction = ParameterDirection.Output;
                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                    value = Convert.ToDouble(objCmd.Parameters["fee"].Value.ToString());
                    return value;
                }
                catch (Exception ex)
                {
                    return value;
                }
                finally
                {
                    objConn.Close();
                    objCmd.Dispose();
                }
                
            }
        }


        public int GetExamineeBatchStatus(string accid)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string status = "", validity = "";
                int accessExam = 0;

                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("select e.CURRENTBATCHID,r.VALIDITY from EXAMINEEACCOUNTS e,COURSEREGISTRATION r where r.EXAMINEEACCID=e.ACCID and e.ACCID=" + accid, objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            status = reader.GetValue(reader.GetOrdinal("CURRENTBATCHID")).ToString();
                            validity = reader.GetValue(reader.GetOrdinal("VALIDITY")).ToString();
                        }

                        if (validity.Equals("Valid") && int.Parse(status) > 0)
                        {
                            accessExam = 1;
                        }

                        reader.Close();
                    }
                    return accessExam;

                }
                catch (Exception ex)
                {
                    return accessExam;
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public int checkUniqueUserName(string userName)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                int count=0;
                try
                {
                    objConn.Open();
                    OracleCommand objCmd1 = new OracleCommand("select * from LOGININFO where USERNAME ='" + userName+"'", objConn);
                    using (OracleDataReader reader = objCmd1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count++;
                        }

                        reader.Close();
                    }
                  
                    return  count;

                }
                catch (Exception ex)
                {
                    return  count;
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        


        public DataTable GetLoginRec(string accID)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("Select * from LOGINRECORD Where ACCID=" + accID, con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }

        public string GetSpecificTopicID(string topicName)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string id="";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("SELECT TOPICID from TOPIC WHERE TOPICNAME = '" + topicName+"'", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = reader.GetValue(reader.GetOrdinal("TOPICID")).ToString();
                        }

                        reader.Close();
                    }
                    return id;

                }
                catch (Exception ex)
                {
                    return id;
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public string GetExamineeExamCourseBatch(string accid)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string id = "Course Name : ";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("select c.COURSENAME, b.BATCHID from BATCH b, COURSE c, EXAMINEEACCOUNTS e where e.CURRENTBATCHID=b.BATCHID and  c.COURSEID=b.COURSEID and e.ACCID="+accid, objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id =id + reader.GetValue(reader.GetOrdinal("COURSENAME")).ToString();
                            id= id + " Batch ID: " + reader.GetValue(reader.GetOrdinal("BATCHID")).ToString();
                        }

                        reader.Close();
                    }
                    return id;

                }
                catch (Exception ex)
                {
                    return id;
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public double GetCourseFee(string batchID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string feeTK = "";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("select FEETK from BATCH b, COURSE c where b.COURSEID=c.COURSEID and b.BATCHID=" + batchID, objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            feeTK =reader.GetValue(reader.GetOrdinal("FEETK")).ToString();
                        }

                        reader.Close();
                    }
                    return Convert.ToDouble(feeTK);

                }
                catch (Exception ex)
                {
                    return  Convert.ToDouble(feeTK);
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public int GetOldPIN(string accID)
        {
            string pin = "";

            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.CommandText = "GetOldPIN";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("ID", OracleDbType.Int32).Value = accID;
                objCmd.Parameters.Add("pin", OracleDbType.Char, 100).Direction = ParameterDirection.Output;
                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                    pin = objCmd.Parameters["pin"].Value.ToString();
                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    objConn.Close();
                    objCmd.Dispose();
                }
                return Convert.ToInt32(pin);
            }
        }


        public int GetLastQID()
        {
            int value;
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.Parameters.Add("returnVal", OracleDbType.Int32, ParameterDirection.ReturnValue);
                objCmd.CommandText = "GetLastQID";
                objCmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                    value = Convert.ToInt32(objCmd.Parameters["returnVal"].Value.ToString());
                    return value;
                }
                catch (Exception ex)
                {
                    return 1;
                }
                finally
                {
                    objConn.Close();
                    objCmd.Dispose();
                }

            }
        }

        public int GetLastOpenCourseBatchID(string courseName)
        {
            int value;
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.Parameters.Add("returnVal", OracleDbType.Int32, ParameterDirection.ReturnValue);
                objCmd.CommandText = "GetLastOpenCourseBatchID";
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("cname", OracleDbType.Varchar2).Value = courseName;

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                    value = Convert.ToInt32(objCmd.Parameters["returnVal"].Value.ToString());
                    return value;
                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    objConn.Close();
                    objCmd.Dispose();
                }

            }
        }

        public int GetLastTID()
        {
            int value;
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.Parameters.Add("returnVal", OracleDbType.Int32, ParameterDirection.ReturnValue);
                objCmd.CommandText = "GetLastTID";
                objCmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                    value = Convert.ToInt32(objCmd.Parameters["returnVal"].Value.ToString());
                    return value;
                }
                catch (Exception ex)
                {
                    return 1;
                }
                finally
                {
                    objConn.Close();
                    objCmd.Dispose();
                }

            }
        }

        public int GetLastTopicID()
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string LastTopicID = "";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("SELECT TOPICID from TOPIC WHERE ROWNUM <= 1 order by TOPICID desc", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LastTopicID = reader.GetValue(reader.GetOrdinal("TOPICID")).ToString();
                        }

                        reader.Close();
                    }
                    return Convert.ToInt32(LastTopicID) + 1;

                }
                catch (Exception ex)
                {
                    return  1000;
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public string GetRegisteredBatch(string accID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string count = "";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("select nvl(CURRENTBATCHID,0) as CURRENTBATCHID  from EXAMINEEACCOUNTS where ACCID=" + accID, objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count = reader.GetValue(reader.GetOrdinal("CURRENTBATCHID")).ToString();
                        }

                        reader.Close();
                    }
                    return count;

                }
                catch (Exception ex)
                {
                    return count;
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public int GetLastRegID()
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string lastREGID = "";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("SELECT REGNO from COURSEREGISTRATION WHERE ROWNUM <= 1 order by REGNO desc", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lastREGID = reader.GetValue(reader.GetOrdinal("REGNO")).ToString();
                        }

                        reader.Close();
                    }
                    return Convert.ToInt32(lastREGID) + 1;

                }
                catch (Exception ex)
                {
                    return 1;
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public int GetLastCourseID()
        {
            int value;
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                OracleCommand objCmd = new OracleCommand();
                objCmd.Connection = objConn;
                objCmd.Parameters.Add("returnVal", OracleDbType.Int32, ParameterDirection.ReturnValue);
                objCmd.CommandText = "GetLastCourseID";
                objCmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                    value = Convert.ToInt32(objCmd.Parameters["returnVal"].Value.ToString());
                    return value;
                }
                catch (Exception ex)
                {
                    return 1;
                }
                finally
                {
                    objConn.Close();
                    objCmd.Dispose();
                }

            }
        }


        public List<string> GetEmailAndSecretAns(string id, string tableName)
        {
            List<string> list = new List<string>();

            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string email = "", ans = "";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("select a.EMAIL, l.SECRETQUEANS from "+tableName+" a, LOGININFO l where l.ACCID=a.ACCID and a.ACCID= " +id, objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            email = reader.GetValue(reader.GetOrdinal("EMAIL")).ToString();
                            ans = reader.GetValue(reader.GetOrdinal("SECRETQUEANS")).ToString();
                        }
                        list.Add(email);
                        list.Add(ans);
                        reader.Close();
                    }
                    return list;

                }
                catch (Exception ex)
                {
                    list.Add(ex.ToString());
                    return list;
                }
                finally
                {
                    objConn.Close();
                }

            }
        }



        public string GetAccStatus(string id)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string status = "";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("SELECT STATUS from LOGININFO WHERE ACCID= "+id+"", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            status = reader.GetValue(reader.GetOrdinal("STATUS")).ToString();
                        }

                        reader.Close();
                    }
                    return status;

                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public int GetLastBatchID()
        {


            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string lastCourseID = "";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("SELECT BATCHID from BATCH WHERE ROWNUM <= 1 order by BATCHID desc", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lastCourseID = reader.GetValue(reader.GetOrdinal("BATCHID")).ToString();
                        }

                        reader.Close();
                    }
                    return Convert.ToInt32(lastCourseID) + 1;

                }
                catch (Exception ex)
                {
                    return 1;
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public int GetAdvisorCourseID(string id)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string CourseID = "";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("select c.COURSEID from COURSE c, ADVISORACCOUNTS a, BATCH b where a.CURRENTBATCHID=b.BATCHID and c.COURSEID=b.COURSEID and a.ACCID="+id, objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CourseID = reader.GetValue(reader.GetOrdinal("COURSEID")).ToString();
                        }

                        reader.Close();
                    }
                    return Convert.ToInt32(CourseID);

                }
                catch (Exception ex)
                {
                    return Convert.ToInt32(CourseID);
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public string InvalidACC(string accID, string tableName )
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "UPDATE " + tableName + " SET VALIDITY = 'Invalid' WHERE ACCID = " + accID;
                    objCmd.ExecuteNonQuery();
                    return "Account Invalid";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public string UpdateBatchRating(string batchID, double rating, int counter, string id, int rate)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "UPDATE BATCH SET C_RATING= " + rating + ", C_RATING_COUNTER = "+counter+" WHERE BATCHID = " + batchID;
                    objCmd.ExecuteNonQuery();
                    objCmd.CommandText = "UPDATE COURSEREGISTRATION SET RATE="+rate+" WHERE EXAMINEEACCID=" + id ;
                    objCmd.ExecuteNonQuery();
                    return "Thanks For Your Feedback Rating";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public string ValidACC(string accID, string tableName)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "UPDATE " + tableName + " SET VALIDITY = 'Valid' WHERE ACCID = " + accID;
                    objCmd.ExecuteNonQuery();
                    return "Account Valid";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        
        public string UpdateCourse(string courseID, string courseName,string courseProg)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "UPDATE COURSE SET COURSENAME= '" +courseName+ "',  C_PROGRAM='" +courseProg+ "' WHERE COURSEID = " +courseID;
                    objCmd.ExecuteNonQuery();
                    return "UPDATED";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public string DeleteCourse(int courseID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "DELETE FROM COURSE WHERE COURSEID =" + courseID;
                    objCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }
                return "DELETED";
            }
        }

        public string InsertCourse(string courseID, string courseName, string coureProg)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "INSERT INTO COURSE ( COURSEID, COURSENAME, C_PROGRAM ) VALUES ( " + courseID + ", '" + courseName + "', '" + coureProg + "')" ;
                    objCmd.ExecuteNonQuery();
                    return "INSERTED";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public string UploadNotice(string noticeID, string Notice, string date, string status, string accID, string time)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "INSERT INTO NOTICE VALUES ( " + noticeID + ", '" + Notice + "', '" + date + "', '" + status + "', " + accID +", '" + time + "')";
                    objCmd.ExecuteNonQuery();
                    return "Notice Uploded";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        
        public string DeleteBatch(int batchID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "DELETE FROM BATCH WHERE BATCHID =" + batchID;
                    objCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }
                return "DELETED";
            }
        }


        public string DeleteNotice(int noticeID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "DELETE FROM NOTICE WHERE NoticeID =" + noticeID;
                    objCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }
                return "DELETED";
            }
        }



        public string UpdateBatch(string batchID, string batchName, string courseID, string advisorID, string fee, string prog, string status)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "UPDATE BATCH SET COURSEID=" + courseID + ", ADVISORID=" + courseID + ", FEETK=" + fee + ",  P_SESSION='" + prog + "', STATUS='"+ status +"' WHERE BATCHID =" + batchID;
                    objCmd.ExecuteNonQuery();
                    return "UPDATED";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public string InsertBatch(string batchID, string batchName, string courseID, string advisorID, string fee, string prog, string status )
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "INSERT INTO BATCH ( BATCHID, BATCHNAME, COURSEID,	ADVISORID, FEETK, P_SESSION, STATUS ) VALUES ( " + batchID + ", '" + batchName + "', " + courseID + "," + advisorID + ",'" + fee + "','" + prog + "','" + status + "')";
                    objCmd.ExecuteNonQuery();
                    return "INSERTED";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public string UpdateSecretQuesAns(string id, string ans)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "UPDATE LOGININFO SET SECRETQUEANS='" +ans+ "' WHERE ACCID = " +1001;
                    objCmd.ExecuteNonQuery();
                    return "UPDATED!";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public string GetSecretQuesAns(string id)
        {
            string ans="";

            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("Select * from LOGININFO Where ACCID="+id+"", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ans = reader.GetValue(reader.GetOrdinal("SECRETQUEANS")).ToString();
                        }

                        reader.Close();
                    }
                    return ans;

                }
                catch (Exception ex)
                {
                    return ans;
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public int GetExamineeBatchRatingStatus(string id, string batchID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                int checkRateStatus = 0;
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("select count(*) as Status from COURSEREGISTRATION where EXAMINEEACCID="+id+" and BATCHID = "+batchID+" and VALIDITY='Valid' and RATE=0", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            checkRateStatus = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("STATUS")));
                        }

                        reader.Close();
                    }

                    return checkRateStatus;
                }
                    
                catch(Exception ex)
                {
                    return checkRateStatus;
                }

                finally
                {
                    objConn.Close();
                }

            }
        }


        public int GetNewACCID()
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                int NewACCID =0;
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("Select ACCID from LOGININFO  WHERE ROWNUM <=1 Order By ACCID DESC", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader.GetValue(reader.GetOrdinal("ACCID")).ToString();
                            NewACCID = Convert.ToInt32(id) +1;
                        }
                        
                        reader.Close();
                    }
                    return NewACCID;

                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public string CheckAdvisorValidity(string advisorID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string result="";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("Select * from ADVISORACCOUNTS", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader.GetValue(reader.GetOrdinal("ACCID")).ToString(); 
                            string validity = reader.GetValue(reader.GetOrdinal("VALIDITY")).ToString();
                            string uname = reader.GetValue(reader.GetOrdinal("USERNAME")).ToString();

                            if (id.Equals(advisorID) || uname.Equals(advisorID))
                            {
                                if (validity.Equals("Valid"))
                                {
                                    result = "Valid";
                                }
                                else
                                {
                                    result = "Invalid";
                                }

                            }

                        }

                        reader.Close();
                        return result;
                    }


                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public string CheckSecreAns(string getID, string getSecrectAns)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string result = "";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("Select SECRETQUEANS from LOGININFO WHERE ACCID ="+getID, objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ans = reader.GetValue(reader.GetOrdinal("SECRETQUEANS")).ToString();


                                haveID = true;
                                if (ans.Equals(getSecrectAns))
                                {
                                    result = "Matched";                                    
                                }
                                else
                                {
                                    result = "Incorrect ANS";
                                }                           
                        }
                        reader.Close();
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }



        public string CheckID(string getID)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string result = "";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("Select * from LOGININFO", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader.GetValue(reader.GetOrdinal("ACCID")).ToString();
                            string pin = reader.GetValue(reader.GetOrdinal("PIN")).ToString();
                            string status = reader.GetValue(reader.GetOrdinal("STATUS")).ToString();

                            if (id.Equals(getID))
                            {
                                haveID = true;
                                result = "Found";
                            }
                        }
                        if (!haveID)
                        {
                            result = "Incorrect ID";
                            haveID = false;
                        }
                        reader.Close();

                    }
                    return result;
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }


        public string ChangePass(int id, int pass)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "UPDATE LOGININFO SET PIN =" +pass+ " WHERE ACCID="+id+"";
                    objCmd.ExecuteNonQuery();
                    return "Password Changed Successfully";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }
                
            }
        }


        public List<string> GetExamineeCourseName(int id)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string courseName = "";
                List<string> list = new List<string>();

                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("select COURSENAME from COURSEREGISTRATION,BATCH,COURSE where batch.batchid=COURSEREGISTRATION.batchid and batch.courseid=course.courseid and COURSEREGISTRATION.VALIDITY='Valid' and EXAMINEEACCID="+id, objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courseName = reader.GetValue(reader.GetOrdinal("COURSENAME")).ToString();
                            list.Add(courseName);
                        }



                        reader.Close();

                    }
                    return list;
                }
                catch (Exception ex)
                {
                    list.Add(ex.ToString());
                    return list;
                }
                finally
                {
                    objConn.Close();
                }

            }

        }

        public List<string> GetExamineeTopicName(string courseName)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string topicName = "";
                List<string> list = new List<string>();

                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("select TOPICNAME from TOPIC,COURSE where topic.courseid=course.courseid and coursename='" + courseName + "'", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            topicName = reader.GetValue(reader.GetOrdinal("TOPICNAME")).ToString();
                            list.Add(topicName);
                        }



                        reader.Close();

                    }
                    return list;
                }
                catch (Exception ex)
                {
                    list.Add(ex.ToString());
                    return list;
                }
                finally
                {
                    objConn.Close();
                }

            }

        }

        public string GetExamineeNotes(string topicName)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string image = "";


                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("select NOTES from TOPIC where topicname='" + topicName + "'", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            image = reader.GetValue(reader.GetOrdinal("NOTES")).ToString();

                        }



                        reader.Close();

                    }
                    return image;
                }
                catch (Exception ex)
                {

                    return image;
                }
                finally
                {
                    objConn.Close();
                }

            }

        }

        public string GetExamineeReference(string topicName)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string image = "";


                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("select REFERENCE from TOPIC where topicname='" + topicName + "'", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            image = reader.GetValue(reader.GetOrdinal("REFERENCE")).ToString();
                        }



                        reader.Close();

                    }
                    return image;
                }
                catch (Exception ex)
                {

                    return image;
                }
                finally
                {
                    objConn.Close();
                }

            }

        }

        public DataTable GetQuestion(int id)
        {

            string ConString = "Data Source=XE;User Id=AdbmsProject;Password=project;";
            using (OracleConnection con = new OracleConnection(ConString))
            {
                OracleCommand cmd = new OracleCommand("select e.ACCID,b.BATCHID,coursename,s.QID, q.QUE,q.OPTIONA,q.OPTIONB,q.OPTIONC,q.OPTIOND,q.CORRECTOPTION from QUEBANK q, SETQUESTION s, EXAMINEEACCOUNTS e, BATCH b,course where s.COURSE_ID=b.COURSEID and course.courseid=b.COURSEID and e.CURRENTBATCHID=b.BATCHID and q.QID=s.QID and e.ACCID=" + id + "", con);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                return ds.Tables[0];
            }
        }



        public int GetLastRecID()
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                string lastRID = "";
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = new OracleCommand("select RECID_seq.nextval from dual;", objConn);
                    using (OracleDataReader reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lastRID = reader.GetValue(reader.GetOrdinal("NEXTVAL")).ToString();
                        }

                        reader.Close();
                    }
                    return Convert.ToInt32(lastRID);

                }
                catch (Exception ex)
                {
                    return 1;
                }
                finally
                {
                    objConn.Close();
                }

            }
        }

        public string InsertExamineeResult(int recId, int aId, string grd, int bId, string fdate, double mrk)
        {
            using (OracleConnection objConn = new OracleConnection(strConnectionString))
            {
                try
                {
                    objConn.Open();
                    OracleCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.Text;
                    objCmd.CommandText = "INSERT into RESULTRECORD(RecID,AccID,Grade, BatchID,FinishDate,Mark) values(" + recId + ", " + aId + ",'" + grd + "'," + bId + ",'" + fdate + "'," + mrk + ")";
                    objCmd.ExecuteNonQuery();
                    objCmd.CommandText = "Update EXAMINEEACCOUNTS Set CURRENTBATCHID=0 where ACCID=" + aId;
                    objCmd.ExecuteNonQuery();
                    return "Exam Submited";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    objConn.Close();
                }

            }
        }     



    }
}
