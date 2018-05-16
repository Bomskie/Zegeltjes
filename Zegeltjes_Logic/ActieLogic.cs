using System.Collections.Generic;
using System.Data;

namespace Zegeltjes_Logic
{
    public class ActieLogic
    {
        public List<Zegeltjes_Models.Actie> HaalHuidigeActiesOp()
        {
            Zegeltjes_DAL.SelecteerAlleGeldigeActieCommand selecteerAlleGeldigeActie = new Zegeltjes_DAL.SelecteerAlleGeldigeActieCommand();
            return selecteerAlleGeldigeActie.Execute();
            
        }
    }
}