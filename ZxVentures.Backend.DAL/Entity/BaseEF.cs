using System.Linq;
using ZxVentures.Backend.DAL.Context;
using ZxVentures.Backend.DAL.Interface;
using ZxVentures.Backend.Model.Entities;

namespace ZxVentures.Backend.DAL.Entity
{
    public class BaseEF: IRepository
    {
        private readonly ProjectContext _context;

        public BaseEF() { }

        public BaseEF(ProjectContext context)
        {
            _context = context;
        }

        public IQueryable<PontoDeVenda> All => _context.PontoDeVenda.AsQueryable();

        public PontoDeVenda Get(int key)
        {
            return _context.PontoDeVenda.Find(key);
        }

        public string GetCNPJ(string cnpj)
        {
            return _context.PontoDeVenda.FirstOrDefault(q => q.Document == cnpj)?.Document;
        }

        public void Add(PontoDeVenda obj)
        {
            _context.PontoDeVenda.Add(obj);
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            PontoDeVenda pdv = _context.PontoDeVenda.FirstOrDefault(q => q.Id == id);
            _context.Remove(pdv);
            _context.SaveChanges();
        }
    }
}
