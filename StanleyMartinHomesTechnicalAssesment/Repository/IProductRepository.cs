using StanleyMartinHomesTechnicalAssesment.Models.ApiModels;

namespace StanleyMartinHomesTechnicalAssesment.Repository
{
    public interface IProductsRepository
    {
        public IEnumerable<ProductSearchResultModel> Search_SegregatedData(string query);
        public IEnumerable<ProductSearchResultModel> Search_Flattened_RelatedData(string query);
    }
}
