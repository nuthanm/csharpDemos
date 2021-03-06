using LinqDemos.Entity;
using LinqDemos.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqDemos
{
    public class LinqQueries
    {
        #region Properties
        public List<Product> Products { get; set; }
        public bool UseQuerySyntax { get; set; }
        #endregion Properties
        public LinqQueries()
        {
            Products = ProductRepository.GetAll();
        }

        public List<Product> GetAllColumns()
        {
            /*
            Issue 1:
            Cannot implicitly convert type 'System.Collections.Generic.IEnumerable<LinqDemos.Entity.Product>' to 'System.Collections.Generic.List<LinqDemos.Entity.Product>'. An explicit conversion exists (are you missing a cast?) [LinqDemos]csharp(CS0266)
            Solution:
            Change from ProductRepository.GetAll().Select(prod => prod) to ProductRepository.GetAll().Select(prod => prod).ToList()

            */

            if (UseQuerySyntax)
            {
                return (from product in Products
                        select product).ToList();
            }
            else
            {
                return Products.Select(prod => prod).ToList();
            }
        }

        public List<string> GetSingleColumn()
        {
            if (UseQuerySyntax)
            {
                return (from product in Products select product.Name).ToList();
            }
            else
            {
                return Products.Select(prod => prod.Name).ToList();
            }
        }

        public void GetSpeficColumns()
        {
            if (UseQuerySyntax)
            {
                Products = (from prod in Products
                            select new Product
                            {
                                Id = prod.Id,
                                Name = prod.Name,
                                Color = prod.Color
                            }).ToList();
            }
            else
            {
                Products = Products.Select(
                    prod => new Product
                    {
                        Id = prod.Id,
                        Name = prod.Name,
                        Color = prod.Color
                    }).ToList();

            }
        }

        public void AnonymousClass()
        {

            if (UseQuerySyntax)
            {
                var anonymousProdcuts = (from prod in Products
                                         select new
                                         {
                                             Id = prod.Id,
                                             Name = prod.Name,
                                             Color = prod.Color
                                         }).ToList();

                foreach (var prod in anonymousProdcuts)
                {
                    Console.WriteLine($"Name:{prod.Name}");
                }
            }
            else
            {
                var anonymousProdcuts = Products.Select(
                    prod => new
                    {
                        Id = prod.Id,
                        Name = prod.Name,
                        Color = prod.Color
                    }).ToList();

                foreach (var prod in anonymousProdcuts)
                {
                    Console.WriteLine($"Name:{prod.Name}");
                }
            }
        }

        public List<Product> OrderBy()
        {
            if (UseQuerySyntax)
            {
                return (from product in Products orderby product.Name select product).ToList();
            }
            else
            {
                /*
                Issue: Unhandled exception. System.InvalidOperationException: Failed to compare two elements in the array.
                Solution: Change from Products.OrderBy(prod => prod).ToList(); to Products.OrderBy(prod => prod.Name).ToList();
                */
                return Products.OrderBy(prod => prod.Name).ToList();
                /*
                Note: .Select() method is optional when you are simply selecting an entire object as return value.
                */
            }
        }

        public List<Product> OrderByDescending()
        {
            if (UseQuerySyntax)
            {
                return (from product in Products orderby product.Name descending select product).ToList();
            }
            else
            {
                return Products.OrderByDescending(prod => prod.Name).ToList();
            }
        }

        public List<Product> OrderByTwoFields()
        {
            if (UseQuerySyntax)
            {
                return (from product in Products orderby product.Color descending, product.Name select product).ToList();
            }
            else
            {
                return Products.OrderByDescending(prod => prod.Color).ThenBy(prod => prod.Name).ToList();
            }
        }

        public List<Product> WhereExpression()
        {
            if (UseQuerySyntax)
            {
                return (from product in Products where product.Color == "Green" select product).ToList();
            }
            else
            {
                return Products.Where(prod => prod.Color == "Green").ToList();
            }
        }

        public List<Product> WhereWithMultipleExpressions()
        {
            if (UseQuerySyntax)
            {
                return (from product in Products where product.Color == "Green" && product.StandardCost > 10 select product).ToList();
            }
            else
            {
                return Products.Where(prod => prod.Color == "Green" && prod.StandardCost > 10).ToList();
            }
        }

        public List<Product> UseCustomExtensionMethod()
        {
            if (UseQuerySyntax)
            {
                return (from product in Products select product).ByColor("White").ToList();
            }
            else
            {
                return Products.ByColor("White").ToList();
            }
        }

        public Product FirstWithOutAnyWhere()
        {
            if (UseQuerySyntax)
            {
                return (from product in Products select product).First();
            }
            else
            {
                return Products.First();
            }
        }


        public Product FirstWithyWhereNotExists()
        {
            try
            {
                if (UseQuerySyntax)
                {
                    return (from product in Products select product).First(x => x.Color == "Orange");
                }
                else
                {
                    return Products.First(x => x.Color == "Orange");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No product found with that search item: " + ex.Message);
                return new Product();
            }
        }

        public Product FirstOrDefaultWithyWhereNotExists()
        {
            if (UseQuerySyntax)
            {
                return (from product in Products select product).FirstOrDefault(x => x.Color == "Orange");
            }
            else
            {
                return Products.FirstOrDefault(x => x.Color == "Orange");
            }
        }

        public Product FirstWithyWhereExists()
        {
            if (UseQuerySyntax)
            {
                return (from product in Products select product).First(x => x.Color == "White");
            }
            else
            {
                return Products.First(x => x.Color == "White");
            }
        }

        public Product LastWithOutAnyWhere()
        {
            if (UseQuerySyntax)
            {
                return (from product in Products select product).Last();
            }
            else
            {
                return Products.Last();
            }
        }

        public Product LastWithyWhereNotExists()
        {
            try
            {
                if (UseQuerySyntax)
                {
                    return (from product in Products select product).Last(x => x.Color == "Orange");
                }
                else
                {
                    return Products.Last(x => x.Color == "Orange");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No product found with that search item: " + ex.Message);
                return new Product();
            }
        }

        public Product LastOrDefaultWithyWhereNotExists()
        {
            if (UseQuerySyntax)
            {
                return (from product in Products select product).LastOrDefault(x => x.Color == "Orange");
            }
            else
            {
                return Products.LastOrDefault(x => x.Color == "Orange");
            }
        }

        public Product LastWithyWhereExists()
        {
            if (UseQuerySyntax)
            {
                return (from product in Products select product).Last(x => x.Color == "White");
            }
            else
            {
                return Products.Last(x => x.Color == "White");
            }
        }


        public Product SingleWithOutAnyWhere()
        {
            if (UseQuerySyntax)
            {
                return (from product in Products select product).Single();
            }
            else
            {
                return Products.Single();
            }
        }

        public Product SingleWithyWhereNotExists()
        {
            try
            {
                if (UseQuerySyntax)
                {
                    return (from product in Products select product).Single(x => x.Color == "White");
                }
                else
                {
                    return Products.Single(x => x.Color == "White");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No product found with that search item using Single(): " + ex.Message);
                return new Product();
            }
        }

        public Product SingleOrDefaultWithWhereNotExists()
        {
            try
            {
                if (UseQuerySyntax)
                {
                    return (from product in Products select product).SingleOrDefault(x => x.Color == "Orange");
                }
                else
                {
                    return Products.SingleOrDefault(x => x.Color == "Orange");
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine("More than one element found using SingleOrDefault() " + ex.Message);
                return new Product();
            }
        }

        public Product SingleWithWhereExists()
        {
            try
            {
            if (UseQuerySyntax)
            {
                return (from product in Products select product).Single(x => x.Color == "White");
            }
            else
            {
                return Products.Single(x => x.Color == "White");
            }
            }
            catch(Exception ex)
            {
                Console.WriteLine("More than one item found using Single() method: "+ ex.Message);
                return new Product();
            }
        }

         public List<string> GetDistinctRows()
        {
            if (UseQuerySyntax)
            {
                return (from product in Products select product.Color).Distinct().ToList();
            }
            else
            {
                return Products.Select(x => x.Color).Distinct().ToList();
            }
        }

        public List<string> LinqOp()
        {
            if (UseQuerySyntax)
            {
                return (from product in Products select product.Color).Distinct().ToList();
            }
            else
            {
                return Products.Select(x => x.Color).Distinct().ToList();
            }
        }

        public bool LinqWithAny()
        {
            if (UseQuerySyntax)
            {
                return (from product in Products where product.Color == "Nani" select product.Color).Any();
            }
            else
            {
                return Products.Where(x=>x.Color  == "Potti").Any();
            }
        }
    }
}