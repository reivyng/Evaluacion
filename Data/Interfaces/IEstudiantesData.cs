using Entity.Model;

namespace Data.Interfaces
{
    public interface IEstudiantesData : IBaseData<Estudiantes>
    {
        Task<bool> UpdatePartial(Estudiantes estudiantes);
        Task<bool> ActiveAsync(int id, bool status);
    }
}
