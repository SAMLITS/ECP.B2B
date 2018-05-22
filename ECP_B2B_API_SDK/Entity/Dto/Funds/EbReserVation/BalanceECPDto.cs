using System;
using System.Collections.Generic;
using System.Text;

namespace ECP_B2B_API_SDK.Entity.Dto.Funds.EbReserVation
{

    public class BalanceECPDto
    {
        /// <summary>
        /// 当前可用余额
        /// </summary>
        public double? ENABLEDAMOUNT { get; set; }
        /// <summary>
        /// 当前不可用余额
        /// </summary>
        public double? DISABLEDAMOUNT { get; set; }
    }
}
