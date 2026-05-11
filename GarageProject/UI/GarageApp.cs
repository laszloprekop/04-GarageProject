using System.Data;
using GarageProject.Application;
using GarageProject.Domain;
using Terminal.Gui.Input;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;
using TGui = Terminal.Gui.App.Application;

namespace GarageProject.UI;

public class GarageApp : Window
{
    private readonly GarageHandler _garageHandler;
    private TableView _tableView = null!;
    private Label _statusLabel = null!;

    public GarageApp()
    {
        _garageHandler = new GarageHandler();
        _garageHandler.CreateGarage(15);
        _garageHandler.Populate();

        Add(BuildMenubar());
        Add(BuildTitleBar());
        Add(BuildTable());
        Add(BuildStatusLine());
        Add(BuildStatusBar());

        RefreshTable();
    }

    private void RefreshTable()
    {
        var table = new DataTable();
        table.Columns.Add("Reg. No");
        table.Columns.Add("Type");
        table.Columns.Add("Color");
        table.Columns.Add("Wheels");

        foreach (var v in _garageHandler.GetAll())
            table.Rows.Add(v.RegistrationNumber, v.GetType().Name, v.Color, v.NumberOfWheels);
        _tableView.Table = new DataTableSource(table);
        _statusLabel.Text = StatusText();
    }

    private string StatusText() =>
        _garageHandler.GetByType()
            .Select(g => $"{g.Key}: {g.Count()}")
            .DefaultIfEmpty("Empty garage")
            .Aggregate((a, b) => $"{a} | {b}");

    private StatusBar BuildStatusBar() => new([
            new Shortcut(Key.F3,  "Park",   ShowAddDialog),
            new Shortcut(Key.F4,  "Unpark", RemoveSelected),
            new Shortcut(Key.F5,  "Search", ShowSearchDialog),
            new Shortcut(Key.F10, "Quit",   RequestStop),
        ]
    );

    protected override bool OnKeyDown(Key key)
    {
        if (key == Key.N.WithCtrl) { ShowAddDialog();   return true; }
        if (key == Key.D.WithCtrl) { RemoveSelected();  return true; }
        if (key == Key.Q.WithCtrl) { RequestStop();     return true; }
        return base.OnKeyDown(key);
    }


    private View BuildStatusLine()
    {
        _statusLabel = new Label { X = 0, Y = Pos.AnchorEnd(2), Width = Dim.Fill() };
        return _statusLabel;
    }

    private TableView BuildTable()
    {
        _tableView = new TableView
        {
            X = 0, Y = 2,
            Width = Dim.Fill(),
            Height = Dim.Fill(2),
            FullRowSelect = true,
        };
        _tableView.Style.ShowHorizontalHeaderUnderline = true;
        _tableView.Style.ShowVerticalCellLines = true;
        return _tableView;
    }

    private Label BuildTitleBar() => new()
    {
        X = 0, Y = 1,
        Width = Dim.Fill(),
        Text = TitleText(),
        SchemeName = "Accent",
    };

    private string TitleText() =>
        $"GARAGE Project v1.0 - {_garageHandler.GetAll().Count()}/{_garageHandler.Capacity} spaces used";

    private MenuBar BuildMenubar() => new()
    {
        Menus =
        [
            new MenuBarItem("_Garage",
            [
                new MenuItem("_Add vehicle", "", ShowAddDialog),
                new MenuItem("_Remove selected", "", ShowAddDialog),
                null!,
                new MenuItem("_Quit", "", RequestStop),
            ]),
            new MenuBarItem("_Search",
            [
                new MenuItem("By _registration", "", ShowSearchDialog),
                new MenuItem("By _properties", "", ShowFilterDialog),
            ]),
        ]
    };

    private void ShowAddDialog()
    {
        var dialog = new AddVehicleDialog(_garageHandler, RefreshTable);
        App!.Run(dialog, ex => false);
    }

    private void ShowSearchDialog()
    {
        App!.Run(new SearchDialog(_garageHandler), ex => false);
    }

    private void ShowFilterDialog()
    {
        throw new NotImplementedException();
    }

    private void RemoveSelected()
    {
        if (_tableView.Table is null || _tableView.Table.Rows == 0) return;
        if (_tableView.Value != null)
        {
            var row = _tableView.Value.Cursor.Y;
            var reg = _tableView.Table[row, 0].ToString()!;
            var removed = _garageHandler.RemoveVehicle(reg);
            if (removed is not null) RefreshTable();
        }
    }
}
