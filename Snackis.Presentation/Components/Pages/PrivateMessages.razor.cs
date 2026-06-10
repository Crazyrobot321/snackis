using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using System.Security.Claims;

namespace Snackis.Presentation.Components.Pages
{
    public partial class PrivateMessages
    {
        private List<ApplicationPrivateMessage> Messages = default!;
        private List<ApplicationUser> users = new();
        private List<ApplicationPrivateMessage> SentMessages = new();

        private string currentUserId = "";
        private bool showForm = false;

        private string? errorMessage = null;
        private string newMessageRecipient = "";
        private string newMessageSubject = "";
        private string newMessageContent = "";

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            currentUserId = authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

            if (!string.IsNullOrEmpty(currentUserId))
            {
                await FetchMessages();
                await LoadUsers();
                SentMessages = await PrivateMessageServiceApi.GetSentAsync(currentUserId);
            }
        }
        private async Task FetchMessages()
        {
            try
            {
                Messages = await PrivateMessageServiceApi.GetInboxAsync(currentUserId);
            }
            catch (Exception ex)
            {
                errorMessage = $"Error fetching messages: {ex.Message}";
            }
        }
        private async Task LoadUsers()
        {
            try
            {
                users = await userManager.Users
                    .Where(u => u.Id != currentUserId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                errorMessage = $"Error loading users: {ex.Message}";
            }
        }
        private void ShowForm()
        {
            showForm = true;
        }

        private void CloseForm()
        {
            showForm = false;
            ResetFormFields();
        }
        private async Task SendMessage()
        {
            if (string.IsNullOrEmpty(newMessageRecipient))
            {
                errorMessage = "You must select a recipient.";
                return;
            }

            try
            {
                await PrivateMessageServiceApi.SendMessageAsync(currentUserId, newMessageRecipient, newMessageSubject, newMessageContent);

                CloseForm();
                await FetchMessages();
            }
            catch (Exception ex)
            {
                errorMessage = $"Error sending message: {ex.Message}";
            }
        }

        private void ResetFormFields()
        {
            newMessageRecipient = "";
            newMessageSubject = "";
            newMessageContent = "";
        }
    }
}