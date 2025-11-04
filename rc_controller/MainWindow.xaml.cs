using System.IO.Ports;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace rc_controller
{
    public partial class MainWindow : Window
    {
        private SerialPort _serialPort = new();


        public MainWindow()
        {
            InitializeComponent();
            // focus on window to allow keypress
            this.Focusable = true;
            this.KeyDown += Window_KeyDown;
            this.KeyUp += Window_KeyUp;
        }


        /* ------------- SERIAL CONNECTION ------------- */
        private void InitializeSerialPort(string comport)
        {
            if (comport != "--Select COM port--")
            {
                _serialPort = new SerialPort
                {
                    PortName = comport,
                    BaudRate = 9600,
                    Parity = Parity.None,
                    DataBits = 8,
                    StopBits = StopBits.One
                };
                // test if connection can be made
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
            else
            {
                MessageBox.Show("Serial port not yet selected!");
            }
        }


        /* ------------- SERIAL READ/WRITE COMMANDS ------------- */
        private void SendCommand(string data, TextBox tbox, String addtext = "")
        {
            if (!(_serialPort == null))
            {
                try
                {
                    _serialPort.WriteLine(data);
                    Thread.Sleep(100);  // delay for arduino response
                    string newText = _serialPort.ReadLine().Trim();
                    tbox.Text = newText + addtext;

                    // sensor texbox color change
                    switch (data)
                    {
                        case "v": VoltageColor(newText); break;
                        case "b": SoilColor(newText); break;
                        case "n": HumColor(newText); break;
                        case "m": TempColor(newText); break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to send command to serial port: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show($"Serial port not yet connected!");
            }
        }

        /* ------------- TEXTBOX COLOR CHANGER ------------- */
        private void VoltageColor(String v)
        {
            if (float.Parse(v) < 7.2)
            {
                VoltageTextBox.Foreground = Brushes.Red;
            }
            else if (7.2 <= float.Parse(v) && float.Parse(v) < 7.6)
            {
                VoltageTextBox.Foreground = Brushes.Yellow;
            }
            else if (7.6<=float.Parse(v))
            {
                VoltageTextBox.Foreground = Brushes.LimeGreen;
            }
        }
        private void SoilColor(String s)
        {
            if (float.Parse(s) < 30)
            {
                MoistureTextBox.Foreground = Brushes.Red;
            }
            else if (30<=float.Parse(s))
            {
                MoistureTextBox.Foreground = Brushes.RoyalBlue;
            }
        }

        private void HumColor(String h)
        {
            if (float.Parse(h) < 30)
            {
                HumidityTextBox.Foreground = Brushes.Red;
            }
            else if (30 <= float.Parse(h))
            {
                HumidityTextBox.Foreground = Brushes.RoyalBlue;
            }
        }

        private void TempColor(String t)
        {
            if (float.Parse(t) < 30)
            {
                TemperatureTextBox.Foreground = Brushes.RoyalBlue;
            }
            else if (30 <= float.Parse(t) && float.Parse(t) < 35)
            {
                TemperatureTextBox.Foreground = Brushes.Orange;
            }
            else if (35 <= float.Parse(t))
            {
                TemperatureTextBox.Foreground = Brushes.Red;
            }
        }


        /* ------------- BUTTON PRESS/RELEASE EVENTS ------------- */
        // Establish Serial Connection
        private void CONNECT_BUTTON(object sender, RoutedEventArgs e) => InitializeSerialPort(ComPortComboBox.Text);

        // Directional Movements
        private void ButtonForward_Pressed(object sender, RoutedEventArgs e) => SendCommand("w", DebugTextBox);
        private void ButtonForward_Released(object sender, RoutedEventArgs e) => SendCommand("x", DebugTextBox);
        private void ButtonBackward_Pressed(object sender, RoutedEventArgs e) => SendCommand("s", DebugTextBox);
        private void ButtonBackward_Released(object sender, RoutedEventArgs e) => SendCommand("x", DebugTextBox);
        private void ButtonLeft_Pressed(object sender, RoutedEventArgs e) => SendCommand("a", DebugTextBox);
        private void ButtonLeft_Released(object sender, RoutedEventArgs e) => SendCommand("x", DebugTextBox);
        private void ButtonRight_Pressed(object sender, RoutedEventArgs e) => SendCommand("d", DebugTextBox);
        private void ButtonRight_Released(object sender, RoutedEventArgs e) => SendCommand("x", DebugTextBox);

        // Motor Speed
        private void MotorMinus(object sender, RoutedEventArgs e) => SendCommand("z", DebugTextBox);
        private void MotorPlus(object sender, RoutedEventArgs e) => SendCommand("c", DebugTextBox);

        // Servo 1 Control
        private void S1minus_press(object sender, RoutedEventArgs e) => SendCommand("t", DebugTextBox);
        private void S1minus_Released(object sender, RoutedEventArgs e) => SendCommand("p", DebugTextBox);
        private void S1plus_press(object sender, RoutedEventArgs e) => SendCommand("g", DebugTextBox);
        private void S1plus_Released(object sender, RoutedEventArgs e) => SendCommand("p", DebugTextBox);

        // Servo 2 Control
        private void S2minus_press(object sender, RoutedEventArgs e) => SendCommand("y", DebugTextBox);
        private void S2minus_Released(object sender, RoutedEventArgs e) => SendCommand("p", DebugTextBox);
        private void S2plus_press(object sender, RoutedEventArgs e) => SendCommand("h", DebugTextBox);
        private void S2plus_Released(object sender, RoutedEventArgs e) => SendCommand("p", DebugTextBox);

        // Servo 3 Control
        private void S3minus_press(object sender, RoutedEventArgs e) => SendCommand("u" , DebugTextBox);
        private void S3minus_Released(object sender, RoutedEventArgs e) => SendCommand("p", DebugTextBox);
        private void S3plus_press(object sender, RoutedEventArgs e) => SendCommand("i", DebugTextBox);
        private void S3plus_Released(object sender, RoutedEventArgs e) => SendCommand("p", DebugTextBox);

        // Sensor Readings
        private void VOLTAGE_BUTTON(object sender, RoutedEventArgs e) => SendCommand("v", VoltageTextBox, " V");
        private void HUMIDITY_BUTTON(object sender, RoutedEventArgs e) => SendCommand("n", HumidityTextBox, "%");
        private void MOISTURE_BUTTON(object sender, RoutedEventArgs e) => SendCommand("b", MoistureTextBox, "%");
        private void TEMPERATURE_BUTTON(object sender, RoutedEventArgs e) => SendCommand("m", TemperatureTextBox, " C");


        /* ------------- KEYBOARD HANDLING ------------- */
        // Button Press
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W: ButtonForward_Pressed(sender, new RoutedEventArgs()); break;
                case Key.S: ButtonBackward_Pressed(sender, new RoutedEventArgs()); break;
                case Key.A: ButtonLeft_Pressed(sender, new RoutedEventArgs()); break;
                case Key.D: ButtonRight_Pressed(sender, new RoutedEventArgs()); break;
                case Key.Z: MotorMinus(sender, new RoutedEventArgs()); break;
                case Key.C: MotorPlus(sender, new RoutedEventArgs()); break;
                case Key.V: VOLTAGE_BUTTON(sender, new RoutedEventArgs()); break;
                case Key.B: MOISTURE_BUTTON(sender, new RoutedEventArgs()); break;
                case Key.N: HUMIDITY_BUTTON(sender, new RoutedEventArgs()); break;
                case Key.M: TEMPERATURE_BUTTON(sender, new RoutedEventArgs()); break;
                case Key.T: S1minus_press(sender, new RoutedEventArgs()); break;
                case Key.G: S1plus_press(sender, new RoutedEventArgs()); break;
                case Key.Y: S2minus_press(sender, new RoutedEventArgs()); break;
                case Key.H: S2plus_press(sender, new RoutedEventArgs()); break;
                case Key.U: S3minus_press(sender, new RoutedEventArgs()); break;
                case Key.I: S3plus_press(sender, new RoutedEventArgs()); break;
            }
        }

        // Button Release
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W: ButtonForward_Released(sender, new RoutedEventArgs()); break;
                case Key.S: ButtonBackward_Released(sender, new RoutedEventArgs()); break;
                case Key.A: ButtonLeft_Released(sender, new RoutedEventArgs()); break;
                case Key.D: ButtonRight_Released(sender, new RoutedEventArgs()); break;
                case Key.T: S1minus_Released(sender, new RoutedEventArgs()); break;
                case Key.G: S1plus_Released(sender, new RoutedEventArgs()); break;
                case Key.Y: S2minus_Released(sender, new RoutedEventArgs()); break;
                case Key.H: S2plus_Released(sender, new RoutedEventArgs()); break;
                case Key.U: S3minus_Released(sender, new RoutedEventArgs()); break;
                case Key.I: S3plus_Released(sender, new RoutedEventArgs()); break;
            }
        }

        /* ------------- END OF CODE ------------- */
    }
}
