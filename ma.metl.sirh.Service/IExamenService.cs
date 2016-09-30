using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public interface IExamenService : IEntityService<Examen>
    {
        Examen GetById(int Id);
        List<ResultatRapportDto> GetListExamen();
        Examen GetByAnneeAndIdGrade(string Annee, int idGrade);
    }
}
