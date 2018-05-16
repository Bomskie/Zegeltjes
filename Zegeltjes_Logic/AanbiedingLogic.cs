using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Zegeltjes_Logic
{
    public class AanbiedingLogic
    {
        public string VoegAanbiedingToe(int gebruikerID, string omschrijving, int actieID, string type)
        {
            Zegeltjes_DAL.VoegAanbiedingToeCommand voegAanbiedingToe = new Zegeltjes_DAL.VoegAanbiedingToeCommand(gebruikerID, omschrijving, actieID, type);
            if (voegAanbiedingToe.Execute())
            {
                return null;
            }
            else
            {
                return "Er is iets misgegaan";
            }
        }

        public List<Zegeltjes_Models.Aanbieding> HaalMijnAanbiedingenOp(int gebruikerID)
        {
            Zegeltjes_DAL.HaalAlMijnAanbiedingenOpCommand haalAlMijnAanbiedingenOpCommand = new Zegeltjes_DAL.HaalAlMijnAanbiedingenOpCommand(gebruikerID);
            return haalAlMijnAanbiedingenOpCommand.Execute();
        }

        public List<Zegeltjes_Models.Aanbieding> HaalAanbiedingenOp(string plaats)
        {
            Zegeltjes_DAL.HaalAanbiedingenOpCommand haalAanbiedingOpCommand = new Zegeltjes_DAL.HaalAanbiedingenOpCommand(plaats);
            return haalAanbiedingOpCommand.Execute();
        }

        public Zegeltjes_Models.Aanbieding HaalAanbiedingOp(int aanbiedingID)
        {
            Zegeltjes_DAL.HaalAanbiedingOpCommand haalAanbiedingOpCommand = new Zegeltjes_DAL.HaalAanbiedingOpCommand(aanbiedingID);
            return haalAanbiedingOpCommand.Execute();
        }

        public bool VerwijderAanbieding(int aanbiedingID, int gebruikerID)
        {
            Zegeltjes_DAL.HaalAanbiedingOpCommand haalAanbiedingOpCommand = new Zegeltjes_DAL.HaalAanbiedingOpCommand(aanbiedingID);
            if (haalAanbiedingOpCommand.Execute().Gebruiker.ID == gebruikerID)
            {
                Zegeltjes_DAL.VerwijderAanbiedingCommand verwijderAanbieding = new Zegeltjes_DAL.VerwijderAanbiedingCommand(aanbiedingID);
                if (verwijderAanbieding.Execute())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public List<Zegeltjes_Models.Claim> HaalClaimsOp(int aanbiedingID)
        {
            Zegeltjes_DAL.HaalClaimsOpCommand haalClaimsOpCommand = new Zegeltjes_DAL.HaalClaimsOpCommand(aanbiedingID);
            return haalClaimsOpCommand.Execute();
        }
        public bool ClaimAanbieding(int aanbiedingID, int gebruikerID)
        {
            Zegeltjes_DAL.HaalAanbiedingOpCommand haalAanbiedingOpCommand = new Zegeltjes_DAL.HaalAanbiedingOpCommand(aanbiedingID);
            if (haalAanbiedingOpCommand.Execute().Gebruiker.ID != gebruikerID)
            {
                if (HaalClaimsOp(aanbiedingID).Any(g => g.GebruikerID == gebruikerID))
                {
                    return false;
                }
                else
                {
                    Zegeltjes_DAL.ClaimAanbiedingCommand claimAanbieding = new Zegeltjes_DAL.ClaimAanbiedingCommand(aanbiedingID, gebruikerID);
                    claimAanbieding.Execute();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
