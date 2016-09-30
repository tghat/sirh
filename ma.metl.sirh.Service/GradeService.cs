using ma.metl.sirh.Model;
using ma.metl.sirh.Model.Dto;
using ma.metl.sirh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ma.metl.sirh.Service
{
    public class GradeService : EntityService<Grade>, IGradeService
    {
        IUnitOfWork _unitOfWork;
        IGradeRepository _GradeRepository;

        public GradeService(IUnitOfWork unitOfWork, IGradeRepository GradeRepository)
            : base(unitOfWork, GradeRepository)
        {
            _unitOfWork = unitOfWork;
            _GradeRepository = GradeRepository;
        }


        public Grade GetById(int Id)
        {
            return _GradeRepository.GetById(Id);
        }

        public Grade GetGradeByCode(string codeCateg, string codeCorps, string codeCadre, string codeGrade)
        {     
            return _GradeRepository.GetGradeByCode(codeCateg, codeCorps, codeCadre, codeGrade);
        }

        public GradeDto GetGradeDtoByCode(string codeCateg, string codeCorps, string codeCadre, string codeGrade)
        {
            Grade garde = _GradeRepository.GetGradeByCode(codeCateg, codeCorps, codeCadre, codeGrade);
            GradeDto dto = new GradeDto();
            if (garde != null)
            {
                Mapper.CreateMap<Grade, GradeDto>();
                dto = Mapper.Map<GradeDto>(garde);

            }
            return dto;
        }
    }
}
