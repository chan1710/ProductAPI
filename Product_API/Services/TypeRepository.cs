using Product_API.Data;
using Product_API.Models;
using Product_API.Services;
using Type = Product_API.Data.Type;

namespace WebAppAPI.Services
{
    public class LoaiRepository : ITypeRepository
    {
        private readonly MyDbContext _context;

        public LoaiRepository(MyDbContext context)
        {
            _context = context;
        }
        public TypeVM Add(TypeModel type)
        {
            var _type = new Type
            {
                TypeName = type.TypeName
            };
            _context.Add(_type);
            _context.SaveChanges();

            return new TypeVM
            {
                TypeId = _type.TypeId,
                TypeName = _type.TypeName
            };
        }

        public void Delete(int id)
        {
            var type = _context.Types.SingleOrDefault(t => t.TypeId == id);
            if (type != null)
            {
                _context.Remove(type);
                _context.SaveChanges();
            }
        }

        public List<TypeVM> GetAll()
        {
            var type = _context.Types.Select(x => new TypeVM
            {
                TypeId = x.TypeId,
                TypeName = x.TypeName,
            });
            return type.ToList();
        }

        public TypeVM GetById(int id)
        {
            var type = _context.Types.SingleOrDefault(t => t.TypeId == id);
            if (type != null)
            {
                return new TypeVM
                {
                    TypeId = type.TypeId,
                    TypeName = type.TypeName,
                };
            }
            return null;
        }

        public void Update(TypeVM type)
        {
            var _type = _context.Types.SingleOrDefault(t => t.TypeId == type.TypeId);
            type.TypeName = type.TypeName;
            _context.SaveChanges();
        }
    }
}
