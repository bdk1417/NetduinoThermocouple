using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoThermocouple
{
    public class Program
    {
        public static SPI max6675Spi;

        public static void Main()
        {
            SPI.Configuration[] max6675SpiCfg;
            // variables that will be used to extract the information from
            // the incoming serial bits from the MAX6675 chip
            byte[] outBfr = new byte[2];    
            byte[] inBfr = new byte[2];

            // data will be used to store the extracted information
            short data;
            double tempF;
            double tempC;
            double tempK;
            int count = 0;

            max6675SpiCfg = new SPI.Configuration[1];

            max6675SpiCfg[0] = new SPI.Configuration(
                Pins.GPIO_PIN_D8,       //  CS to digtalOut pin 9
                false,                  //  CS pin active state
                0,                      //  CS port set up time
                0,                      //  CS port hold time
                false,                  //  Idle state of clock
                true,                   //  Signal edge to sample on
                1000,                   //  Clock speed in KHz
                SPI_Devices.SPI1        //  Using SPI bus 1
                );

            // Creating a new instance of SPI object 
            max6675Spi = new SPI (max6675SpiCfg[0]);

            //Reading from MAX6675 board and displaying data
            while(true)
            {
                max6675Spi.Config = max6675SpiCfg[0];
                max6675Spi.WriteRead(outBfr, inBfr);

                data = (short) (inBfr[0] << 5 | inBfr[1] >> 3);

                tempC = data / 4.0;
                tempK = tempC + 273.15;
                tempF = tempC * 1.8 + 32.0;

                // using debug to output the information to the computer screen
                Debug.Print(" Reading:" + count.ToString("F"));
                Debug.Print(" Temp F:" + tempF.ToString("F"));
                Debug.Print(" Temp C:" + tempC.ToString("F"));
                Debug.Print(" ");
               
                
                Thread.Sleep(500);
            
                

                count++;
            }
        }

    }
}


// Much of this code was created by refrencing http://citizenscientistsleague.com/2011/08/the-netduino-part-iii/
// Thanks to the Netduino community and to G.P> Crawford at Citizen Scientists League for the helpful guide.