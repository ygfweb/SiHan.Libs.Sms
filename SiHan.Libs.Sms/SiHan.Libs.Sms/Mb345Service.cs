using SiHan.Libs.Sms.ServiceReference;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SiHan.Libs.Sms
{
    /// <summary>
    /// 凌凯短信服务
    /// </summary>
    public class Mb345Service : ISmsService
    {
        /// <summary>
        /// 凌凯账户
        /// </summary>
        public string UserName { get; }
        /// <summary>
        /// 凌凯用户名
        /// </summary>
        public string Password { get; }
        /// <summary>
        /// 短信签名
        /// </summary>
        public string SuffixName { get; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public int OperateTime { get; }


        /// <summary>
        /// 凌凯短信服务
        /// </summary>
        /// <param name="userName">凌凯账号</param>
        /// <param name="password">凌凯密码</param>
        /// <param name="suffixName">企业签名</param>
        /// <param name="operateTime">验证码过期时间，必须大于0</param>
        public Mb345Service(string userName, string password, string suffixName = "思翰", int operateTime = 10)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            SuffixName = suffixName ?? throw new ArgumentNullException(nameof(suffixName));
            if (operateTime <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(operateTime), "验证码过期时间必须大于0");
            }
            OperateTime = operateTime;
        }

        /// <summary>
        /// 获取短信内容
        /// </summary>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        protected string GetSmsMessage(int code)
        {
            return $"您的验证码是：{code.ToString()}，{this.OperateTime}分钟内有效！若非本人操作，请忽略。【{this.SuffixName}】";
        }

        /// <summary>
        /// 获取错误提示
        /// </summary>
        /// <param name="resultCode">响应代码</param>
        /// <returns></returns>
        protected string GetResultMsg(int resultCode)
        {
            if (resultCode > 0)
            {
                return "发送成功";
            }
            else
            {
                string msg = "未知错误";
                switch (resultCode)
                {
                    case -1:
                        msg = "账号未注册";
                        break;
                    case -2:
                        msg = "其他错误";
                        break;
                    case -3:
                        msg = "帐号或密码错误";
                        break;
                    case -5:
                        msg = "余额不足，请充值";
                        break;
                    case -6:
                        msg = "定时发送时间不是有效的时间格式";
                        break;
                    case -7:
                        msg = "提交信息末尾未加签名，请添加中文的企业签名【 】或内容乱码";
                        break;
                    case -8:
                        msg = "发送内容需在1到300字之间";
                        break;
                    case -9:
                        msg = "发送号码为空";
                        break;
                    case -10:
                        msg = "定时时间不能小于系统当前时间";
                        break;
                    case -100:
                        msg = "IP黑名单";
                        break;
                    case -102:
                        msg = "账号黑名单";
                        break;
                    case -103:
                        msg = "IP未导白";
                        break;
                    default:
                        break;
                }
                return msg;
            }
        }
        /// <summary>
        /// 发送验证码，发送失败将抛出异常，通过捕捉SmsException异常，可以获得发送失败原因
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="code">验证码</param>
        public async Task SendCodeAsync(string phone, int code)
        {
            if (!PhoneValidator.IsMatchPhone(phone))
            {
                throw new ArgumentOutOfRangeException(nameof(phone), "手机号格式不正确");
            }
            if (!PhoneValidator.IsMatchCode(code))
            {
                throw new ArgumentOutOfRangeException(nameof(code));
            }
            string msgText = this.GetSmsMessage(code);
            LinkWSSoapClient client = new LinkWSSoapClient(LinkWSSoapClient.EndpointConfiguration.LinkWSSoap);
            var result = await client.BatchSendAsync(new BatchSendRequest()
            {
                CorpID = this.UserName.Trim(),
                Pwd = this.Password.Trim(),
                Mobile = phone,
                Content = msgText,
                SendTime = "",
                Cell = ""
            });
            int resultCode = result.BatchSendResult;
            if (resultCode < 0)
            {
                throw new SmsException(resultCode.ToString(), this.GetResultMsg(resultCode));
            }
        }
    }
}
