using System;
using System.Collections.Generic;
using System.Text;

namespace SiHan.Libs.Sms
{
    /// <summary>
    /// 短信发送异常
    /// </summary>
    public class SmsException : Exception
    {
        /// <summary>
        /// 状态代码
        /// </summary>
        public string ResultCode { get; set; }
        /// <summary>
        /// 状态说明
        /// </summary>
        public string ResultMsg { get; set; }

        /// <summary>
        /// 短信发送异常
        /// </summary>
        /// <param name="resultCode">状态码</param>
        /// <param name="resultMsg">状态说明</param>
        /// <param name="errorMsg">错误提示</param>
        public SmsException(string resultCode, string resultMsg, string errorMsg = "短信发送失败") : base(errorMsg)
        {
            this.ResultCode = resultCode;
            this.ResultMsg = resultMsg;
        }
        /// <summary>
        /// 短信发送异常
        /// </summary>
        /// <param name="errorMsg">错误提示</param>
        public SmsException(string errorMsg = "短信发送失败") : this("", "", errorMsg)
        {
        }
    }
}
