using Domain.Contracts.SpecificationPattern;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.CustomerSpec
{
    public class CustomerByApplicationUserIdSpecification : BaseSpecifications<Customer, int>
    {
        public CustomerByApplicationUserIdSpecification(string applicationUserId)
            : base(c => c.ApplicationUserId == applicationUserId)
        {
        }
    }
}