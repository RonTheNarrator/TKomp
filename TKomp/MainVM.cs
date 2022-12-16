using System;
using System.Windows.Input;

namespace TKomp
{
    public class MainVM
    {
        public MainModel Model { get; set; }

        public MainVM() {
            Model = new MainModel();
        }

        private ICommand _testConnection;

        public ICommand TestConnection {
            get 
            {
                return _testConnection ??= new TestConnectionCommand(Model);
            }
        }

        private ICommand _loadData;

        public ICommand LoadData
        {
            get
            {
                return _loadData ??= new LoadDataCommand(Model);
            }
        }

        private class TestConnectionCommand : ICommand
        {
            public event EventHandler? CanExecuteChanged;

            private MainModel mainModel;

            public TestConnectionCommand(MainModel mainModel)
            {
                this.mainModel = mainModel;
            }

            public bool CanExecute(object? parameter)
            {
                return true;
            }

            public void Execute(object? parameter)
            {
                if (parameter != null)
                {
                    var isConnectionOk = BuisnessLogic.TestDbConnection(this, new DbCommandArgs((string)parameter));
                    if (isConnectionOk)
                    {
                        mainModel.ConnectionState = ConnectionStateEnum.Ok;
                        return;
                    }
                    mainModel.ConnectionState = ConnectionStateEnum.Fail;
                }
            }
        }

        private class LoadDataCommand : ICommand
        {
            public event EventHandler? CanExecuteChanged;

            private MainModel mainModel;

            public LoadDataCommand(MainModel mainModel)
            {
                this.mainModel = mainModel;
            }

            public bool CanExecute(object? parameter)
            {
                return true;
            }

            public void Execute(object? parameter)
            {
                if (parameter == null)
                    return;
                try
                {
                    mainModel.ColumnList = BuisnessLogic.UpdateData(this, new DbCommandArgs((string)parameter));
                }
                catch (Exception)
                {
                    mainModel.ConnectionState = ConnectionStateEnum.Fail;
                }
            }
        }
    }
}
