﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ShiftRegisterSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        // use these contants for controlling how the initial time interval for clocking in serial data to the shift register.
        private const double TIMER_INTERVAL = 100; // value is in milliseconds and denotes the timer interval
        private const double TIME_DELAY = 1;

        //the 74HC595N has 5 input pins that are used to control the device.
        //see the datasheet http://www.ti.com/lit/ds/symlink/sn74hc595.pdf for details
        //Shift Reister Clock (SRCLK): the clock for the serial input to the shift register
        private const int SRCLK_PIN = 0; // GPIO 0 is pin 27 on the RPI2 header 
        private GpioPin shiftRegisterClock;

        //Serial input (SER): the serial data input to the shift register. Use in conjunction with SRCLK.
        private const int SER_PIN = 1; //GPIO 1 is pin 28 on the RPI2 header.
        private GpioPin serial;

        //Storage Register Clock (RCLK): the clock for the clocking data from the serial onput to the parallel output in the shift register.
        private const int RCLK_PIN = 5; // Gpio pin 5 is pin 29 on the RPI 2 header
        private GpioPin registerClock;

        // Output Enable (OE): when set low, each of the eight shift register outputs (Q0,Q1,...Q7) are set high/low depending on the binary value in the storage register
        private const int OE_PIN = 6; // GPIO pin 6 is pin 31 oin the RPI2 header
        private GpioPin outputEnable;

        // Storage Register Clock (SRCLK): the clock for clocking the current 8 bits of data from teh serial input register to the storage register
        private const int SRCLR_PIN = 12; //GPIO 12 is pin 32 on RPi2 header
        private GpioPin shiftRegisterClear;

        private DispatcherTimer timer;
        private byte pinMask = 0x01;
        private bool areLedsInverted = true;

        private SolidColorBrush redBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        private SolidColorBrush grayBrush = new SolidColorBrush(Windows.UI.Colors.LightGray);

        public MainPage()
        {
            this.InitializeComponent();

            //register for the unloaded event so we can clean up upon exit
            Unloaded += MainPage_Unloaded;

            InitializeSystem();

        }

        private void InitializeSystem()
        {
            //Initialize the GPIO pins we will use for the bit-banging our serial data to the shift register
            var gpio = GpioController.GetDefault();

            //show an error if there is no GPIO controller
            if (gpio == null)
            {
                GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }

            //setup the RPi2 GPIO that controls the shift register
            shiftRegisterClock = gpio.OpenPin(SRCLK_PIN);
            serial = gpio.OpenPin(SER_PIN);
            registerClock = gpio.OpenPin(RCLK_PIN);
            outputEnable = gpio.OpenPin(SRCLR_PIN);

            //show an error if the pin wasn;t initialized properly


        }

        /// </summary>
        
    }
}
