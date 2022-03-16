using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Category>> GetAllByCategoryId(int id);

        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);

        IDataResult<List<Product>> GetAllByUnitsInStock(short min, short max);

        IDataResult<List<ProductDetailDto>> GetProductDetailDtos();
        IResult Add(Product product);
        IDataResult<Product> GetById(int productid);



    }
}
