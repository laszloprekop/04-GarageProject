using GarageProject.Application;
using GarageProject.Domain;
using Terminal.Gui.App;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace GarageProject.UI;

public class GarageApp : Window
{
    private readonly GarageHandler _garageHandler;
    private TableView _tableView = null!;
    private Label _statusLabel = null!;

    public GarageApp()
    {
        _garageHandler = new GarageHandler();
        _garageHandler.CreateGarage(10);
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
        throw new NotImplementedException();
    }

    private View BuildStatusBar()
    {
        throw new NotImplementedException();
    }

    private View BuildStatusLine()
    {
        throw new NotImplementedException();
    }

    private View BuildTable()
    {
        throw new NotImplementedException();
    }

    private View BuildTitleBar()
    {
        throw new NotImplementedException();
    }

    private View BuildMenubar()
    {
        throw new NotImplementedException();
    }
}