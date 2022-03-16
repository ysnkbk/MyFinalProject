using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entities.DTOs;
using Core.Utilities.Results;
using Business.Constans;
using FluentValidation;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Aspects.Autofac.Validation;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
                
        }
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 20)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);

            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll());
        }

     

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));

        }

        public IDataResult<List<Category>> GetAllByCategoryId(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Product>> GetAllByUnitsInStock(short min, short max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitsInStock >= min && p.UnitsInStock < max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetailDtos()
        {
            throw new NotImplementedException();
        }
        
        [ValidationAspect(typeof(ProductValidator))]  
        public IResult Add(Product product)
        {
            //Validation   
            //Business codes
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

        }

        public IDataResult<Product> GetById(int productid)
        {
            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<Product>(Messages.MaintenanceTime);

            }
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId==productid));
        }
    }
}
