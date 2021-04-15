using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WEA.Web.Helpers.Identity
{
    public static class IdentityExtensions
    {
        public static bool IsSuperAdmin(this IIdentity identity)
        {
            try
            {
                var str = identity.GetDetail<string>(CustomClaimTypes.IsSuperAdmin);
                if (string.IsNullOrEmpty(str)) return false;
                return bool.Parse(str);
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static DateTime? PaymentExpiredDate(this IIdentity identity)
        {
            try
            {
                var data = identity.GetDetail<DateTime>(CustomClaimTypes.PaymentExDate);
                if (data == default(DateTime))
                {
                    return null;
                }
                return data;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool IsOwner(this IIdentity identity)
        {
            try
            {
                var str = identity.GetDetail<string>(CustomClaimTypes.IsOwner);
                if (string.IsNullOrEmpty(str)) return false;
                return bool.Parse(str);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private static T GetDetail<T>(this IIdentity identity, string claimType) where T : IConvertible
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            if (identity is ClaimsIdentity ci)
            {
                var id = ci.Claims.FirstOrDefault(x => x.Type == claimType);
                if (id != null)
                {
                    return (T)Convert.ChangeType(id.Value, typeof(T), CultureInfo.InvariantCulture);
                }
            }

            return default(T);
        }
    }
}
