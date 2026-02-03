<!-- $lib/components/ui/filter/FilterTabs.svelte -->
<script>
    /**
     * @typedef {Object} TabOption
     * @property {string} value
     * @property {string} label
     * @property {number} [count]
     * @property {boolean} [highlight] - 강조 뱃지 표시 여부
     */

    let {
        options = [],
        value = $bindable('all'),
    } = $props();
</script>

<div class="flex gap-1 p-1 bg-gray-100 dark:bg-gray-800 rounded-lg overflow-x-auto">
    {#each options as option}
        <button
            onclick={() => value = option.value}
            class="px-3 py-2 text-sm font-medium rounded-md transition-colors whitespace-nowrap flex items-center gap-1 {value === option.value ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
        >
            {option.label}
            {#if option.highlight && option.count > 0}
                <span class="px-1.5 py-0.5 text-xs bg-amber-100 text-amber-700 dark:bg-amber-900 dark:text-amber-300 rounded-full">
                    {option.count}
                </span>
            {:else if option.count !== undefined}
                <span class="ml-1 text-xs text-gray-400">({option.count})</span>
            {/if}
        </button>
    {/each}
</div>