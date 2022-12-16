using System.Collections.Generic;
using System.ComponentModel;

namespace TKomp
{
    public class MainModel : INotifyPropertyChanged
    {
        private const string ConnectionOkString = "Połączenie Ok";
        private const string ConnectionFailString = "Połączenie nieudane";
        private string _connectionString="";

        public string ConnectionString
        {
            get => _connectionString;
            set { 
                _connectionString = value;
                Changed(nameof(ConnectionString));
            }
        }

        private List<string> _columnList = new List<string>();

        public List<string> ColumnList
        {
            get => _columnList;
            set
            {
                _columnList = value;
                Changed(nameof(ColumnList));
            }
        }

        private ConnectionStateEnum _connectionState = ConnectionStateEnum.Untested;

        public ConnectionStateEnum ConnectionState { 
            get => _connectionState;
            set {
                _connectionState = value;
                Changed(nameof(ConnectionState));
                Changed(nameof(StatusText));
            }
        }

        public string StatusText
        {
            get => GetStatusText(ConnectionState);
        }

        private string GetStatusText(ConnectionStateEnum state) {
            switch (state)
            {
                case ConnectionStateEnum.Ok:
                    return ConnectionOkString;
                case ConnectionStateEnum.Fail:
                    return ConnectionFailString;
                default:
                    return string.Empty;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public enum ConnectionStateEnum
    {
        Untested,
        Ok,
        Fail
    }
}
