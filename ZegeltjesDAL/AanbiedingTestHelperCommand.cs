namespace Zegeltjes_DAL
{
    public class AanbiedingTestHelperCommand : Command<int>
    {
        public override int Execute()
        {
            DraaiQuery("SELECT MAX(`AanbiedingID`) FROM aanbieding");
            int res = 0;
            int.TryParse(dtResult.Rows[0][0].ToString(), out res);
            return res;
        }
    }
}
