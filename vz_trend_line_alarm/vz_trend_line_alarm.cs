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
        protected override void Initialize()
        {
            var stackPanel = new StackPanel 
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                BackgroundColor = Color.Gold,
                Opacity = 0.5
            };


            var button = new Button 
            {
                Text = "my button",
                Margin = 10
            };

            stackPanel.AddChild(button);
            Chart.AddControl(stackPanel);

        }

        public override void Calculate(int index)
        {
            if (IsLastBar)
            {
                DisplaySpreadOnChart();
                // Notifications.PlaySound("C:\\ALARM.WAV");
            }
        }


        private void DisplaySpreadOnChart()
        {

            var color = Colors.Black;
            var txt = "";
            foreach (var t_line in Chart.FindAllObjects<ChartTrendLine>())
            {
                if (t_line.Comment == "ALARM")
                {
                    var pipvalue = 1 / Symbol.PipSize;
                    double Y1 = Math.Round(t_line.Y1, Symbol.Digits);
                    double Y2 = Math.Round(t_line.Y2, Symbol.Digits);

                    var X1 = Bars.OpenTimes.GetIndexByTime(t_line.Time1);
                    var X2 = Bars.OpenTimes.GetIndexByTime(t_line.Time2);
                    var X_live = Bars.OpenTimes.GetIndexByTime(Time);
                    //var X_live = t_line.Time1.GetDateTimeFormats();

                    double xDiff = X2 - X1;
                    var yDiff = Y2 - Y1;
                    //var yDiff = Math.Round(Y2 - Y1, Symbol.Digits);
                    var pip = Math.Round(yDiff * pipvalue, 1);

                    var price = Symbol.Bid;

                    var shib = (yDiff) / (xDiff);
                    //Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
                    var a = X_live * shib;


                    txt = txt + a + " YDIFF:" + yDiff + " *** XLIVE:[" + X_live + "] " + " PRICE:[" + price + "] " + "PIPVALUE:(" + pipvalue + ") Y1:(" + Y1 + ") Y2:(" + Y2 + ") PIP:(" + pip + ") X1:(" + X1 + ")  X2:(" + X2 + ") Bar:(" + xDiff + ") SHIB:(" + shib + ")\n";
                    if (Y1 < Y2)
                    {
                        t_line.Color = Color.Red;
                    }
                    else
                    {
                        t_line.Color = Color.Green;
                    }

                }
            }

            ChartObjects.DrawText("mylabel", txt, StaticPosition.TopLeft, color);

        }
    }
}
