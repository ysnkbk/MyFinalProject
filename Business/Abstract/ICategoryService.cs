using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Category GetById(int categoryId);

        IDataResult<List<Category>> GetAll();

    }
}
