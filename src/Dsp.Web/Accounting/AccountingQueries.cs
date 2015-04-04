using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dsp.Web.Accounting
{
    public static class AccountingQueries
    {
        public static IQueryable<Activity> ToActivities(this IQueryable<AccountActivity> query)
        {
            return query.Select(a => new Activity
            {
                AccountName = a.Account.UserName,
                Description = a.Description,
                IPAddress = a.IPAddress,
                OccurrenceUtc = a.OccurrenceUtc
            });
        }
    }
}