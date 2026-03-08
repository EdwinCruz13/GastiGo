using Application.Features.Finances.DTOs;
using Application.Features.Finances.Interfaces;


namespace Application.Features.Finances.Services
{
    public class NatureService
    {
        private readonly INatureRepository _natureRepository;

        /// <summary>
        /// Construtor da classe NatureService, que recebe uma instância de INatureRepository para realizar operaciones relacionada a naturaleza
        /// </summary>
        /// <param name="natureRepository"></param>
        public NatureService(INatureRepository natureRepository)
        {
            _natureRepository = natureRepository;
        }

        /// <summary>
        /// retorna la lista de naturalezas, si no se encuentran naturalezas, devuelve una lista vacía
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<NatureDTO?>> GetAllNaturesAsync()
        {
            var natures = await _natureRepository.GetAllNaturesAsync();
            return natures.Select(n => n == null ? null : new NatureDTO
            {
                NatureID = n.Id,
                Name = n.Name,
                Abbre = n.Abbre
            });
        }

        /// <summary>
        /// retorna una naturaleza por su id, si no se encuentra la naturaleza, devuelve null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<NatureDTO?> GetNatureByIdAsync(Guid id)
        {
            
            var nature = await _natureRepository.GetNatureByIdAsync(id);
            return nature == null ? null : new NatureDTO
            {
                NatureID = nature.Id,
                Name = nature.Name,
                Abbre = nature.Abbre
            };
        }
    }
}
