namespace InterfacePontBascule.Business
{
    public class NumTicketBonManagement : INumTicketBonManagement
    {


        public string GenerateNextNum(string str)
        {

            if (String.IsNullOrEmpty(str))
            {
                str = "0";
            }
            int max = int.Parse(str);

            max++;

            return $"{max:0000000}";


        }
    }
}
