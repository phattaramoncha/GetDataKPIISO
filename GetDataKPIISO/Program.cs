using GetDataKPIISO.cls;

namespace GetDataKPIISO
{
    class Program
    {
        static void Main(string[] args)
        {
            clsHO clsHO = new clsHO();
            // clsHO.excHO();
            //
            // clsFix clsFix = new clsFix();
            // clsFix.excFix();

            clsHO.UpdateHO();
        }
    }
}
