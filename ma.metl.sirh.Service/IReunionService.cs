using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ma.metl.sirh.Service
{
    public interface IReunionService : IEntityService<Reunion>
    {
        Reunion GetById(int Id);
        List<EventDto> GetListReunion(DateTime fromDate, DateTime toDate);
        List<EventDto> GetListReunion(DateTime fromDate, DateTime toDate, int commissionId, int gradeId, string annee, string examenType);
        List<Reunion> GetReunionByDate(int idCom);
    }
}
