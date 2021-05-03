using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEA.Presentation.Helpers.Identity.Authorization.Requirements
{
    public class ConstantOperationRequirements
    {
        public static OperationAuthorizationRequirement Create = new OperationAuthorizationRequirement { Name = ConstantOperations.CreateOperation };
        public static OperationAuthorizationRequirement Update = new OperationAuthorizationRequirement { Name = ConstantOperations.UpdateOperation };
        public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement { Name = ConstantOperations.DeleteOperation };
        public static OperationAuthorizationRequirement Read = new OperationAuthorizationRequirement { Name = ConstantOperations.ReadOperation };
    }
}
