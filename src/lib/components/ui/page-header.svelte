<!-- $lib/components/ui/page-header/page-header.svelte -->
<script>
    import Icon from '$lib/icons/icon.svelte';
    
    /**
     * @type {{
     *   title: string,
     *   description?: string,
     *   children?: import('svelte').Snippet
     * }}
     */
    let { 
        title,
        description = '',
        children
    } = $props();
</script>

<div class="flex items-center justify-between">
    <div class="flex items-center gap-4">
        <div>
            <h1 class="text-2xl font-bold text-gray-800 dark:text-gray-100">{title}</h1>
            {#if description}
                <p class="text-gray-500 dark:text-gray-400 mt-1">{description}</p>
            {/if}
        </div>
    </div>
    
    {#if children}
        <div class="flex items-center gap-2">
            {@render children()}
        </div>
    {/if}
</div>