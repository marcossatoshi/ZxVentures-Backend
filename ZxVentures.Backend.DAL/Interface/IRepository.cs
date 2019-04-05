using System.Collections.Generic;
using System.Linq;
using ZxVentures.Backend.Model.Entities;

namespace ZxVentures.Backend.DAL.Interface
{
    public interface IRepository
    {
        IQueryable<PontoDeVenda> All { get; }
        PontoDeVenda Get(int key);
        string GetCNPJ(string cnpj);
        void Add(PontoDeVenda obj);
        void Delete(int id);
    }
}
