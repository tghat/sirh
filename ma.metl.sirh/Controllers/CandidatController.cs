using ma.metl.sirh.Model;
using ma.metl.sirh.Service;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ma.metl.sirh.Controllers
{
    public class CandidatController : Controller
    {
        readonly ICandidatService _candidatService;
        readonly ILigneRejeteeService _ligneRejeteeService;
        readonly IFluxService _fluxService;

        public CandidatController(ICandidatService candidatService, ILigneRejeteeService ligneRejeteeService, IFluxService fluxService)
        {
            _candidatService = candidatService;
            _ligneRejeteeService = ligneRejeteeService;
            _fluxService = fluxService;
        }     

        public void Create(List<Candidat> listCandidat, List<LigneRejetee> ligneRejetee, Flux flux)
        {
            //Insertion des candidats dans la base de données
            foreach (Candidat c in listCandidat)
            {
                _candidatService.Create(c);
            }

            foreach (LigneRejetee ligne in ligneRejetee)
            {
                _ligneRejeteeService.Create(ligne);
            }

            _fluxService.Create(flux);
        }
    }
}