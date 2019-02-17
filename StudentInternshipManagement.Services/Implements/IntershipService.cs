using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using StudentInternshipManagement.Models.Constants;
using StudentInternshipManagement.Models.Entities;
using StudentInternshipManagement.Repositories.Implements;

namespace StudentInternshipManagement.Services.Implements
{
    #region Interface

    public interface IInternshipService : IGenericService<Internship>
    {
        void AssignInternship();
        void CreateGroup();
        IQueryable<Internship> GetByLatestSemester();
        IQueryable<Internship> GetBySemester(int semesterId);
        void ProcessRegistration();
        IQueryable<Internship> GetByStudent(int studentId);
    }

    #endregion

    #region Class

    public class InternshipService : GenericService<Internship>, IInternshipService
    {
        private readonly IEmailService _emailService;
        private readonly ISemesterService _semesterService;

        public InternshipService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public InternshipService(IUnitOfWork unitOfWork, ISemesterService semesterService, IEmailService emailService) :
            base(unitOfWork)
        {
            _semesterService = semesterService;
            _emailService = emailService;
        }

        public IQueryable<Internship> GetByStudent(int studentId)
        {
            return UnitOfWork.Repository<Internship>().TableNoTracking.Where(i => i.StudentId == studentId);
        }

        public IQueryable<Internship> GetBySemester(int semesterId)
        {
            return UnitOfWork.Repository<Internship>().TableNoTracking.Where(i => i.Class.SemesterId == semesterId);
        }

        public IQueryable<Internship> GetByLatestSemester()
        {
            int semesterId = _semesterService.GetLatest().Id;
            return GetBySemester(semesterId);
        }

        public void ProcessRegistration()
        {
            AssignInternship();
            CreateGroup();
            _emailService.SendProcessEmail();
        }

        public void AssignInternship()
        {
            var lateRegisteredInternships = new List<Internship>();
            List<CompanyTrainingMajor> leftMajors =
                UnitOfWork.Repository<CompanyTrainingMajor>().TableNoTracking.ToList();

            foreach (Internship item in GetByLatestSemester().OrderByDescending(i => i.RegistrationDate).ToList())
                if (item.Major.AvailableTraineeCount > 0)
                {
                    item.Status = InternshipStatus.Success;
                    item.Major.AvailableTraineeCount--;
                    UnitOfWork.Repository<Internship>().Update(item);
                }
                else
                {
                    lateRegisteredInternships.Add(item);
                    leftMajors.Remove(item.Major);
                }

            foreach (Internship item in lateRegisteredInternships)
            {
                CompanyTrainingMajor major = leftMajors.FirstOrDefault(m => m.TrainingMajorId == item.TrainingMajorId);
                if (major != null)
                {
                    item.TrainingMajorId = major.TrainingMajorId;
                    item.CompanyId = major.CompanyId;
                    item.Status = InternshipStatus.Success;
                    major.AvailableTraineeCount--;
                    if (major.AvailableTraineeCount == 0) leftMajors.Remove(major);

                    UnitOfWork.Repository<Internship>().Update(item);
                    lateRegisteredInternships.Remove(item);
                }
                else
                {
                    CompanyTrainingMajor randomMajor =
                        leftMajors.FirstOrDefault(m => m.TrainingMajor.SubjectId == item.Class.SubjectId);
                    if (randomMajor != null)
                    {
                        item.TrainingMajorId = randomMajor.TrainingMajorId;
                        item.CompanyId = randomMajor.CompanyId;
                        item.Status = InternshipStatus.Success;
                        randomMajor.AvailableTraineeCount--;
                        if (randomMajor.AvailableTraineeCount == 0) leftMajors.Remove(randomMajor);

                        UnitOfWork.Repository<Internship>().Update(item);
                        lateRegisteredInternships.Remove(item);
                    }
                }
            }

            foreach (Internship item in lateRegisteredInternships)
            {
                item.Status = InternshipStatus.Failed;
                UnitOfWork.Repository<Internship>().Update(item);
            }
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public void CreateGroup()
        {
            List<IGrouping<CompanyTrainingMajor, Internship>> groupByMajors = GetByLatestSemester()
                .Where(i => i.Status == InternshipStatus.Success)
                .GroupBy(i => i.Major).ToList();
            IQueryable<Teacher> teachers = UnitOfWork.Repository<Teacher>().TableNoTracking;
            Dictionary<Teacher, int> teacherAssign =
                teachers.ToDictionary(t => t, t => InternshipConstants.GroupsPerTeacher);
            foreach (IGrouping<CompanyTrainingMajor, Internship> item in groupByMajors)
            {
                List<Internship> members = item.Select(i => i).ToList();
                var groups = new List<List<Internship>>();
                for (var i = 0; i < members.Count; i += InternshipConstants.StudentsPerGroups)
                    groups.Add(members.GetRange(i, Math.Min(InternshipConstants.StudentsPerGroups, members.Count - i)));
                //while (members.Any())
                //{
                //    groups.Add(members.Take(5).ToList());
                //    members = members.Skip(5);
                //}

                var groupId = 1;
                foreach (List<Internship> groupItem in groups)
                {
                    var group = new Group
                    {
                        GroupName =
                            $"{groupItem.FirstOrDefault().Major.Company.CompanyName}-{groupItem.FirstOrDefault().Major.TrainingMajor.TrainingMajorName}-{groupId}",
                        ClassId = groupItem.FirstOrDefault().Class.Id,
                        CompanyId = groupItem.FirstOrDefault().Major.CompanyId,
                        TrainingMajorId = groupItem.FirstOrDefault().Major.TrainingMajorId,
                        Members = groupItem.Select(g => g.Student).ToList(),
                        LeaderId = groupItem.OrderByDescending(g => g.Student.Cpa).FirstOrDefault().Student
                            .Id
                    };
                    Teacher teacher = teacherAssign.FirstOrDefault(t =>
                        t.Key.Department.Id ==
                        groupItem.FirstOrDefault().Class.Subject.Department.Id && t.Value > 0).Key;
                    group.TeacherId = teacher.Id;
                    teacherAssign[teacher]--;
                    UnitOfWork.Repository<Group>().Add(group);
                    groupId++;
                }
            }
        }
    }

    #endregion

}