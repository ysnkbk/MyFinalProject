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
using Business.CCS;
using Core.Utilities.Business;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Performance;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
           
                
        }
        [CacheAspect] //key,value
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
        
        [SecuredOperation("admin,editor,product.add")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        [PerformanceAspect(10)]
        public IResult Add(Product product)
        {

            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfProductNameExists(product.ProductName));
            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
          
           

        }
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfProductNameExists(product.ProductName),CheckIfCategoryLimitExceded());
            if (result != null)
            {
                return result;
            }
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
            
           

        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productid)
        {
            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<Product>(Messages.MaintenanceTime);

            }
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId==productid));
        }
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (!result)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.ProductNameAlreadyExists);
        }
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll().Data.Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
