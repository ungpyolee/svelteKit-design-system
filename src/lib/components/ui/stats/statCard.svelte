<!-- $lib/components/ui/stats/StatCard.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";

    let {
        value = 0,
        label = '',
        icon = 'Folder',
        color = 'blue', // blue, green, amber, red, gray, slate, purple
    } = $props();

    const colorConfig = {
        blue: 'bg-blue-100 dark:bg-blue-900 text-blue-600 dark:text-blue-400',
        green: 'bg-green-100 dark:bg-green-900 text-green-600 dark:text-green-400',
        amber: 'bg-amber-100 dark:bg-amber-900 text-amber-600 dark:text-amber-400',
        red: 'bg-red-100 dark:bg-red-900 text-red-600 dark:text-red-400',
        gray: 'bg-gray-100 dark:bg-gray-800 text-gray-600 dark:text-gray-400',
        slate: 'bg-slate-100 dark:bg-slate-800 text-slate-600 dark:text-slate-400',
        purple: 'bg-purple-100 dark:bg-purple-900 text-purple-600 dark:text-purple-400',
    };
</script>

<div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-4">
    <div class="flex items-center gap-3">
        <div class="w-10 h-10 rounded-xl flex items-center justify-center {colorConfig[color]}">
            <Icon name={icon} size="sm" />
        </div>
        <div>
            <p class="text-xl font-bold text-gray-900 dark:text-white">{value}</p>
            <p class="text-xs text-gray-500 dark:text-gray-400">{label}</p>
        </div>
    </div>
</div>