using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Snackis.Application.Interfaces.Api;
using Snackis.Domain.Entities;
using System.Security.Claims;

namespace Snackis.Presentation.Components.Pages
{
    public partial class TopicDetails
    {
        [Parameter]
        public string CurrentTopicId { get; set; } = "";
        public List<ApplicationPost> Posts { get; set; } = new();
        public ApplicationTopic? CurrentTopic { get; set; }
        public ApplicationPost newPost { get; set; } = new();

        private string currentUserId = "";

        private int? editingPostId = null;
        private string editPostContent = "";

        private string reportReason = "";
        private bool reporting = false;
        private int? ReportId = null;
        private string reportStatus = "";

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity?.IsAuthenticated ?? false)
            {
                currentUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            }

            await FetchCurrentTopic();
            await FetchPosts();
        }

        private async Task FetchPosts()
        {
            try
            {
                Posts = await PostService.GetAllPostsAsync(int.Parse(CurrentTopicId));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching posts: {ex.Message}");
            }
        }

        private async Task FetchCurrentTopic()
        {
            try
            {
                CurrentTopic = await TopicService.GetTopicByIdAsync(int.Parse(CurrentTopicId));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching topic: {ex.Message}");
            }
        }

        private async Task CreatePost()
        {
            try
            {
                if(string.IsNullOrWhiteSpace(currentUserId))
                    return;
                if (string.IsNullOrWhiteSpace(newPost.Content))
                    return;

                newPost.UserId = currentUserId;
                newPost.TopicId = int.Parse(CurrentTopicId);
                newPost.CreatedAt = DateTime.UtcNow;

                await PostService.CreatePostAsync(newPost);

                newPost = new ApplicationPost();
                await FetchPosts();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred while creating post: {ex.Message}");
            }
        }

        private void EditPost(ApplicationPost post)
        {
            editingPostId = post.Id;
            editPostContent = post.Content;
        }
        private void ReportPost(ApplicationPost post)
        {
            reporting = true;
            ReportId = post.Id;
        }

        private void Cancel()
        {
            editingPostId = null;
            editPostContent = "";
            reporting = false;
            reportReason = "";
            ReportId = null;
            reportStatus = "";
        }

        private async Task SaveEdit()
        {
            try
            {
                var post = Posts.FirstOrDefault(p => p.Id == editingPostId);
                if (post == null) return;

                post.Content = editPostContent;
                await PostService.UpdatePostAsync(post);

                editingPostId = null;
                editPostContent = "";
                await FetchPosts();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating post: {ex.Message}");
            }
        }

        private async Task DeletePost(int id)
        {
            try
            {
                await PostService.DeletePostAsync(id);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting post: {ex.Message}");
            }
        }
        private async Task SendReport()
        {
            try
            {
                if (ReportId <= 0)
                    return;
                var report = new ApplicationReport
                {
                    PostId = ReportId,
                    TopicId = int.Parse(CurrentTopicId),
                    ReporterUserId = currentUserId,
                    Reason = reportReason,
                    ReportedAt = DateTime.UtcNow
                };
                await ReportService.CreateReportAsync(report);
                await FetchPosts();
                reportStatus = "Your report has been sent will be reviewed within short";
                reporting = false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"SendReport error: {ex.Message} | {ex.InnerException?.Message}");
            }
        }
    }
}