using Entity.Model;

namespace Data.Interfaces
{
    public interface IcursosData : IBaseData<Cursos>
    {
        Task<bool> UpdatePartial(Cursos cursos);
        Task<bool> ActiveAsync(int id, bool status);
    }
}
