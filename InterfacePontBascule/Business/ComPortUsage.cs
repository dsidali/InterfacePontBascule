using System.IO.Ports;

namespace InterfacePontBascule.Business
{
    public class ComPortUsage
    {
        public static string ReadData(SerialPort serialPort1)
        {
            string poidsValueLabel = "0";
            try
            {


                serialPort1.Close();
              //  serialPort1.PortName = ConfigurationManager.AppSettings["comport"];
              //  serialPort1.BaudRate = int.Parse(ConfigurationManager.AppSettings["BaudRate"]);
                serialPort1.Parity = Parity.None;
              //  serialPort1.DataBits = int.Parse(ConfigurationManager.AppSettings["DataBits"]); ;
                serialPort1.StopBits = StopBits.One;
                serialPort1.ReceivedBytesThreshold = 1;
                serialPort1.DtrEnable = true;
                serialPort1.RtsEnable = true;
                serialPort1.Open();

     
          

                int reading = serialPort1.ReadByte();
                poidsValueLabel = serialPort1.ReadExisting();
                serialPort1.Close();
                serialPort1.Dispose();

                return poidsValueLabel;

            }
            catch (Exception e)
            {
                //    Console.WriteLine(e);
                //throw;
              //  MessageBox.Show(e.Message);
                poidsValueLabel = "0";

                return poidsValueLabel;
            }

        }

    }
}
