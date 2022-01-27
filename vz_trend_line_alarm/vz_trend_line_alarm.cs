using System;
using cAlgo.API;
using cAlgo.API.Internals;
using cAlgo.API.Indicators;
//vahid.zahani@gmail.com
namespace cAlgo.Indicators
{
    [Indicator(IsOverlay = true, AccessRights = AccessRights.None)]
    public class vz_trend_line_alarm : Indicator
    {
        public override void Calculate(int index)
        {
            if (IsLastBar)
                DisplaySpreadOnChart();
        }

        private void DisplaySpreadOnChart()
        {


            var xx = "";
            foreach (var ii in Chart.FindAllObjects<ChartTrendLine>())
            {
                if (ii.Comment == "ALARM")
                    xx = xx + Math.Round(ii.Y1, Symbol.Digits) + " - " + Math.Round(ii.Y2, Symbol.Digits) + " - " + ii.Time1.Ticks + " - " + ii.Time2.Ticks + "\t" + ii.Comment + "\n";
            }
            ChartObjects.DrawText("mylabel", xx, StaticPosition.TopLeft, Colors.Yellow);
        }
    }
}
