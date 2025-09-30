using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Consts;
public class RegexPatterns
{
    public const string Password = "(?=(.*[0-9]))(?=.*[\\!@#$%^&*()\\\\[\\]{}\\-_+=~`|:;\"'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{6,}";
    public const string PhoneNumber = "^01[0-2,5]{1}[0-9]{8}$";
    public const string Time = @"^(?:[01]\d|2[0-3]):[0-5]\d$";
}
