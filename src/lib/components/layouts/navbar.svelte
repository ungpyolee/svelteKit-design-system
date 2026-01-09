<script>
    import { sidebar, theme } from '$lib/stores';
    import Icon from '$lib/icons/icon.svelte';
    import { ThemeToggle } from "$lib/components/ui";
    
    import { cn } from '$lib/utils';
</script>
<div
    class={cn("fixed top-0 left-0 right-0 p-3 bg-white dark:bg-gray-900" 
            ,"border-b border-gray-100 dark:border-gray-800",
            $sidebar.expanded? "left-[300px]": "left-[84px]")}
>
    <nav class="flex">
        <div class="">
            <button
                onclick={() => sidebar.toggle()}
                class="p-2 rounded-md hover:bg-gray-50 dark:hover:bg-gray-800"
                aria-label="사이드바 토글"
                >
                {#if $sidebar.expanded}
                    <Icon name="ListIndentDecrease" size="lg" />
                {:else}
                    <Icon name="ListIndentIncrease" size="lg" />
                {/if}
            </button>
        </div>
        <div class="ms-auto">
            <ThemeToggle/>
        </div>
    </nav>
</div>

