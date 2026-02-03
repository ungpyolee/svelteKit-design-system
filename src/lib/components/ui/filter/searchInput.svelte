<!-- $lib/components/ui/filter/SearchInput.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";

    let {
        value = $bindable(''),
        placeholder = '검색...',
        width = 'w-64',
    } = $props();
</script>

<div class="relative">
    <Icon name="Search" size="sm" class="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400" />
    <input
        type="text"
        bind:value={value}
        placeholder={placeholder}
        class="pl-10 pr-10 py-2 {width} border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-sm text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
    />
    {#if value}
        <button
            onclick={() => value = ''}
            class="absolute right-3 top-1/2 -translate-y-1/2 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300"
        >
            <Icon name="X" size="xs" />
        </button>
    {/if}
</div>