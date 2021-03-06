﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECP.Util.Jwt.LTS.Extend
{
    /// <summary>
    /// Jwt 验证
    /// </summary>
    public class BearerAuthorizeAttribute : AuthorizeAttribute
    {
        public BearerAuthorizeAttribute() : base("Bearer") { }
    }
}
