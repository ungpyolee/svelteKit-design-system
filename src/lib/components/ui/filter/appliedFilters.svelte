<!-- $lib/components/ui/filter/AppliedFilters.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";

    /**
     * @typedef {Object} FilterTag
     * @property {string} key - 고유 키
     * @property {string} label - 표시 텍스트
     * @property {string} color - blue, purple, green, gray
     * @property {() => void} onRemove - 제거 함수
     */

    let {
        filters = [],
    } = $props();

    const colorConfig = {
        blue: 'bg-blue-100 dark:bg-blue-900 text-blue-700 dark:text-blue-300',
        purple: 'bg-purple-100 dark:bg-purple-900 text-purple-700 dark:text-purple-300',
        green: 'bg-green-100 dark:bg-green-900 text-green-700 dark:text-green-300',
        gray: 'bg-gray-100 dark:bg-gray-800 text-gray-700 dark:text-gray-300',
    };
</script>

{#if filters.length > 0}
    <div class="flex flex-wrap items-center gap-2 pt-3 border-t border-gray-100 dark:border-gray-800">
        <span class="text-xs text-gray-500 dark:text-gray-400">적용된 필터:</span>
        {#each filters as filter (filter.key)}
            <span class="inline-flex items-center gap-1 px-2 py-1 text-xs {colorConfig[filter.color] || colorConfig.gray} rounded-full">
                {filter.label}
                <button onclick={filter.onRemove} class="hover:opacity-70">
                    <Icon name="X" size="xs" />
                </button>
            </span>
        {/each}
    </div>
{/if}