using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Mvc.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEA.Web.Helpers.Extensions
{
    public static class DevExtremeExtensions
    {
        public static DataGridBuilder<T> WEAGrid<T>(this DataGridBuilder<T> builder)
        {
            return builder
                    .FilterRow(filterRow => filterRow
                        .Visible(false)
                        .ResetOperationText("Sıfırla")
                        .ApplyFilterText("Tətbiq et")
                        .BetweenEndText("Bitmə")
                        .BetweenStartText("Başlama")
                        .ShowAllText("Hamısına bax")
                        .ApplyFilter(GridApplyFilterMode.Auto)
                        .OperationDescriptions(o => o
                            .Between("Arasında olsun")
                            .Contains("İbarət olsun")
                            .EndsWith("Sonlansın")
                            .StartsWith("Başlasın")
                            .Equal("Bərabər olsun")
                            .LessThan("Kiçik olsun")
                            .LessThanOrEqual("Kiçik yaxud bərabər olsun")
                            .GreaterThan("Böyük olsun")
                            .GreaterThanOrEqual("Böyük yaxud bərabər olsun")
                            .NotContains("İçində olmasın")
                            .NotEqual("Fərqli olsun")
                        )
                    )
                            .NoDataText("Məlumat yoxdur").LoadPanel(c => c.Text("Yüklənir"));
        }
    }
}
