<script>
    import { sidebar, theme } from "$lib/stores";
    import { cn } from "$lib/utils";
    import { Logo } from "$lib/components/ui";
</script>
<div class={cn("fixed border-r border-gray-100 dark:border-gray-800 left-0 top-0 bottom-0 h-screen",
            "bg-white dark:bg-gray-900",
            $sidebar.expanded? "w-[300px]": "w-[84px]"
)}>
    <nav class="flex flex-col w-full min-h-screen">
        <Logo/>
    </nav>
</div>