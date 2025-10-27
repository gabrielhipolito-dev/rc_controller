using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.IO.Ports;
using System.Windows.Input;
using System.Reflection;

namespace rc_controller
{
    public partial class MainWindow : Window
    {
        SerialPort _serialPort = new SerialPort();
        UIElement elemUI;

        char data;
        bool click = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeSerialPort();
        }

        private void InitializeSerialPort()
        {
            _serialPort = new SerialPort
            {
                PortName = "COM18",   // Set your COM port
                BaudRate = 9600,     // Set your baud rate
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                //ReadTimeout = 200,
                //WriteTimeout = 50
            };

            _serialPort.DataReceived += new SerialDataReceivedEventHandler(Recieve);  // Add event handler for incoming data
            //_serialPort.Open();
        }
        private void Recieve(object sender, SerialDataReceivedEventArgs e)
        {
            // Collecting the characters received to our 'buffer' (string).
            Debug.WriteLine(_serialPort.ReadLine());
        }

        private void Button_Pressed(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Name == "forward_click")
            {
                data = 'a';

                _serialPort.Write(data.ToString());
            }

            else if (btn.Name == "backward_click")
            {
                data = 'c';

                _serialPort.Write(data.ToString());
            }

            else if (btn.Name == "steer_r")
            {
                data = 'i';

                _serialPort.Write(data.ToString());
            }

            else if (btn.Name == "steer_l")
            {
                data = 'k';

                _serialPort.Write(data.ToString());
            }

            else if (btn.Name == "forward_r")
            {
                data = 'e';

                _serialPort.Write(data.ToString());
            }

            else if (btn.Name == "forward_l")
            {
                data = 'g';

                _serialPort.Write(data.ToString());
            }

            else if (btn.Name == "backward_r")
            {
                data = 'g';

                _serialPort.Write(data.ToString());
            }

            else if (btn.Name == "backward_l")
            {
                data = 'e';

                _serialPort.Write(data.ToString());
            }

            labelControl.Content = btn.Content;
        }

        private void Button_Unpressed(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.Name == "forward_click")
            {
                data = 'b';

                _serialPort.Write(data.ToString());
            }

            else if (btn.Name == "backward_click")
            {
                data = 'd';

                _serialPort.Write(data.ToString());
            }

            else if (btn.Name == "steer_r")
            {
                data = 'j';

                _serialPort.Write(data.ToString());
            }

            else if (btn.Name == "steer_l")
            {
                data = 'l';

                _serialPort.Write(data.ToString());
            }

            else if (btn.Name == "forward_r")
            {
                data = 'f';

                _serialPort.Write(data.ToString());
            }

            else if (btn.Name == "forward_l")
            {
                data = 'h';

                _serialPort.Write(data.ToString());
            }

            else if (btn.Name == "backward_r")
            {
                data = 'h';

                _serialPort.Write(data.ToString());
            }

            else if (btn.Name == "backward_l")
            {
                data = 'f';

                _serialPort.Write(data.ToString());
            }

            labelControl.Content = "RC CONTROLLER";
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggle = (ToggleButton)sender;

            if (toggle.Name == "forward_toggle")
            {
                backward_toggle.IsChecked = false;
                labelControl.Content = "Forward";

                data = 'a';

                _serialPort.Write(data.ToString());
            }

            else if (toggle.Name == "backward_toggle")
            {
                forward_toggle.IsChecked = false;
                labelControl.Content = "Backward";

                data = 'c';

                _serialPort.Write(data.ToString());
            }
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggle = (ToggleButton)sender;

            if (toggle.Name == "forward_toggle")
            {
                backward_toggle.IsChecked = false;

                labelControl.Content = "Forward";

                data = 'b';

                _serialPort.Write(data.ToString());
            }

            else if (toggle.Name == "backward_toggle")
            {
                forward_toggle.IsChecked = false;

                labelControl.Content = "Backward";

                data = 'd';

                _serialPort.Write(data.ToString());
            }

            labelControl.Content = "RC CONTROLLER";
        }

        private void Mode_Checked(object sender, RoutedEventArgs e)
        {
            click = true;

            forward_toggle.IsChecked = false;
            forward_toggle.Visibility = Visibility.Hidden;
            forward_toggle.IsEnabled = false;

            backward_toggle.IsChecked = false;
            backward_toggle.Visibility = Visibility.Hidden;
            backward_toggle.IsEnabled = false;

            forward_click.Visibility = Visibility.Visible;
            forward_click.IsEnabled = true;

            backward_click.Visibility = Visibility.Visible;
            backward_click.IsEnabled = true;
            labelControl.Content = "Normal Mode";
        }

        private void Mode_Unchecked(object sender, RoutedEventArgs e)
        {
            click = false;

            forward_toggle.IsChecked = false;
            forward_toggle.Visibility = Visibility.Visible;
            forward_toggle.IsEnabled = true;

            backward_toggle.IsChecked = false;
            backward_toggle.Visibility = Visibility.Visible;
            backward_toggle.IsEnabled = true;

            forward_click.Visibility = Visibility.Hidden;
            forward_click.IsEnabled = false;

            backward_click.Visibility = Visibility.Hidden;
            backward_click.IsEnabled = false;
            labelControl.Content = "Toggle Mode";

        }

        private void Headlight_Unchecked(object sender, RoutedEventArgs e)
        {
            labelControl.Content = "Headlight OFF";

            data = 'n';

            _serialPort.Write(data.ToString());
        }
        private void Headlight_Checked(object sender, RoutedEventArgs e)
        {
            labelControl.Content = "Headlight ON";

            data = 'm';

            _serialPort.Write(data.ToString());
        }

        private void key_pressed(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                if (click == false)
                {
                    ToggleButton toggle = forward_toggle;

                    toggle.IsChecked = !toggle.IsChecked;
                }

                else
                {
                    elemUI = forward_click;

                    elemUI.RaiseEvent(new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseLeftButtonDownEvent });
                    typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(elemUI, new object[] { true });
                }
            }

            else if (e.Key == Key.S)
            {
                if (click == false)
                {
                    ToggleButton toggle = backward_toggle;

                    toggle.IsChecked = !toggle.IsChecked;
                }

                else
                {
                    elemUI = backward_click;

                    elemUI.RaiseEvent(new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseLeftButtonDownEvent });
                    typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(elemUI, new object[] { true });
                }
            }

            else if (e.Key == Key.D)
            {
                elemUI = steer_r;

                elemUI.RaiseEvent(new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseLeftButtonDownEvent });
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(elemUI, new object[] { true });
            }

            else if (e.Key == Key.A)
            {
                elemUI = steer_l;

                elemUI.RaiseEvent(new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseLeftButtonDownEvent });
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(elemUI, new object[] { true });
            }

            else if (e.Key == Key.E)
            {
                elemUI = forward_r;

                elemUI.RaiseEvent(new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseLeftButtonDownEvent });
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(elemUI, new object[] { true });
            }

            else if (e.Key == Key.Q)
            {
                elemUI = forward_l;

                elemUI.RaiseEvent(new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseLeftButtonDownEvent });
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(elemUI, new object[] { true });
            }

            else if (e.Key == Key.C)
            {
                elemUI = backward_r;

                elemUI.RaiseEvent(new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseLeftButtonDownEvent });
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(elemUI, new object[] { true });
            }

            else if (e.Key == Key.Z)
            {
                elemUI = backward_l;

                elemUI.RaiseEvent(new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseLeftButtonDownEvent });
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(elemUI, new object[] { true });
            }

            else if (e.Key == Key.R)
            {
                ToggleButton toggle = mode;

                toggle.IsChecked = !toggle.IsChecked;
            }

            else if (e.Key == Key.F)
            {
                ToggleButton toggle = headlight;

                toggle.IsChecked = !toggle.IsChecked;
            }
        }

        private void key_released(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                if (click == false)
                {

                }

                else
                {
                    elemUI = forward_click;

                    elemUI.RaiseEvent(new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseLeftButtonUpEvent });
                    typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(elemUI, new object[] { false });
                }
            }

            else if (e.Key == Key.S)
            {
                if (click == false)
                {

                }

                else
                {
                    elemUI = backward_click;

                    elemUI.RaiseEvent(new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseLeftButtonUpEvent });
                    typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(elemUI, new object[] { false });
                }
            }

            else if (e.Key == Key.D)
            {
                elemUI = steer_r;

                elemUI.RaiseEvent(new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseLeftButtonUpEvent });
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(elemUI, new object[] { false });
            }

            else if (e.Key == Key.A)
            {
                elemUI = steer_l;

                elemUI.RaiseEvent(new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseLeftButtonUpEvent });
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(elemUI, new object[] { false });
            }

            else if (e.Key == Key.E)
            {
                elemUI = forward_r;

                elemUI.RaiseEvent(new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseLeftButtonUpEvent });
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(elemUI, new object[] { false });
            }

            else if (e.Key == Key.Q)
            {
                elemUI = forward_l;

                elemUI.RaiseEvent(new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseLeftButtonUpEvent });
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(elemUI, new object[] { false });
            }

            else if (e.Key == Key.C)
            {
                elemUI = backward_r;

                elemUI.RaiseEvent(new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseLeftButtonUpEvent });
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(elemUI, new object[] { false });
            }

            else if (e.Key == Key.Z)
            {
                elemUI = backward_l;

                elemUI.RaiseEvent(new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount, MouseButton.Left) { RoutedEvent = UIElement.PreviewMouseLeftButtonUpEvent });
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(elemUI, new object[] { false });
            }
        }

        // Add more event handlers for additional buttons here
    }
}
