using System;
using System.Collections.Generic;
using System.Text;

namespace WEA.SharedKernel
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    {
    }
}
