using System;
using System.Net;
using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.Implementation
{
    public class CredentialRepository : Repository<Credential>, ICredentialRepository
    {
        public CredentialRepository(DbContext context) : base(context)
        {
        }

        public EmploymentContext context { get { return Context as EmploymentContext; } }
    }
}
