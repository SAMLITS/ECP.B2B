using Microsoft.AspNetCore.Authorization; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECP.Util.Jwt.LTS.Extend
{
    public class ValidJtiRequirement : IAuthorizationRequirement
    {
    }

    public class ValidJtiHandler : AuthorizationHandler<ValidJtiRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidJtiRequirement requirement)
        {  

            // 检查 Jti 是否存在
            var jti = context.User.FindFirst("jti")?.Value;
            if (jti == null)
            {
                context.Fail(); // 显式的声明验证失败
                return Task.CompletedTask;
            }

            // 检查 jti 是否在黑名单
            //var tokenExists = _dbContext.BlackRecords.Any(r => r.Jti == jti);
            //if (tokenExists)
            //{
            //    context.Fail();
            //}
            //else
            //{
            context.Succeed(requirement); // 显式的声明验证成功
            //}
            return Task.CompletedTask;
        }
    }
}
