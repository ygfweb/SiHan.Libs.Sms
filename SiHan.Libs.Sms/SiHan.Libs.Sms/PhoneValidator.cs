using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SiHan.Libs.Sms
{
    /// <summary>
    /// 手机号验证器
    /// </summary>
    public static class PhoneValidator
    {
        /// <summary>
        /// 手机号正则表达式
        /// </summary>
        private static readonly Regex PhoneRegex = new Regex(@"^1\d{10}$", RegexOptions.Compiled);

        /// <summary>
        /// 检查手机号格式是否正确，正确返回true，否则返回false
        /// </summary>
        public static bool IsMatchPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return false;
            }
            else
            {
                return PhoneRegex.IsMatch(phone);
            }
        }
        /// <summary>
        /// 检查验证码格式是否正确（正确的验证码必须大于100，即至少3位数）
        /// </summary>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        public static bool IsMatchCode(int code)
        {
            return code > 100;
        }
    }
}
