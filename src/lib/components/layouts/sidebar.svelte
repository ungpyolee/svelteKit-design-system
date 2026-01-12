<script>
    import { sidebar, theme } from "$lib/stores";
    import { cn } from "$lib/utils";
    import { Logo } from "$lib/components/ui";
    import Icon from "$lib/icons/icon.svelte";
    import LogoUrl from '$lib/assets/logo.png';
    import LogoLightUrl from '$lib/assets/logo-light.png';
    import LogoSymbolUrl from '$lib/assets/logo-icon.png';
    import LogoSymbolLightUrl from '$lib/assets/logo-icon-light.png';

    let dropdownStates = {
        dashboard: false,
        products: false,
        settings: false,
        components: false,
        forms: false,
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
            $sidebar.expanded? "": " group"
)}>
    <nav class="flex flex-col w-full min-h-screen h-full">
        <div class="py-3 px-5 ">
            <a href="/" title="Logo 로고" class="w-full">
                {#if $sidebar.expanded}
                    {#if $theme === 'dark'}
                    <img src={LogoLightUrl} alt="Logo" width="168" height="40"/>
                    {:else}
                        <img src={LogoUrl} alt="Logo" width="168" height="40"/>
                    {/if}
                {:else}
                    {#if $theme === 'dark'}
                        <img src={LogoSymbolLightUrl} alt="Logo" width="43" height="40"/>
                    {:else}
                        <img src={LogoSymbolUrl} alt="Logo" width="43" height="40"/>
                    {/if}
                {/if}
            </a>
        </div>
        {#if $sidebar.expanded}
        <ul class="overflow-y-auto p-3 min-h-full w-[280px]">
            <!-- 드롭다운 메뉴 1: Dashboard -->
            <li class="relative">
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
            </li>

            <!-- 메뉴 타이틀 1 : UI Elements -->
            <li>
                <p class="py-1 mt-1 px-2 text-gray-500 dark:text-gray-400 font-semibold">UI Elements</p>
            </li>

            <!-- 드롭다운 메뉴 2: Components -->
            <li class="relative">
                <button
                    on:click|stopPropagation={() => toggleDropdown('components')}
                    class="w-full flex items-center justify-between gap-3 px-4 py-2.5 rounded-lg text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                >
                    <div class="flex items-center gap-3">
                    <Icon name="FileText" size="md"/>
                    <span class="font-medium">Components</span>
                    </div>
                    <div class="flex items-center justify-center transition-transform duration-200 {dropdownStates.components ? 'rotate-180' : ''}">
                        <Icon name="ChevronDown" size="md"/>
                    </div>
                </button>
                
                <div 
                    class="overflow-hidden transition-all duration-200 flex {dropdownStates.components ? 'opacity-100' : 'max-h-0 opacity-0'}"
                >
                    <div class="min-h-full w-[1px] bg-gray-100 dark:bg-gray-700 mx-6 my-2"></div>
                    <ul class="mt-1 space-y-1 w-full">
                        <a 
                            href="/components/typography"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Typography
                        </a>
                        <a 
                            href="/components/colors"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Colors
                        </a>
                        <a 
                            href="/components/dropdown"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Dropdown
                        </a>
                        <a 
                            href="/components/card"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Card
                        </a>
                        <a 
                            href="/components/carousel"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Carousel
                        </a>
                        <a 
                            href="/components/avatars"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Avatars
                        </a>
                        <a 
                            href="/components/progress-bar"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Pogress bar
                        </a>
                        <a 
                            href="/components/tab-accordion"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Tab & Accordion
                        </a>
                        <a 
                            href="/components/pagination"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Pagination
                        </a>
                        <a 
                            href="/components/badges"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Badges
                        </a>
                        <a 
                            href="/components/tooltip-popover"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Tooltip & Popover
                        </a>
                        <a 
                            href="/components/videos"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Videos
                        </a>
                        <a 
                            href="/components/star-ratings"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Star Ratings
                        </a>
                        <a 
                            href="/components/tags"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Tags
                        </a>
                        <a 
                            href="/components/list"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            List
                        </a>
                        <a 
                            href="/components/calendar"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Calendar
                        </a>
                        <a 
                            href="/components/radio"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Radio
                        </a>
                        <a 
                            href="/components/switch"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Switch
                        </a>
                        <a 
                            href="/components/upload"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Upload
                        </a>
                    </ul>
                </div>
            </li>

            <!-- 드롭다운 메뉴 3: Forms -->
            <li class="relative">
                <button
                    on:click|stopPropagation={() => toggleDropdown('forms')}
                    class="w-full flex items-center justify-between gap-3 px-4 py-2.5 rounded-lg text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                >
                    <div class="flex items-center gap-3">
                    <Icon name="Form" size="md"/>
                    <span class="font-medium">Forms</span>
                    </div>
                    <div class="flex items-center justify-center transition-transform duration-200 {dropdownStates.forms ? 'rotate-180' : ''}">
                        <Icon name="ChevronDown" size="md"/>
                    </div>
                </button>
                
                <div 
                    class="overflow-hidden transition-all duration-200 {dropdownStates.forms ? 'max-h-96 opacity-100' : 'max-h-0 opacity-0'}"
                >
                    <div class="ml-8 mt-1 space-y-1">
                    <a 
                        href="/forms/list"
                        class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                    >
                        Product List
                    </a>
                    <a 
                        href="/forms/categories"
                        class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                    >
                        Categories
                    </a>
                    <a 
                        href="/forms/inventory"
                        class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                    >
                        Inventory
                    </a>
                    </div>
                </div>
            </li>


            <!-- 드롭다운 메뉴 4: Products -->
            <li class="relative">
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
            </li>


            <!-- 메뉴 타이틀 2 : Pages  -->
            <li>
                <p class="py-1 mt-1 px-2 text-gray-500 dark:text-gray-400 font-semibold">Pages</p>
            </li>

            <!-- 일반 메뉴 아이템 -->
            <li>
                <a 
                href="/customers"
                class="flex items-center gap-3 px-4 py-2.5 rounded-lg text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                >
                <Icon name="Users" size="md"/>
                <span class="font-medium">Customers</span>
                </a>
            </li>      

            <!-- 드롭다운 메뉴 3: Settings -->
            <li class="relative">
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
            </li>
        </ul>
            
        {:else}
        <ul class="overflow-y-auto p-3 min-h-full group-hover:w-[280px]">

            <!-- 드롭다운 메뉴 1: Dashboard -->
            <li class="w-[60px] flex justify-center py-2.5 group-hover:hidden text-gray-700 dark:text-gray-300">
                <Icon name="LayoutDashboard" size="lg"/>
            </li>
            <li class="relative hidden group-hover:block">
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
            </li>


            <li>
                <p class="py-1 mt-1 px-2 text-gray-500 dark:text-gray-400 font-semibold hidden group-hover:block">UI Elements</p>
            </li>

            <!-- 드롭다운 메뉴 2: Components -->
            <li class="w-[60px] flex justify-center py-2.5 group-hover:hidden text-gray-700 dark:text-gray-300">
                <Icon name="FileText" size="lg"/>
            </li>

            <li class="relative hidden group-hover:block">
                <button
                    on:click|stopPropagation={() => toggleDropdown('components')}
                    class="w-full flex items-center justify-between gap-3 px-4 py-2.5 rounded-lg text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                >
                    <div class="flex items-center gap-3">
                    <Icon name="FileText" size="md"/>
                    <span class="font-medium">Components</span>
                    </div>
                    <div class="flex items-center justify-center transition-transform duration-200 {dropdownStates.components ? 'rotate-180' : ''}">
                        <Icon name="ChevronDown" size="md"/>
                    </div>
                </button>
                
                <div 
                    class="overflow-hidden transition-all duration-200 flex {dropdownStates.components ? 'opacity-100' : 'max-h-0 opacity-0'}"
                >
                    <div class="min-h-full w-[1px] bg-gray-100 dark:bg-gray-700 mx-6 my-2"></div>
                    <ul class="mt-1 space-y-1 w-full">
                        <a 
                            href="/components/typography"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Typography
                        </a>
                        <a 
                            href="/components/colors"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Colors
                        </a>
                        <a 
                            href="/components/dropdown"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Dropdown
                        </a>
                        <a 
                            href="/components/card"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Card
                        </a>
                        <a 
                            href="/components/carousel"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Carousel
                        </a>
                        <a 
                            href="/components/avatars"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Avatars
                        </a>
                        <a 
                            href="/components/progress-bar"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Pogress bar
                        </a>
                        <a 
                            href="/components/tab-accordion"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Tab & Accordion
                        </a>
                        <a 
                            href="/components/pagination"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Pagination
                        </a>
                        <a 
                            href="/components/badges"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Badges
                        </a>
                        <a 
                            href="/components/tooltip-popover"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Tooltip & Popover
                        </a>
                        <a 
                            href="/components/videos"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Videos
                        </a>
                        <a 
                            href="/components/star-ratings"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Star Ratings
                        </a>
                        <a 
                            href="/components/tags"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Tags
                        </a>
                        <a 
                            href="/components/list"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            List
                        </a>
                        <a 
                            href="/components/calendar"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Calendar
                        </a>
                        <a 
                            href="/components/radio"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Radio
                        </a>
                        <a 
                            href="/components/switch"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Switch
                        </a>
                        <a 
                            href="/components/upload"
                            class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                        >
                            Upload
                        </a>
                    </ul>
                </div>
            </li>

            <!-- 드롭다운 메뉴 3: Forms -->
            <li class="w-[60px] flex justify-center py-2.5 group-hover:hidden text-gray-700 dark:text-gray-300">
                <Icon name="Form" size="lg"/>
            </li>

            <li class="relative hidden group-hover:block">
                <button
                    on:click|stopPropagation={() => toggleDropdown('forms')}
                    class="w-full flex items-center justify-between gap-3 px-4 py-2.5 rounded-lg text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                >
                    <div class="flex items-center gap-3">
                    <Icon name="Form" size="md"/>
                    <span class="font-medium">Forms</span>
                    </div>
                    <div class="flex items-center justify-center transition-transform duration-200 {dropdownStates.forms ? 'rotate-180' : ''}">
                        <Icon name="ChevronDown" size="md"/>
                    </div>
                </button>
                
                <div 
                    class="overflow-hidden transition-all duration-200 {dropdownStates.forms ? 'max-h-96 opacity-100' : 'max-h-0 opacity-0'}"
                >
                    <div class="ml-8 mt-1 space-y-1">
                    <a 
                        href="/forms/list"
                        class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                    >
                        Product List
                    </a>
                    <a 
                        href="/forms/categories"
                        class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                    >
                        Categories
                    </a>
                    <a 
                        href="/forms/inventory"
                        class="block px-4 py-2 rounded-lg text-sm text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                    >
                        Inventory
                    </a>
                    </div>
                </div>
            </li>
        </ul>
        {/if}
    </nav>
</div>

