using Entity.Model;

namespace Data.Interfaces
{
    public interface IInscripcionesData : IBaseData<Inscripciones>
    {
        Task<bool> UpdatePartial(Inscripciones inscripciones);
    }
}