using JobPortal.Database.Repo;
using JobPortal.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;   
        }
        public async Task<Job> Add(Job entity, int userId)
        {
            try
            {
                var _job = new Job();
                _job.Title = entity.Title;
                _job.Description = entity.Description;
                _job.CreatedBy = userId;
                _job.CreatedAt = DateTime.Now;
                _job.EndAt = DateTime.Now.AddMonths(1);
                _job.IsActive = true;
                return await _jobRepository.AddAsync(_job);
            }
            catch(Exception ex)
            {
                Log.Error("Error occurred : {0}", ex);
            }
            return null;
        }

        public async Task<IEnumerable<Job>> AddJobs(List<Job> entities)
        {
            IEnumerable<Job> job = await _jobRepository.AddAsync(entities);
            return job;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var jobId = await _jobRepository.GetById(id);
                if (jobId != null)
                {
                    _jobRepository.Delete(jobId);
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                Log.Error("Error occurred : {0}", ex);
            }
            return false;
        }

        public async Task<IEnumerable<Job>> GetJobs(PagedParameters pagedParameters)
        {
            return await _jobRepository.Jobs(pagedParameters);
        }

        public async Task<IEnumerable<Job>> GetMyJobs(int userId, PagedParameters pagedParameters)
        {
            return await _jobRepository.GetMyJobs(userId, pagedParameters);
        }

        public async Task<Job> Update(Job entity)
        {
            try
            {
                var _job = await _jobRepository.GetById(entity.Id);
                if (_job != null)
                {
                    _job.Title = entity.Title;
                    _job.Description = entity.Description;
                    _job.CreatedBy = entity.CreatedBy;
                    _job.CreatedAt = _job.CreatedAt;
                    _job.EndAt = entity.EndAt;
                    _job.IsActive = entity.IsActive;
                    _jobRepository.Update(_job);
                    return _job;
                }
                return entity;
            }
            catch(Exception ex)
            {
                Log.Error("Error occurred : {0}", ex);
            }
            return null;
        }
    }
}
