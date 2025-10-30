using System;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using System.Threading;

namespace rc_controller
{
    public partial class MainWindow : Window
    {
        private SerialPort _serialPort;

        public MainWindow()
        {
            InitializeComponent();
            

            // Set focus to window to capture key presses
            this.Focusable = true;
            this.KeyDown += Window_KeyDown;
            this.KeyUp += Window_KeyUp;
        }

        private void InitializeSerialPort(string comport)
        {

            _serialPort = new SerialPort
            {
                PortName = comport,   // Change to your Arduino COM port
                BaudRate = 9600,
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One
            };


            try
            {
                _serialPort.Open();
                MessageBox.Show("Serial port opened successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open serial port: {ex.Message}");
            }
        }

        // CONNECT
        private void CONNECT_BUTTON(object sender, RoutedEventArgs e) => InitializeSerialPort(ComPortComboBox.Text);

        private void SendDataToArduino(string data)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.WriteLine(data);
                Thread.Sleep(100);
                string newText = _serialPort.ReadLine();
                DebugTextBox.Text = newText;
                
              
                // read response from Arduino
            }
            else
            {
                MessageBox.Show("Serial port is not open!");
            }
        }

        private void SendDataToBattery(string data)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.WriteLine(data);
                string newText = _serialPort.ReadLine();
                VoltageTextBox.Text = newText;
              

                // read response from Arduino
            }
            else
            {
                MessageBox.Show("Serial port is not open!");
            }
        }
        private void SendDataToHumidity(string data)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.WriteLine(data);
                string newText = _serialPort.ReadLine();
                HumidityTextBox.Text = newText;
           


                // read response from Arduino
            }
            else
            {
                MessageBox.Show("Serial port is not open!");
            }
        }
        private void SendDataToMoisture(string data)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.WriteLine(data);
                Thread.Sleep(250);
                string newText = _serialPort.ReadLine();
                MoistureTextBox.Text = newText;
      

                // read response from Arduino
            }
            else
            {
                MessageBox.Show("Serial port is not open!");
            }
        }
        private void SendDataToTemperature(string data)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.WriteLine(data);
                Thread.Sleep(250);
                string newtemp = _serialPort.ReadLine();
                TemperatureTextBox.Text = newtemp;
                // read response from Arduino
            }
            else
            {
                MessageBox.Show("Serial port is not open!");
            }
        }


        // Button press/release events
        private void ButtonForward_Pressed(object sender, RoutedEventArgs e) => SendDataToArduino("w");
        private void ButtonForward_Released(object sender, RoutedEventArgs e) => SendDataToArduino("x");

        private void ButtonBackward_Pressed(object sender, RoutedEventArgs e) => SendDataToArduino("s");
        private void ButtonBackward_Released(object sender, RoutedEventArgs e) => SendDataToArduino("x");

        private void ButtonLeft_Pressed(object sender, RoutedEventArgs e) => SendDataToArduino("a");
        private void ButtonLeft_Released(object sender, RoutedEventArgs e) => SendDataToArduino("x");

        private void ButtonRight_Pressed(object sender, RoutedEventArgs e) => SendDataToArduino("d");
        private void ButtonRight_Released(object sender, RoutedEventArgs e) => SendDataToArduino("x");

        private void mMinus(object sender, RoutedEventArgs e) => SendDataToArduino("z");
        private void mPlus(object sender, RoutedEventArgs e) => SendDataToArduino("c");

        // servo mechanics
        // servo 1
        private void s1minus_press(object sender, RoutedEventArgs e) => SendDataToArduino("t");
        private void s1minus_Released(object sender, RoutedEventArgs e) => SendDataToArduino("p");


        private void s1plus_press(object sender, RoutedEventArgs e) => SendDataToArduino("y");
        private void s1plus_Released(object sender, RoutedEventArgs e) => SendDataToArduino("p");
        // servo 2
        private void s2minus_press(object sender, RoutedEventArgs e) => SendDataToArduino("g");
        private void s2minus_Released(object sender, RoutedEventArgs e) => SendDataToArduino("p");


        private void s2plus_press(object sender, RoutedEventArgs e) => SendDataToArduino("h");
        private void s2plus_Released(object sender, RoutedEventArgs e) => SendDataToArduino("p");

        // servo 3
        private void s3minus_press(object sender, RoutedEventArgs e) => SendDataToArduino("u");
        private void s3minus_Released(object sender, RoutedEventArgs e) => SendDataToArduino("p");


        private void s3plus_press(object sender, RoutedEventArgs e) => SendDataToArduino("i");
        private void s3plus_Released(object sender, RoutedEventArgs e) => SendDataToArduino("p");

        // servo 4
        private void s4minus_press(object sender, RoutedEventArgs e) => SendDataToArduino("j");
        private void s4minus_Released(object sender, RoutedEventArgs e) => SendDataToArduino("p");


        private void s4plus_press(object sender, RoutedEventArgs e) => SendDataToArduino("k");
        private void s4plus_Released(object sender, RoutedEventArgs e) => SendDataToArduino("p");






        // BATERRY, TEMPERATURE, MOISTURE AND HUMIDITY READER
        private void VOLTAGE_BUTTON(object sender, RoutedEventArgs e) => SendDataToBattery("v");

        private void HUMIDITY_BUTTON(object sender, RoutedEventArgs e) => SendDataToHumidity("n");
        

        private void MOISTURE_BUTTON(object sender, RoutedEventArgs e) => SendDataToMoisture("b");

        private void TEMPERATURE_BUTTON(object sender, RoutedEventArgs e) => SendDataToTemperature("m");

        


        // Keyboard handling
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W: ButtonForward_Pressed(sender, null); break;
                case Key.S: ButtonBackward_Pressed(sender, null); break;
                case Key.A: ButtonLeft_Pressed(sender, null); break;
                case Key.D: ButtonRight_Pressed(sender, null); break;
                case Key.Z: mMinus(sender, null); break;
                case Key.C: mPlus(sender, null); break;
                case Key.V: VOLTAGE_BUTTON(sender, null); break;
                case Key.B: MOISTURE_BUTTON(sender, null); break;
                case Key.N: HUMIDITY_BUTTON(sender, null); break;
                case Key.M: TEMPERATURE_BUTTON(sender, null); break;
                case Key.T: s1minus_press(sender, null); break;
                case Key.Y: s1plus_press(sender, null); break;
                case Key.G: s2minus_press(sender, null); break;
                case Key.H: s2plus_press(sender, null); break;
                case Key.U: s3minus_press(sender, null); break;
                case Key.I: s3plus_press(sender, null); break;
                case Key.J: s4minus_press(sender, null); break;
                case Key.K: s4plus_press(sender, null); break;

            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W: ButtonForward_Released(sender, null); break;
                case Key.S: ButtonBackward_Released(sender, null); break;
                case Key.A: ButtonLeft_Released(sender, null); break;
                case Key.D: ButtonRight_Released(sender, null); break;
                case Key.T: s1minus_Released(sender, null); break;
                case Key.Y: s1plus_Released(sender, null); break;
                case Key.G: s2minus_Released(sender, null); break;
                case Key.H: s2plus_Released(sender, null); break;
                case Key.U: s3minus_Released(sender, null); break;
                case Key.I: s3plus_Released(sender, null); break;
                case Key.J: s4minus_Released(sender, null); break;
                case Key.K: s4plus_Released(sender, null); break;
            }
        }

        private void VoltageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
