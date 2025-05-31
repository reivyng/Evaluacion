using Entity.Model;

namespace Data.Interfaces
{
    public interface IEstudiantesData : IBaseModelData<Estudiantes>
    {
        Task<bool> ActiveAsync(int id, bool status);
        Task<bool> UpdatePartial(Estudiantes estudiantes);
    }
}
