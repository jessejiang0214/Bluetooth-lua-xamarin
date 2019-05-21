using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace BluetoothLua
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            ScriptText = _initLuaScript;
            _formattedString = new FormattedString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        string _initLuaScript = @"
    function trackerPressedButton (macAddress, trackerName)
        wrapper:ConnectTracker(macAddress)
    end

    function trackerConnected (macAddress, trackerName)
        wrapper:AuthenticateTracker(macAddress)
    end

    function trackerDidAuthenticate (macAddress, authenticate)
        print (tostring(authenticate))
        wrapper:DisconnectFromTracker(macAddress)
    end    

    function main()
        wrapper:StartScan()
    end
        ";

        string _scriptText;
        public string ScriptText
        {
            get => _scriptText;
            set
            {
                _scriptText = value;
                OnPropertyChanged(nameof(ScriptText));
            }
        }

        FormattedString _formattedString;
        public FormattedString OutputFormattedText => _formattedString;


        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
