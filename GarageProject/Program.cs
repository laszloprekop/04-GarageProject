using Terminal.Gui.App;
using Terminal.Gui.Drawing;
using GarageProject.UI;

using var app = Application.Create().Init();
app.Run(new GarageApp { BorderStyle = LineStyle.Rounded });
