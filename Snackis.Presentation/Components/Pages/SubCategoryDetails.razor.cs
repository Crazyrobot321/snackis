using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Snackis.Application.Interfaces.Api;
using Snackis.Application.Services;
using Snackis.Domain.Entities;
using System.Security.Claims;

namespace Snackis.Presentation.Components.Pages
{
    public partial class SubCategoryDetails
    {
        [Parameter]
        public string CatId { get; set; } = "";

        private ApplicationSubCategory? currentSubCategory;
        public List<ApplicationTopic> Topics { get; set; } = new();
        private IBrowserFile? selectedImage;
        private string? ImageUrl;

        private bool showNewTopicForm = false;
        private string newTopicTitle = "";
        private string newTopicContent = "";
        private string currentUserId = "";

        private string? errorMessage;

        private int? editingTopicId = null;
        private string editTopicTitle = "";
        private string editTopicContent = "";

        protected override async Task OnInitializedAsync()
        {
            //Fetch current user ID
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            currentUserId = authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            currentSubCategory = await SubCategoryServiceApi.GetByIdAsync(int.Parse(CatId));
            await FetchTopics();
        }
        private void OnImageSelected(InputFileChangeEventArgs e)
        {
            selectedImage = e.File;
        }

        private async Task FetchTopics()
        {
            try
            {
                Topics = await TopicServiceApi.GetAllTopicsBySubId(int.Parse(CatId));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching topics: {ex.Message}");
            }
        }

        private void EditTopic(ApplicationTopic topic)
        {
            editingTopicId = topic.Id;
            editTopicTitle = topic.Title;
            editTopicContent = topic.Description;
        }

        private void CancelEdit()
        {
            editingTopicId = null;
            editTopicTitle = "";
            editTopicContent = "";
        }

        private async Task SaveEdit()
        {
            try
            {
                var topic = Topics.FirstOrDefault(t => t.Id == editingTopicId);
                if (topic == null) return;

                topic.Title = editTopicTitle;
                topic.Description = editTopicContent;
                await TopicServiceApi.UpdateTopicAsync(topic);

                editingTopicId = null;
                editTopicTitle = "";
                editTopicContent = "";
                await FetchTopics();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating topic: {ex.Message}");
            }
        }

        private async Task DeleteTopic(int id)
        {
            try
            {
                await TopicServiceApi.DeleteTopicAsync(id);
                await FetchTopics();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting topic: {ex.Message}");
            }
        }

        private void OpenNewTopicForm()
        {
            showNewTopicForm = true;
            StateHasChanged();
        }

        private void CloseNewTopicForm()
        {
            showNewTopicForm = false;
            newTopicTitle = "";
            newTopicContent = "";
            StateHasChanged();
        }

        private async Task SubmitNewTopic()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(newTopicTitle) || string.IsNullOrWhiteSpace(newTopicContent))
                    return;
                if (selectedImage != null)
                {
                    using var stream = selectedImage.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
                    ImageUrl = await blobStorageService.UploadImageAsync(stream, selectedImage.Name);
                }
                var newTopic = new ApplicationTopic
                {
                    Title = newTopicTitle,
                    Description = newTopicContent,
                    AuthorId = currentUserId,
                    SubCategoryId = int.Parse(CatId),
                    CategoryId = currentSubCategory!.CategoryId,
                    Created = DateTime.UtcNow,
                    ImageSource = ImageUrl
                };

                await TopicServiceApi.CreateTopicAsync(newTopic);
                await FetchTopics();
                CloseNewTopicForm();
                StateHasChanged();
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}