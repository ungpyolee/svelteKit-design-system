<script>
    import Modal from "$lib/components/ui/Modal.svelte";
    import Icon from "$lib/icons/icon.svelte";
    
    let showBasicModal = $state(false);
    let showConfirmModal = $state(false);
    let showFormModal = $state(false);
</script>

<div class="flex flex-wrap gap-4">
    <button 
        onclick={() => showBasicModal = true}
        class="px-4 py-2 bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
    >
        Basic Modal
    </button>
    <button 
        onclick={() => showConfirmModal = true}
        class="px-4 py-2 bg-danger text-white rounded-lg hover:bg-danger-600 transition-colors"
    >
        Confirm Modal
    </button>
    <button 
        onclick={() => showFormModal = true}
        class="px-4 py-2 bg-success text-white rounded-lg hover:bg-success-600 transition-colors"
    >
        Form Modal
    </button>
</div>

<!-- Basic Modal -->
<Modal bind:isOpen={showBasicModal} title="Basic Modal" size="md">
    <p class="text-gray-600 dark:text-gray-400">
        This is a basic modal with simple content. You can close it by clicking the X button, clicking outside, or pressing Escape.
    </p>
    
    {#snippet footer()}
        <button 
            onclick={() => showBasicModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            Cancel
        </button>
        <button 
            onclick={() => showBasicModal = false}
            class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
        >
            Confirm
        </button>
    {/snippet}
</Modal>

<!-- Confirm Modal -->
<Modal bind:isOpen={showConfirmModal} title="Delete Item" size="sm">
    <div class="flex flex-col items-center text-center">
        <div class="w-12 h-12 rounded-full bg-danger-100 dark:bg-danger-900/30 flex items-center justify-center text-danger mb-4">
            <Icon name="Trash" size="md" />
        </div>
        <p class="text-gray-800 dark:text-gray-200 font-medium mb-2">Are you sure?</p>
        <p class="text-sm text-gray-500 dark:text-gray-400">
            This action cannot be undone. This will permanently delete the item.
        </p>
    </div>
    
    {#snippet footer()}
        <button 
            onclick={() => showConfirmModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            Cancel
        </button>
        <button 
            onclick={() => showConfirmModal = false}
            class="px-4 py-2 text-sm font-medium bg-danger text-white rounded-lg hover:bg-danger-600 transition-colors"
        >
            Delete
        </button>
    {/snippet}
</Modal>

<!-- Form Modal -->
<Modal bind:isOpen={showFormModal} title="Create New Item" size="lg">
    <div class="space-y-4">
        <div>
            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">Name</label>
            <input 
                type="text" 
                placeholder="Enter name"
                class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
            />
        </div>
        <div>
            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">Email</label>
            <input 
                type="email" 
                placeholder="Enter email"
                class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
            />
        </div>
        <div>
            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">Description</label>
            <textarea 
                rows="3"
                placeholder="Enter description"
                class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all resize-none"
            ></textarea>
        </div>
    </div>
    
    {#snippet footer()}
        <button 
            onclick={() => showFormModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            Cancel
        </button>
        <button 
            onclick={() => showFormModal = false}
            class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 transition-colors"
        >
            Create
        </button>
    {/snippet}
</Modal>