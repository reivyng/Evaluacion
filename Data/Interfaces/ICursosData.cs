using Entity.Model;

namespace Data.Interfaces
{
    public interface IcursosData : IBaseModelData<Cursos>
    {
        Task<bool> ActiveAsync(int id, bool status);
        Task<bool> UpdatePartial(Cursos cursos);
    }
}
