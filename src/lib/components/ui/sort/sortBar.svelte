<!-- $lib/components/ui/sort/SortBar.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";

    /**
     * @typedef {Object} SortOption
     * @property {string} value
     * @property {string} label
     */

    let {
        options = [],
        value = $bindable(''),
        label = '정렬:',
    } = $props();

    function toggle(optionValue) {
        value = value === optionValue ? '' : optionValue;
    }
</script>

<div class="px-6 py-3 border-b border-gray-100 dark:border-gray-800 flex items-center gap-4 overflow-x-auto">
    <span class="text-xs font-medium text-gray-500 dark:text-gray-400 whitespace-nowrap">{label}</span>
    <div class="flex items-center gap-1">
        {#each options as option}
            <button
                onclick={() => toggle(option.value)}
                class="px-3 py-1.5 text-xs font-medium rounded-md transition-colors whitespace-nowrap {value === option.value ? 'bg-primary text-white' : 'text-gray-500 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 hover:text-gray-700 dark:hover:text-gray-300'}"
            >
                {option.label}
            </button>
        {/each}
    </div>
    {#if value}
        <button
            onclick={() => value = ''}
            class="text-xs text-gray-400 hover:text-gray-600 dark:hover:text-gray-300 flex items-center gap-1"
        >
            <Icon name="X" size="xs" />
            정렬 해제
        </button>
    {/if}
</div>