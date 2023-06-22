using InterfacePontBascule.Data;
using System.IO.Ports;

namespace InterfacePontBascule.Business
{
    public class ComPortUsage : IComPortUsage
    {
        private readonly ApplicationDbContext _context;
        public ComPortUsage(ApplicationDbContext context)
        {
            _context = context;
        }


        public string ReadData()
        {
            SerialPort serialPort1 = new SerialPort();
            var comPort = _context.ComPorts.FirstOrDefault();
            if (comPort == null)
            {
                return null;
            }

            string poidsValueLabel = "0";
            try
            {


                serialPort1.Close();
                serialPort1.PortName = comPort.PortName;
                serialPort1.BaudRate = comPort.BaudeRate;
                serialPort1.Parity = Parity.None;
                serialPort1.DataBits = comPort.DataBits;
                serialPort1.StopBits = StopBits.One;
                serialPort1.ReceivedBytesThreshold = comPort.ReceivedBytesThreshold;
                serialPort1.DtrEnable = comPort.DtrEnable;
                serialPort1.RtsEnable = comPort.RtsEnable;
                serialPort1.Open();



                Thread.Sleep(comPort.DureeAttente);
                int reading = serialPort1.ReadByte();
               // poidsValueLabel = serialPort1.ReadExisting();
                poidsValueLabel = serialPort1.ReadTo(comPort.StopCharacter);
                serialPort1.Close();
                serialPort1.Dispose();

                return poidsValueLabel;

            }
            catch (Exception e)
            {
                //    Console.WriteLine(e);
                throw;
                //  MessageBox.Show(e.Message);
                // poidsValueLabel = "0";

                //    return poidsValueLabel;
            }

        }




    }
}
