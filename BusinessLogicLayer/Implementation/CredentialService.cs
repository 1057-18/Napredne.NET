using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BusinessLogicLayer.Interface;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;

namespace BusinessLogicLayer.Implementation
{
    public class CredentialService : ICredentialService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CredentialService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Credential entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Credential> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Credential> Find(Expression<Func<Credential, bool>> expression)
        {
            return _unitOfWork.Credentials.Find(expression);
        }

        public Credential Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Credential> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Credential> GetAllWithOutTracking()
        {
            throw new NotImplementedException();
        }

        public void Remove(Credential entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Credential> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(Credential entity)
        {
            throw new NotImplementedException();
        }
    }
}
