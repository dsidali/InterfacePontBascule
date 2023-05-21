using System.IO.Ports;

namespace InterfacePontBascule.Models
{
    public class ComPort
    {
        public int Id { get; set; }

        public string PortName { get; set; }

        public int BaudeRate { get; set; }
        public int DataBits { get; set; }


     

        public int ReceivedBytesThreshold { get; set; }

        public bool DtrEnable { get; set; }

        public bool RtsEnable { get; set; }
        /*
        serialPort1.Parity = Parity.None
            serialPort1.StopBits = StopBits.One;
        serialPort1.ReceivedBytesThreshold = 1;
        serialPort1.DtrEnable = true;
        serialPort1.RtsEnable = true;

            */

    }
}
