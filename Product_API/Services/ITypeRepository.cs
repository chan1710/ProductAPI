using Product_API.Models;

namespace Product_API.Services
{
    public interface ITypeRepository
    {
        List<TypeVM> GetAll();
        TypeVM GetById(int id);
        TypeVM Add(TypeModel type);
        void Update(TypeVM type);
        void Delete(int id);
    }
}
