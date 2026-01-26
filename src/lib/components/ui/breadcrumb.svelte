<!-- $lib/components/ui/breadcrumb/breadcrumb.svelte -->
<script>
    import Icon from '$lib/icons/icon.svelte';
    /**
     * @type {{
     *   items: Array<{ label: string, href?: string }>
     * }}
     */
    let { items = [] } = $props();
</script>

<nav class="flex items-center gap-2 text-sm text-gray-500 dark:text-gray-400">
    {#each items as item, index}
        {#if index > 0}
            <Icon name="ChevronRight" size="xs" />
        {/if}
        
        {#if item.href && index < items.length - 1}
            <a href={item.href} class="hover:text-primary transition-colors">
                {item.label}
            </a>
        {:else}
            <span class="text-gray-800 dark:text-gray-200">
                {item.label}
            </span>
        {/if}
    {/each}
</nav>