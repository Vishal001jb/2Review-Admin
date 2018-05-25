using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Login.ModelClass;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using _2ReviewEmployeeSideHomeScreen.ModelClasses;
using System.Net;

namespace Login.Service
{
    public class AzureDataService
    {
        public MobileServiceClient MobileService { get; set; }
        public IMobileServiceSyncTable<Question> QuesTable { get; set; }
        public IMobileServiceSyncTable<Answer> AnswerTable { get; set; }
        public IMobileServiceSyncTable<Assignment> AssignTable { get; set; }
        public IMobileServiceSyncTable<Designation> DesigTable { get; set; }
        public IMobileServiceSyncTable<Employee> EmpTable { get; set; }
        public IMobileServiceSyncTable<EmployeeCredential> EmpCredentialTable { get; set; }
        public IMobileServiceSyncTable<EmployeeDesignation> EmpDesigTable { get; set; }
        public IMobileServiceSyncTable<Employee_Perfomance> EmpPerformanceTable { get; set; }
        public IMobileServiceSyncTable<Employee_Points> EmpPointsTable { get; set; }
        public IMobileServiceSyncTable<Form> FormTable { get; set; }
        public IMobileServiceSyncTable<Form_Question> FormQuesTable { get; set; }
        public IMobileServiceSyncTable<QuestionDesignation> QuesDesigTable { get; set; }
        public IMobileServiceSyncTable<Reviewable> ReviewableTable { get; set; }
        public IMobileServiceSyncTable<Reviewee> RevieweeTable { get; set; }
        public IMobileServiceSyncTable<Round> RoundTable { get; set; }
        public IMobileServiceSyncTable<TempAssignment> TempAssignmentTable { get; set; }

        public async Task<bool> Initialize()
        {
            //EP = new List<Employee_Perfomance>();
            MobileService = new MobileServiceClient("https://2review.azurewebsites.net");
            const string path = "2Review.db";
            var store = new MobileServiceSQLiteStore(path);


            store.DefineTable<Question>();
            store.DefineTable<Answer>();
            store.DefineTable<Assignment>();
            store.DefineTable<TempAssignment>();
            store.DefineTable<Designation>();
            store.DefineTable<Employee>();
            store.DefineTable<EmployeeCredential>();
            store.DefineTable<EmployeeDesignation>();
            store.DefineTable<Employee_Perfomance>();
            store.DefineTable<Employee_Points>();
            store.DefineTable<Form>();
            store.DefineTable<Form_Question>();
            store.DefineTable<QuestionDesignation>();
            store.DefineTable<Reviewable>();
            store.DefineTable<Reviewee>();
            store.DefineTable<Round>();

            Debug.WriteLine("Pending operations in the sync context queue: {0}", MobileService.SyncContext.PendingOperations);

            MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            QuesTable = MobileService.GetSyncTable<Question>();
            AnswerTable = MobileService.GetSyncTable<Answer>();
            AssignTable = MobileService.GetSyncTable<Assignment>();
            DesigTable = MobileService.GetSyncTable<Designation>();
            EmpTable = MobileService.GetSyncTable<Employee>();
            EmpCredentialTable = MobileService.GetSyncTable<EmployeeCredential>();
            EmpDesigTable = MobileService.GetSyncTable<EmployeeDesignation>();
            EmpPerformanceTable = MobileService.GetSyncTable<Employee_Perfomance>();
            EmpPointsTable = MobileService.GetSyncTable<Employee_Points>();
            FormTable = MobileService.GetSyncTable<Form>();
            FormQuesTable = MobileService.GetSyncTable<Form_Question>();
            QuesDesigTable = MobileService.GetSyncTable<QuestionDesignation>();
            ReviewableTable = MobileService.GetSyncTable<Reviewable>();
            RevieweeTable = MobileService.GetSyncTable<Reviewee>();
            RoundTable = MobileService.GetSyncTable<Round>();
            TempAssignmentTable = MobileService.GetSyncTable<TempAssignment>();
            Sync();


            return true;
        }

        public async Task<List<RevieweeListForRound>> GetEmployeeListForReview()
        {
            var tempemp = await (from emp in EmpTable select emp).ToListAsync();
            var rev = await (from r in RevieweeTable select r).ToListAsync();
            var emptable = (from emp in tempemp where !emp.Id.Equals(from r in rev select r.Employee_Id) select emp).ToList();
            var desigtable = await (from d in DesigTable select d).ToListAsync();
            var empdesigtable = await (from ed in EmpDesigTable select ed).ToListAsync();
            var common = (from ed in empdesigtable
                          join emp in emptable on ed.Employee_Id equals emp.Id
                          join d in desigtable on ed.Designation_Id equals d.Id
                          select new RevieweeListForRound
                          {
                              RevieweeName = emp.Employee_First_Name,
                              Designation = d.DesignationName
                          }).ToList();

            return common;
        }

        public async Task<List<RevieweeListForRound>> GetRevieweeListForReview(string RoundId)
        {
            var emptable = await (from emp in EmpTable select emp).ToListAsync();
            var desigtable = await (from d in DesigTable select d).ToListAsync();
            var rev = await (from r in RevieweeTable where r.Round_Id == RoundId select r).ToListAsync();
            var common = (from r in rev
                          join emp in emptable on r.Employee_Id equals emp.Id
                          join d in desigtable on r.Designation_Id equals d.Id
                          select new RevieweeListForRound
                          {
                              RevieweeName = emp.Employee_First_Name,
                              Designation = d.DesignationName,
                              Total = r.Total
                          }).ToList();

            return common;
        }

        public async Task AddReviewee(string RoundId,string EmpId,string EmpDesigId)
        {
            Reviewee r = new Reviewee();
            r.Round_Id = RoundId;
            r.Employee_Id = EmpId;
            r.Designation_Id = EmpDesigId;

            RevieweeTable.InsertAsync(r);
            await Sync();
        }

        public async Task AddReviewable(string RoundId, string EmpId, string EmpDesigId)
        {
            Reviewable r = new Reviewable();
            r.Round_Id = RoundId;
            r.Employee_Id = EmpId;
            r.Designation_Id = EmpDesigId;

            ReviewableTable.InsertAsync(r);
            await Sync();
        }

        public async Task<string> GetEmployeeDesignation(string EmpId)
        {
            var EmpDesigId = await (from ed in EmpDesigTable where ed.Employee_Id == EmpId select ed.Designation_Id).ToListAsync();

            var EmpDesigName = await (from d in DesigTable where d.Id == EmpDesigId[0] select d.DesignationName).ToListAsync();

            return EmpDesigName[0];
        }

        public async Task<List<Employee>> GetReviewableListForReview(string RoundId)
        {
            var RevieweeList = await (from R in RevieweeTable where R.Round_Id == RoundId select R.Id).ToListAsync();
            List<Employee> ReviewableList = new List<Employee>();
            if (RevieweeList.Count > 0)
            {
                var emptable = await (from emp in EmpTable select emp).ToListAsync();
                var empdesigtable = await (from ed in EmpDesigTable select ed).ToListAsync();
            }
            else
                ReviewableList = await (from rl in EmpTable select rl).ToListAsync();
            return ReviewableList;
           }
        //Reviewable CRUD

        public async Task AddReviewable()
        {
            var review = new Reviewable
            {
                Employee_Id = "7ac79e347b4a4b43aab36178b8dc0c85",
                Designation_Id = "aee32dcd217a4f8794bc33dd02dc0fe7",
                Round_Id = "2018-03-14 09:46:17 Round 1",
                Status = "Pending",
                Total = 10
            };
            await ReviewableTable.InsertAsync(review);
            await Sync();
        }



        //Detail Of Employee

        public async Task<string> AddEmployee(string id, string Fn, string Mn, string Ln, string Gen, string Mno, string Img, string Eid, string Adr, string City, string Pincode, string Country, string State, DateTime Jd)
        {
            var review = new Employee
            {
                EmployeeCredential_Id = id,
                Employee_First_Name = Fn,
                Employee_Middle_Name = Mn,
                Employee_Last_Name = Ln,
                Employee_Gender = Gen,
                Employee_Mobile_No = Mno,
                Employee_Image = Img,
                Employee_Email_Id = Eid,
                Employee_Address = Adr,
                Employee_City = City,
                Employee_Pincode = Pincode,
                Employee_Country = Country,
                Employee_State = State,
                Employee_Joining_Date = Jd
            };

            await EmpTable.InsertAsync(review);
            await Sync();
            return review.Id;
        }

        public async Task<List<Employee>> GetEmployee()
        {
            await Sync();
            var result = await EmpTable.OrderByDescending(E => E.Employee_First_Name).ToListAsync();

            return result;
        }

        //

        // Employee Credential

        public async Task<string> AddEmployeeCredential(string username, string password)
        {
            var review = new EmployeeCredential
            {
                UserName = username,
                Password = password
            };

            await EmpCredentialTable.InsertAsync(review);
            await Sync();
            return review.Id;
        }

        // Check Credential

        public async Task<int> CheckCredential(string un, string pwd)
        {
            List<EmployeeCredential> result = new List<EmployeeCredential>();
            Task t = Task.Run(async () => result = await EmpCredentialTable.Where(Ec => Ec.UserName == un && Ec.Password == pwd).ToListAsync());
            t.Wait();
            return result.Count;
        }

        //Question CRUD

        //public async Task<bool> DeleteQuestion(Question id)
        //{
        //    await QuesTable.DeleteAsync(id);
        //    await Sync();
        //    return true;
        //}

        public async Task<string> AddQuestion(string QuestionText)
        {
            var review = new Question
            {
                Question_Text = QuestionText
            };

            await QuesTable.InsertAsync(review);
            await Sync();
            return review.Id;
        }

        //Designation CRUD

        public async Task<List<Designation>> GetDesignation()
        {
            List<Designation> result = new List<Designation>();
            Task t= Task.Run(async () => result = await DesigTable.OrderByDescending(D => D.Id).ToListAsync());
            t.Wait();
            return result;
        }

        public async Task<bool> DeleteDesignation(Designation id)
        {
            await DesigTable.DeleteAsync(id);
            await Sync();
            return true;
        }

        public async Task<List<data>> Get()
        {
            await Sync();

            var roundTable = await (from rnd in RoundTable
                                    select rnd).ToListAsync();
            var reviewableTable = await (from rnd1 in ReviewableTable
                                         select rnd1).ToListAsync();
            var revieweeTable = await (from rnd2 in RevieweeTable
                                       select rnd2).ToListAsync();

            var common = (from rnd in roundTable
                          join rew in revieweeTable on rnd.Id equals rew.Round_Id
                          join rewb in reviewableTable on rnd.Id equals rewb.Round_Id
                          select new data
                          {
                              rn = rnd.Round_Name,
                              st = rnd.Status,
                              re = rew.Total,
                              reb = rewb.Total,
                              rd = rnd.RoundDate,
                              rt = rnd.RoundTime
                          }).ToList();

            //var common = (from rnd in roundTable
            //              from rew in revieweeTable.Where(rew => rew.Round_Id == rnd.Id).DefaultIfEmpty()
            //              from rewb in reviewableTable.Where(rewb => rewb.Round_Id == rnd.Id).DefaultIfEmpty()
            //              select new data
            //              {
            //                  rn = rnd.Round_Name,
            //                  st = rnd.Status,
            //                  re = rew.Total,
            //                  reb = rewb.Total,
            //                  rd = rnd.RoundDate,
            //                  rt = rnd.RoundTime
            //              }).ToList();

            return common;
        }

        public async Task<List<EmpDesig>> GetEmpDesig()
        {

            var employeeDesignationTable = await (from ed in EmpDesigTable
                                                  select ed).ToListAsync();
            var employeeTable = await (from e in EmpTable
                                       select e).ToListAsync();
            var designationTable = await (from d in DesigTable
                                          select d).ToListAsync();

            var common = (from ed in employeeDesignationTable
                          join e in employeeTable on ed.Employee_Id equals e.Id
                          join d in designationTable on ed.Designation_Id equals d.Id
                          select new EmpDesig
                          {
                              EmpType = e.Employee_First_Name,
                              EmpDesi = d.DesignationName,
                              EmpId = e.Id,
                              EmpDesiId = d.Id
                          }).ToList();
            return common;
        }

        public async Task AddDesignation(string DesignationName)
        {
            var review = new Designation
            {
                DesignationName = DesignationName
            };

            await DesigTable.InsertAsync(review);
            await Sync();
        }

        //EmployeeDesignation CRUD

        public async Task AddEmployeeDesignation(string EmployeeId, string DesignationId)
        {
            var review = new EmployeeDesignation
            {
                Employee_Id = EmployeeId,
                Designation_Id = DesignationId
            };

            await EmpDesigTable.InsertAsync(review);
            await Sync();
        }

        //QuestionDesignation CRUD

        public async Task AddQuestionDesignation(string QuestionId, string DesignationId)
        {
            var review = new QuestionDesignation
            {
                Question_Id = QuestionId,
                Designation_Id = DesignationId
            };

            await QuesDesigTable.InsertAsync(review);
            await Sync();
        }

        //Round CRUD

        public async Task<List<Round>> GetRound()
        {
            var result = await RoundTable.OrderByDescending(R => R.Round_Name).ToListAsync();
            return result;
        }

        public async Task AddRound(string Round_Name, string Round_Date, string Round_Time)
        {
            var review = new Round
            {
                Round_Name = Round_Name,
                Status = "Pending",
                RoundDate = Round_Date,
                RoundTime = Round_Time,
            };
            await RoundTable.InsertAsync(review);
            await Sync();
        }

        //Employee PErformance CRUD

        public async Task<List<string>> GetEmpRoundWisePerformance()
        {
            await Sync();
            var result = await EmpPerformanceTable.Select(EPT => EPT.Id).ToListAsync();
            return result;
        }

        //public async Task<List<QuestionDesignation>> GetQuestionDesignation()
        //{
        //    await Sync();
        //    var result = await QuestionDesignationTable.OrderByDescending(Q => Q.Id).ToListAsync();
        //    return result;
        //}

        public async Task<List<QueDesig>> GetQuestionDesignation()
        {

            var designationTable = await (from d in DesigTable
                                          select d).ToListAsync();
            var questionTable = await (from q in QuesTable
                                       select q).ToListAsync();
            var questionDesignatioTable = await (from qd in QuesDesigTable
                                                 select qd).ToListAsync();

            var common = (from qd in questionDesignatioTable
                          join q in questionTable on qd.Question_Id equals q.Id
                          join d in designationTable on qd.Designation_Id equals d.Id
                          select new QueDesig
                          {
                              Designation_Name = d.DesignationName,
                              Question_Name = q.Question_Text,
                              Q_Id = q.Id,
                              D_Id = d.Id
                          }).ToList();
            return common;
        }

        public async Task<QueDesig> GetSelectedQuestionDesignation(string qid, string did)
        {

            var designationTable = await (from d in DesigTable
                                          select d).ToListAsync();
            var questionTable = await (from q in QuesTable
                                       select q).ToListAsync();
            var questionDesignatioTable = await (from qd in QuesDesigTable
                                                 select qd).ToListAsync();

            var common = (QueDesig)from qd in questionDesignatioTable
                                   join q in questionTable on qid equals q.Id
                                   join d in designationTable on did equals d.Id
                                   select new QueDesig
                                   {
                                       Designation_Name = d.DesignationName,
                                       Question_Name = q.Question_Text,
                                       Q_Id = q.Id,
                                       D_Id = d.Id
                                   };
            return common;
        }

        public async Task Sync()
        {


            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                // The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                // Use a different query name for each unique query in your program.

                await EmpPointsTable.PullAsync("allEmployeePoints", EmpPointsTable.CreateQuery());
                await AnswerTable.PullAsync("allAnswer", AnswerTable.CreateQuery());
                await AssignTable.PullAsync("allAssignment", AssignTable.CreateQuery());
                await DesigTable.PullAsync("allDesignation", DesigTable.CreateQuery());
                await EmpTable.PullAsync("allEmployee", EmpTable.CreateQuery());
                await EmpCredentialTable.PullAsync("allEmployeeCredentials", EmpCredentialTable.CreateQuery());
                await EmpDesigTable.PullAsync("allEmployeeDesignation", EmpDesigTable.CreateQuery());
                await EmpPerformanceTable.PullAsync("allEmployeePerformance", EmpPerformanceTable.CreateQuery());
                await FormTable.PullAsync("allForms", FormTable.CreateQuery());
                await FormQuesTable.PullAsync("allFormQuestion", FormQuesTable.CreateQuery());
                await QuesTable.PullAsync("allQuestion", QuesTable.CreateQuery());
                await QuesDesigTable.PullAsync("allQuestionDesignation", QuesDesigTable.CreateQuery());
                await ReviewableTable.PullAsync("allReviewable", ReviewableTable.CreateQuery());
                await RevieweeTable.PullAsync("allReviewee", RevieweeTable.CreateQuery());
                await RoundTable.PullAsync("allRound", RoundTable.CreateQuery());
                await MobileService.SyncContext.PushAsync();
            }
            catch (MobileServicePushFailedException exc)
            {

                Debug.WriteLine("Error Message : " + exc.Message);

                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                    //Simple error/ conflict handling.
                    if (syncErrors != null)
                    {

                        foreach (var error in syncErrors)
                        {
                            Debug.WriteLine(@"error.Operation Kind: {0}.", error.OperationKind);
                            Debug.WriteLine(@"Mobile Service Table Operation Kind: {0}.", MobileServiceTableOperationKind.Update);
                            Debug.WriteLine(@"error.Result: {0}.", error.Result);
                            Debug.WriteLine(@"error.Status: {0}.", error.Status);

                            Debug.WriteLine(@"HttpStatusCode.PreconditionFailed: {0}.", HttpStatusCode.PreconditionFailed);
                            if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                            {
                                //Update failed, reverting to server's copy.
                                await error.CancelAndUpdateItemAsync(error.Result);
                            }
                            else if (error.Status == HttpStatusCode.PreconditionFailed)
                            {
                                // Discard local change.
                                await error.CancelAndDiscardItemAsync();
                            }
                            Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                        }
                    }
                }
            }
        }
    
    
    }

    public class data
    {
        public string rn { get; set; }
        public string st { get; set; }
        public int re { get; set; }
        public int reb { get; set; }
        public string rd { get; set; }
        public string rt { get; set; }
    }

    public class EmpDesig
    {
        public string EmpType { get; set; }
        public string EmpDesi { get; set; }
        public string EmpId { get; set; }
        public string EmpDesiId { get; set; }
    }

    public class QueDesig
    {
        public string Question_Name { get; set; }
        public string Designation_Name { get; set; }
        public string D_Id { get; set; }
        public string Q_Id { get; set; }
    }
}