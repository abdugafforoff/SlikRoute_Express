namespace BIS_project.IServices;

public interface IGenericService<T, Tdto>
{
     Task<List<T>> GetAll();
     Task<T> GetById(int id);
     Task<bool> Insert(Tdto item);
     Task<bool> Delete(int id);
     Task<T> Update(int id, Tdto item);
}