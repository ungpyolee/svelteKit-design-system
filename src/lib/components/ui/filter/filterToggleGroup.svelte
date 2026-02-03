<!-- $lib/components/ui/filter/FilterToggleGroup.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";

    let {
        label = '',
        options = [],
        selected = $bindable([]),
        color = 'blue', // blue, purple, green
    } = $props();

    const colorConfig = {
        blue: 'bg-blue-100 dark:bg-blue-900 border-blue-300 dark:border-blue-700 text-blue-700 dark:text-blue-300',
        purple: 'bg-purple-100 dark:bg-purple-900 border-purple-300 dark:border-purple-700 text-purple-700 dark:text-purple-300',
        green: 'bg-green-100 dark:bg-green-900 border-green-300 dark:border-green-700 text-green-700 dark:text-green-300',
    };

    function toggle(option) {
        if (selected.includes(option)) {
            selected = selected.filter(o => o !== option);
        } else {
            selected = [...selected, option];
        }
    }
</script>

<div>
    {#if label}
        <label class="block text-xs font-medium text-gray-500 dark:text-gray-400 mb-2">{label}</label>
    {/if}
    <div class="flex flex-wrap gap-2">
        {#each options as option}
            <button
                onclick={() => toggle(option)}
                class="px-3 py-1.5 text-sm rounded-lg border transition-colors {selected.includes(option) ? colorConfig[color] : 'bg-white dark:bg-gray-800 border-gray-200 dark:border-gray-700 text-gray-600 dark:text-gray-400 hover:border-gray-300 dark:hover:border-gray-600'}"
            >
                {#if selected.includes(option)}
                    <Icon name="Check" size="xs" class="inline mr-1" />
                {/if}
                {option}
            </button>
        {/each}
    </div>
</div>