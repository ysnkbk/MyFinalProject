using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;


namespace ConsoleUI
{
    //SOLID
    // open closed principle
    public class Program
    {
        static void Main(string[] args)
        {
            //ProductTest();
            //CategoryTest();
            //IoC
            //Data Transformation Object==dto
            ICategoryService categoryService = new CategoryManager(new EfCategoryDal());
            var result = categoryService.GetAll().Data;
            foreach (var value in result)
            {
                Console.WriteLine(value.CategoryId);

            }
            IProductService productService = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));
            var result1 = productService.GetAll();
            foreach (var item in result1.Data)
            {
                Console.WriteLine(item.ProductName);

            }

        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll().Data )
            {
                Console.WriteLine(category.CategoryName);


            }
        }

        
    }
}
