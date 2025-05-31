using Microsoft.AspNetCore.Components;

namespace EHMSWebApp.Shared
{
    public partial class DeleteConfirmation
    {
        [Parameter] public bool Show { get; set; }
        [Parameter] public string Title { get; set; } = "Confirm Deletion";
        [Parameter] public string Message { get; set; } = "Are you sure you want to delete this item?";
        [Parameter] public EventCallback<int> OnDeleteConfirmed { get; set; }
        [Parameter] public EventCallback OnCancel { get; set; }
        [Parameter]
        public int ItemId { get; set; }

        private void Cancel()
        {
            OnCancel.InvokeAsync();
        }

        private async Task ConfirmDelete()
        {
            if (OnDeleteConfirmed.HasDelegate)
            {
                await OnDeleteConfirmed.InvokeAsync(ItemId);
            }
        }
    }
}
