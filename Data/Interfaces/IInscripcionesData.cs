using Entity.Model;

namespace Data.Interfaces
{
    public interface IInscripcionesData : IBaseModelData<Inscripciones>
    {
        Task<bool> UpdatePartial(Inscripciones inscripciones);
    }
}