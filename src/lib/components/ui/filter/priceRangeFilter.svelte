<!-- $lib/components/ui/filter/PriceRangeFilter.svelte -->
<script>
    let {
        min = $bindable(''),
        max = $bindable(''),
        label = '가격 범위',
        minPlaceholder = '최소',
        maxPlaceholder = '최대',
    } = $props();
</script>

<div class="flex-1 min-w-[200px] max-w-[300px]">
    {#if label}
        <label class="block text-xs font-medium text-gray-500 dark:text-gray-400 mb-2">{label}</label>
    {/if}
    <div class="flex items-center gap-2">
        <input
            type="number"
            bind:value={min}
            placeholder={minPlaceholder}
            class="w-full px-3 py-2 text-sm border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none"
        />
        <span class="text-gray-400">~</span>
        <input
            type="number"
            bind:value={max}
            placeholder={maxPlaceholder}
            class="w-full px-3 py-2 text-sm border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none"
        />
    </div>
</div>