<script>
    import { sidebar, theme } from "$lib/stores";
    import { cn } from "$lib/utils";
    import { Logo } from "$lib/components/ui";
  import Icon from "$lib/icons/icon.svelte";

    let dropdownStates = {
        dashboard: false,
        products: false,
        settings: false
    };
    
    
    // 특정 드롭다운 토글 함수
    function toggleDropdown(key) {
        // 다른 드롭다운은 모두 닫고 선택한 것만 토글
        Object.keys(dropdownStates).forEach(k => {
        if (k === key) {
            dropdownStates[k] = !dropdownStates[k];
        } else {
            dropdownStates[k] = false;
        }
        });
    }
  
    // 외부 클릭 시 모든 드롭다운 닫기
    function handleClickOutside(event) {
        const sidebar = event.currentTarget;
        if (!sidebar.contains(event.target)) {
        Object.keys(dropdownStates).forEach(k => {
            dropdownStates[k] = false;
        });
        }
    }

</script>

<svelte:window on:click={handleClickOutside} />


<div class={cn("fixed border-r border-gray-100 dark:border-gray-800 left-0 top-0 bottom-0 h-screen",
            "bg-white dark:bg-gray-900",
            $sidebar.expanded? "w-[300px]": "w-[84px]"
)}>
    <nav class="flex flex-col w-full min-h-screen px-3">
        <div class="py-3 px-2 mb-3">
            <Logo/>
        </div>
        {#if $sidebar.expanded}
            <div class="relative">
                <button
                    on:click|stopPropagation={() => toggleDropdown('dashboard')}
                    class="w-full flex items-center justify-between gap-3 px-4 py-2.5 rounded-lg text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                >
                    <div class="flex items-center gap-3">
                    <Icon name="LayoutDashboard" size="md"/>
                    <span class="font-medium">Dashboard</span>
                    </div>
                    <div class="flex items-center justify-center transition-transform duration-200 {dropdownStates.dashboard ? 'rotate-180' : ''}">
                        <Icon name="ChevronDown" size="md"/>
                    </div>
                </button>
                
                <!-- 드롭다운 내용 -->
                <div 
                    class="overflow-hidden transition-all duration-200 {dropdownStates.dashboard ? 'max-h-96 opacity-100' : 'max-h-0 opacity-0'}"
                >
                    <div class="ml-8 mt-1 space-y-1">
                    <a 
                        href="/dashboard/analytics"
                        class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                    >
                        Analytics
                    </a>
                    <a 
                        href="/dashboard/reports"
                        class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                    >
                        Reports
                    </a>
                    <a 
                        href="/dashboard/metrics"
                        class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                    >
                        Metrics
                    </a>
                    </div>
                </div>
                </div>

                <!-- 드롭다운 메뉴 2: Products -->
                <div class="relative">
                <button
                    on:click|stopPropagation={() => toggleDropdown('products')}
                    class="w-full flex items-center justify-between gap-3 px-4 py-2.5 rounded-lg text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                >
                    <div class="flex items-center gap-3">
                    <Icon name="Package" size="md"/>
                    <span class="font-medium">Products</span>
                    </div>
                    <div class="flex items-center justify-center transition-transform duration-200 {dropdownStates.products ? 'rotate-180' : ''}">
                        <Icon name="ChevronDown" size="md"/>
                    </div>
                </button>
                
                <div 
                    class="overflow-hidden transition-all duration-200 {dropdownStates.products ? 'max-h-96 opacity-100' : 'max-h-0 opacity-0'}"
                >
                    <div class="ml-8 mt-1 space-y-1">
                    <a 
                        href="/products/list"
                        class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                    >
                        Product List
                    </a>
                    <a 
                        href="/products/categories"
                        class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                    >
                        Categories
                    </a>
                    <a 
                        href="/products/inventory"
                        class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                    >
                        Inventory
                    </a>
                    </div>
                </div>
                </div>

                <!-- 일반 메뉴 아이템 -->
                <a 
                href="/customers"
                class="flex items-center gap-3 px-4 py-2.5 rounded-lg text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                >
                <Icon name="Users" size="md"/>
                <span class="font-medium">Customers</span>
                </a>

                <!-- 드롭다운 메뉴 3: Settings -->
                <div class="relative">
                <button
                    on:click|stopPropagation={() => toggleDropdown('settings')}
                    class="w-full flex items-center justify-between gap-3 px-4 py-2.5 rounded-lg text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                >
                    <div class="flex items-center gap-3">
                    <Icon name="Settings" size="md"/>
                    <span class="font-medium">Settings</span>
                    </div>
                    <div class="flex items-center justify-center transition-transform duration-200 {dropdownStates.settings ? 'rotate-180' : ''}">
                        <Icon name="ChevronDown" size="md"/>
                    </div>
                </button>
                
                <div 
                    class="overflow-hidden transition-all duration-200 {dropdownStates.settings ? 'max-h-96 opacity-100' : 'max-h-0 opacity-0'}"
                >
                    <div class="ml-8 mt-1 space-y-1">
                    <a 
                        href="/settings/profile"
                        class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                    >
                        Profile
                    </a>
                    <a 
                        href="/settings/security"
                        class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                    >
                        Security
                    </a>
                    <a 
                        href="/settings/notifications"
                        class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                    >
                        Notifications
                    </a>
                    </div>
                </div>
            </div>
        {:else}
            <div>
                dd
            </div>
        {/if}
    </nav>
</div>

