using Product_API.Models;

namespace Product_API.Services
{
    public class TypeRepositoryInMemory : ITypeRepository
    {
        static List<TypeVM> types = new List<TypeVM>
        {
            new TypeVM {TypeId = 1, TypeName = "Tivi"},
            new TypeVM {TypeId = 2, TypeName = "Tulanh"},
            new TypeVM {TypeId = 3, TypeName = "Dieuhoa"},
            new TypeVM {TypeId = 4, TypeName = "Maygiat"}
        };
        public TypeVM Add(TypeModel type)
        {
            var _type = new TypeVM
            {
                TypeId = types.Max(lo => lo.TypeId) + 1,
                TypeName = type.TypeName
            };
            types.Add(_type);
            return _type;
        }

        public void Delete(int id)
        {
            var _type = types.SingleOrDefault(t => t.TypeId == id);
            types.Remove(_type);
        }

        public List<TypeVM> GetAll()
        {
            return types;
        }

        public TypeVM GetById(int id)
        {
            return types.SingleOrDefault(t => t.TypeId == id);
        }

        public void Update(TypeVM type)
        {
            var _type = types.SingleOrDefault(t => t.TypeId == type.TypeId);
            if (_type != null)
            {
                _type.TypeName = type.TypeName;
            }
        }
    }
}
