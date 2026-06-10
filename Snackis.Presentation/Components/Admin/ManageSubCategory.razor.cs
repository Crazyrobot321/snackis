using Snackis.Domain.Entities;

namespace Snackis.Presentation.Components.Admin
{
    public partial class ManageSubCategory
    {
        private List<ApplicationSubCategory> subCategories = new();
        private List<ApplicationCategory> categories = new();
        private ApplicationSubCategory currentSubCategory = new();

        private string? successMessage;
        private string? errorMessage;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                categories = await CategoryServiceApi.GetAllCategoriesAsync();
                subCategories = await SubCategoryServiceApi.GetAllAsync();
            }
            catch (Exception ex)
            {
                errorMessage = $"Couldn't load data: {ex.Message}";
            }
        }

        private string GetCategoryName(int categoryId)
        {
            foreach (var cat in categories)
            {
                if (cat.Id == categoryId)
                {
                    return cat.Name;
                }
            }
            return "Unkown";
        }

        private async Task Submit()
        {
            errorMessage = null;
            successMessage = null;

            try
            {
                if (currentSubCategory.CategoryId <= 0)
                {
                    errorMessage = "You have to select a category!";
                    return;
                }

                if (currentSubCategory.Id > 0)
                {
                    await SubCategoryServiceApi.UpdateAsync(currentSubCategory);
                    successMessage = "Underkategorin har sparats!";
                }
                else
                {
                    await SubCategoryServiceApi.CreateAsync(currentSubCategory);
                    successMessage = "Underkategorin har skapats!";
                }

                ResetForm();
                await LoadData();
            }
            catch (Exception ex)
            {
                errorMessage = $"Ett fel uppstod: {ex.Message}";
            }
        }

        private void EditSubCategory(ApplicationSubCategory subCategory)
        {
            currentSubCategory = new ApplicationSubCategory
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                Description = subCategory.Description,
                CategoryId = subCategory.CategoryId
            };
            successMessage = null;
            errorMessage = null;
        }

        private async Task DeleteSubCategory(int id)
        {
            errorMessage = null;
            successMessage = null;

            try
            {
                await SubCategoryServiceApi.DeleteAsync(id);
                successMessage = "Underkategorin har tagits bort!";

                if (currentSubCategory.Id == id)
                {
                    ResetForm();
                }

                await LoadData();
            }
            catch (Exception ex)
            {
                errorMessage = $"Kunde inte ta bort: {ex.Message}";
            }
        }

        private void ResetForm()
        {
            currentSubCategory = new ApplicationSubCategory();
        }
    }
}