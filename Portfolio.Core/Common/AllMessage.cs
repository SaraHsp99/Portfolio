using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Common;
public class AllMessage
{
    #region Message Client
    public const string DeleteSuccess = "حذف انجام شد";
    public const string UserNameIsNotClinic = "نام کاربری یا کلمه عبور برای این مرکز نمی باشد ";
    public const string IsLock = "تعداد دفعات وارد كردن رمز بيش از حد مجاز است";
    public const string NotActive = "کاربر غیرفعال می باشد";
}
    #endregion
public class ClaimName
{
    public const string Email = "Email";
    public const string UserId = "PrnId";
    public const string SuperAdmin = "SuperAdmin";
    public const string Language = "Language";
}

