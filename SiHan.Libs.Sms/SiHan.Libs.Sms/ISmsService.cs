using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SiHan.Libs.Sms
{
    /// <summary>
    /// 短信服务
    /// </summary>
    public interface ISmsService
    {
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="code">验证码（必须是整数）</param>
        /// <returns></returns>
        Task SendCodeAsync(string phone, int code);
    }
}

