using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;

        }
        public List<Product> GetAll()
        {
            return _productDal.GetAll();

        }

     

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p=>p.UnitPrice>=min && p.UnitPrice<=max);

        }

        public List<Category> GetAllByCategoryId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByUnitsInStock(short min, short max)
        {
            return _productDal.GetAll(p=>p.UnitsInStock>=min && p.UnitsInStock<max);
        }
    }
}
