using Microsoft.EntityFrameworkCore;
using StanleyMartinHomesTechnicalAssesment.DataEntities;
using StanleyMartinHomesTechnicalAssesment.DataEntities.Models;
using StanleyMartinHomesTechnicalAssesment.Models.ApiModels;
using StanleyMartinHomesTechnicalAssesment.Extensions;

namespace StanleyMartinHomesTechnicalAssesment.Repository
{
    public class ProductRepository : IProductsRepository
    {
        private ApiContext _context;
        public ProductRepository(ApiContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductSearchResultModel> Search_SegregatedData(string query)
        {

            List<ProductSearchResultModel> results = new List<ProductSearchResultModel>();

            if (string.IsNullOrEmpty(query)) return results;

            // Method A : Based on the assumption results data set are segregated data

            List<Product> products = _context.Products
                .AsEnumerable()
                .Where(x => x.ContainsValue(query))
                .ToList();

            List<Project> projects = _context.Projects
                .AsEnumerable()
                .Where(x => x.ContainsValue(query))
                .ToList();
            List<Metro> metros = _context.Metros
                .AsEnumerable()
                .Where(x => x.ContainsValue(query))
                .ToList();

            foreach (var product in products)
            {
                results.Add(new ProductSearchResultModel()
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    ProjectGroupID = product.ProjectGroupID.ToString(),
                });
            }

            foreach (var project in projects)
            {
                results.Add(new ProductSearchResultModel()
                {
                    FullName = project.FullName,
                    ProjectGroupID = project.ProjectGroupID.ToString() // needeed if search by id and does not exist in products 
                });
            }

            foreach (var metro in metros)
            {
                results.Add(new ProductSearchResultModel()
                {
                    MetroAreaTitle = metro.MetroAreaTitle,
                });
            }

            return results.OrderBy(index => index.ProductName);

        }


        public IEnumerable<ProductSearchResultModel> Search_Flattened_RelatedData(string query)
        {

            var metros = _context.Metros;
            var projects = _context.Projects;
            var products = _context.Products;

            var results = from metro in metros
                          join project in projects on metro.MetroAreaID equals project.MetroAreaID into mp
                          from project in mp.DefaultIfEmpty()
                          join product in products on project.ProjectGroupID equals product.ProjectGroupID into pp
                          from product in pp.DefaultIfEmpty()
                          where (!String.IsNullOrEmpty(product.ProjectName) && product.ProjectName.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                                (!String.IsNullOrEmpty(product.ProductName) && product.ProductName.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                                (!String.IsNullOrEmpty(product.ProductID) && product.ProductID.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                                (!String.IsNullOrEmpty(product.ProjectGroupID.ToString()) && product.ProjectGroupID.ToString().Contains(query, StringComparison.OrdinalIgnoreCase)) ||

                                (!String.IsNullOrEmpty(project.ProjectGroupID.ToString()) && project.ProjectGroupID.ToString().Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                                (!String.IsNullOrEmpty(project.FullName.ToString()) && project.FullName.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                                (!String.IsNullOrEmpty(project.Status.ToString()) && project.Status.ToString().Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                                (!String.IsNullOrEmpty(project.MetroAreaID.ToString()) && project.MetroAreaID.ToString().Contains(query, StringComparison.OrdinalIgnoreCase)) ||

                                (!String.IsNullOrEmpty(metro.MetroAreaID.ToString()) && metro.MetroAreaID.ToString().Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                                (!String.IsNullOrEmpty(metro.MetroAreaTitle) && metro.MetroAreaTitle.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                                (!String.IsNullOrEmpty(metro.MetroAreaStateAbr) && metro.MetroAreaStateAbr.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                                (!String.IsNullOrEmpty(metro.MetroAreaStateName) && metro.MetroAreaStateName.Contains(query, StringComparison.OrdinalIgnoreCase))
                          orderby product.ProductName
                          select new ProductSearchResultModel
                          {
                              ProductName = product.ProductName ?? "N/A",
                              ProductID = product.ProductID ?? "N/A",
                              MetroAreaTitle = metro.MetroAreaTitle ?? "N/A",
                              FullName = project.FullName ?? "N/A",
                              ProjectGroupID = project.ProjectGroupID.ToString() ?? "N/A"
                          };

            return results;
        }
    }
}
