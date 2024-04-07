public interface ICategoriesService{
    Task<IEnumerable<Categorie>> GetCategories(); // Method Signature   // IEnumerable: Returns Multible Category
    Task<Categorie> GetCategorById(byte id);
    Task<Categorie> Add(Categorie category); // Async -> Task<>
    Categorie Update(Categorie category); 
    Categorie Delete(Categorie category);
}