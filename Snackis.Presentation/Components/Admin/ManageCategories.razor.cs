using Snackis.Domain.Entities;

namespace Snackis.Presentation.Components.Admin
{
    public partial class ManageCategories
    {
        private List<ApplicationCategory> categories = new();
        private ApplicationCategory currentCategory = new();

        private string? successMessage;
        private string? errorMessage;
        protected override async Task OnInitializedAsync()
        {
            await LoadCategories();
        }

        private async Task LoadCategories()
        {
            try
            {
                categories = await CategoryServiceApi.GetAllCategoriesAsync();
            }
            catch (Exception ex)
            {
                errorMessage = $"Failed to load categories: {ex.Message}";
            }
        }

        private async Task Submit()
        {
            errorMessage = null;
            successMessage = null;

            try
            {
                if (currentCategory.Id > 0)
                {
                    await CategoryServiceApi.UpdateCategoryAsync(currentCategory);
                    successMessage = $"Category '{currentCategory.Name}' updated successfully!";
                }
                else
                {
                    await CategoryServiceApi.CreateCategoryAsync(currentCategory);
                    successMessage = $"Category '{currentCategory.Name}' created successfully!";
                }

                ResetForm();
                await LoadCategories();
            }
            catch (Exception ex)
            {
                errorMessage = $"Action failed: {ex.Message}";
            }
        }

        private void EditCategory(ApplicationCategory category)
        {
            currentCategory = new ApplicationCategory
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            successMessage = null;
            errorMessage = null;
        }

        private async Task DeleteCategory(int id)
        {
            errorMessage = null;
            successMessage = null;

            try
            {
                await CategoryServiceApi.DeleteCategoryAsync(id);
                successMessage = "Category deleted successfully!";

                if (currentCategory.Id == id)
                {
                    ResetForm();
                }

                await LoadCategories();
            }
            catch (Exception ex)
            {
                errorMessage = "Failed to delete category: " + ex;
            }
        }

        private void ResetForm()
        {
            currentCategory = new ApplicationCategory();
        }
    }
}